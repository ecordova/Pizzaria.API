using Pizzaria.API.Domain.RequestModels;
using Pizzaria.API.Domain.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pizzaria.API.Services.Interfaces
{
    public interface IProdutosService
    {
        Task<ProdutoResponse> cadastrarProduto(ProdutoRequest formProduto);
        Task<IEnumerable<ProdutosBuscarResponse>> buscarProdutos(string pNome);
    }
}
