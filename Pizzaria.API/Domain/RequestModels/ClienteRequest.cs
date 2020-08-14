using System.ComponentModel.DataAnnotations;

namespace Pizzaria.API.Domain.RequestModels
{
    public class ClienteRequest
    {
        [Required]
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }

        [StringLength(2, MinimumLength = 0, ErrorMessage = @"A UF deve conter no máximo dois caracteres ou em deixar em branco.")]
        public string Uf { get; set; }
    }

    public class ClienteBuscar
    {
        public int ClienteID { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Endereco { get; set; }
    }
}
