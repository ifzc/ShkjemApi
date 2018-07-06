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
    public class TeamController : ApiController
    {
        private KeJianDb JianDb { get; set; } = new KeJianDb();

        /// <summary>
        /// 获取所有团队风采数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<Team>> GetTeamAll()
        {
            return await JianDb.Team
                .Where(_ => true)
                .OrderByDescending(_ => _.CreateTime)
                .ToListAsync();
        }

        /// <summary>
        /// 新增或修改团队风采数据
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        [HttpPost]
        public object CreatedofModied(Team team)
        {
            team.CreateTime = DateTime.Now;
            if (team.Id == 0)
            {
                return CreateTeam(team);
            }
            else
            {
                return ModifiedTeam(team);
            }
        }

        /// <summary>
        /// 新增团队风采数据
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        [HttpPost]
        public Team CreateTeam([FromBody]Team team)
        {
            var entity = JianDb.Team.Add(team);
            JianDb.SaveChanges();
            return team;
        }

        /// <summary>
        /// 修改团队风采数据
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        [HttpPost]
        public object ModifiedTeam(Team team)
        {
            try
            {
                JianDb.Entry<Team>(team).State = EntityState.Modified;
                JianDb.SaveChanges();
                return new { State = true };
            }
            catch (Exception e)
            {
                return new { State = false, Messages = e.Message };
            }
        }

        /// <summary>
        /// 删除团队风采数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public object DeleteTeam(int id)
        {
            try
            {
                Team team = new Team { Id = id };
                JianDb.Entry<Team>(team).State = EntityState.Deleted;
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
