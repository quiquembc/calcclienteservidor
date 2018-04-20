using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
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

		[HttpPost]
		[Route("Journal")]
		public JournalResponse ReturnJournal([FromBody] JournalRequest RequestedUser)
		{
			var currentUser = UsersHandler.KnownOrNot(RequestedUser.Id);
			var Journal = new JournalResponse
			{
				Operations = currentUser.Operations
			};
			return Journal;
		}

		[HttpPost]
		[Route("add")]
		public SumResponse Postadd([FromBody] SumRequest sumandos)
		{
			SumResponse sum = new SumResponse
			{
				Sum = 0
			};
			foreach (int num in sumandos.Addends)
			{
				sum.Sum = sum.Sum + num;
			}
			if (Request.Headers["X-Evi-Tracking-Id"].Any())
			{
				var identification = Request.Headers["X-Evi-Tracking-Id"];
				var currentUser = UsersHandler.KnownOrNot(identification);
				currentUser.Operations.Add(new AddOperation(sumandos, sum));
			}
			return sum;
		}

		[HttpPost]
		[Route("sub")]
		public SubResponse Postsub([FromBody] SubRequest restandos)
		{
			SubResponse resta = new SubResponse
			{
				Difference = restandos.Minuend + restandos.Subtrahend
			};
			if (Request.Headers["X-Evi-Tracking-Id"].Any())
			{
				var identification = Request.Headers["X-Evi-Tracking-Id"];
				var currentUser = UsersHandler.KnownOrNot(identification);
				currentUser.Operations.Add(new SubtractOperation(restandos, resta));
			}
			return resta;
		}

		[HttpPost]
		[Route("mult")]
		public MultResponse Postmult([FromBody] MultRequest factores)
		{
			MultResponse multip = new MultResponse
			{
				Product = 1
			};
			foreach (int num in factores.Factors)
			{
				multip.Product = multip.Product * num;
			}
			if (Request.Headers["X-Evi-Tracking-Id"].Any())
			{
				var identification = Request.Headers["X-Evi-Tracking-Id"];
				var currentUser = UsersHandler.KnownOrNot(identification);
				currentUser.Operations.Add(new MultOperation(factores, multip));
			}
			return multip;
		}

		[HttpPost]
		[Route("div")]
		public IActionResult Postdiv([FromBody] DivRequest numeros)
		{
			int dividend;
			int divisor;
			try
			{
				dividend = numeros.Dividend;
				divisor = numeros.Divisor;
			}
			catch (Exception e)
			{
				return StatusCode(400, e);
			}
			DivResponse div;
			try
			{
				div = new DivResponse
				{
					Quotient = dividend / divisor,
					Remainder = dividend % divisor
				};
			}
			catch (Exception e)
			{
				return StatusCode(400, e);
			}
			if (Request.Headers["X-Evi-Tracking-Id"].Any())
			{
				var identification = Request.Headers["X-Evi-Tracking-Id"];
				var currentUser = UsersHandler.KnownOrNot(identification);
				currentUser.Operations.Add(new DivisionOperation(numeros, div));
			}
			return new JsonResult(div);
		}

		[HttpPost]
		[Route("sqrt")]
		public SqrtResponse Postsqrt([FromBody] SqrtRequest entry)
		{
			SqrtResponse raiz = new SqrtResponse
			{
				Square = Math.Sqrt(entry.Number)
			};
			if (Request.Headers["X-Evi-Tracking-Id"].Any())
			{
				var identification = Request.Headers["X-Evi-Tracking-Id"];
				var currentUser = UsersHandler.KnownOrNot(identification);
				currentUser.Operations.Add(new SqrtOperation(entry, raiz));
			}
			return raiz;
		}
	}

	public class UsersHandler
	{
		public static User KnownOrNot(string identification)
		{
			if (CalculatorController.MyBook == null)
			{
				CalculatorController.MyBook = new UsersBook();
				var currentUser = new User(identification);
				CalculatorController.MyBook.Users.Add(currentUser);
				return currentUser;
			}
			else
			{
				if (CalculatorController.MyBook.Users.Where(user => user.Login == identification).Count() == 1)
				{
					return CalculatorController.MyBook.Users.Find(user => user.Login == identification);
				}
				else
				{
					var currentUser = new User(identification);
					CalculatorController.MyBook.Users.Add(currentUser);
					return currentUser;
				}
			}
		}
	}
}