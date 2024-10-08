using ATMWebAPI.Model;
using ATMWebAPI.ORM;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.ConstrainedExecution;

namespace ATMWebAPI.Repositorio
{
    public class EnderecoRepositorio
    {
        private BdAtmContext _context;
        public EnderecoRepositorio(BdAtmContext context)
        {
            _context = context;
        }
        public void Add(Endereco endereco)
        {
            // Cria uma nova entidade do tipo tbCliente a partir do objeto Funcionario recebido
            var tbEndereco = new TbEndereco()
            {
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                Cep = endereco.Cep,
                PontoReferencia = endereco.PontoReferencia,
                FkCliente = endereco.FkCliente
            };

            // Adiciona a entidade ao contexto
            _context.TbEnderecos.Add(tbEndereco);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbEndereco = _context.TbEnderecos.FirstOrDefault(e => e.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbEndereco != null)
            {
                // Remove a entidade do contexto
                _context.TbEnderecos.Remove(tbEndereco);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Endereço não encontrado.");
            }
        }
        public List<Endereco> GetAll()
        {
            List<Endereco> listEnd = new List<Endereco>();

            var listTb = _context.TbEnderecos.ToList();

            foreach (var item in listTb)
            {
                var endereco = new Endereco
                {
                    Logradouro = item.Logradouro,
                    Numero = item.Numero,
                    Cidade = item.Cidade,
                    Estado = item.Estado,
                    Cep = item.Cep,
                    PontoReferencia = item.PontoReferencia,
                    FkCliente = item.FkCliente
                };

                listEnd.Add(endereco);
            }

            return listEnd;
        }
        public Endereco GetById(int id)
        {
            // Busca o endereco pelo ID no banco de dados
            var item = _context.TbEnderecos.FirstOrDefault(e => e.Id == id);

            // Verifica se o Endereço foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe endereco
            var endereco = new Endereco
            {
                Logradouro = item.Logradouro,
                Numero = item.Numero,
                Cidade = item.Cidade,
                Estado = item.Estado,
                Cep = item.Cep,
                PontoReferencia = item.PontoReferencia,
                FkCliente = item.FkCliente 
            };

            return endereco; // Retorna o endereco encontrado
        }
        public void Update(Endereco endereco)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbEndereco = _context.TbEnderecos.FirstOrDefault(e => e.Id == endereco.Id);

            // Verifica se a entidade foi encontrada
            if (tbEndereco != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbEndereco.Logradouro = endereco.Logradouro;
                tbEndereco.Numero = endereco.Numero;
                tbEndereco.Cidade = endereco.Cidade;
                tbEndereco.Estado = endereco.Estado;
                tbEndereco.Cep = endereco.Cep;
                tbEndereco.PontoReferencia = endereco.PontoReferencia;
                tbEndereco.FkCliente = endereco.FkCliente;

                // Atualiza as informações no contexto
                _context.TbEnderecos.Update(tbEndereco);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Endereço não encontrado.");
            }
        }
    }
}
