using Microsoft.AspNetCore.Mvc;
using Pizzaria.API.Domain.RequestModels;
using Pizzaria.API.Domain.ResponseModel;
using Pizzaria.API.Services;
using System.Net;
using System.Threading.Tasks;

namespace Pizzaria.API.Controllers
{
    [ApiController]
    [Route("/v1/[controller]")]
    public class ClientesController : Controller
    {
        private readonly IClientesService _clientesService;

        public ClientesController(IClientesService clientesService)
        {
            _clientesService = clientesService;
        }

        [HttpPost]
        [Route("cadastrarCliente")]
        [ProducesResponseType(typeof(ErrorResponse<ClienteRequest>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ClienteResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> cadastrarCliente(ClienteRequest formCliente)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResponse
                {
                    Status = "Erro",
                    Code = 0,
                    Description = "Informaçoes inválidas"
                });

            var result = await _clientesService.cadastrarCliente(formCliente);

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
        [Route("buscarClientes")]
        [ProducesResponseType(typeof(ClienteBuscarResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> buscarClientes([FromQuery] string pClienteID, string pNome, string pTelefone, string pCelular)
        {
            pClienteID = string.IsNullOrEmpty(pClienteID) ? "" : pClienteID;
            pNome = string.IsNullOrEmpty(pNome) ? "" : pNome;
            pTelefone = string.IsNullOrEmpty(pTelefone) ? "" : pTelefone;
            pCelular = string.IsNullOrEmpty(pCelular) ? "" : pCelular;

            var result = await _clientesService.buscarClientes(pClienteID, pNome, pTelefone, pCelular);

            if (result != null)
                return Ok(result);
            else
            {
                return BadRequest(new ErrorResponse
                {
                    Status = "Erro",
                    Description = "Nenhum Cliente Encontrado"
                });
            }
        }
    }
}
