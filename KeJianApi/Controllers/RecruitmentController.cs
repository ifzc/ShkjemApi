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

    [RequestAuthorize]
    public class RecruitmentController : ApiController
    {

        private KeJianDb JianDb { get; set; } = new KeJianDb();

        /// <summary>
        /// 按照类型获取所有招聘信息
        /// </summary>
        /// <param name="type">类型 0：所有 1：研发类 2：服务类 3：营销类</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<Recruitment>> GetRecruitmentAll(int type)
        {
            if (type == 0)
            {
                return await JianDb.Recruitment.Where(_ => true).ToListAsync();
            }
            else
            {
                return await JianDb.Recruitment.Where(_ => _.Type == type).ToListAsync();
            }
        }

        /// <summary>
        /// 新增或需改招聘信息
        /// </summary>
        /// <param name="recruitment"></param>
        /// <returns></returns>
        [HttpPost]
        public object CreateofModified([FromBody]Recruitment recruitment)
        {
            if (recruitment.Id == 0)
            {
                return CreateRecruitment(recruitment);
            }
            else
            {
                return ModifiedRecruitment(recruitment);
            }
        }

        /// <summary>
        /// 新增招聘信息
        /// </summary>
        /// <param name="recruitment"></param>
        /// <returns></returns>
        [HttpPost]
        public Recruitment CreateRecruitment([FromBody]Recruitment recruitment) //[FromBody]
        {
            recruitment.CreateTime = DateTime.Now;
            var entity = JianDb.Recruitment.Add(recruitment);
            JianDb.SaveChanges();
            return entity;
        }

        /// <summary>
        /// 需改招聘信息
        /// </summary>
        /// <param name="recruitment"></param>
        /// <returns></returns>
        [HttpPost]
        public object ModifiedRecruitment([FromBody]Recruitment recruitment)
        {
            try
            {
                JianDb.Entry<Recruitment>(recruitment).State = EntityState.Modified;
                JianDb.SaveChanges();
                return new { State = true };
            }
            catch (Exception e)
            {
                return new { State = false, Messages = e.Message };
            }
        }

        /// <summary>
        /// 删除招聘信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public object DeleteRecruitment(int id)
        {
            try
            {
                Recruitment recruitment = new Recruitment { Id = id };
                JianDb.Entry<Recruitment>(recruitment).State = EntityState.Deleted;
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
