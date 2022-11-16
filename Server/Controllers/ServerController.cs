using System.Net.Sockets;
using System.Net;
using System.Text;
using Task = System.Threading.Tasks.Task;
using Server.ServerModels;
using Server.Models;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

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
        private DbA8ec2dZedicus52001Context _dbContext;

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
            _listenTask = new Task(Listen, _tokenSource.Token);
            _listenTask.Start();
            _dbContext = new DbA8ec2dZedicus52001Context();
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
                _clients.Remove(client);
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

        public void SetMessagesFromClient(string msg)
        {
            StateUpdating?.Invoke(msg);
        }

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

                LoginDatum loginData = _dbContext.LoginData
                    .FirstOrDefault(x => x.Password.Equals(password) && x.Login.Equals(login));

                if (loginData == null)
                {
                    SendMessageToClient(datas[0].Substring(datas[0].IndexOf('=') + 1), "loginigResult=false\n");
                    return;
                }
                Employee employee = _dbContext.Employees.FirstOrDefault(x => x.LoginDataId.Equals(loginData.Id));
                Person person = _dbContext.Persons.FirstOrDefault(x => x.Id.Equals(employee.PersonId));
                Fio fio = _dbContext.Fios.FirstOrDefault(x => x.Id.Equals(person.FioId));
                Adress adress = _dbContext.Adresses.FirstOrDefault(x => x.Id.Equals(person.Id));
                EmployeesRole employeeRole = _dbContext.EmployeesRoles.FirstOrDefault(x => x.Id.Equals(employee.RoleId));
                UsersRole userRole = _dbContext.UsersRoles.FirstOrDefault(x => x.Id.Equals(employeeRole.UserRoleId));
                Description employeeDesc = _dbContext.Descriptions.FirstOrDefault(x => x.Id.Equals(employeeRole.DescriptionId));
                Server.Models.Task task = _dbContext.Tasks.FirstOrDefault(x => x.Id.Equals(employee.TaskId));
                Description taskDesc = _dbContext.Descriptions.FirstOrDefault(x => x.Id.Equals(task.DescriptionId));
                TaskCondition taskCondition = _dbContext.TaskConditions.FirstOrDefault(x => x.Id.Equals(task.TaskConditionId));
                Description conditionDesc = _dbContext.Descriptions.FirstOrDefault(x => x.Id.Equals(taskCondition.DescriptionId));
                Importance taskImportance = _dbContext.Importances.FirstOrDefault(x => x.Id.Equals(task.ImportanceId));
                Description importanceDesc = _dbContext.Descriptions.FirstOrDefault(x => x.Id.Equals(taskImportance.DescriptionId));
                Term taskTerm = _dbContext.Terms.FirstOrDefault(x => x.Id.Equals(task.TermId));
                Project project = _dbContext.Projects.FirstOrDefault(x => x.Id.Equals(task.ProjectId));
                Description projectDesc = _dbContext.Descriptions.FirstOrDefault(x => x.Id.Equals(project.DescriptionId));

                User userData = new User();
                userData.FillFio(fio.FirstName, fio.LastName, fio.Patronymic, person.Birthday);
                userData.FillAddress(adress.Country, adress.City, adress.Street, adress.HouseNumber, adress.FullAdress);
                userData.FillPhoneNumbers(person.PhoneNumbers);
                userData.FillEmails(person.Emails);
                userData.FillRoleInfo(userRole.Id, userRole.Name, employeeDesc.Title, employeeDesc.Description1);

                userData.FillUserTask(task.Id, taskDesc.Title, taskDesc.Description1, taskCondition.Id, conditionDesc.Title,
                    taskImportance.Id, importanceDesc.Title, taskTerm.CreationDate, taskTerm.ToComplete);

                userData.FillUserProject(project.Id, projectDesc.Title, projectDesc.Description1, project.Images);

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
            /*IEnumerable<ProjectTask> tasks = from task in ProjectTaskController.Tasks
                                             where
                                             ProjectTaskController.Tasks.All(t => t.ProjectId.Equals(projectId))
                                             select task;

            string json = "allTasks=";
            json += JsonConvert.SerializeObject(tasks);
            SendMessageToClient(client, json);*/
        }
    }
}
