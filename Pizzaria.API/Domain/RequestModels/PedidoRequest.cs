using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzaria.API.Domain.RequestModels
{
    public class PedidoRequest
    {
        public int? ClienteID { get; set; }
        public string NomeCliente { get; set; }
        public string EnderecoCompleto { get; set; }
        public string Proximidade { get; set; }
        public string TelefoneContato { get; set; }
        public decimal? ValorFrete { get; set; }

        public List<PedidoItensRequest> itensPedido { get; set; }
    }
}
