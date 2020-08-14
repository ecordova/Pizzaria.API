using System.ComponentModel.DataAnnotations;

namespace Pizzaria.API.Domain.RequestModels
{
    public class ProdutoRequest
    {
        public string Nome { get; set; }

        [DataType(DataType.Currency)]
        public double Valor { get; set; }
    }
}
