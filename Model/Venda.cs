using ATMWebAPI.ORM;
using System.Text.Json.Serialization;

namespace ATMWebAPI.Model
{
    public class Venda
    {
        public int Id { get; set; }

        public int Quantidade { get; set; }

        public decimal Valor { get; set; }

        public int FkCliente { get; set; }

        public int FkProduto { get; set; }

        public virtual TbCliente FkClienteNavigation { get; set; } = null!;

        public virtual TbProduto FkProdutoNavigation { get; set; } = null!;

        [JsonIgnore] // Ignora a serialização deste campo
        public byte[]? NotaFiscalVenda { get; set; }

        [JsonIgnore] // Ignora a serialização deste campo
        public string? FotoBase64 => NotaFiscalVenda != null ? Convert.ToBase64String(NotaFiscalVenda) : null;

        public string UrlNotaFiscalVenda { get; set; } // Certifique-se de que esta propriedade esteja visível
    }
}
