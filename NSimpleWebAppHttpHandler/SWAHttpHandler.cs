using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Compilation;
using System.Web.SessionState;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NSimpleWebAppHttpHandler
{
	public class SWAHttpHandler : IHttpHandler, IRequiresSessionState
	{
		private static Dictionary<string, SWACustomHttpHandler> _customHttpHandlers;
		
		public void ProcessRequest(HttpContext context)
		{
			string rawUrl = context.Request.RawUrl;
			int slashIndex = rawUrl.LastIndexOf("/", StringComparison.Ordinal);
			if (slashIndex >= 0)
				rawUrl = rawUrl.Substring(slashIndex + 1);
			HttpHandlerBilgiOlusturmaSonucu handlerBilgiOlusturmaSonucu = handlerBilgiOlustur(rawUrl);
			if (!handlerBilgiOlusturmaSonucu.Basarili)
			{
				context.Response.Write(JsonConvert.SerializeObject(new HttpRequestHata {Mesaj = "HTTP handler bilgisi oluşturulamadı (1622.65 - " + handlerBilgiOlusturmaSonucu.Mesaj + " - " + rawUrl + ")"}, new IsoDateTimeConverter()));
				return;
			}
			object donusDegeri = ilgiliHandlerinIlgiliMetodunuCalistir(handlerBilgiOlusturmaSonucu.Bilgi, context, rawUrl);
			if (donusDegeri != null)
				context.Response.Write(JsonConvert.SerializeObject(donusDegeri, new IsoDateTimeConverter()));
		}

		private object ilgiliHandlerinIlgiliMetodunuCalistir(HttpHandlerBilgi handlerBilgi, HttpContext context, string rawUrl)
		{
			try
			{
				//TODO: CETİN PARAMETRESİZ
				ParameterInfo[] metodParametreleri = handlerBilgi.Metod.GetParameters();
				string requestJson = new StreamReader(context.Request.InputStream).ReadToEnd();
				List<object> parametreler = new List<object> { JsonConvert.DeserializeObject(requestJson,  metodParametreleri[0].ParameterType)};
				if (metodParametreleri.Count() > 1)
					parametreler.Add(context);
				return handlerBilgi.Metod.Invoke(Activator.CreateInstance(handlerBilgi.Type), parametreler.ToArray());
			}
			catch (Exception ex)
			{
				StringBuilder sb = new StringBuilder();
				//sb.Append("Kullanıcı: " + IAMSessionNesneleri.Kullanici.KullaniciAdi + "\n");
				sb.Append("Request: " + new StreamReader(context.Request.InputStream).ReadToEnd() + "\n");
				sb.Append("URL: " + rawUrl + "\n");
				sb.Append(ex.Message + " -- " + ex.StackTrace);
				if (ex.InnerException != null)
					sb.Append(ex.InnerException.Message + " -- " + ex.InnerException.StackTrace);
				//Araclar.RastgeleIsimliDosyaKaydet("iamHttpHandlerHata", sb.ToString(), "txt");
				//	context.Response.Write(JsonConvert.SerializeObject(IstekSonuc.Hata("HTTP isteği işlenirken hata oluştu (" + sb + ")")));
				//throw new Exception((ex.InnerException??ex).Message);
				return new HttpRequestHata {Mesaj = "Sunucuda istek işlenirken hata oluştu"};
				//context.Response.Write(JsonConvert.SerializeObject(IstekSonuc.Hata((ex.InnerException ?? ex).Message)));
			}
		}

		private HttpHandlerBilgiOlusturmaSonucu handlerBilgiOlustur(string rawUrl)
		{
			tumDllleriTaraVeCustomHandlerlarDictionaryOlustur();
			int index = rawUrl.LastIndexOf(".", StringComparison.Ordinal);
			if (index > 0)
			{
				string[] sinifVeMetodAdlari = rawUrl.Substring(0, index).Split(new []{'.'}, StringSplitOptions.RemoveEmptyEntries);
				if (sinifVeMetodAdlari.Length == 2)
				{
					string sinifAdi = sinifVeMetodAdlari[0];
					string metodAdi = sinifVeMetodAdlari[1];
					if (_customHttpHandlers.ContainsKey(sinifAdi))
					{
						MethodInfo mi = _customHttpHandlers[sinifAdi].Methods[metodAdi];
						if (mi != null)
							return new HttpHandlerBilgiOlusturmaSonucu { Basarili = true, Bilgi = new HttpHandlerBilgi { Type = _customHttpHandlers[sinifAdi].Type, Metod = mi } };
					}
				}
			}
			return new HttpHandlerBilgiOlusturmaSonucu{Basarili = false, Mesaj = "Unable to create SWA Http Handler. URL: " + rawUrl };
		}

		private static void tumDllleriTaraVeCustomHandlerlarDictionaryOlustur()
		{
			if (_customHttpHandlers != null)
				return;
			_customHttpHandlers = new Dictionary<string, SWACustomHttpHandler>();
			foreach (Assembly assembly in BuildManager.GetReferencedAssemblies())
			{
				foreach (Type type in assembly.YuklenebilirTipleriAl())
				{
					if (type.IsDefined(typeof (SWAModuleClass), false))
					{
						Dictionary<string, MethodInfo> apiMethods = new Dictionary<string, MethodInfo>();
						foreach (MethodInfo mi in type.GetMethods())
						{
							object[] attrs = mi.GetCustomAttributes(typeof (SWAModuleMethod), false);
							if (attrs.Length == 1)
								apiMethods.Add(((SWAModuleMethod)attrs[0]).Name, mi);
						}
						string classAttributeValue = ((SWAModuleClass) type.GetCustomAttributes(typeof (SWAModuleClass), false)[0]).Name;
						_customHttpHandlers.Add(classAttributeValue, new SWACustomHttpHandler { Methods = apiMethods, Name = classAttributeValue, Type = type });
					}
				}
			}
		}

		public bool IsReusable
		{
			get { return true; }
		}
	}

	public class HttpHandlerBilgiOlusturmaSonucu
	{
		public bool Basarili;
		public string Mesaj;
		public HttpHandlerBilgi Bilgi;
	}

	public class HttpHandlerBilgi
	{
		public Type Type;
		public MethodInfo Metod;
	}

	public class HttpRequestHata
	{
		public string Mesaj;
		public bool ExceptionVar = true;
	}

	public class SWACustomHttpHandler
	{
		public string Name;
		public Type Type;
		public Dictionary<string, MethodInfo> Methods = new Dictionary<string, MethodInfo>();
	}
}
