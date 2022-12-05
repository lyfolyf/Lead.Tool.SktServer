using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Lead.Tool.SktServer
{
    public partial class DebugUI : UserControl
    {
        SktSeverTool _Proxy = null;

        public DebugUI(SktSeverTool value)
        {
            InitializeComponent();
            _Proxy = value;
            dataGridView1.DataSource = _Proxy._dicPointSocket;
            _Proxy.MesEvent += new MesRecved(ShowMes);
        }


        private void ShowMes(string SktName, string mes)
        {
            this.BeginInvoke(new Action<string, string>((x, y) => {
                this.richTextBoxRecived.AppendText( x + ":" + y+"\r\n");
            }),SktName,mes);
        }
        private void buttonInit_Click(object sender, EventArgs e)
        {
            _Proxy.Init();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            _Proxy.Start();
        }

        private void buttonTerminate_Click(object sender, EventArgs e)
        {
            _Proxy.Terminate();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            _Proxy.SendInfoByPoint(comboBox1.SelectedItem.ToString(), richTextBox1.Text.Trim());
        }

        public void AddPoint(ConnectedSocketInfo value)
        {
            Action<ConnectedSocketInfo> ac = (x) =>{
                _Proxy._dicPointSocket.Add(x);
                comboBox1.Items.Add(x.Point);
            };

            this.Invoke(ac,value);
        }

        public void RemovePoint(ConnectedSocketInfo value)
        {
            Action<ConnectedSocketInfo> ac = (x) => {
                _Proxy._dicPointSocket.Remove(x);
                comboBox1.Items.Remove(x.Point);
            };

            this.Invoke(ac, value);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_Proxy.State == Interface.IToolState.ToolInit)
            {
                buttonInit.BackColor = Color.Green;
                buttonStart.BackColor = Color.Gray;
                buttonTerminate.BackColor = Color.Gray;
            }
            if (_Proxy.State == Interface.IToolState.ToolRunning)
            {
                buttonInit.BackColor = Color.Green;
                buttonStart.BackColor = Color.Green;
                buttonTerminate.BackColor = Color.Gray;
            }
            if (_Proxy.State == Interface.IToolState.ToolTerminate)
            {
                buttonInit.BackColor = Color.Gray;
                buttonStart.BackColor = Color.Gray;
                buttonTerminate.BackColor = Color.Red;
            }
        }
    }
}
