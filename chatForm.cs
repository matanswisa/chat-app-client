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
        private const string clientFolderPath = @"C:\Users\Matan\Desktop\ChatApp\ClientFiles";

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
            RequestLoop(this.username + "connected to the server");
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
            byte[] buffer = Encoding.ASCII.GetBytes(text.ToLower());
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }

        async void func()
        {
            string res;
            while (true)
            {
                res = string.Empty;
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
                if (res != string.Empty)
                    listMessage.Items.Add(res);
            }
        }
        async Task<string> ReceiveResponse()
        {
            var buffer = new byte[2048];
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
        /////////////////////

        public chatForm()
        {
            InitializeComponent();
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {

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
            cmbPort.SelectedIndex = 0;
            connectBtn.Enabled = true;
            btnLogout.Enabled = false;
            btnSendingMsg.Enabled = false;
            listMessage.Enabled = false;
            cmbPort.Enabled = false;
            txtbxChat.Enabled = false;
            txbName.Enabled = true;
            txbName.Text = "";
            Exit();
        }

        private void BtnSendingMsg_Click(object sender, EventArgs e)
        {
            /*  byte[] byData = System.Text.Encoding.ASCII.GetBytes(txtbxChat.Text);
              soc.Send(byData); */
            if (txtbxChat.Text.CompareTo(string.Empty) == 0) // במקרה ואין שם
            {
                txbName.Focus();
                errorProvider1.SetError(txbName, "לא הוכנס תוכן להודעה");
            } else
            {
             //   listMessage.Items.Add(txbName.Text + " >> " + txtbxChat.Text);
                RequestLoop(txtbxChat.Text);
            }

        }

  



        private void Button1_Click(object sender, EventArgs e)
        {
            // פעולה זאת תדאג להתנתקות מהשרת ויציאה מהצ'אט
         //   DisconnectFromChat();
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
            dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                try
                { 
                    string path = dialog.FileName;
                    ClientSocket.SendFile(path);
                }
                catch(Exception ex)
                {

                }
            }
        }
    }

     /*class Client
    {
        // סוקטים שאחראים לחיבור של השרת , מקבלים את כתובת האינטרנט של המחשב
        private   Socket ClientSocket = new Socket
         (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private const int PORT = 8080;
        private string username;
        private int port;
        private string lastMsgFromServer;
        public Client(string username , int port)
        {

            setUserName(username);
            setPort(port);
            // המתודות כאן יהיו אחראיות להתחברות לשרת
            ConnectToServer();
           /* RequestLoop();
            Exit();
        }

        public string getLastMsgFromServer()
        {
            return this.lastMsgFromServer;
        }

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

            //  Console.Clear();
            // Console.WriteLine("Connected");
        }

        public void RequestLoop(string text)
        {
            //   Console.WriteLine(@"<Type ""exit"" to properly disconnect client>");
                SendRequest(text);
                func();
        }

        /// <summary>
        /// Close socket and exit program.
        /// </summary>
        public void Exit()
        {
            SendString("exit"); // Tell the server we are exiting
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();
            ClientSocket = new Socket // תוספת שלי
         (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Environment.Exit(0);
        }

        private void SendRequest(string str)
        {
  
            string request = str;
            SendString(request);

            if (request.ToLower() == "exit")
            {
                Exit();
            }
        }

        /// <summary>
        /// Sends a string to the server with ASCII encoding.
        /// </summary>
        private void SendString(string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text.ToLower());
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }

        async void func()
        {
            string res=string.Empty;
            while (true)
            {
                await Task.Run(async () =>
                {
                    res = await ReceiveResponse();
                });
            }
        }
        async Task<string> ReceiveResponse()
        {
            var buffer = new byte[2048];
            int res = await solveTheProblem(buffer);
            if (res == 0) return string.Empty;
            var data = new byte[res];
            Array.Copy(buffer, data, res);
            string text = Encoding.ASCII.GetString(data);
            return text;
        }
         private async Task<int> solveTheProblem(byte[] buffer)
        {
            return  ClientSocket.Receive(buffer, SocketFlags.None);
        } */ 
    
}
