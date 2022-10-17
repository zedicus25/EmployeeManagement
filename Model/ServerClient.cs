using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    internal class ServerClient
    {
        public event Action<string> StateUpdating;
        public event Action<string> GetServerMessage;

        private static TcpClient _tcpClient;
        private static NetworkStream _tcpStream;

        private static readonly int PORT = 8008;
        private static readonly string HOST = "127.0.0.1";

        private CancellationTokenSource _tokenSource;
        private Task _receiveTask;

        public ServerClient()
        {
            _tokenSource = new CancellationTokenSource();
            _tcpClient = new TcpClient();
            TryConnect();
        }

        public void SendMessageToServer(string message)
        {
            try
            {
                byte[] data = Encoding.Unicode.GetBytes(message);
                _tcpStream.Write(data, 0, data.Length);
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
                    byte[] data = new byte[256];
                    StringBuilder sb = new StringBuilder();
                    int byteCount = 0;
                    do
                    {
                        byteCount = _tcpStream.Read(data, 0, data.Length);
                        sb.Append(Encoding.Unicode.GetString(data, 0, byteCount));
                    } while (_tcpStream.DataAvailable);

                    if (sb.Length > 0)
                        GetServerMessage?.Invoke(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                StateUpdating?.Invoke($"Connection error {ex.Message}");
                Disconnect();
            }
        }

        private void Disconnect()
        {
            _tokenSource.Cancel();
            _tcpStream.Close();
            _tcpClient.Close();
            StateUpdating?.Invoke("You disconnected from server");
        }

        ~ServerClient()
        {
            Disconnect();
        }
    }
}
