using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zek.Extensions;

namespace Zek.DDoS
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        

        private void btnOk_Click(object sender, EventArgs e)
        {
            var port = txtPort.Text.ToInt32();
            var packetData = Encoding.ASCII.GetBytes("aaa");

            var ep = new IPEndPoint(IPAddress.Parse(txtIp.Text), port);

            var clien = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            try
            {
                clien.SendTo(packetData, ep);
                
                txt
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
