using ATMWebAPI.Model;
using ATMWebAPI.ORM;
using ATMWebAPI.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ATMWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnderecoController : ControllerBase
    {
        private readonly EnderecoRepositorio _enderecoRepo;

        public EnderecoController(EnderecoRepositorio enderecoRepo)
        {
            _enderecoRepo = enderecoRepo;
        }


        // GET: api/<EnderecoController>
        [HttpGet]
        public ActionResult<List<Endereco>> GetAll()
        {
            // Chama o repositório para obter todos os produtos
            var enderecos = _enderecoRepo.GetAll();

            // Verifica se a lista de  enderecos está vazia
            if (enderecos == null || !enderecos.Any())
            {
                return NotFound(new { Mensagem = "Nenhum produto encontrado." });
            }

            // Mapeia a lista de  enderecos para incluir a URL da foto
            var listaComUrl = enderecos.Select(endereco => new Endereco
            {
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                Cep = endereco.Cep,
                PontoReferencia = endereco.PontoReferencia,
                FkCliente = endereco.FkCliente
            }).ToList();

            // Retorna a lista de produtos com status 200 OK
            return Ok(listaComUrl);
        }

        // GET: api/Endereco/{id}
        [HttpGet("{id}")]
        public ActionResult<Endereco> GetById(int id)
        {
            // Chama o repositório para obter o produto pelo ID
            var endereco = _enderecoRepo.GetById(id);

            // Se o endereco não for encontrado, retorna uma resposta 404
            if (endereco == null)
            {
                return NotFound(new { Mensagem = "Endereço não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o produto encontrado para incluir a URL da foto
            var enderecoComUrl = new Endereco
            {
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                Cep = endereco.Cep,
                PontoReferencia = endereco.PontoReferencia,
                FkCliente = endereco.FkCliente
            };

            // Retorna o produto com status 200 OK
            return Ok(enderecoComUrl);
        }

        // POST api/<EnderecoController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] EnderecoDto novoEndereco)
        {
            // Cria uma nova instância do modelo Endereco a partir do DTO recebido
            var endereco = new Endereco
            {
                Logradouro = novoEndereco.Logradouro,
                Numero = novoEndereco.Numero,
                Cidade = novoEndereco.Cidade,
                Estado = novoEndereco.Estado,
                Cep = novoEndereco.Cep,
                PontoReferencia = novoEndereco.PontoReferencia,
                FkCliente = novoEndereco.FkCliente
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _enderecoRepo.Add(endereco);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Endereco cadastrado com sucesso!",
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                Cep = endereco.Cep,
                PontoReferencia = endereco.PontoReferencia,
                FkCliente = endereco.FkCliente
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<EnderecoController>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] EnderecoDto enderecoAtualizado)
        {
            // Busca o produto existente pelo Id
            var enderecoExistente = _enderecoRepo.GetById(id);

            // Verifica se o produto foi encontrado
            if (enderecoExistente == null)
            {
                return NotFound(new { Mensagem = "Produto não encontrado." });
            }

            // Atualiza os dados do produto existente com os valores do objeto recebido
            enderecoExistente.Logradouro = enderecoAtualizado.Logradouro;
            enderecoExistente.Numero = enderecoAtualizado.Numero;
            enderecoExistente.Cidade = enderecoAtualizado.Cidade;
            enderecoExistente.Estado = enderecoAtualizado.Estado;
            enderecoExistente.Cep = enderecoAtualizado.Cep;
            enderecoExistente.PontoReferencia = enderecoAtualizado.PontoReferencia;
            enderecoExistente.FkCliente = enderecoAtualizado.FkCliente;

            // Chama o método de atualização do repositório, passando a nova foto
            _enderecoRepo.Update(enderecoExistente);


            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário atualizado com sucesso!",
                Logradouro = enderecoExistente.Logradouro,
                Numero = enderecoExistente.Numero,
                Cidade = enderecoExistente.Cidade,
                Estado = enderecoExistente.Estado,
                Cep = enderecoExistente.Cep,
                PontoReferencia = enderecoExistente.PontoReferencia,
                FkCliente = enderecoExistente.FkCliente
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<EnderecoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o endereco existente pelo Id
            var enderecoExistente = _enderecoRepo.GetById(id);

            // Verifica se o endereco foi encontrado
            if (enderecoExistente == null)
            {
                return NotFound(new { Mensagem = "Endereço não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _enderecoRepo.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Endereço excluído com sucesso!",
                Logradouro = enderecoExistente.Logradouro,
                Numero = enderecoExistente.Numero,
                Cidade = enderecoExistente.Cidade,
                Estado = enderecoExistente.Estado,
                Cep = enderecoExistente.Cep,
                PontoReferencia = enderecoExistente.PontoReferencia,
                FkCliente = enderecoExistente.FkCliente
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}
