using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASP_NET5Udemy.Model;
using RestWithASP_NET5Udemy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASP_NET5Udemy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonControllers : ControllerBase
    {
        private readonly ILogger<PersonControllers> _logger;
        //declaração do service usado
        private IPersonService _personService;

        //injeção da instancia de IPersonService
        //quando criada a instacia de PersonController
        public PersonControllers(ILogger<PersonControllers> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        // Maps GET requests to https://localhost:{port}/api/person
        // Get no parameters for FindAll -> Search All
        [HttpGet] 
        public IActionResult Get()
        {
            return Ok(_personService.FindAll());
        }

        // Maps GET requests to https://localhost:{port}/api/person/{id}
        // receiving an ID as in the Request Path
        // Get with parameters for FindById -> Search by ID
        [HttpGet("{id}")] 
        public IActionResult Get(long id)
        {
            var person = _personService.FindByID(id);
            if (person == null) NotFound();
            return Ok(person);
        }

        // Maps POST requests to https://localhost:{port}/api/person/
        // [FromBody] consumes the JSON object sent in the request body
        [HttpPost] 
        public IActionResult Post([FromBody] Person person)
        {
            if (person == null) BadRequest();
            return Ok(_personService.Create(person));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {
            if (person == null) BadRequest();
            return Ok(_personService.Update(person));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personService.Delete(id);
            return NoContent();
        }

        
    }
}
