using System;
using System.Collections.Generic;

namespace ATMWebAPI.ORM;

public partial class TbProduto
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public decimal Preco { get; set; }

    public byte[]? NotaFiscalFornecedor { get; set; }

    public virtual ICollection<TbVenda> TbVenda { get; set; } = new List<TbVenda>();
}
