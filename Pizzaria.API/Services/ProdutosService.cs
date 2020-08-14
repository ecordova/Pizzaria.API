using Pizzaria.API.Domain.RequestModels;
using Pizzaria.API.Domain.ResponseModels;
using Pizzaria.API.Repository.Interfaces;
using Pizzaria.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pizzaria.API.Services
{
    public class ProdutosService : IProdutosService
    {
        private readonly IProdutosRepository _produtosRepository;

        public ProdutosService(IProdutosRepository produtosRepository)
        {
            _produtosRepository = produtosRepository;
        }

        public async Task<ProdutoResponse> cadastrarProduto(ProdutoRequest formProduto)
        {
            ProdutoResponse sResp = null;

            try
            {
                return await _produtosRepository.cadastrarProduto(formProduto);
            }
            catch
            {
                return sResp;
            }
        }

        public async Task<IEnumerable<ProdutosBuscarResponse>> buscarProdutos(string pNome)
        {
            try
            {
                return await _produtosRepository.buscarProdutos(pNome);
            }
            catch
            {
                return null;
            }
        }
    }
}
