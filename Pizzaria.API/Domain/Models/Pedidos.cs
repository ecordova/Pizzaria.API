using System;
using System.Collections.Generic;

namespace Pizzaria.API.Domain.Models
{
    public partial class Pedidos
    {
        public int PedidoId { get; set; }
        public DateTime? DataEmissao { get; set; }
        public int? ClienteId { get; set; }
        public string NomeCliente { get; set; }
        public string EnderecoCompleto { get; set; }
        public string Proximidade { get; set; }
        public string TelefoneContato { get; set; }
        public int? TotalItens { get; set; }
        public decimal? ValorTotal { get; set; }
        public decimal? ValorFrete { get; set; }
        public decimal? ValorACobrar { get; set; }


        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ClienteId")]
        public Clientes Cliente { get; set; }

        public virtual ICollection<PedidosItens> PedidoItens { get; set; }
    }
}
