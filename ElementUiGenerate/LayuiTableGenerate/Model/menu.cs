using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LayuiTableGenerate.Model
{
    public class Menu
    {
        public int ParentId { set; get; }
        public string Tree { set; get; }
        public string Title { set; get; }
        public string Icon { set; get; }
        public string Path { get; set; }
        public string Url { get; set; }

        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; } = false;
        /// <summary>
        /// 添加人
        /// </summary>
        public int InsertBy { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime InsertDateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 编辑人
        /// </summary>
        public int UpdateBy { get; set; }
        /// <summary>
        /// 编辑时间
        /// </summary>
        public DateTime UpdateDateTime { get; set; }
        /// <summary>
        /// 删除人
        /// </summary>
        public int DeleteBy { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime DeleteDateTime { get; set; }
    }
}
