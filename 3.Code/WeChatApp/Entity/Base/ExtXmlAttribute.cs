using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeChat.Entity
{
    public class MappingXmlAttribute : Attribute
    {
        public string NodeName { get; set; }

        public bool IsCDate { get; set; }

        public MappingXmlAttribute(string nodeName)
        {
            this.NodeName = nodeName;
            this.IsCDate = false;
        }

        public MappingXmlAttribute(string nodeName, bool isCData)
        {
            this.NodeName = nodeName;
            this.IsCDate = IsCDate;
        }
    }
}
