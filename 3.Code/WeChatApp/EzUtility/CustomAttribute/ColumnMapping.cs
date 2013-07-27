using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EzUtility.CustomAttribute
{
    /// <summary>
    /// 自定义属于，用于标识实体对象中的属于所对应的数据库的列
    /// </summary>
    public class ColumnMapping : System.Attribute
    {
        /// <summary>
        /// 数据库列的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否为主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public DbType DataType { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 是否为外键
        /// </summary>
        public bool IsForeignKey { get; set; }

        /// <summary>
        /// 是否为自增列
        /// </summary>
        public bool IsAutoIncrement { get; set; }
    }
}
