using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using servercalculadora.Models;
using Newtonsoft.Json;

namespace servercalculadora.Controllers
{
    [Produces("application/json")]
    [Route("api/Calculadora")]
    public class CalculadoraController : Controller
    {        
        [HttpPost]
        
        public string sumar([FromBody] SumRequest sumandos)
        {
            SumResponse sum = new SumResponse();
            sum.Sum = sumandos.Addends[0] + sumandos.Addends[1];
            string respuesta = JsonConvert.SerializeObject(sum);
            return respuesta;
        }

      /*  [HttpPost]
        [ActionName("resta")]
        public string restar([FromBody] SumRequest sumandos)
        {
            SumResponse sum = new SumResponse();
            sum.Resultado = sumandos.Addens[0] + sumandos.Addens[1];
            string respuesta = JsonConvert.SerializeObject(sum);
            return respuesta;
        }*/
    }
   
}