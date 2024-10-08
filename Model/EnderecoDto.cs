namespace ATMWebAPI.Model
{
    public class EnderecoDto
    {
      
        public string Logradouro { get; set; } 

        public string Cidade { get; set; } 

        public string Estado { get; set; }

        public int Cep { get; set; }

        public string? PontoReferencia { get; set; }

        public int Numero { get; set; }

        public int FkCliente { get; set; }

    }
}
