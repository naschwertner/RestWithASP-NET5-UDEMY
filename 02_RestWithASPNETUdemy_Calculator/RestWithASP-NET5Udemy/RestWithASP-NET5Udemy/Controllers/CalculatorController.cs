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
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;
        private bool isNumber;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }


        [HttpGet("sum/{firstNumber}/{secondNumber}")] //path especifico onde vão ser passados os parâmetros
        public IActionResult Get(string firstNumber, string secondNumber)
        {
            //validação dos números 
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = ConvertTodecimal(firstNumber) + ConvertTodecimal(secondNumber);
                return Ok(sum.ToString());
            }
            return BadRequest("Invalid Input");
        }

        //metodo para conferir se é um número       
        private bool IsNumeric(string strNumber)
        {
            double number;
            bool isNumber = double.TryParse( //metodo TryParse são usados para converter string em decimal
                strNumber,
                System.Globalization.NumberStyles.Any,  //idenfica numero com . ou (variação de país)
                System.Globalization.NumberFormatInfo.InvariantInfo, //idenfica numero com . ou (variação de país)
                out number);
            return isNumber;
        }

        //metodo para converter string para decimal 
        private decimal ConvertTodecimal(string strNumber)
        {
            decimal decimalValue;
            if(decimal.TryParse(strNumber,out decimalValue))
            {
                return decimalValue;
            }
            return 0;
        }
        

        
    }
}
