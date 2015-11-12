using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace rvvrb3
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "All",
            //    routeTemplate: "api/{controller}/"
            //);

            config.Routes.MapHttpRoute(
                name: "API Default",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // config.Routes.MapHttpRoute(
            //     name: "Single",
            //     routeTemplate: "api/{controller}/{property}",
            //     defaults: new { id = RouteParameter.Optional }
            // );
            // config.Routes.MapHttpRoute(
            //     name: "ActionApi",
            //     routeTemplate: "api/{controller}/{action}/"
            // );
            // config.Routes.MapHttpRoute(
            //     name: "ActionApiParams",
            //     routeTemplate: "api/{controller}/{action}/{query}",
            //     defaults: new { query = RouteParameter.Optional }
            // );
        }
    }
}
