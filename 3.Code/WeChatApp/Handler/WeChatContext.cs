using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;

using WeChat.Entity.Biz;

namespace WeChat.Handler
{
    public class WeChatContext
    {
        public const string LoginUrl = "http://mp.weixin.qq.com/cgi-bin/login?lang=zh_CN";
        public const string IndexUrl = "http://mp.weixin.qq.com/cgi-bin/indexpage?t=wxm-index&lang=zh_CN";
        public const string FansUrl = "http://mp.weixin.qq.com/cgi-bin/contactmanagepage?t=wxm-friend&lang=zh_CN&pagesize=10&pageidx=0&type=0&groupid=0";
        public const string SendMsgUrl = "http://mp.weixin.qq.com/cgi-bin/singlesend?t=ajax-response&lang=zh_CN";

        public string UserName { get; set; }

        public string Password { get; set; }
        
        public void Login()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(LoginUrl);
            request.CookieContainer = new CookieContainer();
            request.Method = "POST";
            request.BeginGetRequestStream(new AsyncCallback(LoginRequestCallback), request);
        }

        void LoginRequestCallback(IAsyncResult iar)
        {
            var request = iar.AsyncState as HttpWebRequest;

            string post = string.Format("username:{0},pwd:{1},imgcode:,f:json", this.UserName, this.Password);
            byte[] buffer = Encoding.UTF8.GetBytes(post);
            var stream = request.EndGetRequestStream(iar);
            stream.Write(buffer, 0, buffer.Length);
            stream.Close();

            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();
            byte[] receiveBuffer = new byte[response.ContentLength];
            responseStream.Read(receiveBuffer, 0, receiveBuffer.Length);
            responseStream.Close();

            var result = Encoding.UTF8.GetString(receiveBuffer);
        }

        //public List<UserInfo> GetUserList()
        //{

        //}

        //public bool Send()
        //{

        //}
    }
}
