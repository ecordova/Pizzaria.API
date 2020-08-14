using Pizzaria.API.Domain.RequestModels;
using Pizzaria.API.Domain.ResponseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pizzaria.API.Services.Interfaces
{
    public interface IPedidosService
    {
        Task<PedidoResponse> cadastrarPedido(PedidoRequest formPedido, int pQtde);
        Task<IEnumerable<PedidoViewModel>> consultarPedidos(int? pPedidoID, string pNomeCliente, DateTime? pDataInicio, DateTime? pDataFim);
    }
}
