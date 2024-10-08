using ATMWebAPI.ORM;
using System.Text.Json.Serialization;

namespace ATMWebAPI.Model
{
    public class VendaDto
    {

        public int Quantidade { get; set; }

        public decimal Valor { get; set; }

        public int FkCliente { get; set; }

        public int FkProduto { get; set; }

        public IFormFile NotaFiscalVenda { get; set; }

    }
}
