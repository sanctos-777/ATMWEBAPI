using ATMWebAPI.Model;
using ATMWebAPI.ORM;
using ATMWebAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATMWebAPI.Repositorio
{
    public class ClienteRepositorio 
    {
        private BdAtmContext _context;
        public ClienteRepositorio(BdAtmContext context)
        {
            _context = context;
        }
        public void Add(Cliente cliente, IFormFile documento)
        {
            // Verifica se uma foto foi enviada
            byte[] documentoBytes = null;
            if (documento != null && documento.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    documento.CopyTo(memoryStream);
                    documentoBytes = memoryStream.ToArray();
                }
            }

            // Cria uma nova entidade do tipo tbCliente a partir do objeto Funcionario recebido
            var tbCliente = new TbCliente()
            {
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                DocumentoIdentificacao = documentoBytes // Armazena a foto na entidade
            };

            // Adiciona a entidade ao contexto
            _context.TbClientes.Add(tbCliente);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbCliente = _context.TbClientes.FirstOrDefault(c => c.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbCliente != null)
            {
                // Remove a entidade do contexto
                _context.TbClientes.Remove(tbCliente);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Funcionário não encontrado.");
            }
        }
        public List<Cliente> GetAll()
        {
            List<Cliente> listCli = new List<Cliente>();

            var listTb = _context.TbClientes.ToList();

            foreach (var item in listTb)
            {
                var cliente = new Cliente
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Telefone = item.Telefone
                };

                listCli.Add(cliente);
            }

            return listCli;
        }
        public Cliente GetById(int id)
        {
            // Busca o cliente pelo ID no banco de dados
            var item = _context.TbClientes.FirstOrDefault(c => c.Id == id);

            // Verifica se o cliente foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Funcionario
            var cliente = new Cliente
            {
                Id = item.Id,
                Nome = item.Nome,
                Telefone = item.Telefone,
                Documento = item.DocumentoIdentificacao // Mantém o campo Documento como byte[]
            };

            return cliente; // Retorna o cliente encontrado
        }
        public void Update(Cliente cliente, IFormFile documento)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbCliente = _context.TbClientes.FirstOrDefault(c => c.Id == cliente.Id);

            // Verifica se a entidade foi encontrada
            if (tbCliente != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbCliente.Nome = cliente.Nome;
                tbCliente.Telefone = cliente.Telefone;

                // Verifica se uma nova foto foi enviada
                if (documento != null && documento.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        documento.CopyTo(memoryStream);
                        tbCliente.DocumentoIdentificacao = memoryStream.ToArray(); // Atualiza a foto na entidade
                    }
                }

                // Atualiza as informações no contexto
                _context.TbClientes.Update(tbCliente);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Funcionário não encontrado.");
            }
        }
    }
}
