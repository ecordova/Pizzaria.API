using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Pizzaria.API.Domain.RequestModels;
using Pizzaria.API.Domain.ResponseModel;
using Pizzaria.API.Infrastructure.Extensions;
using Pizzaria.API.Infrastructure.Options;
using Pizzaria.API.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pizzaria.API.Repository
{
    public class ClientesRepository : IClientesRepository
    {

        private readonly string _connectionString;

        public ClientesRepository(IOptions<DatabaseOptions> database)
        {
            _connectionString = database.Value.SqlServer.FixConnectionTimeout();
        }

        public async Task<ClienteResponse> cadastrarCliente(ClienteRequest formCliente)
        {
            var sSql = @"INSERT INTO Clientes (Nome,Telefone,Celular,Cep,Endereco,Numero,Complemento,Bairro,Cidade,UF)
                         VALUES (@Nome,@Telefone,@Celular,@Cep,@Endereco,@Numero,@Complemento,@Bairro,@Cidade,@UF)";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Nome", formCliente.Nome.Trim());
                    parameters.Add("@Telefone", formCliente.Telefone.Trim());
                    parameters.Add("@Celular", formCliente.Celular.Trim());
                    parameters.Add("@Cep", formCliente.Cep.Trim());
                    parameters.Add("@Endereco", formCliente.Endereco.Trim());
                    parameters.Add("@Numero", formCliente.Numero.Trim());
                    parameters.Add("@Complemento", formCliente.Complemento.Trim());
                    parameters.Add("@Bairro", formCliente.Bairro.Trim());
                    parameters.Add("@Bairro", formCliente.Bairro.Trim());
                    parameters.Add("@Cidade", formCliente.Cidade.Trim());
                    parameters.Add("@UF", formCliente.Uf.Trim());

                    await conn.ExecuteAsync(sSql, parameters);
                }

                return (new ClienteResponse()
                {
                    StringResponse = "Cliente Cadastrado"
                });
            }
            catch (Exception e)
            {
                return (new ClienteResponse()
                {
                    StringResponse = "Erro: Cliente Não Cadastrado. [" + e.Message.ToString() + "]"
                });
            }
        }

        public async Task<IEnumerable<ClienteBuscarResponse>> buscarClientes(string pClienteID, string pNome, string pTelefone, string pCelular)
        {
            var sSql = @" select c.ClienteID,c.Nome,c.Telefone,c.Celular,c.Cep,c.Endereco,
                                 c.Numero,c.Complemento,c.Bairro,c.Cidade,c.UF
                          from Clientes c
                          where 1=1
                          and (@ClienteID = '' or c.ClienteId = @ClienteID)
                          and (@Nome = '' or c.Nome like ('%' + @Nome + '%'))
                          and (@Telefone = '' or c.Telefone like ('%' + @Telefone + '%'))
                          and (@Celular = '' or c.Celular like ('%' + @Celular + '%'))
                          order by c.Nome, c.ClienteID; ";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ClienteID", pClienteID.ToString().Trim());
                    parameters.Add("@Nome", pNome.Trim());
                    parameters.Add("@Telefone", pTelefone.Trim());
                    parameters.Add("@Celular", pCelular.Trim());

                    return await conn.QueryAsync<ClienteBuscarResponse>(sSql, parameters);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
