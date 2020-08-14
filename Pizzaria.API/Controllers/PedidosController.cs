using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pizzaria.API.Domain.RequestModels;
using Pizzaria.API.Domain.ResponseModel;
using Pizzaria.API.Domain.ResponseModels;
using Pizzaria.API.Services.Interfaces;

namespace Pizzaria.API.Controllers
{
    [ApiController]
    [Route("/v1/[controller]")]
    public class PedidosController : Controller
    {
        private readonly IPedidosService _pedidosService;

        public PedidosController(IPedidosService pedidosService)
        {
            _pedidosService = pedidosService;
        }

        [HttpPost]
        [Route("cadastrarPedido")]
        [ProducesResponseType(typeof(ErrorResponse<PedidoRequest>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(PedidoResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> cadastrarPedido([FromBody] PedidoRequest formPedido)
        {
            decimal dQtde = 0;
            var sQtdes = new List<decimal> { 0.5m, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            foreach (var item in formPedido.itensPedido)
            {
                if (sQtdes.IndexOf(item.Quantidade) == -1)
                {
                    return BadRequest(new ErrorResponse
                    {
                        Status = "Erro",
                        Description = "Existem Pizzas fora da quantidade permitida, verifique!"
                    });
                }

                dQtde += item.Quantidade;
            }

            if (dQtde != (int)dQtde)
            {
                return BadRequest(new ErrorResponse
                {
                    Status = "Erro",
                    Description = "Existem Pizzas pela metade, verifique!"
                });
            }

            if (dQtde > 10)
            {
                return BadRequest(new ErrorResponse
                {
                    Status = "Erro",
                    Description = "Quantidade de Pizzas nao pode ser maior que 10!"
                });
            }

            var result = await _pedidosService.cadastrarPedido(formPedido, (int)dQtde);

            if (result.StringResponse.Contains("Erro:"))
            {
                return BadRequest(new ErrorResponse
                {
                    Status = "Erro",
                    Description = result.StringResponse
                });
            }
            else
                return Ok(result);
        }

        [HttpGet]
        [Route("consultarPedidos")]
        [ProducesResponseType(typeof(PedidoViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> consultarPedidos([FromQuery] int? pPedidoID, string pNomeCliente, DateTime? pDataInicio, DateTime? pDataFim)
        {

            pPedidoID = !pPedidoID.HasValue || pPedidoID == 0 ? 0 : pPedidoID;
            pNomeCliente = string.IsNullOrEmpty(pNomeCliente) ? "" : pNomeCliente;
            pDataInicio = !pDataInicio.HasValue ? null : pDataInicio;
            pDataFim = !pDataFim.HasValue ? null : pDataFim;

            var result = await _pedidosService.consultarPedidos(pPedidoID, pNomeCliente, pDataInicio, pDataFim);

            if (result != null)
                return Ok(result);
            else
            {
                return BadRequest(new ErrorResponse
                {
                    Status = "Erro",
                    Description = "Nenhum Pedido Encontrado"
                });
            }
        }

    }
}
