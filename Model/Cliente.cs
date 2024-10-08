using System.Text.Json.Serialization;

namespace ATMWebAPI.Model
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }

        [JsonIgnore] // Ignora a serialização deste campo
        public byte[]? Documento { get; set; }

        [JsonIgnore] // Ignora a serialização deste campo
        public string? FotoBase64 => Documento != null ? Convert.ToBase64String(Documento) : null;

        public string UrlDocumento { get; set; } // Certifique-se de que esta propriedade esteja visível
    }
}
