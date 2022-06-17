using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Services;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        
        //declaração do service usado
        private IBookService _personService;

        //injeção da instancia de IPersonService
        //quando criada a instacia de PersonController
        public BookController(ILogger<BookController> logger, IBookService bookService)
        {
            _logger = logger;
            _personService = bookService;
        }

        /// Maps GET requests to https://localhost:{port}/api/book
        // Get no parameters for FindAll -> Search All
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personService.FindAll());
        }

        // Maps GET requests to https://localhost:{port}/api/book/{id}
        // recebendo um ID como no Caminho da Solicitação
        // Get com parâmetros para FindById -> Search by ID
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var book = _personService.FindByID(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        // Maps POST requests to https://localhost:{port}/api/book/
        // [FromBody] consome o objeto JSON enviado no corpo da solicitação
        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            if (book == null) return BadRequest();
            return Ok(_personService.Create(book));
        }

        // Maps PUT requests to https://localhost:{port}/api/book/
        // [FromBody] consome the JSON object sent in the request body
        [HttpPut]
        public IActionResult Put([FromBody] Book book)
        {
            if (book == null) return BadRequest();
            return Ok(_personService.Update(book));
        }

        // Maps DELETE requests to https://localhost:{port}/api/book/{id}
        // recebendo um ID como no Caminho da Solicitação
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personService.Delete(id);
            return NoContent();
        }
    }
}
