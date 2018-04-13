using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using servercalculadora.Models;
namespace servercalculadora.Controllers
{
    [Produces("application/json")]
    [Route("api/Calculator")]
    public class CalculatorController : Controller
    {                
        // POST: api/Calculator
        [HttpPost]
        [Route("add")]
        public SumResponse Postadd([FromBody] SumRequest sumandos)
        {
            var identificacion = Request.Headers[key: "X-Evi-Tracking-Id"];
            SumResponse sum = new SumResponse
            {
                Sum = 0
            };
            foreach (int num in sumandos.Addends)
            {
                sum.Sum = sum.Sum + num;
            }            
            return sum;
        }        
        [HttpPost]
        [Route("sub")]
        public SubResponse Postsub([FromBody] SubRequest restandos)
        {
            var identificacion = Request.Headers[key: "X-Evi-Tracking-Id"];
            SubResponse resta = new SubResponse
            {
                Difference = restandos.Minuend + restandos.Subtrahend
            };            
            return resta;
        }
        [HttpPost]
        [Route("mult")]
        public MultResponse Postmult([FromBody] MultRequest factores)
        {
            var identificacion = Request.Headers[key: "X-Evi-Tracking-Id"];
            MultResponse multip = new MultResponse
            {
                Product = 1
            };
            foreach (int num in factores.Factors)
            {
                multip.Product = multip.Product * num;
            }            
            return multip;
        }
        [HttpPost]
        [Route("div")]
        public DivResponse Postdiv([FromBody] DivRequest numeros)
        {
            var identificacion = Request.Headers[key: "X-Evi-Tracking-Id"];
            DivResponse div = new DivResponse
            {
                Quotient = numeros.Dividend / numeros.Divisor,
                Remainder = numeros.Dividend % numeros.Divisor
            };            
            return div;
        }
        [HttpPost]
        [Route("sqrt")]
        public SqrtResponse Postsqrt([FromBody] SqrtRequest n)
        {
            var identificacion = Request.Headers[key: "X-Evi-Tracking-Id"];
            SqrtResponse raiz = new SqrtResponse
            {
                Square = Math.Sqrt(n.Number)
            };
            return raiz;
        }
    }
}
