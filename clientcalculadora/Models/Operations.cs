using System;
using System.Collections.Generic;
using System.Text;

namespace clientcalculadora.Models
{
	public class StandartOperation : IOperation
	{
		public string info { get; set; }
		public string Calculation()
		{
			return info;
		}
	}
	public class AddOperation : IOperation
	{
		public string info;
		public AddOperation(SumRequest petition, SumResponse devolution)
		{
			string aux = "";
			for (int i = 0; i < petition.Addends.Length; i++)
			{
				if (i < (petition.Addends.Length - 1))
				{
					aux = String.Format(" {0}{1} +", aux, petition.Addends[i]);
				}
				else
				{
					aux = String.Format("{0}{1} = ", aux, petition.Addends[i]);
				}
			}
			info = String.Format("{0}{1}", aux, devolution.Sum);
		}
		public string Calculation()
		{
			return info;
		}
	}
	public class SubtractOperation : IOperation
	{
		public string info;

		public SubtractOperation(SubRequest request, SubResponse response)
		{
			info = String.Format("{0}{1} = {2}", request.Minuend, request.Subtrahend, response.Difference);
		}
		public string Calculation()
		{
			return info;
		}
	}
	public class MultOperation : IOperation
	{
		public string info;
		public MultOperation(MultRequest petition, MultResponse devolution)
		{
			string aux = "";
			for (int i = 0; i < petition.Factors.Length; i++)
			{
				if (i < (petition.Factors.Length - 1))
				{
					aux = String.Format(" {0}{1} *", aux, petition.Factors[i]);
				}
				else
				{
					aux = String.Format("{0}{1} = ", aux, petition.Factors[i]);
				}
			}
			info = String.Format("{0}{1}", aux, devolution.Product);
		}
		public string Calculation()
		{
			return info;
		}
	}
	public class DivisionOperation : IOperation
	{
		public string info;

		public DivisionOperation(DivRequest request, DivResponse response)
		{
			info = String.Format("{0}{1} = {2} ,  Remainder = {3} ", request.Dividend, request.Divisor, response.Quotient, response.Remainder);
		}
		public string Calculation()
		{
			return info;
		}
	}
	public class SqrtOperation : IOperation
	{
		public string info;
		public SqrtOperation(SqrtRequest request, SqrtResponse response)
		{
			info = String.Format("The square root of {0} is {1} ", request.Number, response.Square);
		}
		public string Calculation()
		{
			return info;
		}
	}
	public interface IOperation
	{
		string Calculation();
	}
}
