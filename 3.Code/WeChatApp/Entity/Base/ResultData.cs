using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeChat.Entity
{
    public class ResultData
    {
        public Guid ID { get; set; }

        public bool IsHandle { get; set; }

        public object Data { get; set; }

        public ResultData(bool isHandle)
        {
            this.ID = Guid.NewGuid();
            this.IsHandle = isHandle;
        }

        public ResultData(bool isHandle, object data)
        {
            this.ID = Guid.NewGuid();
            this.IsHandle = isHandle;
            this.Data = data;
        }
    }
}
