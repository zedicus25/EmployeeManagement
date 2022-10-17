using Server.Controller;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        private ServerController _serverController;
        private delegate void SetTextCallback(string text);
        
        public Form1()
        {
            InitializeComponent();
            _serverController = new ServerController();
            _serverController.StateUpdating += UpdateServerState;

        }

        private void UpdateServerState(string msg)
        {
            if (this.logL.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(UpdateServerState);
                this.Invoke(d, new object[] { msg });
            }
            else
            {
                this.logL.Text += msg+"\n";
            }
        }

        ~Form1()
        {
            _serverController.StateUpdating -= UpdateServerState;
        }
    }
}
