using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EzUtility.CustomAttribute
{
    public class ExtensionProperty : Attribute
    {
        public Type TargetType { get; set; }

        public string PropertyName { get; set; }
    }
}
