using KeJianApi.App_Start;
using KeJianApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace KeJianApi.Controllers
{
    /// <summary>
    /// 留言板/需求投递信息服务实现
    /// </summary>
    [RequestAuthorize]
    public class MessageController : ApiController
    {
        private KeJianDb JianDb { get; set; } = new KeJianDb();

        /// <summary>
        /// 获取所有留言板或需求投递
        /// </summary>
        /// <param name="ismess">true 留言板 false 需求投递</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<Messages>> GetMessageAll(bool ismess)
        {
            return await JianDb.Messages
                .Where(_ => _.IsMess == ismess)
                .OrderByDescending(_ => _.CreateTime)
                .ToListAsync();
        }

        /// <summary>
        /// 新增留言板/需求投递数据
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public Messages CreateMessage([FromBody]Messages message)
        {
            message.CreateTime = DateTime.Now;
            var entity = JianDb.Messages.Add(message);
            JianDb.SaveChanges();
            return entity;
        }

        /// <summary>
        /// 修改留言板/需求投递数据
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        public object ModifiedMessage([FromBody]Messages message)
        {
            try
            {
                JianDb.Entry<Messages>(message).State = EntityState.Modified;
                JianDb.SaveChanges();
                return new { State = true };
            }
            catch (Exception e)
            {
                return new { State = false, Messages = e.Message };
            }
        }

        /// <summary>
        /// 删除留言板/需求投递数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public object DeleteMessage(int id)
        {
            try
            {
                Messages message = new Messages { Id = id };
                JianDb.Entry<Messages>(message).State = EntityState.Deleted;
                JianDb.SaveChanges();
                return new { State = true };
            }
            catch (Exception e)
            {
                return new { State = false, Messages = e.Message };
            }
        }
    }
}
