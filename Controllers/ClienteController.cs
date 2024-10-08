using ATMWebAPI.Repositorio;
using ATMWebAPI.Model;
using ATMWebAPI.ORM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ATMWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteRepositorio _clienteRepo;

        public ClienteController(ClienteRepositorio clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }

        // GET: api/Cliente/{id}/foto
        [HttpGet("{id}/foto")]
        public IActionResult GetDocumento(int id)
        {
            // Busca o cliente pelo ID
            var cliente = _clienteRepo.GetById(id);

            // Verifica se o cliente foi encontrado
            if (cliente == null || cliente.Documento == null)
            {
                return NotFound(new { Mensagem = "Documento não encontrada." });
            }

            // Retorna a foto como um arquivo de imagem
            return File(cliente.Documento, "image/jpeg"); // Ou "image/png" dependendo do formato
        }

        // GET: api/<ClienteController>
        [HttpGet]
        public ActionResult<List<Cliente>> GetAll()
        {
            // Chama o repositório para obter todos os clientes
            var clientes = _clienteRepo.GetAll();

            // Verifica se a lista de clientes está vazia
            if (clientes == null || !clientes.Any())
            {
                return NotFound(new { Mensagem = "Nenhum cliente encontrado." });
            }

            // Mapeia a lista de clientes para incluir a URL da foto
            var listaComUrl = clientes.Select(cliente => new Cliente
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                UrlDocumento = $"{Request.Scheme}://{Request.Host}/api/Cliente/{cliente.Id}/documento" // Define a URL completa para a imagem
            }).ToList();

            // Retorna a lista de clientes com status 200 OK
            return Ok(listaComUrl);
        }

        // GET: api/Cliente/{id}
        [HttpGet("{id}")]
        public ActionResult<Cliente> GetById(int id)
        {
            // Chama o repositório para obter o funcionário pelo ID
            var cliente = _clienteRepo.GetById(id);

            // Se o funcionário não for encontrado, retorna uma resposta 404
            if (cliente == null)
            {
                return NotFound(new { Mensagem = "Cliente não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o funcionário encontrado para incluir a URL da foto
            var clienteComUrl = new Cliente
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                UrlDocumento = $"{Request.Scheme}://{Request.Host}/api/Cliente/{cliente.Id}/documento" // Define a URL completa para a imagem
            };

            // Retorna o funcionário com status 200 OK
            return Ok(clienteComUrl);
        }

        // POST api/<ClienteController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] ClienteDto novoCliente)
        {
            // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
            var cliente = new Cliente
            {
                Nome = novoCliente.Nome,
                Telefone = novoCliente.Telefone
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _clienteRepo.Add(cliente, novoCliente.Documento);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Cliente cadastrado com sucesso!",
                Nome = cliente.Nome,
                Telefone = cliente.Telefone
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<ClienteController>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] ClienteDto clienteAtualizado)
        {
            // Busca o funcionário existente pelo Id
            var clienteExistente = _clienteRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (clienteExistente == null)
            {
                return NotFound(new { Mensagem = "Cliente não encontrado." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
            clienteExistente.Nome = clienteAtualizado.Nome;
            clienteExistente.Telefone = clienteAtualizado.Telefone;

            // Chama o método de atualização do repositório, passando a nova foto
            _clienteRepo.Update(clienteExistente, clienteAtualizado.Documento);

            // Cria a URL da foto
            var urlDocumento = $"{Request.Scheme}://{Request.Host}/api/Cliente/{clienteExistente.Id}/documento";

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário atualizado com sucesso!",
                Nome = clienteExistente.Nome,
                Idade = clienteExistente.Telefone,
                UrlDocumento = urlDocumento // Inclui a URL da documento na resposta
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<ClienteController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o funcionário existente pelo Id
            var clienteExistente = _clienteRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (clienteExistente == null)
            {
                return NotFound(new { Mensagem = "Cliente não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _clienteRepo.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário excluído com sucesso!",
                Nome = clienteExistente.Nome,
                Idade = clienteExistente.Telefone
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}
