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
    public class VendaController : ControllerBase
    {
        private readonly VendaRepositorio _vendaRepo;

        public VendaController(VendaRepositorio vendaRepo)
        {
            _vendaRepo = vendaRepo;
        }

        // GET: api/Venda/{id}/notaFiscal
        [HttpGet("{id}/notaFiscal")]
        public IActionResult GetNotaFiscal(int id)
        {
            // Busca o Venda pelo ID
            var venda = _vendaRepo.GetById(id);

            // Verifica se o venda foi encontrado
            if (venda == null || venda.NotaFiscalVenda == null)
            {
                return NotFound(new { Mensagem = "Nota fiscal não encontrada." });
            }

            // Retorna a foto como um arquivo de imagem
            return File(venda.NotaFiscalVenda, "image/jpeg"); // Ou "image/png" dependendo do formato
        }

        // GET: api/<VendaController>
        [HttpGet]
        public ActionResult<List<Venda>> GetAll()
        {
            // Chama o repositório para obter todos os vendas
            var vendas = _vendaRepo.GetAll();

            // Verifica se a lista de vendas está vazia
            if (vendas == null || !vendas.Any())
            {
                return NotFound(new { Mensagem = "Nenhuma venda encontrado." });
            }

            // Mapeia a lista de produtos para incluir a URL da foto
            var listaComUrl = vendas.Select(venda => new Venda
            {
                Id = venda.Id,
                Quantidade = venda.Quantidade,
                Valor = venda.Valor,
                FkCliente = venda.FkCliente,
                FkProduto = venda.FkProduto,
                NotaFiscalVenda = venda.NotaFiscalVenda,
                UrlNotaFiscalVenda = $"{Request.Scheme}://{Request.Host}/api/Venda/{venda.Id}/notaFiscal" // Define a URL completa para a imagem
            }).ToList();

            // Retorna a lista de vendas com status 200 OK
            return Ok(listaComUrl);
        }

        // GET: api/Venda/{id}
        [HttpGet("{id}")]
        public ActionResult<Venda> GetById(int id)
        {
            // Chama o repositório para obter o venda pelo ID
            var venda = _vendaRepo.GetById(id);

            // Se o venda não for encontrado, retorna uma resposta 404
            if (venda == null)
            {
                return NotFound(new { Mensagem = "Venda não encontrada." }); // Retorna 404 com mensagem
            }

            // Mapeia o venda encontrado para incluir a URL da foto
            var vendaComUrl = new Venda
            {
                Id = venda.Id,
                Quantidade = venda.Quantidade,
                Valor = venda.Valor,
                FkCliente = venda.FkCliente,
                FkProduto = venda.FkProduto,
                UrlNotaFiscalVenda = $"{Request.Scheme}://{Request.Host}/api/Venda/{venda.Id}/notaFiscal" // Define a URL completa para a imagem
            };

            // Retorna o venda com status 200 OK
            return Ok(vendaComUrl);
        }

        // POST api/<VendaController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] VendaDto novoVenda)
        {
            // Cria uma nova instância do modelo Venda a partir do DTO recebido
            var venda = new Venda
            {
                Quantidade = novoVenda.Quantidade,
                Valor = novoVenda.Valor,
                FkCliente = novoVenda.FkCliente,
                FkProduto = novoVenda.FkProduto
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _vendaRepo.Add(venda, novoVenda.NotaFiscalVenda);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Venda cadastrada com sucesso!",
                Quantidade = novoVenda.Quantidade,
                Valor = novoVenda.Valor,
                FkCliente = novoVenda.FkCliente,
                FkProduto = novoVenda.FkProduto
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<VendaController>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] VendaDto vendaAtualizado)
        {
            // Busca o produto existente pelo Id
            var vendaExistente = _vendaRepo.GetById(id);

            // Verifica se o produto foi encontrado
            if (vendaExistente == null)
            {
                return NotFound(new { Mensagem = "Venda não encontrada." });
            }

            // Atualiza os dados do produto existente com os valores do objeto recebido
            vendaExistente.Quantidade = vendaAtualizado.Quantidade;
            vendaExistente.Valor = vendaAtualizado.Valor;
            vendaExistente.FkCliente = vendaAtualizado.FkCliente;
            vendaExistente.FkProduto = vendaAtualizado.FkProduto;


            // Chama o método de atualização do repositório, passando a nova foto
            _vendaRepo.Update(vendaExistente, vendaAtualizado.NotaFiscalVenda);

            // Cria a URL da foto
            var urlNotaFiscal = $"{Request.Scheme}://{Request.Host}/api/Venda/{vendaExistente.Id}/notaFiscal";

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Venda atualizada com sucesso!",
                Quantidade = vendaExistente.Quantidade,
                Valor = vendaExistente.Valor,
                FkCliente = vendaExistente.FkCliente,
                FkProduto = vendaExistente.FkProduto,
                UrlNotaFiscal = urlNotaFiscal // Inclui a URL da nota fiscal na resposta
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<VendaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o venda existente pelo Id
            var vendaExistente = _vendaRepo.GetById(id);

            // Verifica se o venda foi encontrado
            if (vendaExistente == null)
            {
                return NotFound(new { Mensagem = "Venda não encontrada." });
            }

            // Chama o método de exclusão do repositório
            _vendaRepo.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Venda excluída com sucesso!",
                Quantidade = vendaExistente.Quantidade,
                Valor = vendaExistente.Valor,
                FkCliente = vendaExistente.FkCliente,
                FkProduto = vendaExistente.FkProduto
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}
