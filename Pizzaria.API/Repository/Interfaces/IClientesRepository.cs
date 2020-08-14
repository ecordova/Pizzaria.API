using Pizzaria.API.Domain.RequestModels;
using Pizzaria.API.Domain.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pizzaria.API.Repository.Interfaces
{
    public interface IClientesRepository
    {
        Task<ClienteResponse> cadastrarCliente(ClienteRequest formCliente);
        Task<IEnumerable<ClienteBuscarResponse>> buscarClientes(string ClienteID, string Nome, string Telefone, string Celular);
    }
}
