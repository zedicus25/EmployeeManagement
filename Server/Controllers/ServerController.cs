using Newtonsoft.Json;
using Server.Models;
using Server.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Controllers
{
    internal class ServerController
    {
        public event Action<string> StateUpdating;
        private TcpListener _tcpListener;
        private List<ServerClient> _clients;
        private readonly int PORT;
        private CancellationTokenSource _tokenSource;
        private Task _listenTask;
        private bool _isEnabled;
        private EmployeeManagement _dbContext;
        private UserTaskController _userTaskController;

        public ServerController(int port = 8008)
        {
            _isEnabled = false;

            _clients = new List<ServerClient>();
            PORT = port;
        }

        public void StartServer()
        {
            if (_isEnabled)
                return;
            _isEnabled = true;
            _tokenSource = new CancellationTokenSource();
            _listenTask = new System.Threading.Tasks.Task(Listen, _tokenSource.Token);
            _listenTask.Start();
            _dbContext = new EmployeeManagement();
            _userTaskController = new UserTaskController(_dbContext);
        }

        private void Listen()
        {
            try
            {
                TryStartServer();
                while (_tokenSource.Token.IsCancellationRequested == false)
                {
                    CathcClients();
                }
            }
            catch (Exception ex)
            {
                StateUpdating?.Invoke($"Server error {ex.Message}");
                StopServer();
            }
        }


        private void CathcClients()
        {
            TcpClient tcpClient = _tcpListener.AcceptTcpClient();
            ServerClient serverClient = new ServerClient(tcpClient, this);
            AddConnection(serverClient);
            SendMessageToClient(serverClient.Id, $"ClientId={serverClient.Id}");
            StateUpdating?.Invoke($"Client {serverClient.Id} connected");
        }

        private void TryStartServer()
        {
            _tcpListener = new TcpListener(IPAddress.Any, PORT);
            _tcpListener.Start();
            StateUpdating?.Invoke("Server is start!");
        }

        public void AddConnection(ServerClient client) => _clients.Add(client);
        public void RemoveConnection(string id)
        {
            ServerClient client = _clients.FirstOrDefault(c => c.Id.Equals(id));
            
            if (client != null)
            {
                client.Close();
                _clients.Remove(client);
            }
        }

        public void StopServer()
        {
            if (_isEnabled == false)
                return;

            _tcpListener.Stop();
            for (int i = 0; i < _clients.Count; i++)
            {
                _clients[i].Close();
            }
            StateUpdating?.Invoke("Server is stop!");
            _clients.Clear();
            _isEnabled = false;
        }

        public void SetMessagesFromClient(string msg) => StateUpdating?.Invoke(msg);


        public void CheckUserLoginPasswordData(string data)
        {
            data = data.Trim();
            string[] datas = data.Split('\n');
            if (datas.Length != 3)
                throw new ArgumentException("Incorrect data");

            for (int i = 0; i < datas.Length; i++)
            {
                datas[i] = datas[i].Trim();
            }

            if (datas[1].ToLower().Contains("login=") && datas[2].ToLower().Contains("password="))
            {
                string login = datas[1].Substring(datas[1].IndexOf('=') + 1).Trim();
                string password = datas[2].Substring(datas[2].IndexOf('=') + 1).Trim();

                LoginData loginData = _dbContext.LoginDatas
                    .FirstOrDefault(x => x.Password.Equals(password) && x.Login.Equals(login));

                if (loginData == null)
                {
                    SendMessageToClient(datas[0].Substring(datas[0].IndexOf('=') + 1), "loginigResult=false\n");
                    return;
                }

                User userData = new User();

                Employee employee = _dbContext.Employees.FirstOrDefault(x => x.LoginDataId == loginData.Id);
                Person person = _dbContext.Persons.FirstOrDefault(x => x.Id == employee.PersonId);
                IEnumerable<Email> emails = _dbContext.Emails.Where(x => x.PersonId == person.Id);
                IEnumerable<Phone_Numbers> phoneNumbers = _dbContext.Phone_Numbers.Where(x => x.PersonId == person.Id);
                FIO fio = _dbContext.FIOs.FirstOrDefault(x => x.Id == person.FIO_Id);
                Adress adress = _dbContext.Adresses.FirstOrDefault(x => x.Id == person.Id);
                EmployeesRole employeeRole = _dbContext.EmployeesRoles.FirstOrDefault(x => x.Id == employee.RoleId);
                UsersRole userRole = _dbContext.UsersRoles.FirstOrDefault(x => x.Id == employeeRole.UserRoleId);
                Description employeeDesc = _dbContext.Descriptions.FirstOrDefault(x => x.Id == employeeRole.DescriptionId);
                if(employee.ProjectTask != null)
                {
                    ProjectTask task = _dbContext.ProjectTasks.FirstOrDefault(x => x.Id == employee.TaskId);
                    Description taskDesc = _dbContext.Descriptions.FirstOrDefault(x => x.Id == task.DescriptionId);
                    TaskCondition taskCondition = _dbContext.TaskConditions.FirstOrDefault(x => x.Id == task.TaskConditionId);
                    Description conditionDesc = _dbContext.Descriptions.FirstOrDefault(x => x.Id == taskCondition.Description_Id);
                    Importance taskImportance = _dbContext.Importances.FirstOrDefault(x => x.Id == task.ImportanceId);
                    Description importanceDesc = _dbContext.Descriptions.FirstOrDefault(x => x.Id == taskImportance.DescriptionId);
                    Term taskTerm = _dbContext.Terms.FirstOrDefault(x => x.Id == task.TermId);
                    Project project = _dbContext.Projects.FirstOrDefault(x => x.Id == task.ProjectId);
                    Description projectDesc = _dbContext.Descriptions.FirstOrDefault(x => x.Id == project.Description_Id);

                    userData.FillUserTask(task.Id, taskDesc.Title, taskDesc.Description1, taskCondition.Id, conditionDesc.Title,
                    taskImportance.Id, importanceDesc.Title, taskTerm.CreationDate, taskTerm.ToComplete);

                    userData.FillUserProject(project.Id, projectDesc.Title, projectDesc.Description1);
                }
                
                userData.FillFio(employee.Id, fio.First_Name, fio.Last_Name, fio.Patronymic, person.Birthday);
                userData.FillAddress(adress.Country, adress.City, adress.Street, adress.House_Number, adress.Full_Adress);
                userData.FillPhoneNumbers(phoneNumbers);
                userData.FillEmails(emails);
                userData.FillRoleInfo(userRole.Id, userRole.Name, employeeDesc.Title, employeeDesc.Description1);

                userData.FillLoginData(loginData.Login, loginData.Password);

                userData.FillEmployeeDate((float)employee.Salary, employee.Avatar);

                string user = userData.ToJson();
                string res = $"loginigResult={true}\n{user}";
                SendMessageToClient(datas[0].Substring(datas[0].IndexOf('=') + 1), res);

            }
        }

        public void SendMessageToClient(string id, string msg)
        {
            ServerClient client = _clients.FirstOrDefault(c => c.Id.Equals(id));
            if (client == null)
                return;

            byte[] bytes = Encoding.Unicode.GetBytes(msg);
            client.NetworkStream.Write(bytes, 0, bytes.Length);
        }

        public void SendTasks(string client, int projectId)
        {
            IEnumerable<UserTask> tasks = _userTaskController.GetUserTasks(projectId);
            StringBuilder sb = new StringBuilder();
            sb.Append("allTasks=");
            sb.Append(JsonConvert.SerializeObject(tasks));
            SendMessageToClient(client, sb.ToString());
        }

        public void GiveTaskToUser(int userId, int taskId) => 
            _userTaskController.SetTaskCondition(userId, taskId, 3);


        public void SendTask(string id, int userId)
        {
            UserTask task = _userTaskController.GetUserTask(userId);
            StringBuilder sb = new StringBuilder();
            sb.Append("myTask=");
            sb.Append(JsonConvert.SerializeObject(task));
            SendMessageToClient(id, sb.ToString());
        }

        public void SubmitTask(int userId, int taskId) =>
            _userTaskController.SetTaskCondition(userId, taskId, 1);

    }
}

