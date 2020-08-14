using Pizzaria.API.Domain.Models;
using Pizzaria.API.Domain.RequestModels;
using Pizzaria.API.Domain.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pizzaria.API.Repository.Interfaces
{
    public interface IProdutosRepository
    {
        Task<ProdutoResponse> cadastrarProduto(ProdutoRequest formProduto);
        Task<IEnumerable<ProdutosBuscarResponse>> buscarProdutos(string pNome);
        Task<Produtos> buscarProduto(int produtoId);
    }
}
