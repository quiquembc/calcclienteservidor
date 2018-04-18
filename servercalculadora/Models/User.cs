using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace servercalculadora.Models
{
	public class User
	{
		public User(string plogin)
		{
			this.Login = plogin;
			this.Operations = new List<IOperation>{ };
		}
		public string Login { get; set; }
		public List<IOperation> Operations { get; set; }
	}
	public class JournalResponse
	{
		public List<IOperation> Operations { get; set; }
	}
	public class JournalRequest
	{
		public string Id { get; set; }
	}
	public class UsersBook
	{
		public List<User> Users = new List<User> { };
	}
	public class AddOperation : IOperation
	{
		public string Calculation;
		public string Date;
		public string Operation;
		public AddOperation(SumRequest petition, SumResponse devolution)
		{
			string aux = "";
			for (int i = 0; i < petition.Addends.Length ; i++)
			{
				if (i<(petition.Addends.Length-1))
				{
					aux = String.Format("{0}{1}+", aux, petition.Addends[i]);
				}
				else
				{
					aux = String.Format("{0}{1}=", aux, petition.Addends[i]);
				}
			}
			Calculation = String.Format("{0}{1}", aux, devolution.Sum);
			Date = DateTime.Now.ToString("u");
			Operation = "Sum";
		}
		public string Info()
		{
			return Calculation;
		}
	}
	public class SubtractOperation : IOperation
	{
		public string Calculation;
		public string Date;
		public string Operation;
		public SubtractOperation(SubRequest request, SubResponse response)
		{
			Calculation = String.Format("{0}{1}={2}",request.Minuend,request.Subtrahend,response.Difference);
			Date = DateTime.Now.ToString("u");
			Operation = "Sub";
		}
		public string Info() => this.Calculation;
	}
	public class MultOperation : IOperation
	{
		public string Calculation;
		public string Date;
		public string Operation;
		public MultOperation(MultRequest petition, MultResponse devolution)
		{
			string aux = "";
			for (int i = 0; i < petition.Factors.Length; i++)
			{
				if (i < (petition.Factors.Length - 1))
				{
					aux = String.Format("{0}{1}*", aux, petition.Factors[i]);
				}
				else
				{
					aux = String.Format("{0}{1}=", aux, petition.Factors[i]);
				}
			}
			Calculation = String.Format("{0}{1}", aux, devolution.Product);
			Date = DateTime.Now.ToString("u");
			Operation = "Mult";
		}
		public string Info()
		{
			return Calculation;
		}
	}
	public class DivisionOperation : IOperation
	{
		public string Calculation;
		public string Date;
		public string Operation;

		public DivisionOperation(DivRequest request, DivResponse response)
		{
			Calculation = String.Format("{0}/{1}=>Quotient={2},Remainder={3} ", request.Dividend, request.Divisor, response.Quotient, response.Remainder);
			Date = DateTime.Now.ToString("u");
			Operation = "Division";
		}
		public string Info()
		{
			return Calculation;
		}
	}
	public class SqrtOperation : IOperation
	{
		public string Calculation;
		public string Date;
		public string Operation;
		public SqrtOperation(SqrtRequest request, SqrtResponse response)
		{
			Calculation = String.Format("The square root of {0} is {1} ", request.Number, response.Square);
			Date = DateTime.Now.ToString("u");
			Operation = "Root";
		}
		public string Info()
		{
			return Calculation;
		}
	}
	public interface IOperation
	{
		string Info();
	}
}