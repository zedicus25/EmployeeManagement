using System.Net.Sockets;
using System.Net;
using System.Text;
using Task = System.Threading.Tasks.Task;
using Server.ServerModels;

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

                /*IEnumerable<Descriptions> descriptions = DataBaseController.GetInstance().GetDescriptions();

                LoginData loginData = DataBaseController.GetInstance().GetLoginData()
                    .FirstOrDefault(x => x.Password.Equals(password) && x.Login.Equals(login));

                if (loginData == null)
                    SendMessageToClient(datas[0].Substring(datas[0].IndexOf('=') + 1), "loginigResult=false\n");*/

                /*Employee employee = DataBaseController.GetInstance().GetEmployees()
                    .FirstOrDefault(x => x.LoginDataId.Equals(loginData.Id));

                #region Person creation
                Person person = DataBaseController.GetInstance().GetPersons()
                    .FirstOrDefault(x => x.Id.Equals(employee.PersonId));

                FIO fio = DataBaseController.GetInstance().GetFIOs()
                    .FirstOrDefault(x => x.Id.Equals(person.FIO_Id));

                Adress adress = DataBaseController.GetInstance().GetAdresses()
                    .FirstOrDefault(x => x.Id.Equals(person.Adress_Id));

                IEnumerable<PhoneNumber> numbers = DataBaseController.GetInstance().GetPhoneNumbers()
                    .Where(x => x.PersonId.Equals(person.Id));

                IEnumerable<Emails> emails = DataBaseController.GetInstance().GetEmails()
                    .Where(x => x.PersonId.Equals(person.Id));

                EmployeRole employeRole = DataBaseController.GetInstance().GetEmployeRoles()
                    .FirstOrDefault(x => x.Id.Equals(employee.RoleId));

                Descriptions employeeRoleDesc = descriptions
                    .FirstOrDefault(x => x.Id.Equals(employeRole.DescriptionId));

                UsersRole userRole = DataBaseController.GetInstance().GetUsersRoles()
                    .FirstOrDefault(x => x.Id.Equals(employeRole.UserRoleId));
                #endregion

                #region Task creation
                ProjectTask task = DataBaseController.GetInstance().GetTasks()
                    .FirstOrDefault(x => x.Id.Equals(employee.TaskId));

                Descriptions taskDesc= descriptions
                    .FirstOrDefault(x => x.Id.Equals(task.DescriptionId));

                TaskCondition condition = DataBaseController.GetInstance().GetTaskConditions()
                    .FirstOrDefault(x => x.Id.Equals(task.TaskConditionId));

                Descriptions taskCondiotionDesc = descriptions
                    .FirstOrDefault(x => x.Id.Equals(condition.Description_Id));

                Importance importance = DataBaseController.GetInstance().GetImportances()
                    .FirstOrDefault(x => x.Id.Equals(task.ImportanceId));

                Descriptions taskImportanceDesc = descriptions
                    .FirstOrDefault(x => x.Id.Equals(importance.DescriptionId));

                Term term = DataBaseController.GetInstance().GetTerms()
                    .FirstOrDefault(x => x.Id.Equals(task.TermId));
                #endregion

                #region Project creation
                Project project = DataBaseController.GetInstance().GetProjects()
                    .FirstOrDefault(x => x.Id.Equals(task.ProjectId));

                Descriptions projectDesc = descriptions
                    .FirstOrDefault(x => x.Id.Equals(project.Description_Id));

                IEnumerable<Images> images = DataBaseController.GetInstance().GetImages()
                    .Where(x => x.ProjectId.Equals(project.Id));
                #endregion


                User userData = new User();
                userData.FillFio(fio.First_Name, fio.Last_Name, fio.Patronymic, person.Birthday);
                userData.FillAddress(adress.Country, adress.City, adress.Street, adress.House_Number, adress.Full_Adress);
                userData.FillPhoneNumbers(numbers);
                userData.FillEmails(emails);
                userData.FillRoleInfo(userRole.Id, userRole.Name, employeeRoleDesc.Title, employeeRoleDesc.Description);
                userData.FillUserTask(task.Id, taskDesc.Title, taskDesc.Description, condition.Id, taskCondiotionDesc.Title,
                    importance.Id, taskImportanceDesc.Title, term.CreationDate, term.ToComplete);
                userData.FillUserProject(project.Id, projectDesc.Title, projectDesc.Description, images);
                userData.FillLoginData(loginData.Login, loginData.Password);
                userData.FillEmployeeDate(employee.Salary, employee.Avatar);*/

            }


            /*bool logResult = UserController.CheckData(data, out User logUser);
            string res = $"loginigResult={logResult}\n";
            if (logResult)
                res += logUser.ToString();
            string[] datas = data.Split('\n');
            string id = datas[0].Substring(datas[0].IndexOf('=')+1);
            SendMessageToClient(id, res);*/
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
