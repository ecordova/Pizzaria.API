using Pizzaria.API.Domain.RequestModels;
using Pizzaria.API.Domain.ResponseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pizzaria.API.Repository.Interfaces
{
    public interface IPedidosRepository
    {
        Task<PedidoResponse> cadastrarPedido(PedidoRequest formPedido, int pTotalItens, decimal pValorTotal);
        Task<IEnumerable<PedidoViewModel>> consultarPedidos(int? pPedidoID, string pNomeCliente, DateTime? pDataInicio, DateTime? pDataFim);
        Task<IEnumerable<PedidoItensViewModel>> consultarPedidosItens(int? pPedidoID, string pNomeCliente, DateTime? pDataInicio, DateTime? pDataFim);
    }
}
