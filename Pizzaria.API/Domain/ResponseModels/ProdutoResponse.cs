namespace Pizzaria.API.Domain.ResponseModels
{
    public class ProdutoResponse
    {
        public string StringResponse { get; set; }
    }

    public class ProdutosBuscarResponse
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
    }
}
