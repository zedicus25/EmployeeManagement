using Server.Model;
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
        private TcpListener tcpListener;
        private List<ServerClient> clients;
        private readonly int PORT;
        private CancellationTokenSource _tokenSource;
        private Task _listenTask;
        private SynchronizationContext _synchronizationContext;

        public ServerController(int port = 8008)
        {
            _tokenSource = new CancellationTokenSource();
            _synchronizationContext = SynchronizationContext.Current;
            clients = new List<ServerClient>();
            PORT = port;
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
                CloseServer();
            }
        }


        private void CathcClients()
        {
            TcpClient tcpClient = tcpListener.AcceptTcpClient();
            ServerClient serverClient = new ServerClient(tcpClient, this);
            AddConnection(serverClient);
            StateUpdating?.Invoke($"Client {serverClient.Id} connected");
        }

        private void TryStartServer()
        {
            tcpListener = new TcpListener(IPAddress.Any, PORT);
            tcpListener.Start();
            StateUpdating?.Invoke("Server is start!");
        }

        public void AddConnection(ServerClient client) => clients.Add(client);
        public void RemoveConnection(string id)
        {
            ServerClient client = clients.FirstOrDefault(c => c.Id.Equals(id));
            if(client != null)
                clients.Remove(client);
        }

        private void CloseServer()
        {
            tcpListener.Stop();
            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Close();
            }
            StateUpdating?.Invoke("Server is stop!");
        }

        public void SetMessagesFromClient(string msg)
        {
            StateUpdating?.Invoke(msg);
            clients.Last().NetworkStream.Write(Encoding.Unicode.GetBytes("message to client"), 0, 17);
        }
    }
}
