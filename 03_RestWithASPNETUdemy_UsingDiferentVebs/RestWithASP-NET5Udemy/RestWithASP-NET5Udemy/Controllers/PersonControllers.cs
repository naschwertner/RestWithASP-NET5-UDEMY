using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
       // private bool isNumber;

        public PersonControllers(ILogger<PersonControllers> logger)
        {
            _logger = logger;
        }

        //Soma 
        [HttpGet("sum/{firstNumber}/{secondNumber}")] //path especifico onde vão ser passados os parâmetros
        public IActionResult Sum(string firstNumber, string secondNumber)
        {
            return BadRequest("Invalid Input");
        }

      
        
    }
}
