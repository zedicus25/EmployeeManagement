using Server.Controller;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Server.Model
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
            tcpClient.Close();
            NetworkStream.Close();
        }


        public void ReceiveMessages()
        {
            try
            {
                while (_tokenSource.Token.IsCancellationRequested == false)
                {
                    try
                    {
                        byte[] data = new byte[256];
                        StringBuilder sb = new StringBuilder();
                        int byteCount = 0;
                        do
                        {
                            byteCount = NetworkStream.Read(data, 0, data.Length);
                            sb.Append(Encoding.Unicode.GetString(data, 0, byteCount));
                        } while (NetworkStream.DataAvailable);

                        if (sb.Length > 0)
                        {
                            string msg = sb.ToString();
                            if (msg.Contains("id=") && msg.Contains("login=") && msg.Contains("password="))
                                _serverController.CheckUserLoginPasswordData(msg);
                            else if(msg.Contains("--getAllTasks") && msg.Contains("id=") && msg.Contains("projectId="))
                            {
                                string[] strs = msg.Split('\n');
                                _serverController.SendTasks(strs[1].Substring(strs[1].IndexOf('=')+1), 
                                    Convert.ToInt32(strs[2].Substring(strs[2].IndexOf('=')+1)));
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

        ~ServerClient()
        {
            Close();
        }
    }
}
