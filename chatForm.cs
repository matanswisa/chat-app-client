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
        private Socket ClientSocket;
        private const int PORT = 8080;
        private string username;
        private string server_ip;
        private string filesFolderLocation;

        /// <summary>
        /// ONLY_MESSAGES , fileNameCmnd , fileContentCmnd commands that allow me to interact with the input from the server.
        /// </summary>
        private const string ONLY_MESSAGE = "ONLY_MESSAGE:";
        private const string fileNameCmnd = "FILE_NAME:";
        private const string fileContentCmnd = "FILE_CONTENT:";
        private string fileName; // name of the file

        /// <summary>
        /// Connecting the client socket to the server
        /// </summary>
        private void ConnectToServer()
        {
             try
                {
                   if (!ClientSocket.Connected) // in case the client not connected
                   {
                    ClientSocket.Connect(server_ip, PORT);
                    RequestLoop(ONLY_MESSAGE+this.username + " joined to server.");
                   }
                }
                catch (SocketException)
                {
                    MessageBox.Show("Error occuared in connecting to the server","Connection Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
        }

        /// <summary>
        /// Take care of all the inputs from the server every time a new request send to the server , 
        /// and the request is waiting to recive data.
        /// </summary>
        /// <param name="text"></param>
        public void RequestLoop(string text) 
        {
                SendRequest(text);
                Listenning(); // listening to the server and reciving data
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
        /// Close socket and exit program.
        /// </summary>
        public void Exit()
        {
            SendString("exit"); // Tell the server we are exiting
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();
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
        async void Listenning()
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
                catch (Exception ex)
                {
                    MessageBox.Show("Error occuared with the connection to the server. \n" +
                        "Please try to exit and reconnect and restart the server. \n" +
                        "details on the exception: \n" + ex.ToString()
                        , "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if(res != string.Empty && res.Contains(fileNameCmnd)) // here we're handling the files requests 
                {
                    fileName = Path.GetFileName(res.Replace(fileNameCmnd, string.Empty)); //deleting the string command from the file name
                    CreateFile(filesFolderLocation + fileName); // creating a new file in the files folder.
                }
                else if(res != string.Empty && res.Contains(fileContentCmnd)) // checking if we get a file content command if it's true we are insertring the reciving data.
                {
                    try
                    {
                        res = res.Replace(fileContentCmnd, string.Empty);
                        WriteToFile(filesFolderLocation + fileName, res);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Some wired error with the file happened");
                    }
                    filesList.Items.Add(fileName); //adding the file to the filesList
                    fileName = string.Empty; // clearing the file path and name
                }
                else if (res != string.Empty && res.Contains(ONLY_MESSAGE)) // in case we recived a message command 
                {
                    res = res.Replace(ONLY_MESSAGE, string.Empty);
                    listMessage.Items.Add(res); // adding the message to the listMessages
                }
            }
        }
        /// <summary>
        /// recives data from the server 
        /// </summary>
        /// <returns></returns>
        async Task<string> ReceiveResponse()
        {
            byte[] buffer = new byte[50000]; 
            int res = await ReciveFromServer(buffer);
            if (res == 0) return string.Empty;
            var data = new byte[res];
            Array.Copy(buffer, data, res);
            string text = Encoding.ASCII.GetString(data);
            return text;
        }


         private async Task<int> ReciveFromServer(byte[] buffer) 
         {
            return  ClientSocket.Receive(buffer, SocketFlags.None);
         }


        /// <summary>
        /// creates a file with the inserted path
        /// </summary>
        /// <param name="path"></param>
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
        /// <summary>
        /// writing the content to file with the inserted path
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
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

        /// <summary>
        /// Creating the client socket connetion with the server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LunchBtn_Click(object sender, EventArgs e)
        {
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string name = txbName.Text;
   
            if (name.CompareTo(string.Empty)==0) 
            {
                txbName.Focus();
                errorProvider1.SetError(txbName, "לא הוכנס שם");
            } 
            else
            {
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
                txtbxChat.Focus();
                errorProvider2.SetError(txtbxChat, "לא הוכנס תוכן להודעה");
            }
            else
            {
                // in case the user try to send files commandes to the server , 
                //we will delete 'FILE_NAME:' or 'FILE_CONTENT:' command , and send the rest of the string.
                if (message.Contains(fileNameCmnd))
                message = message.Replace(fileNameCmnd, string.Empty);
                else if (message.Contains(fileContentCmnd))
                message = message.Replace(fileContentCmnd, string.Empty);
                RequestLoop(ONLY_MESSAGE+message);
            }
            txtbxChat.Text = string.Empty; // clean the text in message box
        }
        //private void Button1_Click(object sender, EventArgs e)
        //{
        //}
        private void Button1_Click_1(object sender, EventArgs e)
        {
            DisconnectFromChat();
        }

        /// <summary>
        /// Disconnecting the client and closing the chat app.
        /// </summary>
        private void DisconnectFromChat()
        {
            if (ClientSocket.Connected) // if the client didn't connect to the server and try to exit it. only connected client can disconnect. 
                Exit();
            else
                MessageBox.Show("You are not connected to the server. \n" +
                    "you have to be connected in order to logout from the chat.", "Logout Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                Dispose();
        }
        /// <summary>
        /// switching from files to messages vise versa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPort.SelectedIndex == 1)
            {
                EnableFiles();
            }
            else
            {
                EnableMessages();
            }

        }
        /// <summary>
        /// Enable files sending control
        /// </summary>
        private void EnableFiles()
        {
            txtbxChat.Enabled = false;
            listMessage.Enabled = false;
            filesList.Enabled = true;
            btnSelectFile.Enabled = true;
            btnSendingMsg.Enabled = false;
        }
        /// <summary>
        /// Enable messages sending control
        /// </summary>
        private void EnableMessages()
        {
            btnSendingMsg.Enabled = true;
            txtbxChat.Enabled = true;
            listMessage.Enabled = true;
            filesList.Enabled = false;
            btnSelectFile.Enabled = false;
        }

        /// <summary>
        /// Uploading the file to the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JSON files (*.json)|*.json|XML files (*.xml)|*.xml"; // file types, that will be allowed to upload
            dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                try
                {        
                    string content;
                    string path = dialog.FileName.ToLower(); 
                    SendString(fileNameCmnd + path); // name of the file
                    await Task.Delay(200);
                    //sending the content of the file
                    if (path.Contains(".json")) // in case the file in format json
                    {
                         content = ReadJSONfile(path);
                        SendString(fileContentCmnd+content); //content of the file 
                    }
                    else if (path.Contains(".xml")) // in case the file in format xml
                    {
                        content = File.ReadAllText(path);
                        SendString(fileContentCmnd+content);
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("You are already sent this file which exist in your 'files' folder.  \n" +
                        "Please send a different file or make sure you deleted the existing one.", "File Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
        }


        /// <summary>
        /// Allow us to select a file and save it on the computer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fileName = filesList.SelectedItem.ToString().ToLower();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json|XML files (*.xml)|*.xml"; // file types, that will be allowed to save
            saveFileDialog.FileName = fileName;
            if (saveFileDialog.ShowDialog()==DialogResult.OK)
            {
                try
                {
                    using (Stream s = File.Open(saveFileDialog.FileName, FileMode.CreateNew))
                    using (StreamWriter sw = new StreamWriter(s))
                    {
                        if (fileName.Contains(".json"))
                        {
                            sw.Write(ReadJSONfile(filesFolderLocation + fileName));
                        }
                        else if (fileName.Contains(".xml"))
                        {
                            sw.Write(File.ReadAllText(filesFolderLocation + fileName));
                        }
                    }
                }
                catch(IOException ex)
                {
                    MessageBox.Show("Something went wrong with saving the file. \n" +
                        "if you tried to override an existing file please change the file name and then save it.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Reads the content of the json file and returns the content as a string.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string ReadJSONfile(string path)
        {
            return JObject.Parse(File.ReadAllText(path)).ToString();
        }

        /// <summary>
        /// connecting to the chat system GUI with the inserted IP address.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click(object sender, EventArgs e)
        {
            if (txb_ip.Text.Equals(string.Empty) && cmb_Login_option.Text.Equals(string.Empty))
            {
                errorProvider3.SetError(txb_ip, "IP addrress not inserted");
                errorProvider4.SetError(cmb_Login_option, "Option not selected");
            }
            else if (cmb_Login_option.Text.Equals(string.Empty))
                errorProvider4.SetError(cmb_Login_option, "Option not selected");
            else if(txb_ip.Text.Equals(string.Empty))
                errorProvider3.SetError(txb_ip, "IP addrress not inserted");
            else
            {
                server_ip = txb_ip.Text;
                cmb_Login_option.Enabled = false;
                btnSystemConnect.Enabled = false;
                chat_panel.Visible = true;
                txbName.Enabled = true;
                txbName.Text = string.Empty;
                connectBtn.Enabled = true;
                txb_ip.Enabled = false;
            }
        }

        /// <summary>
        /// expelnation about the chat system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("הסבר - מערכת הצ'אט מתחברת לכתובת אינטרנט של השרת , אם הצ'אט מחובר למחשב עם כתובת הזהה לשרת, \n " +
                "נצטרך לבחור באופצייה - 'הרצת הצ'אט על המחשב עם כתובת הזהה לשרת' , במידה והכתובת שונה \n " +
                "נבחר באופצייה - 'הרצת הצ'אט במחשב עם כתובת השונה מכתובת השרת'. \n" +
                " הערה: ייתכן ותהיה בעיית התחברות לצ'אט אם כתובת האינטרנט של המחשב שדרכו \n" +
                "אנחנו מתחברים לצ'אט שונה משל השרת , במידה וזה קורה צריך לבטל את ה 'firewall.'",
                "הסבר על התחברות למערכת הצ'אט", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// let us choose in which method we want to enter to the server.
        /// if the server is running on other host or it's running on the same host.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                txb_ip.Enabled = true;
            }
        }

        /// <summary>
        /// Hiding ip in the GUI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
