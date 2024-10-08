using ATMWebAPI.Model;
using ATMWebAPI.ORM;

namespace ATMWebAPI.Repositorio
{
    public class VendaRepositorio
    {
        private BdAtmContext _context;
        public VendaRepositorio(BdAtmContext context)
        {
            _context = context;
        }
        public void Add(Venda venda, IFormFile notaFiscal)
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

            // Cria uma nova entidade do tipo tbVenda a partir do objeto Venda recebido
            var tbVenda = new TbVenda()
            {
                Quantidade = venda.Quantidade,
                Valor = venda.Valor,
                FkCliente = venda.FkCliente,
                FkProduto = venda.FkProduto,
                NotaFiscalVenda = notaFiscalBytes // Armazena a foto na entidade
            };

            // Adiciona a entidade ao contexto
            _context.TbVendas.Add(tbVenda);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbVenda = _context.TbVendas.FirstOrDefault(v => v.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbVenda != null)
            {
                // Remove a entidade do contexto
                _context.TbVendas.Remove(tbVenda);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Venda não encontrado.");
            }
        }
        public List<Venda> GetAll()
        {
            List<Venda> listVen = new List<Venda>();

            var listTb = _context.TbVendas.ToList();

            foreach (var item in listTb)
            {
                var venda = new Venda
                {
                    Id = item.Id,
                    Quantidade = item.Quantidade,
                    FkCliente= item.FkCliente,
                    FkProduto= item.FkProduto
                };

                listVen.Add(venda);
            }

            return listVen;
        }
        public Venda GetById(int id)
        {
            // Busca o venda pelo ID no banco de dados
            var item = _context.TbVendas.FirstOrDefault(p => p.Id == id);

            // Verifica se o venda foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe produto
            var venda = new Venda
            {
                Id = item.Id,
                Quantidade = item.Quantidade,
                FkCliente = item.FkCliente,
                FkProduto = item.FkProduto,
                NotaFiscalVenda = item.NotaFiscalVenda // Mantém o campo NotaFiscalVenda como byte[]
            };

            return venda; // Retorna o produto encontrado
        }
        public void Update(Venda venda, IFormFile notaFiscal)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbVenda = _context.TbVendas.FirstOrDefault(p => p.Id == venda.Id);

            // Verifica se a entidade foi encontrada
            if (tbVenda != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbVenda.Quantidade = venda.Quantidade;
                tbVenda.Valor = venda.Valor;
                tbVenda.FkCliente = venda.FkCliente;
                tbVenda.FkProduto = venda.FkProduto;

                // Verifica se uma nova foto foi enviada
                if (notaFiscal != null && notaFiscal.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        notaFiscal.CopyTo(memoryStream);
                        tbVenda.NotaFiscalVenda = memoryStream.ToArray(); // Atualiza a foto na entidade
                    }
                }

                // Atualiza as informações no contexto
                _context.TbVendas.Update(tbVenda);

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
