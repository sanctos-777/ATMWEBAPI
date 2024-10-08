using ATMWebAPI.Model;
using ATMWebAPI.ORM;

namespace ATMWebAPI.Repositorio
{
    public class ProdutoRepositorio
    {
        private BdAtmContext _context;
        public ProdutoRepositorio(BdAtmContext context)
        {
            _context = context;
        }
        public void Add(Produto produto, IFormFile notaFiscal)
        {
            // Verifica se uma foto foi enviada
            byte[] notaFiscalBytes = null;
            if (notaFiscal != null && notaFiscal.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    notaFiscal.CopyTo(memoryStream);
                    notaFiscalBytes = memoryStream.ToArray();
                }
            }

            // Cria uma nova entidade do tipo tbCliente a partir do objeto Funcionario recebido
            var tbProduto = new TbProduto()
            {
                Nome = produto.Nome,
                Preco = produto.Preco,
                NotaFiscalFornecedor = notaFiscalBytes // Armazena a foto na entidade
            };

            // Adiciona a entidade ao contexto
            _context.TbProdutos.Add(tbProduto);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbProduto = _context.TbProdutos.FirstOrDefault(p => p.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbProduto != null)
            {
                // Remove a entidade do contexto
                _context.TbProdutos.Remove(tbProduto);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Produto não encontrado.");
            }
        }
        public List<Produto> GetAll()
        {
            List<Produto> listPro = new List<Produto>();

            var listTb = _context.TbProdutos.ToList();

            foreach (var item in listTb)
            {
                var produto = new Produto
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Preco = item.Preco
                };

                listPro.Add(produto);
            }

            return listPro;
        }
        public Produto GetById(int id)
        {
            // Busca o produto pelo ID no banco de dados
            var item = _context.TbProdutos.FirstOrDefault(p => p.Id == id);

            // Verifica se o produto foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe produto
            var produto = new Produto
            {
                Id = item.Id,
                Nome = item.Nome,
                Preco = item.Preco,
                NotaFiscalFornecedor = item.NotaFiscalFornecedor // Mantém o campo NotaFiscalFornecedor como byte[]
            };

            return produto; // Retorna o produto encontrado
        }
        public void Update(Produto produto, IFormFile notaFiscal)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbProduto = _context.TbProdutos.FirstOrDefault(p => p.Id == produto.Id);

            // Verifica se a entidade foi encontrada
            if (tbProduto != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbProduto.Nome = produto.Nome;
                tbProduto.Preco = produto.Preco;

                // Verifica se uma nova foto foi enviada
                if (notaFiscal != null && notaFiscal.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        notaFiscal.CopyTo(memoryStream);
                        tbProduto.NotaFiscalFornecedor = memoryStream.ToArray(); // Atualiza a foto na entidade
                    }
                }

                // Atualiza as informações no contexto
                _context.TbProdutos.Update(tbProduto);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Produto não encontrado.");
            }
        }
    }
}
