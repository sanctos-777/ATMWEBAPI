namespace ATMWebAPI.Model
{
    public class ProdutoDto
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public IFormFile NotaFiscalFornecedor { get; set; }
    }
}
    
