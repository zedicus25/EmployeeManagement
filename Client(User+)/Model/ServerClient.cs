using Client_User__.Utilities;
using Client_User__.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client_User__.Model
{
    public class ServerClient
    {
        public event Action<string> StateUpdating;
        public event Action<string> GetServerMessage;
        public event Action<bool, User> LoginingResult;

        public event Action<List<TaskImportant>> GetTaskImportants;
        public event Action<List<TaskCondition>> GetTaskConditions;
        public event Action<List<Employee>> GetEmployees;
        public event Action<List<UserProject>> GetProjects;
        public event Action<List<UserTask>> GetAllTasks;

        public string IdOnServer { get; private set; }
        public bool CanSendMessagesToServer { get; private set; }

        private TcpClient _tcpClient;
        public bool IsConnected => _tcpClient.Connected;
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
            _tryConnectCTS = new CancellationTokenSource();  
            _stringBuilder = new StringBuilder();
            _tryConnectTask = new Task(TryConnect, _tryConnectCTS.Token);
            _tryConnectTask.Start();
        }

        public void SendQuerryForImportance()
        {
            _stringBuilder.Append("--getImportance\n");
            _stringBuilder.Append($"id={IdOnServer}\n");
            SendMessageToServer(_stringBuilder.ToString());
            _stringBuilder.Clear();
        }
        public void SendQuerryForConditions()
        {
            _stringBuilder.Append("--getConditions\n");
            _stringBuilder.Append($"id={IdOnServer}\n");
            SendMessageToServer(_stringBuilder.ToString());
            _stringBuilder.Clear();
        }
        public void SendQuerryForEmployees()
        {
            _stringBuilder.Append("--getEmployees\n");
            _stringBuilder.Append($"id={IdOnServer}\n");
            SendMessageToServer(_stringBuilder.ToString());
            _stringBuilder.Clear();
        }
        public void SendQuerryForProjects()
        {
            _stringBuilder.Append("--getProjects\n");
            _stringBuilder.Append($"id={IdOnServer}\n");
            SendMessageToServer(_stringBuilder.ToString());
            _stringBuilder.Clear();
        }

        public void SendQuerryForAllTasks()
        {
            _stringBuilder.Append("--getAllTasksAdmin\n");
            _stringBuilder.Append($"id={IdOnServer}\n");
            SendMessageToServer(_stringBuilder.ToString());
            _stringBuilder.Clear();
        }

        public void DeleteTask(int taskId)
        {
            _stringBuilder.Append("--removeTask\n");
            _stringBuilder.Append($"id={IdOnServer}\n");
            _stringBuilder.Append($"taskId={taskId}\n");
            SendMessageToServer(_stringBuilder.ToString());
            _stringBuilder.Clear();
        }
        public void UpdateTask(int taskId, UserTask task)
        {
            _stringBuilder.Append("--updateTask\n");
            _stringBuilder.Append($"oldTaskId={taskId}\n");
            _stringBuilder.Append($"newTask={JsonConvert.SerializeObject(task)}\n");
            SendMessageToServer(_stringBuilder.ToString());
            _stringBuilder.Clear();
        }

        public void QuerrySetTaskToEmployee(int taskId, int employeeId)
        {
            _stringBuilder.Append("--setTaskToEmployee\n");
            _stringBuilder.Append($"userId={employeeId}\n");
            _stringBuilder.Append($"taskId={taskId}\n");
            SendMessageToServer(_stringBuilder.ToString());
            _stringBuilder.Clear();
        }

        public void SendMessageToServer(string message)
        {
            if (message.ToLower().Contains("drop") || message.ToLower().Contains("delete") ||
                message.ToLower().Contains("alter") || message.ToLower().Contains("table"))
                return;
            try
            {
                byte[] data = Encoding.Unicode.GetBytes(message);
                _tcpStream?.WriteAsync(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                StateUpdating?.Invoke($"Connection error {ex.Message}");
            }
        }
        
        private void TryConnect()
        {
            try
            {
                _tcpClient.Connect(HOST, PORT);
                _tcpStream = _tcpClient.GetStream();
                StateUpdating?.Invoke("You connected to server!");
                CanSendMessagesToServer = true;
                _tryConnectCTS.Cancel();
                _tryConnectCTS.Dispose();

                _reciveCTS = new CancellationTokenSource();
                _receiveTask = new Task(ReceiveMessages, _reciveCTS.Token);
                _receiveTask.Start();
                
            }
            catch (SocketException ex)
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
                        byteCount = _tcpStream.ReadAsync(data, 0, data.Length).Result;
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
                        }
                        else if (msg.Contains("allImportances="))
                        {
                            GetTaskImportants?.Invoke(Parser.GetInstance().GetTaskImportances(msg.Substring(msg.IndexOf('=') + 1)));
                        }
                        else if (msg.Contains("allConditions="))
                        {
                            GetTaskConditions?.Invoke(Parser.GetInstance().GetTaskConditions(msg.Substring(msg.IndexOf('=') + 1)));
                        }
                        else if (msg.Contains("allEmployees="))
                        {
                            GetEmployees?.Invoke(Parser.GetInstance().GetEmployees(msg.Substring(msg.IndexOf('=') + 1)));
                        }
                        else if (msg.Contains("allProjects="))
                        {
                            GetProjects?.Invoke(Parser.GetInstance().GetProjects(msg.Substring(msg.IndexOf('=') + 1)));
                        }
                        else if (msg.Contains("allTasksAdmin="))
                        {
                            GetAllTasks?.Invoke(Parser.GetInstance().GetAllUserTasks(msg.Substring(msg.IndexOf('=') + 1)));
                        }
                        else if (msg.Contains("--disconnect"))
                        {
                            _reciveCTS.Cancel();
                            CanSendMessagesToServer = false;
                            MainVM.GetInstance().LogOut();
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
            _reciveCTS.Cancel();
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
