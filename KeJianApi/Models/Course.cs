using System.ComponentModel.DataAnnotations;

namespace KeJianApi.Models
{
    public class Course
    {

        /// <summary>
        /// 历程ID
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
}
