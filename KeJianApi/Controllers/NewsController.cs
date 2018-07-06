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
    /// 新闻咨询/行业动态服务接口
    /// </summary>
    [RequestAuthorize]
    public class NewsController : ApiController
    {
        private KeJianDb JianDb { get; set; } = new KeJianDb();

        /// <summary>
        /// 获取新闻咨询/行业动态（按照最新修改时间降序）
        /// </summary>
        /// <param name="type">查询类型 0：所有 1：新闻资讯 2：行业动态</param>
        /// <param name="num">查询条数</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<News>> GetNewsAll(int type, int num)
        {

            if (type == 0)
            {
                return await JianDb.News
                    .Where(_ => true)
                    .OrderByDescending(_ => _.CreateTime)
                    .Take((int)num)
                    .ToListAsync();
            }
            else
            {
                return await JianDb.News
                    .Where(_ => _.Type == type)
                    .OrderByDescending(_ => _.CreateTime)
                    .Take((int)num)
                    .ToListAsync();
            }

        }

        /// <summary>
        /// 根据ID获取新闻咨询/行业动态信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<News> GetNewsById(int id)
        {
            return await JianDb.News.Where(_ => _.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 新增或修改新闻咨询/行业动态
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        [HttpPost]
        public object CreatedofModied(News news)
        {
            news.CreateTime = DateTime.Now;
            if (news.Id == 0)
            {
                return CreateNews(news);
            }
            else
            {
                return ModifiedNews(news);
            }
        }

        /// <summary>
        /// 新增新闻咨询/行业动态
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        [HttpPost]
        public News CreateNews([FromBody]News news) //[FromBody]
        {
            var entity = JianDb.News.Add(news);
            JianDb.SaveChanges();
            return entity;
        }

        /// <summary>
        /// 修改新闻咨询/行业动态
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        [HttpPost]
        public object ModifiedNews(News news)
        {
            try
            {
                JianDb.Entry<News>(news).State = EntityState.Modified;
                JianDb.SaveChanges();
                return new { State = true };
            }
            catch (Exception e)
            {
                return new { State = false, Messages = e.Message };
            }
        }

        [HttpPost]
        public object DeleteNews(int id)
        {
            try
            {
                News news = new News { Id = id };
                JianDb.Entry<News>(news).State = EntityState.Deleted;
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
