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
		private static string Sumar()
		{
			SumRequest req = new SumRequest();
			SumResponse rep;
			Console.WriteLine("Introduce cuantos numeros deseas sumar");
			int cuantos = Int32.Parse(Console.ReadLine());
			req.Addends = new int[cuantos];
			Console.WriteLine("Introduce los " + cuantos + " numeros a sumar");
			for (int i = 0; i < cuantos; i++)
			{
				req.Addends[i] = Int32.Parse(Console.ReadLine());
			}
			var hrequest = (HttpWebRequest)WebRequest.Create("http://localhost:52890/api/Calculator/add");
			hrequest.ContentType = "application/json";
			hrequest.Method = "POST";
			hrequest.Headers.Add("X-Evi-Tracking-Id:" + XEviTrackingId);
			using (var sw = new StreamWriter(hrequest.GetRequestStream()))
			{
				string json = JsonConvert.SerializeObject(req);
				sw.Write(json);
				sw.Flush();
				sw.Close();
			}
			var hrespond = (HttpWebResponse)hrequest.GetResponse();
			using (var sr = new StreamReader(hrespond.GetResponseStream()))
			{
				var response1 = sr.ReadToEnd();
				sr.Close();
				rep = JsonConvert.DeserializeObject<SumResponse>(response1);
			}
			return rep.Sum.ToString();
		}
		private static string Multiplicar()
		{
			MultRequest req = new MultRequest();
			MultResponse rep;
			Console.WriteLine("Introduce cuantos numeros deseas multiplicar");
			int cuantos = Int32.Parse(Console.ReadLine());
			req.Factors = new int[cuantos];
			Console.WriteLine("Introduce los " + cuantos + " numeros a multiplicar");
			for (int i = 0; i < cuantos; i++)
			{
				req.Factors[i] = Int32.Parse(Console.ReadLine());
			}
			var hrequest = (HttpWebRequest)WebRequest.Create("http://localhost:52890/api/Calculator/mult");
			hrequest.ContentType = "application/json";
			hrequest.Method = "POST";
			hrequest.Headers.Add("X-Evi-Tracking-Id:" + XEviTrackingId);
			using (var sw = new StreamWriter(hrequest.GetRequestStream()))
			{
				string json = JsonConvert.SerializeObject(req);
				sw.Write(json);
				sw.Flush();
				sw.Close();
			}
			var hrespond = (HttpWebResponse)hrequest.GetResponse();
			using (var sr = new StreamReader(hrespond.GetResponseStream()))
			{
				var response1 = sr.ReadToEnd();
				sr.Close();
				rep = JsonConvert.DeserializeObject<MultResponse>(response1);
			}
			return rep.Product.ToString();
		}
		private static string Restar()
		{
			SubRequest peti = new SubRequest();
			SubResponse resp;
			Console.WriteLine("Introduce minuendo y sustraendo");
			peti.Minuend = Int32.Parse(Console.ReadLine());
			peti.Subtrahend = Int32.Parse("-" + Console.ReadLine());
			var hrequest = (HttpWebRequest)WebRequest.Create("http://localhost:52890/api/Calculator/sub");
			hrequest.ContentType = "application/json";
			hrequest.Method = "POST";
			hrequest.Headers.Add("X-Evi-Tracking-Id:" + XEviTrackingId);
			using (var sw = new StreamWriter(hrequest.GetRequestStream()))
			{
				string json = JsonConvert.SerializeObject(peti);
				sw.Write(json);
				sw.Flush();
				sw.Close();
			}
			var hrespond = (HttpWebResponse)hrequest.GetResponse();
			using (var sr = new StreamReader(hrespond.GetResponseStream()))
			{
				var response1 = sr.ReadToEnd();
				sr.Close();
				resp = JsonConvert.DeserializeObject<SubResponse>(response1);
			}
			return resp.Difference.ToString();
		}
		private static string Dividir()
		{
			DivRequest peti = new DivRequest();
			DivResponse resp;
			Console.WriteLine("Introduce dividendo y divisor");
			peti.Dividend = Int32.Parse(Console.ReadLine());
			peti.Divisor = Int32.Parse(Console.ReadLine());
			var hrequest = (HttpWebRequest)WebRequest.Create("http://localhost:52890/api/Calculator/div");
			hrequest.ContentType = "application/json";
			hrequest.Method = "POST";
			hrequest.Headers.Add("X-Evi-Tracking-Id:" + XEviTrackingId);
			using (var sw = new StreamWriter(hrequest.GetRequestStream()))
			{
				string json = JsonConvert.SerializeObject(peti);
				sw.Write(json);
				sw.Flush();
				sw.Close();
			}
			var hrespond = (HttpWebResponse)hrequest.GetResponse();
			using (var sr = new StreamReader(hrespond.GetResponseStream()))
			{
				var response1 = sr.ReadToEnd();
				sr.Close();
				resp = JsonConvert.DeserializeObject<DivResponse>(response1);
			}
			return String.Format("El cociente de la operacion es: {0} y el resto es: {1}", resp.Quotient, resp.Remainder);
		}
		private static string Raiz()
		{
			SqrtRequest peti = new SqrtRequest();
			SqrtResponse resp;
			Console.WriteLine("Introduce el número, cuya raíz cuadrada quieras saber");
			peti.Number = Int32.Parse(Console.ReadLine());
			var hrequest = (HttpWebRequest)WebRequest.Create("http://localhost:52890/api/Calculator/sqrt");
			hrequest.ContentType = "application/json";
			hrequest.Method = "POST";
			hrequest.Headers.Add("X-Evi-Tracking-Id:" + XEviTrackingId);
			using (var sw = new StreamWriter(hrequest.GetRequestStream()))
			{
				string json = JsonConvert.SerializeObject(peti);
				sw.Write(json);
				sw.Flush();
				sw.Close();
			}
			var hrespond = (HttpWebResponse)hrequest.GetResponse();
			using (var sr = new StreamReader(hrespond.GetResponseStream()))
			{
				var response1 = sr.ReadToEnd();
				sr.Close();
				resp = JsonConvert.DeserializeObject<SqrtResponse>(response1);
			}
			return String.Format("La raíz cuadrada del número dado es: {0}", resp.Square); 
		}
		private static void RequestJournal()
		{
			List<StandartOperation> response;
			var hrequest = (HttpWebRequest)WebRequest.Create("http://localhost:52890/api/Calculator/Journal");
			hrequest.ContentType = "application/json";
			hrequest.Method = "GET";
			hrequest.Headers.Add("X-Evi-Tracking-Id:" + XEviTrackingId);			
			var hrespond = (HttpWebResponse)hrequest.GetResponse();
			using (var sr = new StreamReader(hrespond.GetResponseStream()))
			{
				var response1 = sr.ReadToEnd();
				sr.Close();
				response = JsonConvert.DeserializeObject<List<StandartOperation>>(response1);
			}
			foreach (var operation in response)
			{
				Console.WriteLine(operation.Calculation());
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
