using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeChat.Entity.Message
{
    public class ImageMsg : MsgBase
    {
        [MappingXml("PicUrl",true)]
        public string PicUrl { get; set; }

        [MappingXml("MsgId")]
        public long MsgId { get; set; }
    }
}
