using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebDiskBrowser
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services
			//config.Formatters.Add(config.Formatters.JsonFormatter);
			//config.Formatters.Remove(config.Formatters.XmlFormatter);
			//var xml = GlobalConfiguration.Configuration.Formatters.XmlFormatter;
			//xml.UseXmlSerializer = true;
			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{action}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
