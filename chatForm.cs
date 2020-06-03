using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace ChatApp
{
    public partial class chatForm : Form
    {
        Socket soc;
        IPAddress ipAdd;
        IPEndPoint remoteEP;

        public chatForm()
        {
            InitializeComponent();
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
 
;
          //  server.Connect(ipep);
        }

        private void LunchBtn_Click(object sender, EventArgs e)
        {
            // פעולה זו אחראית על ההתחברות לצ'אט ולשרת ובודקת תקינות של קלט השם
            string name = txbName.Text;
   
            if (name.CompareTo("")==0) // במקרה ואין שם
            {
                txbName.Focus();
                errorProvider1.SetError(txbName, "לא הוכנס שם");
            } 
            else
            {
                // כאן יתאפשר ההתחברות לשרת
                ConnectToChat();
            }
        }
        private void ConnectToChat()
        {
            // קוראים לפעולה הזו מתי שמתחברים לצ'אט
            // מתודה זו אחראית על איפשור לחיצה על הכפתורים והכתיבה בטקסט בוקס
            btnLogout.Enabled = true;
            btnSendingMsg.Enabled = true;
            listMessage.Enabled = true;
            cmbPort.Enabled = true;
            txtbxChat.Enabled = true;
            cmbPort.SelectedIndex = 0;
            txbName.Enabled = false;
            connectBtn.Enabled = false;
        }
        private void DisconnectFromChat()
        {
            cmbPort.SelectedIndex = 0;
            connectBtn.Enabled = true;
            btnLogout.Enabled = false;
            btnSendingMsg.Enabled = false;
            listMessage.Enabled = false;
            cmbPort.Enabled = false;
            txtbxChat.Enabled = false;
            txbName.Enabled = true;
            txbName.Text = "";
        }

        private void BtnSendingMsg_Click(object sender, EventArgs e)
        {
            /*  byte[] byData = System.Text.Encoding.ASCII.GetBytes(txtbxChat.Text);
              soc.Send(byData); */
            if (txtbxChat.Text.CompareTo("") == 0) // במקרה ואין שם
            {
                txbName.Focus();
                errorProvider1.SetError(txbName, "לא הוכנס תוכן להודעה");
            } else 
            listMessage.Items.Add(txbName.Text + " >> " + txtbxChat.Text);
        }

  



        private void Button1_Click(object sender, EventArgs e)
        {
            // פעולה זאת תדאג להתנתקות מהשרת ויציאה מהצ'אט
            DisconnectFromChat();
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        private void CmbPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            // החלפה בין פורט 8080 הודעות לפורט קבצים 8081
            if(cmbPort.SelectedIndex==1)
            {
                EnableFiles();
            }
            else
            {
                EnableMessages();
            }
        }
        private void EnableFiles()
        {
            txtbxChat.Enabled = false;
            listMessage.Enabled = false;
            filesList.Enabled = true;
            btnSelectFile.Enabled = true;
            btnSendingMsg.Enabled = false;
        }
        private void EnableMessages()
        {
            btnSendingMsg.Enabled = true;
            txtbxChat.Enabled = true;
            listMessage.Enabled = true;
            filesList.Enabled = false;
            btnSelectFile.Enabled = false;
        }

        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            // מתודה זו תהיה אחראית על העלאת קבצים ושיתופם לשרת
            //  FolderBrowserDialog FBD = new FolderBrowserDialog();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JSON files (*.json)|*.json|XML files (*.xml)|*.xml"; // file types, that will be allowed to upload
            dialog.Multiselect = true; // allow/deny user to upload more than one file at a time
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                String path = dialog.FileName; // get name of file
                using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open), new UTF8Encoding())) // do anything you want, e.g. read it
                {
                    // ...
                }
            }
        }
    }
}
