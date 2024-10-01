namespace UdpDemoClientWF
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtIpAddress = new TextBox();
            label2 = new Label();
            numPort = new NumericUpDown();
            txtMessage = new TextBox();
            label3 = new Label();
            btnSend = new Button();
            groupBox1 = new GroupBox();
            txtLog = new TextBox();
            label4 = new Label();
            cbType = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)numPort).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 17);
            label1.Name = "label1";
            label1.Size = new Size(97, 25);
            label1.TabIndex = 0;
            label1.Text = "IP Address";
            // 
            // txtIpAddress
            // 
            txtIpAddress.Location = new Point(125, 11);
            txtIpAddress.Name = "txtIpAddress";
            txtIpAddress.Size = new Size(150, 31);
            txtIpAddress.TabIndex = 0;
            txtIpAddress.Text = "127.0.0.1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 57);
            label2.Name = "label2";
            label2.Size = new Size(44, 25);
            label2.TabIndex = 2;
            label2.Text = "Port";
            // 
            // numPort
            // 
            numPort.Location = new Point(125, 51);
            numPort.Maximum = new decimal(new int[] { 65534, 0, 0, 0 });
            numPort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPort.Name = "numPort";
            numPort.Size = new Size(150, 31);
            numPort.TabIndex = 1;
            numPort.Value = new decimal(new int[] { 12345, 0, 0, 0 });
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(125, 133);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.ScrollBars = ScrollBars.Vertical;
            txtMessage.Size = new Size(220, 85);
            txtMessage.TabIndex = 3;
            txtMessage.Text = "Hello world!";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 94);
            label3.Name = "label3";
            label3.Size = new Size(49, 25);
            label3.TabIndex = 5;
            label3.Text = "Type";
            // 
            // btnSend
            // 
            btnSend.Location = new Point(12, 230);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(333, 43);
            btnSend.TabIndex = 4;
            btnSend.Text = "Send Packet";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtLog);
            groupBox1.Location = new Point(362, 11);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(565, 262);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Received Data";
            // 
            // txtLog
            // 
            txtLog.Location = new Point(6, 24);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(553, 232);
            txtLog.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 136);
            label4.Name = "label4";
            label4.Size = new Size(74, 25);
            label4.TabIndex = 8;
            label4.Text = "Payload";
            // 
            // cbType
            // 
            cbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbType.FormattingEnabled = true;
            cbType.Items.AddRange(new object[] { "Text Message", "Sound", "Exit Trigger" });
            cbType.Location = new Point(125, 91);
            cbType.Name = "cbType";
            cbType.Size = new Size(220, 33);
            cbType.TabIndex = 2;
            cbType.SelectedIndexChanged += cbType_SelectedIndexChanged;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(939, 285);
            Controls.Add(cbType);
            Controls.Add(label4);
            Controls.Add(groupBox1);
            Controls.Add(btnSend);
            Controls.Add(label3);
            Controls.Add(txtMessage);
            Controls.Add(numPort);
            Controls.Add(label2);
            Controls.Add(txtIpAddress);
            Controls.Add(label1);
            MaximizeBox = false;
            Name = "frmMain";
            Text = "UDP Test App";
            FormClosing += frmMain_FormClosing;
            Load += frmMain_Load;
            ((System.ComponentModel.ISupportInitialize)numPort).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtIpAddress;
        private Label label2;
        private NumericUpDown numPort;
        private TextBox txtMessage;
        private Label label3;
        private SplitContainer splitContainer1;
        private Button btnSend;
        private GroupBox groupBox1;
        private TextBox txtLog;
        private Label label4;
        private ComboBox cbType;
    }
}
