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
        private EmployeeController _employeeController;
        private ProjectsController _projectController;

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
            _employeeController = new EmployeeController(_dbContext);
            _projectController = new ProjectsController(_dbContext);
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
                var employeeDesc = _dbContext.EmployeeRoleDescriptions.FirstOrDefault(x => x.Id == employeeRole.DescriptionId);
                Project project = _dbContext.Projects.FirstOrDefault(x => x.Id == employee.ProjectId);
                var projectDesc = _dbContext.ProjectDescriptions.FirstOrDefault(x => x.Id == project.DescriptionId);
                userData.FillUserProject(project.Id, projectDesc.Title, projectDesc.Description);

                
                userData.FillFio(employee.Id, fio.First_Name, fio.Last_Name, fio.Patronymic, person.Birthday);
                userData.FillAddress(adress.Country, adress.City, adress.Street, adress.House_Number, adress.Full_Adress);
                userData.FillPhoneNumbers(phoneNumbers);
                userData.FillEmails(emails);
                userData.FillRoleInfo(userRole.Id, userRole.Name, employeeDesc.Title, employeeDesc.Description);

                userData.FillLoginData(loginData.Login, loginData.Password);

                userData.FillEmployeeDate((float)employee.Salary);

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

        public void SendProjectTasks(string client, int projectId)
        {
            IEnumerable<UserTask> tasks = _userTaskController.GetAllProjectTasks(projectId);
            StringBuilder sb = new StringBuilder();
            sb.Append("allTasks=");
            sb.Append(JsonConvert.SerializeObject(tasks));
            SendMessageToClient(client, sb.ToString());
        }

        public void GiveTaskToUser(int userId, int taskId) => 
            _userTaskController.SetTaskCondition(userId, taskId, 3);


        public void SendTask(string id, int userId)
        {
            IEnumerable<UserTask> tasks = _userTaskController.GetUsersTask(userId);
            StringBuilder sb = new StringBuilder();
            sb.Append("myTask=");
            sb.Append(JsonConvert.SerializeObject(tasks));
            SendMessageToClient(id, sb.ToString());
        }

        public void SubmitTask(int userId, int taskId) =>
            _userTaskController.SetTaskCondition(userId, taskId, 1);

        public void SendImportances(string id)
        {
            IEnumerable<TaskImportance> importances = _userTaskController.GetImportances();
            StringBuilder sb = new StringBuilder();
            sb.Append("allImportances=");
            sb.Append(JsonConvert.SerializeObject(importances));
            SendMessageToClient(id, sb.ToString());
        }
        public void SendConditions(string id)
        {
            IEnumerable<UserTaskCondtion> conditions = _userTaskController.GetConditions();
            StringBuilder sb = new StringBuilder();
            sb.Append("allConditions=");
            sb.Append(JsonConvert.SerializeObject(conditions));
            SendMessageToClient(id, sb.ToString());
        }

        public void SendEmployees(string id)
        {
            IEnumerable<UserEmployee> employees = _employeeController.GetAllShortEmployeeData();
            StringBuilder sb = new StringBuilder();
            sb.Append("allEmployees=");
            sb.Append(JsonConvert.SerializeObject(employees));
            SendMessageToClient(id, sb.ToString());
        }
        public void SendProjects(string id)
        {
            IEnumerable<UserProject> projects = _projectController.GetAllProjects();
            StringBuilder sb = new StringBuilder();
            sb.Append("allProjects=");
            sb.Append(JsonConvert.SerializeObject(projects));
            SendMessageToClient(id, sb.ToString());
        }

        public void CreateTask(UserTask task) => _userTaskController.AddTask(task);

        public void DeleteTask(int taskId) => _userTaskController.DeleteTask(taskId);
        

        public void UpdateTask(int id, UserTask newTask) => _userTaskController.UpdateTask(id, newTask);

        public void SendAllTasks(string id)
        {
            IEnumerable<UserTask> tasks = _userTaskController.GetAllTasks();
            StringBuilder sb = new StringBuilder();
            sb.Append("allTasksAdmin=");
            sb.Append(JsonConvert.SerializeObject(tasks));
            SendMessageToClient(id, sb.ToString());
        }

    }
}

