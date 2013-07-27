using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EzUtility.CustomAttribute
{
    /// <summary>
    /// 自定义属于，用于标识实体对象所对应的数据库表
    /// </summary>
    public class TableMapping : System.Attribute
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string Name { get; set; }
    }
}
