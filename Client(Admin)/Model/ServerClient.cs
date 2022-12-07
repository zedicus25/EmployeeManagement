using Client_Admin_.Utilities;
using Client_Admin_.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client_Admin_.Model
{
    public class ServerClient
    {
        public event Action<string> StateUpdating;
        public event Action<string> GetServerMessage;
        public event Action<bool, User> LoginingResult;

        public event Action<IEnumerable<EmployeeRole>> GetEmployeeRoles;
        public event Action<IEnumerable<Project>> GetProjects;
        public event Action<IEnumerable<Employee>> GetEmployees;
        public event Action<IEnumerable<Employee>> GetAllEmployees;
        public event Action<IEnumerable<UserRole>> GetUserRoles;

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

        public void SendQuerryForEmployeeRoles()
        {
            _stringBuilder.Append("--getEmployeeRole\n");
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

        public void SendQuerryForEmployees()
        {
            _stringBuilder.Append("--getEmployees\n");
            _stringBuilder.Append($"id={IdOnServer}\n");
            SendMessageToServer(_stringBuilder.ToString());
            _stringBuilder.Clear();
        }
        public void SendQuerryForAllEmployees()
        {
            _stringBuilder.Append("--getEmployeesAdmin\n");
            _stringBuilder.Append($"id={IdOnServer}\n");
            SendMessageToServer(_stringBuilder.ToString());
            _stringBuilder.Clear();
        }
        public void SendQuerryForUserRoles()
        {
            _stringBuilder.Append("--getUserRoles\n");
            _stringBuilder.Append($"id={IdOnServer}\n");
            SendMessageToServer(_stringBuilder.ToString());
            _stringBuilder.Clear();
        }

        public void SetUserRoleForEmployeeRole(int userRole, int employeeRole)
        {
            _stringBuilder.Append("--setEmployeeRole\n");
            _stringBuilder.Append($"userRole={userRole}\n");
            _stringBuilder.Append($"employeeRole={employeeRole}\n");
            SendMessageToServer(_stringBuilder.ToString());
            _stringBuilder.Clear();
        }

        public void UpdateEmployeeRole(int id, EmployeeRole role)
        {
            _stringBuilder.Append("--updateEmployeeRole\n");
            _stringBuilder.Append($"roleId={id}\n");
            _stringBuilder.Append($"employeeRole={JsonConvert.SerializeObject(role)}\n");
            SendMessageToServer(_stringBuilder.ToString());
            _stringBuilder.Clear();
        }
        public void AddProject(Project project)
        {
            _stringBuilder.Append("--addProject\n");
            _stringBuilder.Append($"proj={JsonConvert.SerializeObject(project)}\n");
            SendMessageToServer(_stringBuilder.ToString());
            _stringBuilder.Clear();
        }

        public void RemoveProject(int id)
        {
            _stringBuilder.Append("--removeProject\n");
            _stringBuilder.Append($"projId={id}\n");
            SendMessageToServer(_stringBuilder.ToString());
            _stringBuilder.Clear();
        }
        public void UpdateProject(int id, Project project)
        {
            _stringBuilder.Append("--updateProject\n");
            _stringBuilder.Append($"projId={id}\n");
            _stringBuilder.Append($"newProj={JsonConvert.SerializeObject(project)}\n");
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
                        }
                        else if (msg.Contains("loginigResult="))
                        {
                            bool res = Convert.ToBoolean(msg.Substring(msg.IndexOf('=')+1, (msg.IndexOf('\n') - msg.IndexOf('=') )));
                            User user = null;
                            if (res)
                                user = Parser.GetInstance().GetUser(msg.Substring(msg.IndexOf('\n') + 1));
                            LoginingResult?.Invoke(res, user);
                        }
                        else if(msg.Contains("employeeRoles="))
                        {
                            GetEmployeeRoles?.Invoke(Parser.GetInstance().GetEmployeeRoles(msg.Substring(msg.IndexOf('=') + 1)));
                        }
                        else if (msg.Contains("allEmployees="))
                        {
                            GetEmployees?.Invoke(Parser.GetInstance().GetEmployees(msg.Substring(msg.IndexOf('=') + 1)));
                        }
                        else if (msg.Contains("allEmployeesAdmin="))
                        {
                            GetAllEmployees?.Invoke(Parser.GetInstance().GetEmployees(msg.Substring(msg.IndexOf('=') + 1)));
                        }
                        else if (msg.Contains("allProjects="))
                        {
                            GetProjects?.Invoke(Parser.GetInstance().GetProjects(msg.Substring(msg.IndexOf('=') + 1)));
                        }
                        else if (msg.Contains("allUserRoles="))
                        {
                            GetUserRoles?.Invoke(Parser.GetInstance().GetUserRoles(msg.Substring(msg.IndexOf('=') + 1)));
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
    }
}
