using System.Net;
using System.Net.Sockets;

namespace Zek.Net
{
    /// <summary>
    /// სოკეტის დამხმარე კლასი.
    /// </summary>
    public class SocketHelper
    {
        #region WOL
        /// <summary>
        /// Sends a Wake-On-Lan packet to the specified MAC address.
        /// </summary>
        /// <param name="mac">Physical MAC address to send WOL packet to.</param>
        public static void WakeUp(string mac)
        {
            WakeUp(new MacAddress(mac));
        }
        /// <summary>
        /// Sends a Wake-On-Lan packet to the specified MAC address.
        /// </summary>
        /// <param name="mac">Physical MAC address to send WOL packet to.</param>
        /// <param name="port">Port.</param>
        public static void WakeUp(string mac, int port)
        {
            WakeUp(new MacAddress(mac), port);
        }
        /// <summary>
        /// Sends a Wake-On-Lan packet to the specified MAC address.
        /// </summary>
        /// <param name="mac">Physical MAC address to send WOL packet to.</param>
        public static void WakeUp(MacAddress mac)
        {
            WakeUp(mac.Bytes);
        }
        /// <summary>
        /// Sends a Wake-On-Lan packet to the specified MAC address.
        /// </summary>
        /// <param name="mac">Physical MAC address to send WOL packet to.</param>
        /// <param name="port">Port.</param>
        public static void WakeUp(MacAddress mac, int port)
        {
            WakeUp(mac.Bytes, port);
        }
        /// <summary>
        /// Sends a Wake-On-Lan packet to the specified MAC address.
        /// </summary>
        /// <param name="mac">Physical MAC address to send WOL packet to.</param>
        private static void WakeUp(byte[] mac)
        {
            // Submit WOL packet (WOL packet is sent over UDP 255.255.255.0:7).
            WakeUp(mac, 7);
        }
        /// <summary>
        /// Sends a Wake-On-Lan packet to the specified MAC address.
        /// </summary>
        /// <param name="mac">Physical MAC address to send WOL packet to.</param>
        /// <param name="port">Port.</param>
        private static void WakeUp(byte[] mac, int port)
        {
            SendUdp(IPAddress.Broadcast, port, MergeMacWithMagicPacket(mac));
        }
        
        private static byte[] GetMagicPacket()
        {
            //WOL packet contains a 6-bytes trailer and 16 times a 6-bytes sequence containing the MAC address.
            var packet = new byte[17 * 6];

            // Trailer of 6 times 0xFF.
            for (var i = 0; i < 6; i++)
                packet[i] = 0xff;

            return packet;
        }
        private static byte[] MergeMacWithMagicPacket(byte[] mac)
        {
            return MergeMacWithMagicPacket(mac, GetMagicPacket());
        }
        private static byte[] MergeMacWithMagicPacket(byte[] mac, byte[] magicPacket)
        {
            for (var i = 1; i <= 16; i++)
                for (var j = 0; j < 6; j++)
                    magicPacket[i * 6 + j] = mac[j];

            return magicPacket;
        }
        #endregion

        #region Send
        public static void SendUdp(IPAddress ipAddress, int port, byte[] dgram)
        {
            using (var client = new UdpClient())
            {
                client.Connect(ipAddress, port);
                client.Send(dgram, dgram.Length);
            }
        }
        public static void SendUdp(string ipAddress, string subnetMask, int port, byte[] dgram)
        {
            var address = IPAddress.Parse(ipAddress);
            var mask = IPAddress.Parse(subnetMask);
            var broadcastAddress = IPAddressHelper.GetBroadcastAddress(address, mask);

            using (var client = new UdpClient())
            {
                client.Send(dgram, dgram.Length, broadcastAddress.ToString(), port);
            }
        }
        #endregion
    }
}
