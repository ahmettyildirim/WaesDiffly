using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using WaesDiffly.Constraints;

namespace WaesDiffly
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var constraintsResolver = new DefaultInlineConstraintResolver();
            constraintsResolver.ConstraintMap.Add("diffside", typeof(DiffSide));
            // Web API routes
            config.MapHttpAttributeRoutes(constraintsResolver);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{v1}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
