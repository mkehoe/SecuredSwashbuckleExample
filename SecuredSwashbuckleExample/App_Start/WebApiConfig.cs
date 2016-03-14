using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SecuredSwashbuckleExample.Handlers;
using Swashbuckle.Application;

namespace SecuredSwashbuckleExample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                handler: new ApiKeyHandler(GlobalConfiguration.Configuration)                
            );

            /*config.Routes.MapHttpRoute(
                name: "Swagger",
                routeTemplate: "swagger/ui/index",
                defaults: null,
                constraints: null,
                handler: new ApiKeyUrlHandler(GlobalConfiguration.Configuration)
            );*/

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
