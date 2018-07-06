using KeJianApi;
using KeJianApi.App_Start;
using Swashbuckle.Application;
using System.Web.Http;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace KeJianApi
{
    public class SwaggerConfig
    {
        public static string GetXmlCommentsPath()
        {
            return string.Format("{0}/bin/KeJianApi.xml", System.AppDomain.CurrentDomain.BaseDirectory);
        }
        public static void Register()
        {

            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "KeJianApi");
                        c.IncludeXmlComments(GetXmlCommentsPath());
                        c.OperationFilter<GlobalHttpHeaderFilter>();
                    })
                .EnableSwaggerUi(c => { });
        }
    }
}
