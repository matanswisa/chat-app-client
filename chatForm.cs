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
        // כל המשתנים שהכרחיים לצד לקוח
        private Socket ClientSocket;
        private const int PORT = 8080;
        private string username;
        private int port;
        private string filesFolderLocation;
        private const string fileNameCmnd = "FILE_NAME:"; // הפעולה שתאפשר לשלוח את הקובץ לשרת
        private const string fileContentCmnd = "FILE_CONTENT:";
        private string fileName; // כאן יהיה שם הקובץ
        private string fileContent; // כאן יהיה תוכן הקובץ

        /// מתודות ממחלקת לקוח ///
        private void setPort(int port)
        {
            this.port = port;
        }
        public int getPort()
        {
            return this.port;
        }
        private void setUserName(string name)
        {
            this.username = name;
        }
        public string getUserName()
        {
            return this.username;
        }
        private void ConnectToServer()
        {
            int attempts = 0;

            while (!ClientSocket.Connected)
            {
                try
                {
                    attempts++;
                    //      Console.WriteLine("Connection attempt " + attempts);
                    // Change IPAddress.Loopback to a remote IP to connect to a remote host.
                    ClientSocket.Connect(IPAddress.Loopback, PORT);
                }
                catch (SocketException)
                {
                    // Console.Clear();
                }
            }
            RequestLoop(this.username + " connected to the server");
            //  Console.Clear();
            // Console.WriteLine("Connected");
        }

        public void RequestLoop(string text)
        {
            //   Console.WriteLine(@"<Type ""exit"" to properly disconnect client>");
                SendRequest(text);
                func(); // אחראי על האזנה מהסרבר
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
            SendString(this.username + ">>" + str);
            if (str.ToLower() == "exit")
            {
                Exit();
            }
        }
        /// <summary>
        /// Sends a string to the server with ASCII encoding.
        /// </summary>
        private void SendString(string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }

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
            byte[] buffer = new byte[50000];
            int res = await solveTheProblem(buffer);
            if (res == 0) return string.Empty;
            var data = new byte[res];
            Array.Copy(buffer, data, res);
            string text = Encoding.ASCII.GetString(data);
            return text;
        }
         private async Task<int> solveTheProblem(byte[] buffer) // סנכרון פעולת האזנה , יש צורך לעבוד עליה עוד
        {
            return  ClientSocket.Receive(buffer, SocketFlags.None);
        }


        private void CreateFile(string path)
        {
            try
            {    // Create a new file   
                //path = path.Replace("\\", @"\");
                using (FileStream fs = File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }
            catch(Exception)//need to handle io exception
            {
                
            }
        }
        private void WriteToFile(string path , string content)
        {
            try
            {
                using (FileStream fs = File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(content);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }
            catch (Exception)
            {
            };
            fileName = string.Empty;
            fileContent = string.Empty;
        }
        /////////////////////

        public chatForm()
        {
            InitializeComponent();
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
           filesFolderLocation = @"C:\Users\Matan\Desktop\ChatApp\files\";
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
                setUserName(name);
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
            ConnectToServer();
        }
        private void DisconnectFromChat()
        {
          /*  cmbPort.SelectedIndex = 0;
            connectBtn.Enabled = true;
            btnLogout.Enabled = false;
            btnSendingMsg.Enabled = false;
            listMessage.Enabled = false;
            cmbPort.Enabled = false;
            txtbxChat.Enabled = false;
            txbName.Enabled = true;
            txbName.Text = "";
            Exit();*/
        }

        private void BtnSendingMsg_Click(object sender, EventArgs e)
        {
            /*  byte[] byData = System.Text.Encoding.ASCII.GetBytes(txtbxChat.Text);
              soc.Send(byData); */
            string message = txtbxChat.Text;
            if (message.CompareTo(string.Empty) == 0) // במקרה ואין שם
            {
                txbName.Focus();
                errorProvider1.SetError(txbName, "לא הוכנס תוכן להודעה");
            } else
            {
                // in case the user try to send files commandes to the server , 
                //we will delete 'FILE_NAME:' , 'FILE_CONTENT:' commandes , and send the rest of the string.
                if (message.Contains(fileNameCmnd))
                message = message.Replace(fileNameCmnd, string.Empty);
                else if (message.Contains(fileContentCmnd))
                message = message.Replace(fileContentCmnd, string.Empty);
                RequestLoop(message);
            }

        }

  



        private void Button1_Click(object sender, EventArgs e)
        {
            // פעולה זאת תדאג להתנתקות מהשרת ויציאה מהצ'אט
         //   DisconnectFromChat();
        }

        private void Button1_Click_1(object sender, EventArgs e)
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
            dialog.Filter = "Text Documents (*.txt)|*.txt|JSON files (*.json)|*.json|XML files (*.xml)|*.xml"; // file types, that will be allowed to upload
            dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                try
                { 
                    string path = dialog.FileName; 
                    SendString(fileNameCmnd + path); // name of the file
                    await Task.Delay(500);
                    ClientSocket.SendFile(path); //content of the file 
                }
                catch(Exception)
                {

                }
            }
        }

        private void FilesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("you pressed an item with index " + filesList.SelectedIndex 
                + " , which it's name , " + filesList.SelectedItem);
            Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                // Prevent updates to the remote version of the file until
                // we finish making changes and call CompleteUpdatesAsync.
                Windows.Storage.CachedFileManager.DeferUpdates(file);
                // write to file
                await Windows.Storage.FileIO.WriteTextAsync(file, file.Name);
                // Let Windows know that we're finished changing the file so
                // the other app can update the remote version of the file.
                // Completing updates may require Windows to ask for user input.
                Windows.Storage.Provider.FileUpdateStatus status =
                    await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
                if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
                {
                    this.textBlock.Text = "File " + file.Name + " was saved.";
                }
                else
                {
                    this.textBlock.Text = "File " + file.Name + " couldn't be saved.";
                }
            }
            else
            {
                this.textBlock.Text = "Operation cancelled.";
            }
        }
    }
}
