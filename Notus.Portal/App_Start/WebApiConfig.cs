using System.Web.Http;

namespace Notus.Portal
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Remove the XM Formatter from the web api
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new {id = RouteParameter.Optional}
                );
        }
    }
}