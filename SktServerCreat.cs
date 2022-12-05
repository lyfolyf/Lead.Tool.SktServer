using Lead.Tool.Interface;
using Lead.Tool.Resources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Lead.Tool.SktServer
{
    public class SktServerCreat : ICreat
    {
        public ITool GetInstance(string Name, string Path)
        {
            return new SktSeverTool(Name,Path);
        }

        public Image Icon
        {
            get
            {
                return (Image)ImageManager.GetImage("服务器");
            }
        }

        public string Name
        {
            get
            {
                return "SktServer";
            }
        }
    }
}
