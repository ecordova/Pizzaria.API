using System;
using System.Collections.Generic;

namespace Pizzaria.API.Domain.Models
{
    public partial class PedidosItens
    {
        public int PedidoItemId { get; set; }
        public int PedidoId { get; set; }
        public int ProdutoId { get; set; }
        public decimal? Quantidade { get; set; }
        public decimal? ValorItem { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ProdutoID")]
        public Produtos Produto { get; set; }
    }
}
