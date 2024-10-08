using System.Text.Json.Serialization;

namespace ATMWebAPI.Model
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        [JsonIgnore] // Ignora a serialização deste campo
        public byte[]? NotaFiscalFornecedor { get; set; }

        [JsonIgnore] // Ignora a serialização deste campo
        public string? FotoBase64 => NotaFiscalFornecedor != null ? Convert.ToBase64String(NotaFiscalFornecedor) : null;

        public string UrlNotaFiscalFornecedor { get; set; } // Certifique-se de que esta propriedade esteja visível
    }
}
