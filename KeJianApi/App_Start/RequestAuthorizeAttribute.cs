using KeJianApi.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Security;

namespace KeJianApi.App_Start
{
    /// <summary>
    /// 自定义此特性用于接口的身份验证
    /// </summary>
    public class RequestAuthorizeAttribute : AuthorizeAttribute
    {

        private KeJianDb JianDb { get; set; } = new KeJianDb();

        /// <summary>
        /// 重写基类的验证方式，加入我们自定义的Ticket验证
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //从http请求的头里面获取身份验证信息，验证是否是请求发起方的ticket
            var authorization = actionContext.Request.Headers.Authorization;
            if ((authorization != null) && (authorization.Parameter != null))
            {
                //解密用户ticket,并校验用户名密码是否匹配
                var encryptTicket = authorization.Parameter;
                if (ValidateTicket(encryptTicket))
                {
                    base.IsAuthorized(actionContext);
                }
                else
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }
            //如果取不到身份验证信息，并且不允许匿名访问，则返回未验证401
            else
            {
                var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
                bool isAnonymous = attributes.Any(a => a is AllowAnonymousAttribute);
                if (isAnonymous) base.OnAuthorization(actionContext);
                else HandleUnauthorizedRequest(actionContext);
            }
        }

        /// <summary>
        /// 校验用户名密码
        /// </summary>
        /// <param name="encryptTicket"></param>
        /// <returns></returns>
        private bool ValidateTicket(string encryptTicket)
        {
            //解密Ticket
            var strTicket = FormsAuthentication.Decrypt(encryptTicket).UserData;

            //从Ticket里面获取用户名和密码
            var index = strTicket.IndexOf("&");
            string strUser = strTicket.Substring(0, index);
            string strPwd = strTicket.Substring(index + 1);

            return JianDb.KUsers.Where(_ => _.LoginName == strUser && _.Password == strPwd).Count() > 0;


        }
    }
}
