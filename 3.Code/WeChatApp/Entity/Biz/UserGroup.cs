using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeChat.Entity.Biz
{
    public class UserGroup
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }
    }

    public enum UserGroupType
    {
        Unnamed,
        BlackList,
        Important
    }
}
