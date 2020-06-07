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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ChatApp
{
    public partial class chatForm : Form
    {
        // כל המשתנים שהכרחיים לצד לקוח
        private Socket ClientSocket;
        private const int PORT = 8080;
        private string username;
        private string server_ip;
        private string filesFolderLocation;
        private const string fileNameCmnd = "FILE_NAME:"; // הפעולה שתאפשר לשלוח את הקובץ לשרת
        private const string fileContentCmnd = "FILE_CONTENT:";
        private string fileName; // name of the file
        private string fileContent; // content of the file
        private void ConnectToServer()
        {
             try
                {
                   if (!ClientSocket.Connected)
                   {
                    ClientSocket.Connect(server_ip, PORT);
                   }
                }
                catch (SocketException)
                {
                    MessageBox.Show("Error occuared in connecting to the server","Connection Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
       
            RequestLoop(this.username + " joined to server.");
        }

        public void RequestLoop(string text)
        {
                SendRequest(text);
                func(); // listening to the server
        }

        /// <summary>
        /// Close socket and exit program.
        /// </summary>
        public void Exit()
        {
            SendString("exit"); // Tell the server we are exiting
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();
        }

        private void SendRequest(string str)
        {
            SendString(this.username + " : " + str);
            if (str.ToLower() == "exit")
            {
                Exit();
            }
        }
        /// <summary>
        /// Sends a string to the server with ASCII encoding.
        /// </summary>
        /// 
        private void SendString(string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }


        /// <summary>
        /// Listens to messages or files from the server
        /// </summary>
        async void func()
        {
            string res;
            while (true)
            {
                res = string.Empty;//error happened here
                try
                {
                    await Task.Run(async () =>
                    {

                        res = await ReceiveResponse();
                    });
                }
                catch (Exception)
                {
                }
                if(res != string.Empty && res.Contains(fileNameCmnd))
                {
                    fileName = Path.GetFileName(res.Replace(fileNameCmnd, string.Empty));
                    //NEED TO CREATE THE FILE
                    filesList.Items.Add(fileName);
                    CreateFile(filesFolderLocation + fileName);

                }
                else if(res != string.Empty && this.filesList.Enabled==true)
                {
                    try
                    {
                      //  fileContent = Path.GetFileName(res.Replace(fileContentCmnd, string.Empty));//here error happened
                        WriteToFile(filesFolderLocation + fileName, res);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Some wired error with the file happened: ");
                    }
       
                }
                else if (res != string.Empty && this.listMessage.Enabled == true)
                    listMessage.Items.Add(res); 
                else if(res != string.Empty && this.filesList.Enabled == true)
                {
                    filesList.Items.Add(res);// need only the name of the file to add to the list
                }
            }
        }
        async Task<string> ReceiveResponse()
        {
            byte[] buffer = new byte[50000]; //need to think on solution to this 
            int res = await ReciveFromServer(buffer);
            if (res == 0) return string.Empty;
            var data = new byte[res];
            Array.Copy(buffer, data, res);
            string text = Encoding.ASCII.GetString(data);
            return text;
        }
         private async Task<int> ReciveFromServer(byte[] buffer) // סנכרון פעולת האזנה , יש צורך לעבוד עליה עוד
        {
            return  ClientSocket.Receive(buffer, SocketFlags.None);
        }


        private void CreateFile(string path)
        {
            try
            {    
                using (FileStream fs = File.Create(path)){};
            }
            catch(Exception)//need to handle io exception
            {
                MessageBox.Show("Error in sending the file , please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void WriteToFile(string path , string content)
        {
            try
            {
                System.IO.File.WriteAllText(path, content);
            }
            catch (Exception)
            {
                MessageBox.Show("Error in sending the file , please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            fileName = string.Empty;
            fileContent = string.Empty;
        }
        /////////////////////////////////////////////////////
        public chatForm()
        {
            InitializeComponent();
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
           filesFolderLocation = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\")) + @"files\";
        }

        private void LunchBtn_Click(object sender, EventArgs e)
        {
            // פעולה זו אחראית על ההתחברות לצ'אט ולשרת ובודקת תקינות של קלט השם
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string name = txbName.Text;
   
            if (name.CompareTo(string.Empty)==0) // במקרה ואין שם
            {
                txbName.Focus();
                errorProvider1.SetError(txbName, "לא הוכנס שם");
            } 
            else
            {
                // כאן יתאפשר ההתחברות לשרת
                this.username = name;
                ConnectToChat();
            }
            cmbPort.Text = "תעבורת הודעות";
        }

        /// <summary>
        /// This method allows the client to connect the chat. 
        /// </summary>
        private void ConnectToChat()
        {
            btnSendingMsg.Enabled = true;
            listMessage.Enabled = true;
            cmbPort.Enabled = true;
            txtbxChat.Enabled = true;
            cmbPort.SelectedIndex = 0;
            txbName.Enabled = false;
            connectBtn.Enabled = false;
            btnExitFromSystem.Enabled = true;
            ConnectToServer();
        }

        /// <summary>
        /// Sending the text message to the server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSendingMsg_Click(object sender, EventArgs e)
        {
            string message = txtbxChat.Text;
            if (message.CompareTo(string.Empty) == 0) // במקרה ואין שם
            {
                txbName.Focus();
                errorProvider1.SetError(txbName, "לא הוכנס תוכן להודעה");
            } else
            {
                // in case the user try to send files commandes to the server , 
                //we will delete 'FILE_NAME:'  command , and send the rest of the string.
                if (message.Contains(fileNameCmnd))
                message = message.Replace(fileNameCmnd, string.Empty);
                else if (message.Contains(fileContentCmnd))
                message = message.Replace(fileContentCmnd, string.Empty);
                RequestLoop(message);
            }
            txtbxChat.Text = string.Empty; // clean the text in message box
        }
        private void Button1_Click(object sender, EventArgs e)
        {
        }
        private void Button1_Click_1(object sender, EventArgs e)
        {
            DisconnectFromChat();
        }
        /// <summary>
        /// Disconnecting the client and closing the chat app.
        /// </summary>
        private void DisconnectFromChat()
        {
            Exit();
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

        private async void BtnSelectFile_Click(object sender, EventArgs e)
        {
            // מתודה זו תהיה אחראית על העלאת קבצים ושיתופם לשרת
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JSON files (*.json)|*.json|XML files (*.xml)|*.xml"; // file types, that will be allowed to upload
            dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                try
                { 
                    string path = dialog.FileName.ToLower(); 
                    SendString(fileNameCmnd + path); // name of the file
                    MessageBox.Show(fileNameCmnd + path);
                    await Task.Delay(300);
                    //sending the content of the file
                    if (path.Contains(".json"))
                    {
                        JObject data = JObject.Parse(File.ReadAllText(path));
                        SendString(data.ToString()); //content of the file 
                        MessageBox.Show(data.ToString());

                    }
                    else if (path.Contains(".xml"))
                    {
                        var xmlString = File.ReadAllText(path);
                        SendString(xmlString);
                        MessageBox.Show(xmlString);
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("You are already sent this file which exist in your 'files' folder.  \n" +
                        "Please send a different file or make sure you deleted the existing one.", "File Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
        }

        private void FilesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*   MessageBox.Show("you pressed an item with index " + filesList.SelectedIndex 
                   + " , which it's name , " + filesList.SelectedItem);

           } */
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            cmb_Login_option.Enabled = false;
            btnSystemConnect.Enabled = false;
            chat_panel.Visible = true;
            txbName.Enabled = true;
            txbName.Text = string.Empty;
            connectBtn.Enabled = true;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("הסבר - מערכת הצ'אט מתחברת לכתובת אינטרנט של השרת , אם הצ'אט מחובר למחשב עם כתובת הזהה לשרת, \n " +
                "נצטרך לבחור באופצייה - 'הרצת הצ'אט על המחשב עם כתובת הזהה לשרת' , במידה והכתובת שונה \n " +
                "נבחר באופצייה - 'הרצת הצ'אט במחשב עם כתובת השונה מכתובת השרת'. \n" +
                " הערה: ייתכן ותהיה בעיית התחברות לצ'אט אם כתובת האינטרנט של המחשב שדרכו \n" +
                "אנחנו מתחברים לצ'אט שונה משל השרת , במידה וזה קורה צריך לבטל את ה 'firewall.'",
                "הסבר על התחברות למערכת הצ'אט", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Cmb_Login_option_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmb_Login_option.SelectedIndex==0)
            {
                txb_ip.Text = IPAddress.Loopback.ToString();
                server_ip = txb_ip.Text;
            }
            else
            {
                txb_ip.Text = string.Empty;
                server_ip = txb_ip.Text;
            }
        }
        public static string GetLocalIPAddress() // this function returns the local IPv4 address of the host.
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        private void Cb_hide_ip_CheckedChanged(object sender, EventArgs e)
        {
            if(cb_hide_ip.Checked)
            {
                txb_ip.PasswordChar='*';
            }
            else
            {
                txb_ip.PasswordChar = '\0';
            }
          
        }

        private void Chat_panel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
