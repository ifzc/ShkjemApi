using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Routing;
using static KeJianApi.WebApiApplication;

namespace KeJianApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            //跨域配置
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // Web API 路由
            config.MapHttpAttributeRoutes();

            RouteTable.Routes.MapHttpRoute(
               name: "KeJianApi",
               routeTemplate: "api/{controller}/{action}/{id}",
               defaults: new { id = RouteParameter.Optional }
           ).RouteHandler = new SessionControllerRouteHandler(); ;
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
