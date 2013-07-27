using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeChat.Entity.Message
{
    public class TextMsg : MsgBase
    {
        [MappingXml("Content",true)]
        public string Content { get; set; }

        [MappingXml("MsgId")]
        public long MsgId { get; set; }
    }
}
