using Microsoft.AspNetCore.Mvc;
using Pizzaria.API.Domain.RequestModels;
using Pizzaria.API.Domain.ResponseModel;
using Pizzaria.API.Domain.ResponseModels;
using Pizzaria.API.Services.Interfaces;
using System.Net;
using System.Threading.Tasks;

namespace Pizzaria.API.Controllers
{
    [ApiController]
    [Route("/v1/[controller]")]
    public class ProdutosController : Controller
    {
        private readonly IProdutosService _produtosService;

        public ProdutosController(IProdutosService produtosService)
        {
            _produtosService = produtosService;
        }

        [HttpPost]
        [Route("cadastrarProdutos")]
        [ProducesResponseType(typeof(ErrorResponse<ProdutoRequest>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProdutoResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> cadastrarProduto(ProdutoRequest formProduto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResponse
                {
                    Status = "Erro",
                    Code = 0,
                    Description = "Informaçoes inválidas"
                });

            var result = await _produtosService.cadastrarProduto(formProduto);

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
        [Route("buscarProdutos")]
        [ProducesResponseType(typeof(ProdutosBuscarResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> buscarProdutos([FromQuery] string pNome)
        {
            pNome = string.IsNullOrEmpty(pNome) ? "" : pNome;

            var result = await _produtosService.buscarProdutos(pNome);

            if (result != null)
                return Ok(result);
            else
            {
                return BadRequest(new ErrorResponse
                {
                    Status = "Erro",
                    Description = "Nenhum Produto Encontrado"
                });
            }
        }
    }
}
