using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI_NGK
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //// Configuración y servicios de API web
            //// Web API configuration and services
            //config.Formatters.Clear();
            //config.Formatters.Add(new JsonMediaTypeFormatter());

            //config.EnableCors();
            //var enableCorsAttribute = new EnableCorsAttribute("*",
            //                                   "*",
            //                                   "GET, PUT, POST, DELETE, OPTIONS");
            //config.EnableCors(enableCorsAttribute);
            //// Web API routes

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }

    //public class BrowserJsonFormatter : JsonMediaTypeFormatter
    //{
    //    public BrowserJsonFormatter()
    //    {
    //        this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
    //        this.SerializerSettings.Formatting = Formatting.Indented;
    //    }

    //    public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
    //    {
    //        base.SetDefaultContentHeaders(type, headers, mediaType);
    //        headers.ContentType = new MediaTypeHeaderValue("application/json");
    //    }
    //}

}
