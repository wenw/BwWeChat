using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeChat.Entity.Biz
{
    public class UserInfo
    {
        public Guid ID { get; set; }

        public string OpenID { get; set; }

        public string NickName { get; set; }

        public DateTime CreateTime { get; set; }

        public string Remark { get; set; }

        public string PicUrl { get; set; }

        public string Location { get; set; }

        public string Signature { get; set; }
    }
}
