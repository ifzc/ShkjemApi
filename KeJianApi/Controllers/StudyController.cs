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
    /// 学习模块服务接口
    /// </summary>
    [RequestAuthorize]
    public class StudyController : ApiController
    {
        private KeJianDb JianDb { get; set; } = new KeJianDb();

        /// <summary>
        /// 获取所有学习模块数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<Study>> GetStudyAll()
        {
            return await JianDb.Study
                .Where(_ => true)
                .OrderByDescending(_ => _.CreateTime)
                .ToListAsync();
        }

        /// <summary>
        /// 添加或修改学习模块数据
        /// </summary>
        /// <param name="study">学习ID</param>
        /// <returns>直接判断状态码：200则成功</returns>
        [HttpPost]
        public object CreatedofModied(Study study)
        {
            study.CreateTime = DateTime.Now;
            if (study.Id == 0)
            {
                return CreateStudy(study);
            }
            else
            {
                return ModifiedStudy(study);
            }
        }

        /// <summary>
        /// 添加学习模块数据
        /// </summary>
        /// <param name="study"></param>
        /// <returns></returns>
        [HttpPost]
        public Study CreateStudy([FromBody]Study study)
        {
            var entity = JianDb.Study.Add(study);
            JianDb.SaveChanges();
            return study;
        }

        /// <summary>
        /// 修改学习模块数据
        /// </summary>
        /// <param name="study"></param>
        /// <returns></returns>
        [HttpPost]
        public object ModifiedStudy(Study study)
        {
            try
            {
                JianDb.Entry<Study>(study).State = EntityState.Modified;
                JianDb.SaveChanges();
                return new { State = true };
            }
            catch (Exception e)
            {
                return new { State = false, Messages = e.Message };
            }
        }

        /// <summary>
        /// 删除学习数据
        /// </summary>
        /// <param name="id">学习ID</param>
        /// <returns>直接判断状态码：200则成功</returns>
        [HttpPost]
        public object DeleteStudy(int id)
        {
            try
            {
                Study study = new Study { Id = id };
                JianDb.Entry<Study>(study).State = EntityState.Deleted;
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
