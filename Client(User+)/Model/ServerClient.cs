﻿using Client_User__.Utilities;
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

        public event Action<IEnumerable<TaskImportant>> GetTaskImportants;
        public event Action<IEnumerable<TaskCondition>> GetTaskConditions;
        public event Action<IEnumerable<Employee>> GetEmployees;
        public event Action<IEnumerable<UserProject>> GetProjects;
        public event Action<IEnumerable<UserTask>> GetAllTasks;
        public event Action<UserTask> GetTaskById;

        public string IdOnServer { get; private set; }

        private TcpClient _tcpClient;
        private NetworkStream _tcpStream;

        private readonly int PORT = 8008;
        private readonly string HOST = "127.0.0.1";
        private StringBuilder _stringBuilder;



        private CancellationTokenSource _tokenSource;
        private Task _receiveTask;

        public ServerClient()
        {
            _tokenSource = new CancellationTokenSource();
            _tcpClient = new TcpClient();
            _stringBuilder = new StringBuilder();
            TryConnect();
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
                _tcpStream?.Write(data, 0, data.Length);
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
                _receiveTask = new Task(ReceiveMessages, _tokenSource.Token);
                _receiveTask.Start();
                
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
                while (_tokenSource.Token.IsCancellationRequested == false)
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
                        else if (msg.Contains("taskByIdAdmin="))
                        {
                            GetTaskById?.Invoke(Parser.GetInstance().GetUserTask(msg.Substring(msg.IndexOf('=') + 1)));
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
            _tokenSource.Cancel();
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