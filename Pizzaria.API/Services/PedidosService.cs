using Pizzaria.API.Domain.RequestModels;
using Pizzaria.API.Domain.ResponseModels;
using Pizzaria.API.Domain.Models;
using Pizzaria.API.Repository.Interfaces;
using Pizzaria.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Storage;

namespace Pizzaria.API.Services
{
    public class PedidosService : IPedidosService
    {
        private readonly IPedidosRepository _pedidosRepository;
        private readonly IProdutosRepository _produtosRepository;

        public PedidosService(IPedidosRepository pedidossRepository, IProdutosRepository produtosRepository)
        {
            _pedidosRepository = pedidossRepository;
            _produtosRepository = produtosRepository;
        }

        public async Task<PedidoResponse> cadastrarPedido(PedidoRequest formPedido, int pQtde)
        {
            PedidoResponse sResp = null;
            decimal dValorTotal = await buscarSomaDosItens(formPedido.itensPedido);

            try
            {
                return await _pedidosRepository.cadastrarPedido(formPedido, pQtde, dValorTotal);
            }
            catch
            {
                return sResp;
            }
        }

        private async Task<decimal> buscarSomaDosItens(List<PedidoItensRequest> itensPedido)
        {
            decimal dTotal = 0;

            foreach (var item in itensPedido)
            {
                Produtos prod = await _produtosRepository.buscarProduto(item.ProdutoId);
                dTotal += prod.Valor * item.Quantidade;
            }

            return dTotal;
        }

        public async Task<IEnumerable<PedidoViewModel>> consultarPedidos(int? pPedidoID, string pNomeCliente, DateTime? pDataInicio, DateTime? pDataFim)
        {
            try
            {
                if (pDataInicio.HasValue)
                    pDataInicio = Convert.ToDateTime((pDataInicio.Value.ToString("dd/MM/yyyy") + " 00:00:00.000").ToString());

                if (pDataFim.HasValue)
                    pDataFim = Convert.ToDateTime((pDataFim.Value.ToString("dd/MM/yyyy") + " 23:59:59.999").ToString());

                IEnumerable<PedidoViewModel> pedidos = await _pedidosRepository.consultarPedidos(pPedidoID, pNomeCliente, pDataInicio, pDataFim);

                IEnumerable<PedidoItensViewModel> pedidosItens = await _pedidosRepository.consultarPedidosItens(pPedidoID, pNomeCliente, pDataInicio, pDataFim);

                var consulta = pedidos.Select(x => new PedidoViewModel()
                {
                    PedidoID = x.PedidoID,
                    DataEmissao = x.DataEmissao,
                    NomeCliente = x.NomeCliente,
                    Endereco = x.Endereco,
                    Proximidade = x.Proximidade,
                    Contato = x.Contato,
                    TotalItens = x.TotalItens,
                    ValorTotal = x.ValorTotal,
                    ValorFrete = x.ValorFrete,
                    ValorACobrar = x.ValorACobrar,

                    Itens = pedidosItens.Where(i => i.PedidoID == x.PedidoID).Select(i => new PedidoItensViewModel()
                    {
                        PedidoID = i.PedidoID,
                        ProdutoID = i.ProdutoID,
                        Nome = i.Nome,
                        Quantidade = i.Quantidade,
                        ValorItem = i.ValorItem
                    }),
                });

                if (consulta.Count() == 0)
                    return null;

                return (IEnumerable<PedidoViewModel>)consulta;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
