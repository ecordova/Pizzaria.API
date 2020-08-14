using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzaria.API.Domain.ResponseModels
{
    public class PedidoResponse
    {
        public string StringResponse { get; set; }
    }

    public class PedidoResponseID
    {
        public int PedidoID { get; set; }
    }

    public class PedidosConsultaResponse
    {
        public int PedidoID { get; set; }
        public string DataEmissao { get; set; }
        public string NomeCliente { get; set; }
        public string Endereco { get; set; }
        public string Proximidade { get; set; }
        public string Contato { get; set; }
        public int TotalItens { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal? ValorFrete { get; set; }
        public decimal ValorACobrar { get; set; }
        public List<PedidosItensConsultaResponse> Itens { get; set; }
    }
}
