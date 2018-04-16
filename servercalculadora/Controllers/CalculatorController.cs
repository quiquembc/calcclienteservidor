using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using servercalculadora.Models;
using servercalculadora;
namespace servercalculadora.Controllers
{
	[Produces("application/json")]
	[Route("api/Calculator")]
	public class CalculatorController : Controller
	{
		public static UsersBook MyBook { get; set; }
		[HttpGet]
		[Route("Index")]
		public String Index()
		{
			return "Calculator server is listening...";
		}
		// POST: api/Calculator
		[HttpPost]
		[Route("add")]
		public SumResponse Postadd([FromBody] SumRequest sumandos)
		{
			string identification = Request.Headers[key: "X-Evi-Tracking-Id"];
			User currentUser=UsersHandler.KnownOrNot(identification);
			SumResponse sum = new SumResponse
			{
				Sum = 0
			};
			foreach (int num in sumandos.Addends)
			{
				sum.Sum = sum.Sum + num;
			}
			UsersHandler.AddAnOperation(new AddOperation(sumandos, sum),currentUser);
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
	public class UsersHandler
	{
		
		public static User KnownOrNot(string identification)
		{
			if (CalculatorController.MyBook==null)
			{
				CalculatorController.MyBook = new UsersBook();
				User currentUser = new User(identification);
				CalculatorController.MyBook.Users.Add(currentUser);
				return currentUser;
			}
			else
			{
				if (CalculatorController.MyBook.Users.Where(user => user.Login==identification).Count()==1)
				{
					return CalculatorController.MyBook.Users.Find(user => user.Login == identification);
				}
				else
				{
					User currentUser = new User(identification);
					CalculatorController.MyBook.Users.Add(currentUser);
					return currentUser;
				}
			}
		}
		public static void AddAnOperation(IOperation newOperation,User currentUser)
		{
			currentUser.Operations.Add(newOperation);
		}
	}
}