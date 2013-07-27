using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Configuration;
using System.Xml.Serialization;

using log4net;

namespace WeChat.Handler
{
    public class RequestHandler
    {
        public string Token = ConfigurationManager.AppSettings["WeChatToken"];

        public void Process(HttpContext context)
        {
            try
            {
                if (context.Request.RequestType.Equals("post",StringComparison.CurrentCultureIgnoreCase))
                {
                    ApiPostResponse(context);
                }
                else
                {
                    ValidationResponse(context);
                }
            }
            catch (Exception ex)
            {
                LogError("error=> Message:{0};{1}StockTrack:{2}{1}", ex.Message, Environment.NewLine, ex.StackTrace);
            }
        }

        bool VerifySignature(string signature, string timestamp, string nonce)
        {
            var list = new List<string>() { Token, timestamp, nonce };
            list.Sort();

            string newValue = string.Empty;
            foreach (var key in list)
                newValue += key;

            var mySignature = FormsAuthentication.HashPasswordForStoringInConfigFile(newValue, FormsAuthPasswordFormat.SHA1.ToString());

            return signature.Equals(mySignature, StringComparison.CurrentCultureIgnoreCase);
        }

        void ValidationResponse(HttpContext context)
        {
            var signature = context.Request["signature"];
            var timestamp = context.Request["timestamp"];
            var nonce = context.Request["nonce"];
            var echostr = context.Request["echostr"];

            if (VerifySignature(signature, timestamp, nonce))
            {
                context.Response.Write(echostr);
                LogInfo("Validation Service Successfully. =>signature:{0}, timestamp:{1}, nonce:{2}, echostr:{3}",
                    signature, timestamp, nonce, echostr);
            }
            else
                LogInfo("Validation Service Failed.=>signature:{0}, timestamp:{1}, nonce:{2}, echostr:{3}",
                    signature, timestamp, nonce, echostr);
        }

        void ApiPostResponse(HttpContext context)
        {
            var signature = context.Request["signature"];
            var timestamp = context.Request["timestamp"];
            var nonce = context.Request["nonce"];

            if (!VerifySignature(signature, timestamp, nonce))
            {
                LogInfo("Post Validation Failed.=>signature:{0}, timestamp:{1}, nonce:{2}, result:{3}",
                    signature, timestamp, nonce, "false");
                return;
            }

            var post = string.Empty;
            byte[] buffer = new byte[context.Request.TotalBytes];
            var stream = context.Request.InputStream;
            stream.Read(buffer, 0, buffer.Length);
            post = Encoding.UTF8.GetString(buffer);

            var result = BizProcessor.Instance.Process(post);

            context.Response.Write(result);

            LogInfo("request=> Post:{0}, Get:{1}, IP:{2};",
                post,
                context.Request.Url.ToString(),
                context.Request.UserHostAddress);
        }

        protected void LogInfo(string format, params object[] args)
        {
            ILog logger = LogManager.GetLogger("InfoLogger");
            logger.InfoFormat(format, args);
        }

        protected void LogError(string format, params object[] args)
        {
            ILog logger = LogManager.GetLogger("ErrorLogger");
            logger.ErrorFormat(format, args);
        }
    }
}
