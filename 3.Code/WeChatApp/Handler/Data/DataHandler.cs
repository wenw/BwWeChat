using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeChat.Handler
{
    public abstract class DataHandler
    {
        public abstract Entity.ResultData Process(MsgType type, object data, WeChatContext context);
    }
}
