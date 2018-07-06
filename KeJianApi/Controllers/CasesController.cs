using KeJianApi.App_Start;
using KeJianApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace KeJianApi.Controllers
{
    /// <summary>
    /// 案例服务接口
    /// </summary>
    [RequestAuthorize]
    public class CasesController : ApiController
    {
        private KeJianDb JianDb { get; set; } = new KeJianDb();
        /// <summary>
        /// 获取所有案例信息（按修改时间排序）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<Cases>> GetCasesAll()
        {
            return await JianDb.Cases.Where(_ => true).OrderByDescending(_ => _.CreateTime).ToListAsync();
        }

        /// <summary>
        /// 根据ID获取案例信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResult<Cases>> GetCasesById(int id)
        {
            var entity = await JianDb.Cases.Where(_ => _.Id == id).FirstOrDefaultAsync();
            return Json(entity);
        }

        /// <summary>
        /// 添加或修改案例
        /// </summary>
        /// <param name="cases">案例Dto</param>
        /// <returns></returns>
        [HttpPost]
        public object CreatedofModied(Cases cases)
        {
            if (cases.Id == 0)
            {
                return CreateCases(cases);
            }
            else
            {
                return ModifiedCases(cases);
            }
        }

        /// <summary>
        /// 添加案例信息
        /// </summary>
        /// <param name="cases"></param>
        /// <returns></returns>
        [HttpPost]
        public Cases CreateCases([FromBody]Cases cases)
        {
            cases.CreateTime = DateTime.Now;
            var entity = JianDb.Cases.Add(cases);
            JianDb.SaveChanges();
            return entity;
        }

        /// <summary>
        /// 修改案例信息
        /// </summary>
        /// <param name="cases"></param>
        /// <returns></returns>
        [HttpPost]
        public object ModifiedCases(Cases cases)
        {
            try
            {
                cases.CreateTime = DateTime.Now;
                JianDb.Entry<Cases>(cases).State = EntityState.Modified;
                JianDb.SaveChanges();
                return new { State = true };
            }
            catch (Exception e)
            {
                return new { State = false, Messages = e.Message };
            }
        }

        /// <summary>
        /// 删除案例信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public object DeleteCases(int id)
        {
            try
            {
                Cases cases = new Cases { Id = id };
                JianDb.Entry<Cases>(cases).State = EntityState.Deleted;
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
