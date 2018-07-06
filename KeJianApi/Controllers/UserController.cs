using KeJianApi.App_Start;
using KeJianApi.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Security;

namespace KeJianApi.Controllers
{
    [RequestAuthorize]
    public class UserController : ApiController
    {
        private KeJianDb JianDb { get; set; } = new KeJianDb();

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="strUser"></param>
        /// <param name="strPwd"></param>
        /// <returns>Token</returns>
        [HttpGet]
        [AllowAnonymous]
        public object Login(string strUser, string strPwd)
        {
            if (ValidateUser(strUser, strPwd))
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(0, strUser, DateTime.Now,
                            DateTime.Now.AddHours(1), true, string.Format("{0}&{1}", strUser, strPwd),
                            FormsAuthentication.FormsCookiePath);
                //返回登录结果、用户信息、用户验证票据信息
                var oUser = new { bRes = true, UserName = strUser, Password = strPwd, Ticket = FormsAuthentication.Encrypt(ticket) };
                //将身份信息保存在session中，验证当前请求是否是有效请求
                HttpContext.Current.Session[strUser] = oUser;
                return oUser;
            }
            else
            {
                return new { bRes = false };
            }
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<IQueryable<KUser>> GetUserAll()
        {
            return Json(JianDb.KUsers.Where(_ => true));
        }

        /// <summary>
        /// 新增或删除用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public object CreateofModified(KUser user)
        {
            if (user.Id == 0)
            {
                return CreateUser(user);
            }
            else
            {
                return ModifiedUser(user);
            }
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public KUser CreateUser(KUser user) //[FromBody]
        {
            user.CreateTime = DateTime.Now;
            var entity = JianDb.KUsers.Add(user);
            JianDb.SaveChanges();
            return entity;
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public object ModifiedUser(KUser user)
        {
            try
            {
                JianDb.Entry<KUser>(user).State = EntityState.Modified;
                JianDb.SaveChanges();
                return new { State = true };
            }
            catch (Exception e)
            {
                return new { State = false, Messages = e.Message };
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public object DeleteUser(int id)
        {
            try
            {
                KUser user = new KUser { Id = id };
                JianDb.Entry<KUser>(user).State = EntityState.Deleted;
                JianDb.SaveChanges();
                return new { State = true };
            }
            catch (Exception e)
            {
                return new { State = false, Messages = e.Message };
            }
        }

        private bool ValidateUser(string strUser, string strPwd)
        {
            return JianDb.KUsers.Where(_ => _.LoginName == strUser && _.Password == strPwd).Count() > 0;
        }
    }
}
