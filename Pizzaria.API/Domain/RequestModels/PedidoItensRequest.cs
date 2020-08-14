using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzaria.API.Domain.RequestModels
{
    public class PedidoItensRequest
    {
        public int ProdutoId { get; set; }
        public decimal Quantidade { get; set; }
    }
}
