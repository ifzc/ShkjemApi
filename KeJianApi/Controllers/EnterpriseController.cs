using KeJianApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace KeJianApi.Controllers
{
    public class EnterpriseController : ApiController
    {
        private KeJianDb JianDb { get; set; } = new KeJianDb();

        /// <summary>
        /// 获取所有合作企业信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<Enterprise>> GetEnterpriseAll()
        {
            return await JianDb.Enterprise
                .Where(_ => true)
                .OrderBy(_ => _.CreateTime)
                .ToListAsync();
        }

        /// <summary>
        /// 添加或修改合作企业信息
        /// </summary>
        /// <param name="enterprise"></param>
        /// <returns></returns>
        [HttpPost]
        public object CreatedofModied(Enterprise enterprise)
        {
            enterprise.CreateTime = DateTime.Now;
            if (enterprise.Id == 0)
            {
                return CreateEnterprise(enterprise);
            }
            else
            {
                return ModifiedEnterprise(enterprise);
            }
        }

        /// <summary>
        /// 添加合作企业信息
        /// </summary>
        /// <param name="enterprise"></param>
        /// <returns></returns>
        [HttpPost]
        public Enterprise CreateEnterprise([FromBody]Enterprise enterprise)
        {
            var entity = JianDb.Enterprise.Add(enterprise);
            JianDb.SaveChanges();
            return enterprise;
        }

        /// <summary>
        /// 修改合作企业信息
        /// </summary>
        /// <param name="enterprise"></param>
        /// <returns></returns>
        [HttpPost]
        public object ModifiedEnterprise(Enterprise enterprise)
        {
            try
            {
                JianDb.Entry<Enterprise>(enterprise).State = EntityState.Modified;
                JianDb.SaveChanges();
                return new { State = true };
            }
            catch (Exception e)
            {
                return new { State = false, Messages = e.Message };
            }
        }

        /// <summary>
        /// 删除合作企业信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public object DeleteEnterprise(int id)
        {
            try
            {
                Enterprise enterprise = new Enterprise { Id = id };
                JianDb.Entry<Enterprise>(enterprise).State = EntityState.Deleted;
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
