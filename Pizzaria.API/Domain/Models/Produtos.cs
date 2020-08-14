using System;
using System.Collections.Generic;

namespace Pizzaria.API.Domain.Models
{
    public partial class Produtos
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
    }
}
