using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using clientcalculadora.Models;
using System.Collections.Generic;

namespace clientcalculadora
{
	class Program
	{
		private static string LaunchRequestAndReceiveResponse(Object request,string operationcall)
		{
			var hrequest = (HttpWebRequest)WebRequest.Create($"http://localhost:52890/api/Calculator/{operationcall}");
			hrequest.ContentType = "application/json";
			hrequest.Method = "POST";
			if (operationcall!="journal") hrequest.Headers.Add("X-Evi-Tracking-Id:" + XEviTrackingId);
			hrequest.Timeout = 5000;
			using (var sw = new StreamWriter(hrequest.GetRequestStream()))
			{
				string json = JsonConvert.SerializeObject(request);
				sw.Write(json);
				sw.Flush();
				sw.Close();
			}
			var hrespond = (HttpWebResponse)hrequest.GetResponse();
			string response1;
			using (var sr = new StreamReader(hrespond.GetResponseStream()))
			{
				response1 = sr.ReadToEnd();
				sr.Close();
			}
			return response1;
		}
		private static string Sumar()
		{
			SumRequest req = new SumRequest();
			Console.WriteLine("Introduce cuantos numeros deseas sumar");
			int cuantos = Int32.Parse(Console.ReadLine());
			req.Addends = new int[cuantos];
			Console.WriteLine("Introduce los " + cuantos + " numeros a sumar");
			for (int i = 0; i < cuantos; i++)
			{
				req.Addends[i] = Int32.Parse(Console.ReadLine());
			}			
			var rep = JsonConvert.DeserializeObject<SumResponse>(LaunchRequestAndReceiveResponse(req, "add"));
			return rep.Sum.ToString();
		}
		private static string Multiplicar()
		{
			MultRequest req = new MultRequest();
			Console.WriteLine("Introduce cuantos numeros deseas multiplicar");
			int cuantos = Int32.Parse(Console.ReadLine());
			req.Factors = new int[cuantos];
			Console.WriteLine("Introduce los " + cuantos + " numeros a multiplicar");
			for (int i = 0; i < cuantos; i++)
			{
				req.Factors[i] = Int32.Parse(Console.ReadLine());
			}			
			var rep = JsonConvert.DeserializeObject<MultResponse>(LaunchRequestAndReceiveResponse(req, "mult"));
			return rep.Product.ToString();
		}
		private static string Restar()
		{
			SubRequest petition = new SubRequest();
			Console.WriteLine("Introduce minuendo y sustraendo");
			petition.Minuend = Int32.Parse(Console.ReadLine());
			petition.Subtrahend = Int32.Parse($"-{Console.ReadLine()}");
			var response = JsonConvert.DeserializeObject<SubResponse>(LaunchRequestAndReceiveResponse(petition, "sub"));
			return response.Difference.ToString();
		}
		private static string Dividir()
		{
			DivRequest petition = new DivRequest();
			Console.WriteLine("Introduce dividendo y divisor");
			petition.Dividend = Int32.Parse(Console.ReadLine());
			petition.Divisor = Int32.Parse(Console.ReadLine());
			var response = JsonConvert.DeserializeObject<DivResponse>(LaunchRequestAndReceiveResponse(petition, "div"));
			return String.Format("El cociente de la operacion es: {0} y el resto es: {1}", response.Quotient, response.Remainder);
		}
		private static string Raiz()
		{
			SqrtRequest petition = new SqrtRequest();
			Console.WriteLine("Introduce el número, cuya raíz cuadrada quieras saber");
			petition.Number = Int32.Parse(Console.ReadLine());
			var response = JsonConvert.DeserializeObject<SqrtResponse>(LaunchRequestAndReceiveResponse(petition, "sqrt"));
			return String.Format("La raíz cuadrada del número dado es: {0}", response.Square); 
		}
		private static void RequestJournal()
		{
			var request = new JournalRequest
			{
				Id = XEviTrackingId
			};
			var response = JsonConvert.DeserializeObject<JournalResponse>(LaunchRequestAndReceiveResponse(request,"journal"));
			foreach (var operation in response.Operations)
			{
				Console.WriteLine(operation.Info());
			}
		}
		public static string XEviTrackingId;
		static void Main(string[] args)
		{
			Console.WriteLine("Bienvenido al programa calculadora");
			Console.WriteLine("Introduce una identificación");
			XEviTrackingId = Console.ReadLine().Trim();
			int menu;
			do
			{
				Console.WriteLine("Introduzca una opción según se desee");
				Console.WriteLine("1 - Sumar");
				Console.WriteLine("2 - Restar");
				Console.WriteLine("3 - Multiplicar");
				Console.WriteLine("4 - Dividir");
				Console.WriteLine("5 - Hacer la raíz cuadrada de un número");
				Console.WriteLine("6 - Consultar operaciones previas (journal)");
				Console.WriteLine("7 - Consultar logs de errores");
				Console.WriteLine("8 - Cambiar de usuario");
				Console.WriteLine("0 - Salir");
				String entrada = Console.ReadLine().Trim();
				menu = Int32.Parse(entrada);
				Console.WriteLine(menu);
				switch (menu)
				{
					case 0:
						Console.WriteLine("Vuelve pronto");
						break;
					case 1:
						Console.WriteLine("SUMA");
						Console.WriteLine(String.Format("El total de la suma es {0}",Sumar()));
						break;
					case 2:
						Console.WriteLine("RESTA");
						Console.WriteLine(String.Format("El resultado es {0}", Restar()));
						break;
					case 3:
						Console.WriteLine("MULTIPLICACIÓN");
						Console.WriteLine(String.Format("El resultado es {0}", Multiplicar()));
						break;
					case 4:
						Console.WriteLine("DIVISIÓN");
						Console.WriteLine(Dividir());
						break;
					case 5:
						Console.WriteLine("RAÍZ CUADRADA");
						Console.WriteLine(Raiz());
						break;
					case 6:
						Console.WriteLine(String.Format("JOURNAL OF USE FOR USER: {0}",XEviTrackingId));
						RequestJournal();
						break;
					case 7:
						Console.WriteLine("en construccion");
						break;
					case 8:
						Console.WriteLine("Introduce una nueva identificación");
						XEviTrackingId = Console.ReadLine().Trim();
						break;
					default:
						Console.WriteLine("No, introduce una opción válida");
						break;
				}
				Console.WriteLine("...");
			}
			while (menu != 0);
		}
	}
}