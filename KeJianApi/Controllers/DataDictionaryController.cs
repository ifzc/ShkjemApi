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
    public class DataDictionaryController : ApiController
    {
        private KeJianDb JianDb { get; set; } = new KeJianDb();

        /// <summary>
        /// 根据key获取数据字典信息
        /// </summary>
        /// <param name="key">获取单条直接传key  多条：key1,key2 （逗号分隔）会以相同顺序获取数据</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<DataDictionary>> GetDataDictionaryAll(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return await JianDb.DataDictionary.ToListAsync();
            }
            string[] keys = key.Split(',');
            List<DataDictionary> data = new List<DataDictionary>();
            for (int i = 0; i < keys.Length; i++)
            {
                string keystr = keys[i];
                data.Add(await JianDb.DataDictionary
                .Where(_ => _.Key == keystr)
                .FirstOrDefaultAsync());
            }
            return data;
        }

        /// <summary>
        /// 新增或修改数据字典
        /// </summary>
        /// <param name="dataDictionary"></param>
        /// <returns></returns>
        [HttpPost]
        public object CreatedofModied(DataDictionary dataDictionary)
        {
            if (dataDictionary.Id == 0)
            {
                return CreateDataDictionary(dataDictionary);
            }
            else
            {
                return ModifiedDataDictionary(dataDictionary);
            }
        }

        /// <summary>
        /// 新增数据字典
        /// </summary>
        /// <param name="dataDictionary"></param>
        /// <returns></returns>
        [HttpPost]
        public DataDictionary CreateDataDictionary([FromBody]DataDictionary dataDictionary)
        {
            int count = JianDb.DataDictionary.Where(_ => _.Key == dataDictionary.Key).Count();
            if (count == 0)
            {
                var entity = JianDb.DataDictionary.Add(dataDictionary);
                JianDb.SaveChanges();
                return dataDictionary;
            }
            else
            {
                throw new Exception("键以存在！");
            }
        }

        /// <summary>
        /// 修改数据字典
        /// </summary>
        /// <param name="dataDictionary"></param>
        /// <returns></returns>
        [HttpPost]
        public object ModifiedDataDictionary(DataDictionary dataDictionary)
        {
            try
            {
                JianDb.Entry<DataDictionary>(dataDictionary).State = EntityState.Modified;
                JianDb.SaveChanges();
                return new { State = true };
            }
            catch (Exception e)
            {
                return new { State = false, Messages = e.Message };
            }
        }

        /// <summary>
        /// 删除数据字典
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public object DeleteDataDictionary(int id)
        {
            try
            {
                DataDictionary dataDictionary = new DataDictionary { Id = id };
                JianDb.Entry(dataDictionary).State = EntityState.Deleted;
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
