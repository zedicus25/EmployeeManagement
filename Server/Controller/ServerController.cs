using Newtonsoft.Json;
using Server.Model;
using Server.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Controller
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
        public UserController UserController { get; private set; }
        public ProjectTaskController ProjectTaskController { get; private set; }

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
            UserController = new UserController();
            ProjectTaskController = new ProjectTaskController();
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
            if(client != null)
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
            bool logResult = UserController.CheckData(data, out User logUser);
            string res = $"loginigResult={logResult}\n";
            if (logResult)
                res += logUser.ToString();
            string[] datas = data.Split('\n');
            string id = datas[0].Substring(datas[0].IndexOf('=')+1);
            SendMessageToClient(id, res);
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
            IEnumerable<ProjectTask> tasks = from task in ProjectTaskController.Tasks
                                  where
                                  ProjectTaskController.Tasks.All(t => t.ProjectId.Equals(projectId))
                                  select task;

            string json = "allTasks=";
            json += JsonConvert.SerializeObject(tasks);
            SendMessageToClient(client, json);
        }
    }
}
