using EmployeeManagement.Utilities;
using EmployeeManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public class ServerClient
    {
        public event Action<string> StateUpdating;
        public event Action<string> GetServerMessage;
        public event Action<bool, User> LoginingResult;
        public event Action<List<UserTask>> AllTasks;
        public event Action<List<UserTask>> MyTask;

        public string IdOnServer { get; private set; }
        public bool IsConnected => _tcpClient.Connected;
        public bool CanSendMessagesToServer { get; private set; }

        private TcpClient _tcpClient;
        private NetworkStream _tcpStream;

        private readonly int PORT = 8008;
        private readonly string HOST = "127.0.0.1";
        private StringBuilder _stringBuilder;
        


        private CancellationTokenSource _reciveCTS;
        private CancellationTokenSource _tryConnectCTS;
        private Task _receiveTask;
        private Task _tryConnectTask;

        public ServerClient()
        {
            _tcpClient = new TcpClient();
            _stringBuilder = new StringBuilder();

            _tryConnectCTS = new CancellationTokenSource();


            _tryConnectTask = new Task(TryConnect, _tryConnectCTS.Token);
            _tryConnectTask.Start();
        }

        public void SendMessageToServer(string message)
        {
            try
            {
                byte[] data = Encoding.Unicode.GetBytes(message);
                _tcpStream?.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                StateUpdating?.Invoke($"Connection error {ex.Message}");
            }
        }
        public void GetAllTasks()
        {
            if(MainViewModel.GetInstance().User == null || MainViewModel.GetInstance().User.Project == null)
                return;
            _stringBuilder.Append("--getAllTasks\n");
            _stringBuilder.Append($"id={IdOnServer}\n");
            _stringBuilder.Append($"projectId={MainViewModel.GetInstance().User.Project.Id}\n");
            SendMessageToServer(_stringBuilder.ToString());
            _stringBuilder.Clear();
        }

        public void GetTaskFromAll(int taskId)
        {
            if (MainViewModel.GetInstance().User == null)
                return;
            if (_stringBuilder == null)
                _stringBuilder = new StringBuilder();
            _stringBuilder.Append("--setMyTask\n");
            _stringBuilder.Append($"id={IdOnServer}\n");
            _stringBuilder.Append($"userDataBaseId={MainViewModel.GetInstance().User.Id}\n");
            _stringBuilder.Append($"taskId={taskId}\n");
            SendMessageToServer(_stringBuilder.ToString());
            _stringBuilder.Clear();
        }

        public void GetMyTask()
        {
            if (MainViewModel.GetInstance().User == null)
                return;
            if (_stringBuilder == null)
                _stringBuilder = new StringBuilder();
            _stringBuilder.Append("--getMyTask\n");
            _stringBuilder.Append($"id={IdOnServer}\n");
            _stringBuilder.Append($"userDataBaseId={MainViewModel.GetInstance().User.Id}\n");
            SendMessageToServer(_stringBuilder.ToString());
            _stringBuilder.Clear();
        }

        public void SubmitTask(int taskId,string branchName, string message)
        {
            if (MainViewModel.GetInstance().User == null)
                return;
            if (_stringBuilder == null)
                _stringBuilder = new StringBuilder();
            _stringBuilder.Append("--submitTask\n");
            _stringBuilder.Append($"id={IdOnServer}\n");
            _stringBuilder.Append($"userDataBaseId={MainViewModel.GetInstance().User.Id}\n");
            _stringBuilder.Append($"taskId={taskId}\n");
            _stringBuilder.Append($"branchName={branchName}\n");
            _stringBuilder.Append($"message={message}\n");
            SendMessageToServer(_stringBuilder.ToString());
            _stringBuilder.Clear();
        }
        private void TryConnect()
        {
            if (_tcpClient != null && IsConnected)
                return;
            try
            {
                _tcpClient.Connect(HOST, PORT);
                _tcpStream = _tcpClient.GetStream();
                CanSendMessagesToServer = true;
                _tryConnectCTS.Cancel();
                _tryConnectCTS.Dispose();


                _reciveCTS = new CancellationTokenSource();
                _receiveTask = new Task(ReceiveMessages, _reciveCTS.Token);
                _receiveTask.Start();
                
            }
            catch(SocketException ex)
            {
                TryConnect();
            }
            catch (System.Exception ex)
            {
                StateUpdating?.Invoke($"Connection error {ex.Message}");
                Disconnect();
            }

        }

        private void ReceiveMessages()
        {
            try
            {
                while (_reciveCTS.Token.IsCancellationRequested == false)
                {
                    byte[] data = new byte[1024];
                    StringBuilder sb = new StringBuilder();
                    int byteCount = 0;
                    do
                    {
                        byteCount = _tcpStream.Read(data, 0, data.Length);
                        sb.Append(Encoding.Unicode.GetString(data, 0, byteCount));
                    } while (_tcpStream.DataAvailable);

                    if (sb.Length > 0)
                    {
                        string msg = sb.ToString();
                        if (msg.Contains("ClientId"))
                        {
                            IdOnServer = msg.Substring(msg.IndexOf('=')+1);
                            continue;
                        }
                        else if (msg.Contains("loginigResult="))
                        {
                            bool res = Convert.ToBoolean(msg.Substring(msg.IndexOf('=')+1, (msg.IndexOf('\n') - msg.IndexOf('=') )));
                            User user = null;
                            if (res)
                                user = Parser.GetInstance().GetUser(msg.Substring(msg.IndexOf('\n') + 1));
                            LoginingResult?.Invoke(res, user);
                            continue;
                        }
                        else if (msg.Contains("allTasks="))
                        {
                            AllTasks?.Invoke(Parser.GetInstance().GetTasks(msg.Substring(msg.IndexOf('=') + 1)));
                        }
                        else if (msg.Contains("myTask="))
                        {
                            MyTask?.Invoke(Parser.GetInstance().GetTasks(msg.Substring(msg.IndexOf('=') + 1)));
                        }
                        else if (msg.Contains("--disconnect"))
                        {
                            _reciveCTS.Cancel();
                            CanSendMessagesToServer = false;
                            MainViewModel.GetInstance().LogOut();
                            CanSendMessagesToServer = true;
                            break;
                        }
                        else
                        {
                            GetServerMessage?.Invoke(msg);
                        }
                        sb.Clear();
                    }
                        
                }
            }
            catch (Exception ex)
            {
                StateUpdating?.Invoke($"Connection error {ex.Message}");
                Disconnect();
            }
        }

        public void Disconnect()
        {
            _reciveCTS?.Cancel();
            _tcpStream?.Close();
            _tcpClient.Close();
            StateUpdating?.Invoke("You disconnected from server");
        }

        ~ServerClient()
        {
            Disconnect();
        }
    }
}
