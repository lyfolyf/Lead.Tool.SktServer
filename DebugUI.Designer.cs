namespace Lead.Tool.SktServer
{
    partial class DebugUI
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonTerminate = new System.Windows.Forms.Button();
            this.buttonInit = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.richTextBoxRecived = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(267, 279);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(391, 186);
            this.richTextBox1.TabIndex = 20;
            this.richTextBox1.Text = "";
            // 
            // buttonSend
            // 
            this.buttonSend.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSend.Location = new System.Drawing.Point(559, 471);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 31);
            this.buttonSend.TabIndex = 19;
            this.buttonSend.Text = "发送";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(309, 476);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(218, 23);
            this.comboBox1.TabIndex = 18;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(194, 26);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(913, 201);
            this.dataGridView1.TabIndex = 17;
            // 
            // buttonTerminate
            // 
            this.buttonTerminate.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonTerminate.Location = new System.Drawing.Point(63, 397);
            this.buttonTerminate.Name = "buttonTerminate";
            this.buttonTerminate.Size = new System.Drawing.Size(98, 51);
            this.buttonTerminate.TabIndex = 16;
            this.buttonTerminate.Text = "终止";
            this.buttonTerminate.UseVisualStyleBackColor = true;
            this.buttonTerminate.Click += new System.EventHandler(this.buttonTerminate_Click);
            // 
            // buttonInit
            // 
            this.buttonInit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonInit.Location = new System.Drawing.Point(63, 262);
            this.buttonInit.Name = "buttonInit";
            this.buttonInit.Size = new System.Drawing.Size(98, 51);
            this.buttonInit.TabIndex = 11;
            this.buttonInit.Text = "启动 ";
            this.buttonInit.UseVisualStyleBackColor = true;
            this.buttonInit.Click += new System.EventHandler(this.buttonInit_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonStart.Location = new System.Drawing.Point(63, 329);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(98, 51);
            this.buttonStart.TabIndex = 21;
            this.buttonStart.Text = "开始";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // richTextBoxRecived
            // 
            this.richTextBoxRecived.Location = new System.Drawing.Point(771, 279);
            this.richTextBoxRecived.Name = "richTextBoxRecived";
            this.richTextBoxRecived.Size = new System.Drawing.Size(404, 186);
            this.richTextBoxRecived.TabIndex = 22;
            this.richTextBoxRecived.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(393, 258);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 23;
            this.label1.Text = "发送框";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(950, 261);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 24;
            this.label2.Text = "接收框";
            // 
            // DebugUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBoxRecived);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonTerminate);
            this.Controls.Add(this.buttonInit);
            this.Name = "DebugUI";
            this.Size = new System.Drawing.Size(1235, 558);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonTerminate;
        private System.Windows.Forms.Button buttonInit;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RichTextBox richTextBoxRecived;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
