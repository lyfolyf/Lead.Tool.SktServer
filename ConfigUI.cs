using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lead.Tool.XML;

namespace Lead.Tool.SktServer
{
    public partial class ConfigUI : UserControl
    {
        SktSeverTool _Proxy = null;
        public ConfigUI(SktSeverTool value)
        {
            InitializeComponent();
            _Proxy = value;

            textBoxIP.Text = _Proxy._Config.IP;
            textBoxPort.Text = _Proxy._Config.Port;
            textBoxHeart.Text = _Proxy._Config.HeartInterval.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Proxy._Config.IP = textBoxIP.Text;
            _Proxy._Config.Port = textBoxPort.Text;
            _Proxy._Config.HeartInterval = Convert.ToInt32(textBoxHeart.Text);
            XmlSerializerHelper.WriteXML(_Proxy._Config, _Proxy._Path, typeof(Config));
            MessageBox.Show(_Proxy._Config.ToolName + "配置文件保存成功！");
        }
    }
}
