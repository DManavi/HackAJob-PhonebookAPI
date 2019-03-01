using System.Linq;
using System.Web.Http;

namespace API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // enable camelCase property name resolver
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

            // Dummy call to trigger database migration and instance creation
            Services.PhonebookContext.Instance.Users.Any();
        }
    }
}
