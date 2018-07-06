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
    /// 发展历程服务接口
    /// </summary>
    [RequestAuthorize]
    public class CourseController : ApiController
    {
        private KeJianDb JianDb { get; set; } = new KeJianDb();

        /// <summary>
        /// 获取所有发展历程（按照年份排序）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<Course>> GetCourseAll()
        {
            return await JianDb.Course
                .Where(_ => true)
                .OrderBy(_ => _.Year)
                .ToListAsync();
        }

        /// <summary>
        /// 新增或修改发展历程
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost]
        public object CreatedofModied(Course course)
        {
            if (course.Id == 0)
            {
                return CreateCourse(course);
            }
            else
            {
                return ModifiedCourse(course);
            }
        }

        /// <summary>
        ///  新增发展历程
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost]
        public Course CreateCourse([FromBody]Course course)
        {
            var entity = JianDb.Course.Add(course);
            JianDb.SaveChanges();
            return course;
        }

        /// <summary>
        /// 修改发展历程
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost]
        public object ModifiedCourse(Course course)
        {
            try
            {
                JianDb.Entry<Course>(course).State = EntityState.Modified;
                JianDb.SaveChanges();
                return new { State = true };
            }
            catch (Exception e)
            {
                return new { State = false, Messages = e.Message };
            }
        }

        /// <summary>
        /// 删除发展历程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public object DeleteCourse(int id)
        {
            try
            {
                Course course = new Course { Id = id };
                JianDb.Entry<Course>(course).State = EntityState.Deleted;
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
