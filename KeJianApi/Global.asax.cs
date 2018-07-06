using KeJianApi.Migrations;
using KeJianApi.Models;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Http.WebHost;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace KeJianApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer<KeJianDb>(new BaseInitializer());
        }

        public class SessionRouteHandler : HttpControllerHandler, IRequiresSessionState
        {
            public SessionRouteHandler(RouteData routeData) : base(routeData)
            {

            }
        }

        public class SessionControllerRouteHandler : HttpControllerRouteHandler
        {
            protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
            {

                return new SessionRouteHandler(requestContext.RouteData);

            }

        }
    }
}
