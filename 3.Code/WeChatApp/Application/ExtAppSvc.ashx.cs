using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.IO;

using WeChat.Handler;

namespace WeChat.Application
{
    /// <summary>
    /// ExtAppSvc 的摘要说明
    /// </summary>
    public class ExtAppSvc : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            RequestHandler handler = new RequestHandler();
            handler.Process(context);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }        
    }
}