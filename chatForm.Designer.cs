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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // chatTitle
            // 
            this.chatTitle.AutoSize = true;
            this.chatTitle.Location = new System.Drawing.Point(143, 105);
            this.chatTitle.Name = "chatTitle";
            this.chatTitle.Size = new System.Drawing.Size(45, 13);
            this.chatTitle.TabIndex = 0;
            this.chatTitle.Text = "הודעות";
            // 
            // filesTitle
            // 
            this.filesTitle.AutoSize = true;
            this.filesTitle.Location = new System.Drawing.Point(397, 105);
            this.filesTitle.Name = "filesTitle";
            this.filesTitle.Size = new System.Drawing.Size(40, 13);
            this.filesTitle.TabIndex = 3;
            this.filesTitle.Text = "קבצים";
            // 
            // sendingMessege
            // 
            this.sendingMessege.AutoSize = true;
            this.sendingMessege.Location = new System.Drawing.Point(639, 201);
            this.sendingMessege.Name = "sendingMessege";
            this.sendingMessege.Size = new System.Drawing.Size(76, 13);
            this.sendingMessege.TabIndex = 5;
            this.sendingMessege.Text = "כתיבת הודעה";
            // 
            // txtbxChat
            // 
            this.txtbxChat.Enabled = false;
            this.txtbxChat.Location = new System.Drawing.Point(590, 226);
            this.txtbxChat.Name = "txtbxChat";
            this.txtbxChat.Size = new System.Drawing.Size(122, 20);
            this.txtbxChat.TabIndex = 6;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Enabled = false;
            this.btnSelectFile.Location = new System.Drawing.Point(595, 147);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(120, 23);
            this.btnSelectFile.TabIndex = 7;
            this.btnSelectFile.Text = "בחירת קובץ";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.BtnSelectFile_Click);
            // 
            // btnSendingMsg
            // 
            this.btnSendingMsg.Enabled = false;
            this.btnSendingMsg.Location = new System.Drawing.Point(590, 265);
            this.btnSendingMsg.Name = "btnSendingMsg";
            this.btnSendingMsg.Size = new System.Drawing.Size(120, 25);
            this.btnSendingMsg.TabIndex = 8;
            this.btnSendingMsg.Text = "שליחה";
            this.btnSendingMsg.UseVisualStyleBackColor = true;
            this.btnSendingMsg.Click += new System.EventHandler(this.BtnSendingMsg_Click);
            // 
            // listMessage
            // 
            this.listMessage.Enabled = false;
            this.listMessage.FormattingEnabled = true;
            this.listMessage.Location = new System.Drawing.Point(51, 127);
            this.listMessage.Name = "listMessage";
            this.listMessage.Size = new System.Drawing.Size(231, 186);
            this.listMessage.TabIndex = 9;
            // 
            // filesList
            // 
            this.filesList.Enabled = false;
            this.filesList.FormattingEnabled = true;
            this.filesList.Location = new System.Drawing.Point(307, 127);
            this.filesList.Name = "filesList";
            this.filesList.Size = new System.Drawing.Size(193, 186);
            this.filesList.TabIndex = 10;
            // 
            // txbName
            // 
            this.txbName.Location = new System.Drawing.Point(590, 58);
            this.txbName.Name = "txbName";
            this.txbName.Size = new System.Drawing.Size(146, 20);
            this.txbName.TabIndex = 14;
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(499, 56);
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
            this.cmbPort.Location = new System.Drawing.Point(132, 38);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(160, 21);
            this.cmbPort.TabIndex = 15;
            this.cmbPort.SelectedIndexChanged += new System.EventHandler(this.CmbPort_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(314, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "תעבורת נתונים";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(101, 401);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "מספר אנשים מחוברים";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(67, 401);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "0";
            // 
            // btnLogout
            // 
            this.btnLogout.Enabled = false;
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnLogout.Location = new System.Drawing.Point(607, 371);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 19;
            this.btnLogout.Text = "התנתקות";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.Location = new System.Drawing.Point(751, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 16);
            this.label4.TabIndex = 20;
            this.label4.Text = ":שם";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(713, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 21;
            this.button1.Text = "יציאה";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click_1);
            // 
            // chatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbPort);
            this.Controls.Add(this.txbName);
            this.Controls.Add(this.connectBtn);
            this.Controls.Add(this.filesList);
            this.Controls.Add(this.listMessage);
            this.Controls.Add(this.btnSendingMsg);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.txtbxChat);
            this.Controls.Add(this.sendingMessege);
            this.Controls.Add(this.filesTitle);
            this.Controls.Add(this.chatTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "chatForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.ChatForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button button1;
    }
}

