using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Hypermedia.Filters;
using RestWithASPNETUdemy.Services;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        
        //declaração do service usado
        private IPersonService _personService;

        //injeção da instancia de IPersonService
        //quando criada a instacia de PersonController
        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        /// Maps GET requests to https://localhost:{port}/api/persons
        // Get no parameters for FindAll -> Search All
        [HttpGet]
        [ProducesResponseType ((200),Type = typeof(List<PersonVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_personService.FindAll());
        }

        // Maps GET requests to https://localhost:{port}/api/person/{id}
        // recebendo um ID como no Caminho da Solicitação
        // Get com parâmetros para FindById -> Search by ID
        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var person = _personService.FindByID(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        // Maps POST requests to https://localhost:{port}/api/person/
        // [FromBody] consome o objeto JSON enviado no corpo da solicitação
        [HttpPost]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(_personService.Create(person));
        }

        // Maps PUT requests to https://localhost:{port}/api/person/
        // [FromBody] consome the JSON object sent in the request body
        [HttpPut]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(_personService.Update(person));
        }

        // Maps DELETE requests to https://localhost:{port}/api/person/{id}
        // recebendo um ID como no Caminho da Solicitação
        [HttpDelete("{id}")]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _personService.Delete(id);
            return NoContent();
        }
    }
}
