﻿using System;
using System.Net;
using System.IO;
using System.Threading;
using System.Collections.Generic;

using Newtonsoft.Json;

using clientcalculadora.Models;

namespace clientcalculadora
{
	class Program
	{
		public static string LaunchRequestAndReceiveResponse(Object request,string operationcall)
		{
			var hrequest = (HttpWebRequest)WebRequest.Create($"http://localhost:52890/api/Calculator/{operationcall}");
			hrequest.ContentType = "application/json";
			hrequest.Method = "POST";
			if ((operationcall!="journal")&&(XEviTrackingId.Length>0)) hrequest.Headers.Add("X-Evi-Tracking-Id:" + XEviTrackingId);
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
		public static string Add()
		{
			SumRequest req = new SumRequest();
			Console.WriteLine("Introduce cuantos numeros deseas sumar");
			int cuantos = ParseStringToInteger(Console.ReadLine());
			req.Addends = new int[cuantos];
			Console.WriteLine("Introduce los " + cuantos + " numeros a sumar");
			for (int i = 0; i < cuantos; i++)
			{
				req.Addends[i] = ParseStringToInteger(Console.ReadLine());
			}
			var rep = JsonConvert.DeserializeObject<SumResponse>(LaunchRequestAndReceiveResponse(req, "add"));
			return rep.Sum.ToString();
		}
		public static string Multiplication()
		{
			MultRequest req = new MultRequest();
			Console.WriteLine("Introduce cuantos numeros deseas multiplicar");
			int cuantos = ParseStringToInteger(Console.ReadLine());
			req.Factors = new int[cuantos];
			Console.WriteLine("Introduce los " + cuantos + " numeros a multiplicar");
			for (int i = 0; i < cuantos; i++)
			{
				req.Factors[i] = ParseStringToInteger(Console.ReadLine());
			}
			var rep = JsonConvert.DeserializeObject<MultResponse>(LaunchRequestAndReceiveResponse(req, "mult"));
			return rep.Product.ToString();
		}
		public static string Subtract()
		{
			SubRequest petition = new SubRequest();
			Console.WriteLine("Introduce minuendo y sustraendo");
			petition.Minuend = ParseStringToInteger(Console.ReadLine());
			petition.Subtrahend = ParseStringToInteger($"-{Console.ReadLine()}");
			var response = JsonConvert.DeserializeObject<SubResponse>(LaunchRequestAndReceiveResponse(petition, "sub"));
			return response.Difference.ToString();
		}
		public static string Division()
		{
			DivRequest petition = new DivRequest();
			Console.WriteLine("Introduce dividendo y divisor");
			petition.Dividend = ParseStringToInteger(Console.ReadLine());
			petition.Divisor = ParseStringToInteger(Console.ReadLine());
			var response = JsonConvert.DeserializeObject<DivResponse>(LaunchRequestAndReceiveResponse(petition, "div"));
			return String.Format("El cociente de la operacion es: {0} y el resto es: {1}", response.Quotient, response.Remainder);
		}
		public static string Root()
		{
			SqrtRequest petition = new SqrtRequest();
			Console.WriteLine("Introduce el número, cuya raíz cuadrada quieras saber");
			petition.Number = ParseStringToInteger(Console.ReadLine());
			var response = JsonConvert.DeserializeObject<SqrtResponse>(LaunchRequestAndReceiveResponse(petition, "sqrt"));
			return String.Format("La raíz cuadrada del número dado es: {0}", response.Square); 
		}
		public static void RequestJournal()
		{
			if (XEviTrackingId!="")
			{
				var request = new JournalRequest
				{
					Id = XEviTrackingId
				};
				var response = JsonConvert.DeserializeObject<JournalResponse>(LaunchRequestAndReceiveResponse(request, "journal"));
				foreach (var operation in response.Operations)
				{
					Console.WriteLine(operation.Info());
				}
			}
			else
			{
				Console.WriteLine("You cannot request access to a journal as long as you are using incognito mode.");
			}
			
		}
		public static int ParseStringToInteger(string readFromKeyboard)
		{
			int aux;
			bool control = true;
			do
			{
				if (Int32.TryParse(readFromKeyboard, out aux))
				{
					control = false;
				}
				else
				{
					Console.WriteLine("Please write a number");
					readFromKeyboard = Console.ReadLine();
				}
			} while (control);
			return aux;
		}
		public static string XEviTrackingId;
		static void Main(string[] args)
		{
			Console.WriteLine("Bienvenido al programa calculadora");
			Console.WriteLine("Introduce una identificación");
			XEviTrackingId = Console.ReadLine().Trim();
			if (XEviTrackingId.Length == 0)
			{
				Console.WriteLine("Incognite mode, you will not be tracked");
			}
			else
			{
				Console.WriteLine($"Welcome home, {XEviTrackingId}");
			}
			int menu;
			do
			{
				Console.WriteLine("Introduzca una opción según se desee");
				Console.WriteLine("1 - Add");
				Console.WriteLine("2 - Subtract");
				Console.WriteLine("3 - Multiplication");
				Console.WriteLine("4 - Division");
				Console.WriteLine("5 - Hacer la raíz cuadrada de un número");
				Console.WriteLine("6 - Consultar operaciones previas (journal)");
				Console.WriteLine("7 - Consultar logs de errores");
				Console.WriteLine("8 - Cambiar de usuario");
				Console.WriteLine("0 - Salir");
				var entrada = Console.ReadLine().Trim();
				menu = ParseStringToInteger(entrada);
				Console.WriteLine(menu);
				switch (menu)
				{
					case 0:
						Console.WriteLine("Vuelve pronto");
						Thread.Sleep(500);
						break;
					case 1:
						Console.WriteLine("SUMA");
						Console.WriteLine(String.Format("El total de la suma es {0}",Add()));
						break;
					case 2:
						Console.WriteLine("RESTA");
						Console.WriteLine(String.Format("El resultado es {0}", Subtract()));
						break;
					case 3:
						Console.WriteLine("MULTIPLICACIÓN");
						Console.WriteLine(String.Format("El resultado es {0}", Multiplication()));
						break;
					case 4:
						Console.WriteLine("DIVISIÓN");
						Console.WriteLine(Division());
						break;
					case 5:
						Console.WriteLine("RAÍZ CUADRADA");
						Console.WriteLine(Root());
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