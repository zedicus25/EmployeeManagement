using Newtonsoft.Json;
using Server.Controllers;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.ServerModels
{
    internal class ServerClient
    {
        public NetworkStream NetworkStream { get; private set; }
        public string Id { get; private set; }
        protected TcpClient tcpClient;
        private ServerController _serverController;
        private CancellationTokenSource _tokenSource;
        private Task _receiveTask;


        public ServerClient(TcpClient client, ServerController server)
        {
            _tokenSource = new CancellationTokenSource();
            Id = Guid.NewGuid().ToString();
            _serverController = server;
            tcpClient = client;
            NetworkStream = tcpClient.GetStream();
            _receiveTask = new Task(ReceiveMessages, _tokenSource.Token);
            _receiveTask.Start();
        }

        public void Close()
        {
            _serverController.SendMessageToClient(Id,"--disconnect");
            tcpClient.Close();
            NetworkStream.Close();
        }

        public async void ReceiveMessages()
        {
            try
            {
                while (_tokenSource.Token.IsCancellationRequested == false)
                {
                    try
                    {
                        byte[] data = new byte[4096];
                        StringBuilder sb = new StringBuilder();
                        int byteCount = 0;
                        do
                        {
                            byteCount = NetworkStream.ReadAsync(data, 0, data.Length).Result;
                            sb.Append(Encoding.Unicode.GetString(data, 0, byteCount));
                        } while (NetworkStream.DataAvailable);

                        if (sb.Length > 0)
                        {
                            string msg = sb.ToString();
                            if(msg.ToLower().Contains("drop") || msg.ToLower().Contains("delete") || 
                                msg.ToLower().Contains("table") || msg.ToLower().Contains("alter"))
                            {
                                sb.Clear();
                            }
                            if (msg.Contains("id=") && msg.Contains("login=") && msg.Contains("password="))
                            {
                                _serverController.CheckUserLoginPasswordData(msg);
                                await Task.Delay(300);
                                sb.Clear();
                            }
                            else if (msg.Contains("--getAllTasks") && msg.Contains("id=") && msg.Contains("projectId="))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.SendProjectTasks(strs[1].Substring(strs[1].IndexOf('=') + 1),
                                    Convert.ToInt32(strs[2].Substring(strs[2].IndexOf('=') + 1)));
                                await Task.Delay(300);
                                sb.Clear();
                            }
                            else if (msg.Contains("--setMyTask") && msg.Contains("id=") && msg.Contains("taskId="))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.GiveTaskToUser(Convert.ToInt32(strs[2].Substring(strs[2].IndexOf('=') + 1)),
                                    Convert.ToInt32(strs[3].Substring(strs[3].IndexOf('=') + 1)));
                                await Task.Delay(300);
                            }
                            else if(msg.Contains("--setTaskToEmployee") && msg.Contains("taskId=") && msg.Contains("userId="))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.GiveTaskToUser(Convert.ToInt32(strs[1].Substring(strs[1].IndexOf('=') + 1)),
                                    Convert.ToInt32(strs[2].Substring(strs[2].IndexOf('=') + 1)));
                                await Task.Delay(300);
                            }
                            else if (msg.Contains("--getMyTask") && msg.Contains("id=") && msg.Contains("userDataBaseId="))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.SendTask(strs[1].Substring(strs[1].IndexOf('=') + 1),
                                     Convert.ToInt32(strs[2].Substring(strs[2].IndexOf('=') + 1)));
                                await Task.Delay(300);
                                sb.Clear();
                            }
                            else if (msg.Contains("--getImportance") && msg.Contains("id="))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.SendImportances(strs[1].Substring(strs[1].IndexOf('=') + 1));
                                await Task.Delay(300);
                            }
                            else if (msg.Contains("--getConditions") && msg.Contains("id="))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.SendConditions(strs[1].Substring(strs[1].IndexOf('=') + 1));
                                await Task.Delay(300);
                            }
                            else if (msg.Contains("--getEmployeeRole") && msg.Contains("id="))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.SendEmployeeRoles(strs[1].Substring(strs[1].IndexOf('=') + 1));
                                await Task.Delay(300);
                            }
                            else if (msg.Contains("--getEmployeesAdmin"))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.SendAllEmployees(strs[1].Substring(strs[1].IndexOf('=') + 1));
                                await Task.Delay(300);
                            }
                            else if (msg.Contains("--getEmployees") && msg.Contains("id="))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.SendEmployees(strs[1].Substring(strs[1].IndexOf('=') + 1));
                                await Task.Delay(300);
                            }
                            else if (msg.Contains("--createEmployeeRole"))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.CreateEmployeeRole(
                                    JsonConvert.DeserializeObject<UserEmployeeRole>(strs[1].Substring(strs[1].IndexOf('=') + 1)));
                                await Task.Delay(300);
                            }
                            else if (msg.Contains("--removeEmployeeRole") && msg.Contains("id="))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.RemoveEmployeeRole(Convert.ToInt32(strs[1].Substring(strs[1].IndexOf('=') + 1)));
                                await Task.Delay(300);
                            }
                            else if (msg.Contains("--updateEmployeeRole") && msg.Contains("roleId=") && msg.Contains("employeeRole="))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.UpdateEmployeeRole(Convert.ToInt32(strs[1].Substring(strs[1].IndexOf('=') + 1)),
                                    JsonConvert.DeserializeObject<UserEmployeeRole>(strs[2].Substring(strs[2].IndexOf('=') + 1)));
                                await Task.Delay(300);
                            }
                            else if (msg.Contains("--setEmployeeRole") && msg.Contains("userRole=") && msg.Contains("employeeRole="))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.SetNewUserRoleForEmployeeRole(Convert.ToInt32(strs[1].Substring(strs[1].IndexOf('=') + 1)),
                                    Convert.ToInt32(strs[2].Substring(strs[2].IndexOf('=') + 1)));
                                await Task.Delay(300);
                            }
                            else if (msg.Contains("--addNewEmployee"))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.CreateEmployee(
                                    JsonConvert.DeserializeObject<UserEmployeeLong>(strs[1].Substring(strs[1].IndexOf('=') + 1)));
                                await Task.Delay(300);
                            }
                            else if (msg.Contains("--removeEmployee"))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.DeleteEmployee(Convert.ToInt32(strs[1].Substring(strs[1].IndexOf('=') + 1)));
                                await Task.Delay(300);
                            }
                            else if (msg.Contains("--updateEmployee") && msg.Contains("empId=") && msg.Contains("newEmp="))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.UpdateEmployee(Convert.ToInt32(strs[1].Substring(strs[1].IndexOf('=') + 1)),
                                    JsonConvert.DeserializeObject<UserEmployeeLong>(strs[2].Substring(strs[2].IndexOf('=') + 1)));
                                await Task.Delay(300);
                            }
                            else if (msg.Contains("--getProjects") && msg.Contains("id="))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.SendProjects(strs[1].Substring(strs[1].IndexOf('=') + 1));
                                await Task.Delay(300);
                            }
                            else if(msg.Contains("--addProject") && msg.Contains("proj="))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.AddProject(
                                    JsonConvert.DeserializeObject<UserProject>(strs[1].Substring(strs[1].IndexOf('=') + 1)));
                                await Task.Delay(300);
                            }
                            else if (msg.Contains("--removeProject") && msg.Contains("projId="))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.DeleteProject(Convert.ToInt32(strs[1].Substring(strs[1].IndexOf('=') + 1)));
                                await Task.Delay(300);
                            }
                            else if (msg.Contains("--updateProject") && msg.Contains("projId=") && msg.Contains("newProj="))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.UpdateProject(Convert.ToInt32(strs[1].Substring(strs[1].IndexOf('=') + 1)),
                                    JsonConvert.DeserializeObject<UserProject>(strs[2].Substring(strs[2].IndexOf('=') + 1)));
                                await Task.Delay(300);
                            }
                            else if (msg.Contains("--submitTask") && msg.Contains("id=") && msg.Contains("userDataBaseId="))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.SubmitTask(Convert.ToInt32(strs[2].Substring(strs[2].IndexOf('=') + 1)),
                                    Convert.ToInt32(strs[3].Substring(strs[3].IndexOf('=') + 1)));
                                await Task.Delay(300);
                            }
                            else if (msg.Contains("--addTaskCondition") && msg.Contains("condition="))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.AddTaskCondition(
                                    JsonConvert.DeserializeObject<UserTaskCondtion>(strs[1].Substring(strs[1].IndexOf('=') + 1)));
                                await Task.Delay(300);
                            }
                            else if (msg.Contains("--removeTaskCondition") && msg.Contains("conditionId="))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.DeleteTaskCondition(Convert.ToInt32(strs[1].Substring(strs[1].IndexOf('=') + 1)));
                                await Task.Delay(300);
                            }
                            else if (msg.Contains("--updateTaskCondition") && msg.Contains("oldConditionId=") && msg.Contains("newCondition="))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.UpdateTaskCondition(Convert.ToInt32(strs[1].Substring(strs[1].IndexOf('=') + 1)),
                                    JsonConvert.DeserializeObject<UserTaskCondtion>(strs[2].Substring(strs[2].IndexOf('=') + 1)));
                                await Task.Delay(300);
                            }
                            else if (msg.Contains("--addTaskImportance") && msg.Contains("importance="))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.AddTaskImportance(
                                   JsonConvert.DeserializeObject<TaskImportance>(strs[1].Substring(strs[1].IndexOf('=') + 1)));
                                await Task.Delay(300);
                            }
                            else if (msg.Contains("--removeTaskImportance") && msg.Contains("importanceId="))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.DeleteTaskImportance(Convert.ToInt32(strs[1].Substring(strs[1].IndexOf('=') + 1)));
                                await Task.Delay(300);
                            }
                            else if (msg.Contains("--updateTaskImportance") && msg.Contains("oldImportanceId=") && msg.Contains("newCondition="))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.UpdateTaskImportance(Convert.ToInt32(strs[1].Substring(strs[1].IndexOf('=') + 1)),
                                    JsonConvert.DeserializeObject<TaskImportance>(strs[2].Substring(strs[2].IndexOf('=') + 1)));
                                await Task.Delay(300);
                            }
                            else if (msg.Contains("--createTask")) 
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.CreateTask(
                                    JsonConvert.DeserializeObject<UserTask>(strs[1].Substring(strs[1].IndexOf('=') + 1)));
                                await Task.Delay(300);
                            }
                           
                            else if (msg.Contains("--removeTask"))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.DeleteTask(Convert.ToInt32(strs[2].Substring(strs[2].IndexOf('=') + 1)));
                                await Task.Delay(300);
                            }
                            else if (msg.Contains("--updateTask"))
                            {
                                string[] strs = msg.Split('\n');        
                                _serverController.UpdateTask(Convert.ToInt32(strs[1].Substring(strs[1].IndexOf('=') + 1)),
                                    JsonConvert.DeserializeObject<UserTask>(strs[2].Substring(strs[2].IndexOf('=') + 1)));
                                await Task.Delay(300);
                            }
                            else if (msg.Contains("--getAllTasksAdmin"))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.SendAllTasks(strs[1].Substring(strs[1].IndexOf('=') + 1));
                                await Task.Delay(300);
                            }
                            else if(msg.Contains("--getUserRoles") && msg.Contains("id="))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.SendAllUserRoles(strs[1].Substring(strs[1].IndexOf('=') + 1));
                                await Task.Delay(300);
                            }
                            else if(msg.Contains("--disconnect") && msg.Contains("id="))
                            {
                                string[] strs = msg.Split('\n');
                                if(strs.Length >= 3)
                                {
                                    if (strs[2] == "true")
                                        _serverController.RemoveConnection(strs[1].Substring(strs[1].IndexOf('=') + 1));
                                }
                                else
                                {
                                    _serverController.RemoveConnection(strs[1].Substring(strs[1].IndexOf('=') + 1));
                                }
                                
                            }
                            else
                                _serverController.SetMessagesFromClient(sb.ToString());
                            sb.Clear();
                        }

                    }
                    catch (Exception ex)
                    { 
                        _serverController.SetMessagesFromClient($"Disconected {Id}");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                _serverController.SetMessagesFromClient($"Connection error {ex.Message}");
            }
            finally
            {
                _serverController.RemoveConnection(Id);
                Close();
            }
        }
    }
}
