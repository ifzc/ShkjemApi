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
    /// 荣誉信息服务接口
    /// </summary>
    [RequestAuthorize]
    public class HonorController : ApiController
    {
        private KeJianDb JianDb { get; set; } = new KeJianDb();

        /// <summary>
        /// 获取所有荣誉信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<Honor>> GetHonorAll()
        {
            return await JianDb.Honor
                .Where(_ => true)
                .OrderByDescending(_ => _.CreateTime)
                .ToListAsync();
        }

        /// <summary>
        /// 添加或修改荣誉信息
        /// </summary>
        /// <param name="honor"></param>
        /// <returns></returns>
        [HttpPost]
        public object CreatedofModied(Honor honor)
        {
            honor.CreateTime = DateTime.Now;
            if (honor.Id == 0)
            {
                return CreateHonor(honor);
            }
            else
            {
                return ModifiedHonor(honor);
            }
        }

        /// <summary>
        /// 添加荣誉信息
        /// </summary>
        /// <param name="honor"></param>
        /// <returns></returns>
        [HttpPost]
        public Honor CreateHonor([FromBody]Honor honor)
        {
            var entity = JianDb.Honor.Add(honor);
            JianDb.SaveChanges();
            return honor;
        }

        /// <summary>
        /// 修改荣誉信息
        /// </summary>
        /// <param name="honor"></param>
        /// <returns></returns>
        [HttpPost]
        public object ModifiedHonor(Honor honor)
        {
            try
            {
                JianDb.Entry<Honor>(honor).State = EntityState.Modified;
                JianDb.SaveChanges();
                return new { State = true };
            }
            catch (Exception e)
            {
                return new { State = false, Messages = e.Message };
            }
        }

        /// <summary>
        /// 删除荣誉信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public object DeleteHonor(int id)
        {
            try
            {
                Honor honor = new Honor { Id = id };
                JianDb.Entry<Honor>(honor).State = EntityState.Deleted;
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
