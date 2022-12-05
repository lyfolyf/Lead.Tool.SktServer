using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lead.Tool.SktServer
{
    public class Config
    {
        public string ToolName { get; set; }
        
        public string IP { get; set; }

        public string Port { get; set; }

        public bool IsHeart { get; set; }
        public int HeartInterval { get; set; }
    }
}
