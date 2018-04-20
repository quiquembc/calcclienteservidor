using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace servercalculadora.Models
{
	public class Error
	{
		public String ErrorCode { get; set; }
		public int ErrorStatus { get; set; }
		public String ErrorMessage { get; set; }
	}
}