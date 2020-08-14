using Pizzaria.API.Domain.RequestModels;
using Pizzaria.API.Domain.ResponseModel;
using Pizzaria.API.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pizzaria.API.Services
{
    public class ClientesService : IClientesService
    {
        private readonly IClientesRepository _clientesRepository;

        public ClientesService(IClientesRepository clientesRepository)
        {
            _clientesRepository = clientesRepository;
        }

        public async Task<ClienteResponse> cadastrarCliente(ClienteRequest formCliente)
        {
            ClienteResponse sResp = null;

            try
            {
                return await _clientesRepository.cadastrarCliente(formCliente);
            }
            catch
            {
                return sResp;
            }
        }

        public async Task<IEnumerable<ClienteBuscarResponse>> buscarClientes(string ClienteID, string Nome, string Telefone, string Celular)
        {
            try
            {
                return await _clientesRepository.buscarClientes(ClienteID, Nome, Telefone, Celular);
            }
            catch
            {
                return null;
            }
        }
    }
}
