using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeChat.Entity.Message
{
    public class MsgBase
    {
        public Guid ID { get; set; }

        [MappingXml("ToUserName",true)]
        public string ToUserID { get; set; }

        [MappingXml("FromUserName", true)]
        public string FromUserID { get; set; }

        [MappingXml("CreateTime")]
        public DateTime CreateTime { get; set; }

        [MappingXml("MsgType",true)]
        public string MsgType { get; set; }

        public MsgBase()
        {
            this.ID = Guid.NewGuid();
        }
    }
}
