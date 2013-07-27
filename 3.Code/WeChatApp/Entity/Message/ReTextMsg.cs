using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeChat.Entity.Message
{
    public class ReTextMsg : MsgBase
    {
        [MappingXml("Content", true)]
        public string Content { get; set; }

        [MappingXml("FuncFlag")]
        public int FuncFlag { get; set; }
    }
}
