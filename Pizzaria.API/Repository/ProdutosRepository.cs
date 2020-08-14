using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Pizzaria.API.Domain.RequestModels;
using Pizzaria.API.Domain.ResponseModels;
using Pizzaria.API.Infrastructure.Extensions;
using Pizzaria.API.Infrastructure.Options;
using Pizzaria.API.Domain.Models;
using Pizzaria.API.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzaria.API.Repository
{
    public class ProdutosRepository : IProdutosRepository
    {
        private readonly string _connectionString;

        public ProdutosRepository(IOptions<DatabaseOptions> database)
        {
            _connectionString = database.Value.SqlServer.FixConnectionTimeout();
        }

        public async Task<ProdutoResponse> cadastrarProduto(ProdutoRequest formProduto)
        {
            var sSql = @"INSERT INTO Produtos (Nome,Valor)
                         VALUES (@Nome,@Valor)";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Nome", formProduto.Nome.Trim());
                    parameters.Add("@Valor", formProduto.Valor);

                    await conn.ExecuteAsync(sSql, parameters);
                }

                return (new ProdutoResponse()
                {
                    StringResponse = "Produto Cadastrado"
                });
            }
            catch (Exception e)
            {
                return (new ProdutoResponse()
                {
                    StringResponse = "Erro: Produto Não Cadastrado. [" + e.Message.ToString() + "]"
                });
            }
        }

        public async Task<IEnumerable<ProdutosBuscarResponse>> buscarProdutos(string pNome)
        {
            var sSql = @" select p.ProdutoID, p.Nome, p.Valor 
                          from Produtos p
                          where 1=1
                          and (@Nome = '' or p.Nome like ('%' + @Nome + '%'))
                          order by p.Nome, p.ProdutoID; ";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Nome", pNome.Trim());

                    return await conn.QueryAsync<ProdutosBuscarResponse>(sSql, parameters);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Produtos> buscarProduto(int produtoId)
        {
            Produtos prod = null;

            var sSql = @" select p.Valor 
                          from Produtos p
                          where 1=1
                          and p.ProdutoID = @ProdutoID
                          order by p.ProdutoID; ";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    prod = await conn.QueryFirstAsync<Produtos>(sSql, new { produtoId });

                    return prod;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}