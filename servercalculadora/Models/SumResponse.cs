using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace servercalculadora.Models
{
	public class SumResponse
	{
		public int Sum { get; set; }
	}
	public class MultRequest
	{
		public int[] Factors { get; set; }
	}
	public class MultResponse
	{
		public int Product { get; set; }
	}
	public class DivRequest
	{
		public int Dividend { get; set; }
		public int Divisor { get; set; }
	}
	public class DivResponse
	{
		public int Quotient { get; set; }
		public int Remainder { get; set; }
	}
	public class SqrtResponse
	{
		public double Square { get; set; }
	}
	public class SqrtRequest
	{
		public double Number { get; set; }
	}
}
