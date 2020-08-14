using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzaria.API.Domain.ResponseModels
{
    public class PedidoViewModel
    {
        public int PedidoID { get; set; }
        public DateTime DataEmissao { get; set; }
        public string NomeCliente { get; set; }
        public string Endereco { get; set; }
        public string Proximidade { get; set; }
        public string Contato { get; set; }
        public int TotalItens { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal? ValorFrete { get; set; }
        public decimal ValorACobrar { get; set; }
        public IEnumerable<PedidoItensViewModel> Itens { get; set; }
    }

    public class PedidoItensViewModel
    {
        public int PedidoID { get; set; }
        public int ProdutoID { get; set; }
        public string Nome { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorItem { get; set; }
    }

    public class PedidosConsResponse
    {
        public PedidosConsResponse(int pedidoID, DateTime dataEmissao, string nomeCliente, string endereco, string proximidade,
                                   string contato, int totalItens, decimal valorTotal, decimal? valorFrete, decimal valorACobrar)
        {
            PedidoID = pedidoID;
            DataEmissao = dataEmissao;
            NomeCliente = nomeCliente;
            Endereco = endereco;
            Proximidade = proximidade;
            Contato = contato;
            TotalItens = totalItens;
            ValorTotal = valorTotal;
            ValorFrete = valorFrete;
            ValorACobrar = valorACobrar;
        }

        public int PedidoID { get; set; }
        public DateTime DataEmissao { get; set; }
        public string NomeCliente { get; set; }
        public string Endereco { get; set; }
        public string Proximidade { get; set; }
        public string Contato { get; set; }
        public int TotalItens { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal? ValorFrete { get; set; }
        public decimal ValorACobrar { get; set; }
    }

    public class PedidosItensConsResponse
    {
        public PedidosItensConsResponse(int pedidoID, int produtoID, string nome, decimal quantidade, decimal valorItem)
        {
            PedidoID = pedidoID;
            ProdutoID = produtoID;
            Nome = nome;
            Quantidade = quantidade;
            ValorItem = valorItem;
        }

        public int PedidoID { get; set; }
        public int ProdutoID { get; set; }
        public string Nome { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorItem { get; set; }
    }
}
