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
       // private bool isNumber;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        //Soma 
        [HttpGet("sum/{firstNumber}/{secondNumber}")] //path especifico onde vão ser passados os parâmetros
        public IActionResult Sum(string firstNumber, string secondNumber)
        {
            //validação dos números 
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }
            return BadRequest("Invalid Input");
        }

        //Subtração
        [HttpGet("subtraction/{firstNumber}/{secondNumber}")] //path especifico onde vão ser passados os parâmetros
        public IActionResult Subtraction(string firstNumber, string secondNumber)
        {
            //validação dos números 
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }
            return BadRequest("Invalid Input");
        }

        //multiplicação
        [HttpGet("multiplication/{firstNumber}/{secondNumber}")] //path especifico onde vão ser passados os parâmetros
        public IActionResult Multiplication(string firstNumber, string secondNumber)
        {
            //validação dos números 
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }
            return BadRequest("Invalid Input");
        }
        
        //divisão
        [HttpGet("division/{firstNumber}/{secondNumber}")]
        public IActionResult Division(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }
            return BadRequest("Invalid Input");
        }

        //Média
        [HttpGet("mean/{firstNumber}/{secondNumber}")] //path especifico onde vão ser passados os parâmetros
        public IActionResult Mean(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = (ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber)) / 2;
                return Ok(sum.ToString());
            }
            return BadRequest("Invalid Input");
        }

        //Raiz quadrada
        [HttpGet("square-root/{firstNumber}")] //path especifico onde vão ser passados os parâmetros
        public IActionResult SquareRoot(string firstNumber)
        {
            //validação dos números 
            if (IsNumeric(firstNumber))
            {
                var squareRoot = Math.Sqrt((double)ConvertToDecimal(firstNumber));
                return Ok(squareRoot.ToString());
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
        private decimal ConvertToDecimal(string strNumber)
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
