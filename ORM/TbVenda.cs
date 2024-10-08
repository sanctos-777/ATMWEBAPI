using System;
using System.Collections.Generic;

namespace ATMWebAPI.ORM;

public partial class TbVenda
{
    public int Id { get; set; }

    public int Quantidade { get; set; }

    public byte[]? NotaFiscalVenda { get; set; }

    public decimal Valor { get; set; }

    public int FkCliente { get; set; }

    public int FkProduto { get; set; }

    public virtual TbCliente FkClienteNavigation { get; set; } = null!;

    public virtual TbProduto FkProdutoNavigation { get; set; } = null!;
}
