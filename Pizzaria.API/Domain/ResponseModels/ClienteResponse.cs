namespace Pizzaria.API.Domain.ResponseModel
{
    public class ClienteResponse
    {
        public string StringResponse { get; set; }
    }

    public class ClienteBuscarResponse
    {
        public int ClienteID { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
    }
}
