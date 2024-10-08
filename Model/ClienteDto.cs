namespace ATMWebAPI.Model
{
    public class ClienteDto
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public IFormFile Documento { get; set; }
    }
}
