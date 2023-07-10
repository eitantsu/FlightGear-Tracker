using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FlightMobileApp.models
{
    public class ClientToSim
    {
        public static Socket socket = null;

        public bool Connect()
        {
            try
            {
                IPAddress ipAddr = IPAddress.Parse(Program.simIP);
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, Convert.ToInt32(Program.valuesPort));
                socket = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.ReceiveTimeout = 10000;
                socket.SendTimeout = 10000;

                socket.Connect(localEndPoint);

                byte[] messageSent = Encoding.ASCII.GetBytes("data\n");
                int byteSent = socket.Send(messageSent);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void Close()
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
