using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzaria.API.Domain.ResponseModels
{
    public class PedidosItensConsultaResponse
    {
        public int PedidoID { get; set; }
        public int ProdutoID { get; set; }
        public string Nome { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorItem { get; set; }
    }
}
