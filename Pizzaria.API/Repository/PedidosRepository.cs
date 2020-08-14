using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pizzaria.API.Domain.Models;
using Pizzaria.API.Domain.RequestModels;
using Pizzaria.API.Domain.ResponseModels;
using Pizzaria.API.Infrastructure.Extensions;
using Pizzaria.API.Infrastructure.Options;
using Pizzaria.API.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzaria.API.Repository
{
    public class PedidosRepository : IPedidosRepository
    {
        private readonly string _connectionString;
        private readonly IProdutosRepository _produtosRepository;

        public PedidosRepository(IOptions<DatabaseOptions> database, IProdutosRepository produtosRepository)
        {
            _connectionString = database.Value.SqlServer.FixConnectionTimeout();
            _produtosRepository = produtosRepository;
        }

        public async Task<PedidoResponse> cadastrarPedido(PedidoRequest formPedido, int pTotalItens, decimal pValorTotal)
        {
            int iPedidoID = 0;

            string sResult = "Pedido Cadastrado: No. ";

            var sSql = @"INSERT INTO Pedidos (DataEmissao,NomeCliente,EnderecoCompleto,Proximidade,TelefoneContato,TotalItens,ValorTotal,ValorFrete,ClienteID)
                         OUTPUT INSERTED.[PedidoID]
                         VALUES (getdate(),@NomeCliente,@EnderecoCompleto,@Proximidade,@TelefoneContato,@TotalItens,@ValorTotal,@ValorFrete";

            if (formPedido.ClienteID == 0)
                sSql += @",null);";
            else
                sSql += @$",{formPedido.ClienteID});";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    SqlCommand insPedido = new SqlCommand(sSql, conn);

                    insPedido.Parameters.Add("@NomeCliente", SqlDbType.VarChar).Value = formPedido.NomeCliente.Trim();
                    insPedido.Parameters.Add("@EnderecoCompleto", SqlDbType.VarChar).Value = formPedido.EnderecoCompleto.Trim();
                    insPedido.Parameters.Add("@Proximidade", SqlDbType.VarChar).Value = formPedido.Proximidade.Trim();
                    insPedido.Parameters.Add("@TelefoneContato", SqlDbType.VarChar).Value = formPedido.TelefoneContato.Trim();
                    insPedido.Parameters.Add("@TotalItens", SqlDbType.Int).Value = pTotalItens;
                    insPedido.Parameters.Add("@ValorTotal", SqlDbType.Decimal).Value = pValorTotal;
                    insPedido.Parameters.Add("@ValorFrete", SqlDbType.Decimal).Value = formPedido.ValorFrete.HasValue ? formPedido.ValorFrete.Value : 0;

                    conn.Open();
                    iPedidoID = (int)insPedido.ExecuteScalar();

                    if (iPedidoID != 0 && formPedido.itensPedido.Count > 0)
                    {
                        sResult += iPedidoID;

                        foreach (var item in formPedido.itensPedido)
                        {
                            sSql = @" INSERT INTO PedidosItens (PedidoID,ProdutoID,Quantidade,ValorItem)
                                  VALUES (@PedidoID,@ProdutoID,@Quantidade,@ValorItem); ";

                            Produtos prod = await _produtosRepository.buscarProduto(item.ProdutoId);

                            var param = new DynamicParameters();
                            param.Add("@PedidoID", iPedidoID);
                            param.Add("@ProdutoID", item.ProdutoId);
                            param.Add("@Quantidade", item.Quantidade);
                            param.Add("@ValorItem", (prod.Valor * item.Quantidade));

                            await conn.ExecuteAsync(sSql, param);
                        }
                    }
                }

                return (new PedidoResponse()
                {
                    StringResponse = sResult
                });
            }
            catch (Exception e)
            {
                return (new PedidoResponse()
                {
                    StringResponse = "Erro: Pedido Não Cadastrado. [" + e.Message.ToString() + "]"
                });
            }
        }

        public async Task<IEnumerable<PedidoViewModel>> consultarPedidos(int? pPedidoID, string pNomeCliente, DateTime? pDataInicio, DateTime? pDataFim)
        {
            IEnumerable<PedidoViewModel> pedidos;

            var sSql = @" select p.PedidoID, p.DataEmissao,
                                 iif(c.ClienteID is null, p.NomeCliente, c.Nome) NomeCliente,
                                 iif(c.ClienteID is null, p.EnderecoCompleto, concat(ltrim(rtrim(c.Endereco)),', ',ltrim(rtrim(c.Numero)), ' ', ltrim(rtrim(c.Complemento)), ' ', ltrim(rtrim(c.Bairro)), ' ', ltrim(rtrim(c.Cep)), ' ', ltrim(rtrim(c.Cidade)), ' ', c.UF)) Endereco,
                                 iif(c.ClienteID is null, p.Proximidade, c.Proximidade) Proximidade,
                                 iif(c.ClienteID is null, p.TelefoneContato, concat(c.Telefone, ' / ', c.Celular)) Contato,
                                 p.TotalItens, p.ValorTotal, p.ValorFrete, p.ValorACobrar --,
                                 --pr.ProdutoID, pr.Nome, pi.Quantidade, pi.ValorItem
                          from Pedidos p
                          left join Clientes c on c.ClienteID = p.ClienteID
                          -- join PedidosItens pi on pi.PedidoID = p.PedidoID
                          -- join Produtos pr on pr.ProdutoID = pi.ProdutoID
                          where 1=1
                          and (@PedidoID = 0 or p.PedidoID = @PedidoID)
                          and (@NomeCliente = '' or isnull(c.Nome,p.NomeCliente) like ('%' + @NomeCliente + '%'))
                          and (@DtInicio is null or p.DataEmissao >= @DtInicio)
                          and (@DtFim is null or p.DataEmissao <= @DtFim) 
                          order by p.DataEmissao;";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@PedidoID", pPedidoID.ToString().Trim());
                    parameters.Add("@NomeCliente", pNomeCliente.Trim());
                    parameters.Add("@DtInicio", pDataInicio);
                    parameters.Add("@DtFim", pDataFim);

                    pedidos = await conn.QueryAsync<PedidoViewModel>(sSql, parameters);

                    return pedidos;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<PedidoItensViewModel>> consultarPedidosItens(int? pPedidoID, string pNomeCliente, DateTime? pDataInicio, DateTime? pDataFim)
        {
            IEnumerable<PedidoItensViewModel> itens;

            var sSql = @" select p.PedidoID, pr.ProdutoID, pr.Nome, pi.Quantidade, pi.ValorItem
                          from Pedidos p
                          left join Clientes c on c.ClienteID = p.ClienteID
                          join PedidosItens pi on pi.PedidoID = p.PedidoID
                          join Produtos pr on pr.ProdutoID = pi.ProdutoID
                          where 1=1
                          and (@PedidoID = 0 or p.PedidoID = @PedidoID)
                          and (@NomeCliente = '' or isnull(c.Nome,p.NomeCliente) like ('%' + @NomeCliente + '%'))
                          and (@DtInicio is null or p.DataEmissao >= @DtInicio)
                          and (@DtFim is null or p.DataEmissao <= @DtFim) 
                          order by pr.ProdutoID;";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@PedidoID", pPedidoID.ToString().Trim());
                    parameters.Add("@NomeCliente", pNomeCliente.Trim());
                    parameters.Add("@DtInicio", pDataInicio);
                    parameters.Add("@DtFim", pDataFim);

                    itens = await conn.QueryAsync<PedidoItensViewModel>(sSql, parameters);

                    return itens;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
