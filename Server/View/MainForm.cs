﻿using Server.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class MainForm : Form
    {
        private ServerController _serverController;
        private delegate void SetTextCallback(string text);
        public MainForm()
        {
            InitializeComponent();
            _serverController = new ServerController();
            _serverController.StateUpdating += UpdateServerState;
        }

        private void UpdateServerState(string msg)
        {
            if (this.logRTB.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(UpdateServerState);
                this.Invoke(d, new object[] { msg });
            }
            else
            {
                this.logRTB.Text += msg + "\n";
            }
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            _serverController.StartServer();
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            _serverController.StopServer();
        }
    }
}
