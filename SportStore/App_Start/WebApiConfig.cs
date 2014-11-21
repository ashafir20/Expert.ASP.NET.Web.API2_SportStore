using System.Web.Http;
using SportStore.Infrastructure;

namespace SportStore
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "OrdersRoute",
                routeTemplate: "nonrest/{controller}/{action}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.DependencyResolver = new CustomResolver();

            GlobalConfiguration.Configuration.Formatters.JsonFormatter
                .SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            /*There is a circular reference in the relationship between the Order and OrderLine classes: an Order has a collection
            of OrderLine objects, each of which contains a reference back to the Order. This is a problem for the standard
            serialization process, which will report an error when it finds such a loop. To prevent this from being a problem,
            I need to change the behavior of the class responsible for serializing objects into JSON so that it simply ignores*/
        }
    }
}

