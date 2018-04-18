using System.Collections.Generic;


namespace clientcalculadora.Models
{
	public class StandartOperation : IOperation
	{
		public string Calculation { get; set; }
		public string Date { get; set; }
		public string Operation { get; set; }
		public string Info()
		{
			return $"Operation: {Operation} Calculation: {Calculation} Date: {Date}";
		}
	}
	public interface IOperation
	{
		string Info();
	}
	public class JournalResponse
	{
		public List<StandartOperation> Operations { get; set; }
	}
	public class JournalRequest
	{
		public string Id { get; set; }
	}
}
