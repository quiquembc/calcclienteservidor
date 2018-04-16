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
	public class UsersBook
	{
		public List<User> Users = new List<User> { };
	}
	public class AddOperation : IOperation
	{
		public string info;
		public AddOperation(SumRequest petition, SumResponse devolution)
		{
			string aux = "";
			for (int i = 0; i < petition.Addends.Length ; i++)
			{
				if (i<(petition.Addends.Length-1))
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
	public interface IOperation
	{
		string Calculation();

	}
}