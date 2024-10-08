using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ATMWebAPI.ORM;

public partial class BdAtmContext : DbContext
{
    public BdAtmContext()
    {
    }

    public BdAtmContext(DbContextOptions<BdAtmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbCliente> TbClientes { get; set; }

    public virtual DbSet<TbEndereco> TbEnderecos { get; set; }

    public virtual DbSet<TbProduto> TbProdutos { get; set; }

    public virtual DbSet<TbUsuario> TbUsuarios { get; set; }

    public virtual DbSet<TbVenda> TbVendas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAB205_17\\SQLEXPRESS;Database=BD_ATM;User Id=adminTarde;Password=admin;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbCliente>(entity =>
        {
            entity.ToTable("TB_CLIENTE");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DocumentoIdentificacao).HasColumnName("documento_identificacao");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.Telefone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefone");
        });

        modelBuilder.Entity<TbEndereco>(entity =>
        {
            entity.ToTable("TB_ENDERECO");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cep).HasColumnName("cep");
            entity.Property(e => e.Cidade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cidade");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FkCliente).HasColumnName("fkCliente");
            entity.Property(e => e.Logradouro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("logradouro");
            entity.Property(e => e.Numero).HasColumnName("numero");
            entity.Property(e => e.PontoReferencia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ponto_referencia");

            entity.HasOne(d => d.FkClienteNavigation).WithMany(p => p.TbEnderecos)
                .HasForeignKey(d => d.FkCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_ENDERECO_TB_CLIENTE");
        });

        modelBuilder.Entity<TbProduto>(entity =>
        {
            entity.ToTable("TB_PRODUTO");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.NotaFiscalFornecedor).HasColumnName("nota_fiscal_fornecedor");
            entity.Property(e => e.Preco)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("preco");
        });

        modelBuilder.Entity<TbUsuario>(entity =>
        {
            entity.ToTable("TB_USUARIO");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Senha)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("senha");
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        modelBuilder.Entity<TbVenda>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TB_VENDA");

            entity.ToTable("TB_VENDAS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FkCliente).HasColumnName("fkCliente");
            entity.Property(e => e.FkProduto).HasColumnName("fkProduto");
            entity.Property(e => e.NotaFiscalVenda).HasColumnName("nota_fiscal_venda");
            entity.Property(e => e.Quantidade).HasColumnName("quantidade");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("valor");

            entity.HasOne(d => d.FkClienteNavigation).WithMany(p => p.TbVenda)
                .HasForeignKey(d => d.FkCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_VENDAS_TB_CLIENTE");

            entity.HasOne(d => d.FkProdutoNavigation).WithMany(p => p.TbVenda)
                .HasForeignKey(d => d.FkProduto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_VENDAS_TB_PRODUTO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
