
using ServiceStack.DataAnnotations;
using System;

namespace LayuiTableGenerate.Classes
{
    [Serializable]
    public class Knowledgetype
    {
        /// <summary>
        /// 主键
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 父ID
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 类别树
        /// </summary>
        public string TreeKey { get; set; }

        /// <summary>
        /// 目录名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public string IsDeleted { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public string SortNo { get; set; }

    }
}