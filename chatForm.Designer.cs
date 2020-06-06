namespace ChatApp
{
    partial class chatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.chatTitle = new System.Windows.Forms.Label();
            this.filesTitle = new System.Windows.Forms.Label();
            this.sendingMessege = new System.Windows.Forms.Label();
            this.txtbxChat = new System.Windows.Forms.TextBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.btnSendingMsg = new System.Windows.Forms.Button();
            this.listMessage = new System.Windows.Forms.ListBox();
            this.filesList = new System.Windows.Forms.ListBox();
            this.txbName = new System.Windows.Forms.TextBox();
            this.connectBtn = new System.Windows.Forms.Button();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnExitFromSystem = new System.Windows.Forms.Button();
            this.chat_panel = new System.Windows.Forms.Panel();
            this.txb_ip = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_hide_ip = new System.Windows.Forms.CheckBox();
            this.btnSystemConnect = new System.Windows.Forms.Button();
            this.cmb_Login_option = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.chat_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            this.SuspendLayout();
            // 
            // chatTitle
            // 
            this.chatTitle.AutoSize = true;
            this.chatTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.chatTitle.Location = new System.Drawing.Point(108, 73);
            this.chatTitle.Name = "chatTitle";
            this.chatTitle.Size = new System.Drawing.Size(42, 15);
            this.chatTitle.TabIndex = 0;
            this.chatTitle.Text = "הודעות";
            // 
            // filesTitle
            // 
            this.filesTitle.AutoSize = true;
            this.filesTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.filesTitle.Location = new System.Drawing.Point(402, 73);
            this.filesTitle.Name = "filesTitle";
            this.filesTitle.Size = new System.Drawing.Size(39, 15);
            this.filesTitle.TabIndex = 3;
            this.filesTitle.Text = "קבצים";
            // 
            // sendingMessege
            // 
            this.sendingMessege.AutoSize = true;
            this.sendingMessege.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.sendingMessege.Location = new System.Drawing.Point(642, 176);
            this.sendingMessege.Name = "sendingMessege";
            this.sendingMessege.Size = new System.Drawing.Size(72, 15);
            this.sendingMessege.TabIndex = 5;
            this.sendingMessege.Text = "כתיבת הודעה";
            // 
            // txtbxChat
            // 
            this.txtbxChat.Enabled = false;
            this.txtbxChat.Location = new System.Drawing.Point(555, 194);
            this.txtbxChat.Name = "txtbxChat";
            this.txtbxChat.Size = new System.Drawing.Size(159, 20);
            this.txtbxChat.TabIndex = 6;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Enabled = false;
            this.btnSelectFile.Location = new System.Drawing.Point(555, 114);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(159, 23);
            this.btnSelectFile.TabIndex = 7;
            this.btnSelectFile.Text = "בחירת קובץ";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.BtnSelectFile_Click);
            // 
            // btnSendingMsg
            // 
            this.btnSendingMsg.Enabled = false;
            this.btnSendingMsg.Location = new System.Drawing.Point(555, 220);
            this.btnSendingMsg.Name = "btnSendingMsg";
            this.btnSendingMsg.Size = new System.Drawing.Size(159, 25);
            this.btnSendingMsg.TabIndex = 8;
            this.btnSendingMsg.Text = "שליחה";
            this.btnSendingMsg.UseVisualStyleBackColor = true;
            this.btnSendingMsg.Click += new System.EventHandler(this.BtnSendingMsg_Click);
            // 
            // listMessage
            // 
            this.listMessage.Enabled = false;
            this.listMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.listMessage.FormattingEnabled = true;
            this.listMessage.ItemHeight = 16;
            this.listMessage.Location = new System.Drawing.Point(17, 95);
            this.listMessage.Name = "listMessage";
            this.listMessage.Size = new System.Drawing.Size(301, 180);
            this.listMessage.TabIndex = 9;
            // 
            // filesList
            // 
            this.filesList.Enabled = false;
            this.filesList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.filesList.FormattingEnabled = true;
            this.filesList.ItemHeight = 16;
            this.filesList.Location = new System.Drawing.Point(349, 95);
            this.filesList.Name = "filesList";
            this.filesList.Size = new System.Drawing.Size(134, 180);
            this.filesList.TabIndex = 10;
            this.filesList.SelectedIndexChanged += new System.EventHandler(this.FilesList_SelectedIndexChanged);
            // 
            // txbName
            // 
            this.txbName.Location = new System.Drawing.Point(555, 24);
            this.txbName.Name = "txbName";
            this.txbName.Size = new System.Drawing.Size(146, 20);
            this.txbName.TabIndex = 14;
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(464, 24);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(75, 23);
            this.connectBtn.TabIndex = 13;
            this.connectBtn.Text = "התחברות";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.LunchBtn_Click);
            // 
            // cmbPort
            // 
            this.cmbPort.Enabled = false;
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.Items.AddRange(new object[] {
            "תעבורת הודעות",
            "תעבורת קבצים"});
            this.cmbPort.Location = new System.Drawing.Point(28, 22);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(269, 21);
            this.cmbPort.TabIndex = 15;
            this.cmbPort.SelectedIndexChanged += new System.EventHandler(this.CmbPort_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(303, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "תעבורת נתונים";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.Location = new System.Drawing.Point(707, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 15);
            this.label4.TabIndex = 20;
            this.label4.Text = ":שם";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnExitFromSystem
            // 
            this.btnExitFromSystem.Location = new System.Drawing.Point(544, 292);
            this.btnExitFromSystem.Name = "btnExitFromSystem";
            this.btnExitFromSystem.Size = new System.Drawing.Size(171, 23);
            this.btnExitFromSystem.TabIndex = 21;
            this.btnExitFromSystem.Text = "יציאה מהמערכת";
            this.btnExitFromSystem.UseVisualStyleBackColor = true;
            this.btnExitFromSystem.Click += new System.EventHandler(this.Button1_Click_1);
            // 
            // chat_panel
            // 
            this.chat_panel.Controls.Add(this.btnExitFromSystem);
            this.chat_panel.Controls.Add(this.label4);
            this.chat_panel.Controls.Add(this.label1);
            this.chat_panel.Controls.Add(this.cmbPort);
            this.chat_panel.Controls.Add(this.txbName);
            this.chat_panel.Controls.Add(this.connectBtn);
            this.chat_panel.Controls.Add(this.filesList);
            this.chat_panel.Controls.Add(this.listMessage);
            this.chat_panel.Controls.Add(this.btnSendingMsg);
            this.chat_panel.Controls.Add(this.btnSelectFile);
            this.chat_panel.Controls.Add(this.txtbxChat);
            this.chat_panel.Controls.Add(this.sendingMessege);
            this.chat_panel.Controls.Add(this.filesTitle);
            this.chat_panel.Controls.Add(this.chatTitle);
            this.chat_panel.Location = new System.Drawing.Point(29, 118);
            this.chat_panel.Name = "chat_panel";
            this.chat_panel.Size = new System.Drawing.Size(754, 331);
            this.chat_panel.TabIndex = 22;
            this.chat_panel.Visible = false;
            this.chat_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.Chat_panel_Paint);
            // 
            // txb_ip
            // 
            this.txb_ip.Enabled = false;
            this.txb_ip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txb_ip.Location = new System.Drawing.Point(498, 19);
            this.txb_ip.Name = "txb_ip";
            this.txb_ip.Size = new System.Drawing.Size(182, 21);
            this.txb_ip.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(700, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 15);
            this.label2.TabIndex = 24;
            this.label2.Text = "כתובת אינטרנט";
            // 
            // cb_hide_ip
            // 
            this.cb_hide_ip.AutoSize = true;
            this.cb_hide_ip.Location = new System.Drawing.Point(498, 44);
            this.cb_hide_ip.Name = "cb_hide_ip";
            this.cb_hide_ip.Size = new System.Drawing.Size(89, 17);
            this.cb_hide_ip.TabIndex = 25;
            this.cb_hide_ip.Text = "הסתר כתובת";
            this.cb_hide_ip.UseVisualStyleBackColor = true;
            this.cb_hide_ip.CheckedChanged += new System.EventHandler(this.Cb_hide_ip_CheckedChanged);
            // 
            // btnSystemConnect
            // 
            this.btnSystemConnect.Location = new System.Drawing.Point(498, 78);
            this.btnSystemConnect.Name = "btnSystemConnect";
            this.btnSystemConnect.Size = new System.Drawing.Size(285, 23);
            this.btnSystemConnect.TabIndex = 26;
            this.btnSystemConnect.Text = "כניסה למערכת הצ\'אט";
            this.btnSystemConnect.UseVisualStyleBackColor = true;
            this.btnSystemConnect.Click += new System.EventHandler(this.Button2_Click);
            // 
            // cmb_Login_option
            // 
            this.cmb_Login_option.FormattingEnabled = true;
            this.cmb_Login_option.Items.AddRange(new object[] {
            "הרצת הצ\'אט על המחשב עם כתובת הזהה לשרת",
            "הרצת הצ\'אט במחשב עם כתובת השונה מכתובת השרת"});
            this.cmb_Login_option.Location = new System.Drawing.Point(34, 18);
            this.cmb_Login_option.Name = "cmb_Login_option";
            this.cmb_Login_option.Size = new System.Drawing.Size(298, 21);
            this.cmb_Login_option.TabIndex = 27;
            this.cmb_Login_option.SelectedIndexChanged += new System.EventHandler(this.Cmb_Login_option_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(338, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 15);
            this.label3.TabIndex = 28;
            this.label3.Text = "סוג ההתחברות למערכת\r\n";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(340, 40);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(118, 23);
            this.button3.TabIndex = 29;
            this.button3.Text = "לחץ עליי להסבר";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // chatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 461);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmb_Login_option);
            this.Controls.Add(this.btnSystemConnect);
            this.Controls.Add(this.cb_hide_ip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txb_ip);
            this.Controls.Add(this.chat_panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "chatForm";
            this.Text = "Chat App";
            this.Load += new System.EventHandler(this.ChatForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.chat_panel.ResumeLayout(false);
            this.chat_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label chatTitle;
        private System.Windows.Forms.Label filesTitle;
        private System.Windows.Forms.Label sendingMessege;
        private System.Windows.Forms.TextBox txtbxChat;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Button btnSendingMsg;
        private System.Windows.Forms.ListBox listMessage;
        private System.Windows.Forms.ListBox filesList;
        private System.Windows.Forms.TextBox txbName;
        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.ComboBox cmbPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnExitFromSystem;
        private System.Windows.Forms.Button btnSystemConnect;
        private System.Windows.Forms.CheckBox cb_hide_ip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txb_ip;
        private System.Windows.Forms.Panel chat_panel;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_Login_option;
        private System.Windows.Forms.ErrorProvider errorProvider2;
    }
}

