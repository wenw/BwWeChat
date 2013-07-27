using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeChat.Handler
{
    public enum MsgType
    {
        None = 0,
        Validation = 1,
        Text = 2,
        Image = 3,
        Location = 4,
        Link = 5,
        Event = 6,
        Music = 7,
        News = 8
    }
}
