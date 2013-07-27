using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using WeChat.Entity;
using WeChat.Entity.Message;
using WeChat.Handler;
using EzUtility.FrameworkExt;

namespace WeChat.Handler
{
    public class BizProcessor
    {
        public List<DataHandler> DataHandlers { get; set; }

        public WeChatContext Context { get; set; }
        
        private static BizProcessor _Instance;

        public static BizProcessor Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new BizProcessor();

                return _Instance;
            }

            set
            {
                _Instance = value;
            }
        }

        public BizProcessor()
        {
            Context = new WeChatContext();

            DataHandlers = new List<DataHandler>();

            DataHandlers.Add(new TextMsgHandler());
            DataHandlers.Add(new ImageMsgHandler());
        }

        public string Process(string post)
        {
            if (post.IsNull())
                return string.Empty;

            string result = null;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(post);
            XmlNode xmlNode = xmlDoc.SelectSingleNode("/xml/MsgType");
            if(xmlNode != null)
            {   
                switch(xmlNode.InnerText)
                {
                    case "text":
                        {
                            var data = ConvertToEntity<TextMsg>(xmlDoc);
                            result = Process(MsgType.Text, data);
                        }
                        break;
                    case "image":
                        {
                            var data = ConvertToEntity<ImageMsg>(xmlDoc);
                            result = Process(MsgType.Image, data);
                        }
                        break;
                    case "location":
                        {

                        }
                        break;
                    case "link":
                        {

                        }
                        break;
                    case "event":
                        {

                        }
                        break;
                }
            }            
            return result;
        }

        public string Process(MsgType type, object data)
        {
            if (DataHandlers == null || DataHandlers.Count <= 0)
                return null;

            string xmlData = null;
            for (int i = 0; i < DataHandlers.Count; i++)
            {
                var result = DataHandlers[i].Process(type, data, Context);
                if (result != null && result.IsHandle)
                {
                    xmlData = ConvertToXml(result.Data);
                    break;
                }
            }

            return xmlData;
        }

        public string ConvertToXml(object data)
        {
            StringBuilder xml = new StringBuilder();
            XmlWriter xWriter = XmlWriter.Create(xml);

            var type = data.GetType();
            var properties = type.GetProperties();

            xWriter.WriteStartElement("xml");
            foreach (var info in properties)
            {
                var mapping = info.GetCustomAttributes(typeof(Entity.MappingXmlAttribute), false);
                if (mapping != null && mapping.Length > 0)
                {
                    var att = mapping[0] as Entity.MappingXmlAttribute;
                    xWriter.WriteStartElement(att.NodeName);
                    if (att.IsCDate)
                    {
                        xWriter.WriteCData(info.GetValue(data, null).ToString());
                    }
                    else
                    {
                        if (info.PropertyType == typeof(DateTime))
                            xWriter.WriteValue(GetWeixinDateTime(info.GetValue(data, null).ToDateTime()));
                        else
                            xWriter.WriteValue(info.GetValue(data, null));
                    }
                    xWriter.WriteEndElement();
                }
            }
            xWriter.WriteEndElement();
            xWriter.Flush();
            xWriter.Close();

            return xml.ToString();
        }

        public T ConvertToEntity<T>(XmlDocument xml)
        {
            var type = typeof(T);
            T data = Activator.CreateInstance<T>();
            var properties = type.GetProperties();
            foreach(var info in properties)
            {
                var mapping = info.GetCustomAttributes(typeof(Entity.MappingXmlAttribute), false);
                if (mapping != null && mapping.Length > 0)
                {
                    var att = mapping[0] as Entity.MappingXmlAttribute;
                    var node = xml.SelectSingleNode(string.Format("/xml/{0}", att.NodeName));

                    if (info.PropertyType == typeof(DateTime))
                        info.SetValue(data, GetDateTimeFromXml(node.InnerText.ToLong()), null);
                    else if (info.PropertyType == typeof(long))
                        info.SetValue(data, node.InnerText.ToLong(), null);
                    else
                        info.SetValue(data, node.InnerText, null);
                }
            }

            return data;
        }

        /// <summary>
        /// 转换微信DateTime时间到C#时间
        /// </summary>
        /// <param name="dateTimeFromXml">微信DateTime</param>
        /// <returns></returns>
        public static DateTime GetDateTimeFromXml(long dateTimeFromXml)
        {
            return new DateTime(1970,1,1).AddTicks((dateTimeFromXml + 8 * 60 * 60) * 10000000);
        }            

        /// <summary>
        /// 获取微信DateTime
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns></returns>
        public static long GetWeixinDateTime(DateTime dateTime)
        {
            return (dateTime.Ticks - new DateTime(1970,1,1).Ticks) / 10000000 - 8 * 60 * 60;
        }
    }
}
