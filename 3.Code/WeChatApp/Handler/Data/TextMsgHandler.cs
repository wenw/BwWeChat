using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WeChat.Entity;
using WeChat.Entity.Message;

namespace WeChat.Handler
{
    public class TextMsgHandler : DataHandler
    {
        public override ResultData Process(MsgType type, object data, WeChatContext context)
        {
            if (type != MsgType.Text)
                return new ResultData(false);

            ResultData result = new ResultData(true);
            var msg = data as TextMsg;

            ReTextMsg replyMsg = new ReTextMsg()
            {
                Content = "你问我答，为您提供公共信息查询、百科知识库以及互动类问答等超炫功能，系统即将上线敬请期待。",
                CreateTime = DateTime.Now,
                FromUserID = msg.ToUserID,
                ToUserID = msg.FromUserID,
                FuncFlag = 0,
                MsgType = "text"
            };

            result.Data = replyMsg;

            return result;
        }
    }
}
