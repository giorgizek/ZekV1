using System;
using System.Globalization;
using System.Net;

namespace Zek.Net
{
    public class IPAddressHelper
    {
        public static bool IsPrivate(string ipAddress)
        {
            return IsPrivate(IPAddress.Parse(ipAddress));
        }
        public static bool IsPrivate(IPAddress ipAddress)
        {
            // http://en.wikipedia.org/wiki/Private_network
            // Private IP Addresses are: 
            //  24-bit block: 10.0.0.0 through 10.255.255.255
            //  20-bit block: 172.16.0.0 through 172.31.255.255
            //  16-bit block: 192.168.0.0 through 192.168.255.255
            //  Link-local addresses: 169.254.0.0 through 169.254.255.255 (http://en.wikipedia.org/wiki/Link-local_address)

            var octets = ipAddress.GetAddressBytes();

            var is24BitBlock = octets[0] == 10;
            if (is24BitBlock) return true; // Return to prevent further processing

            var is20BitBlock = octets[0] == 172 && octets[1] >= 16 && octets[1] <= 31;
            if (is20BitBlock) return true; // Return to prevent further processing

            var is16BitBlock = octets[0] == 192 && octets[1] == 168;
            if (is16BitBlock) return true; // Return to prevent further processing

            var isLinkLocalAddress = octets[0] == 169 && octets[1] == 254;
            return isLinkLocalAddress;
        }


        public static string GetIPAddress()
        {
            try
            {
                return Dns.GetHostAddresses(Dns.GetHostName())[0].ToString();
            }
            catch
            {
                return "0.0.0.0";
            }
        }
        public static string[] GetIPAddressList()
        {

            try
            {
                var addresses = Dns.GetHostAddresses(Dns.GetHostName());

                var result = new string[addresses.Length];
                for (var i = 0; i < addresses.Length; i++)
                {
                    result[i] = addresses[i].ToString();
                }

                return result;
            }
            catch
            {
                return new string[0];
            }

        }

        public static string GetHostName()
        {
            try
            {
                return Dns.GetHostName();
            }
            catch
            {
                return "0.0.0.0";
            }
        }
        public static string GetHostName(string ip)
        {
            try
            {
                var hostEntry = Dns.GetHostEntry(ip);
                return hostEntry.HostName;
            }
            catch
            {
                return "0.0.0.0";
            }
        }



        //[DllImport("iphlpapi.dll", ExactSpelling = true)]
        //private static extern int SendARP(int DestIP, int SrcIP, byte[] pMacAddr, ref uint PhyAddrLen);

        //public static byte[] IPToMacAddress(IPAddress ipAddress)
        //{
        //    byte[] mac = new byte[6];
        //    uint len = (uint)mac.Length;
        //    int res = SendARP((int)ipAddress.Address, 0, mac, ref len);
        //    if (res != 0)
        //        throw new WebException("Error " + res + " looking up " + ipAddress.ToString());
        //    //return new MACAddress(mac);
        //    return mac;
        //}



        public static IPAddress GetBroadcastAddress(IPAddress address, IPAddress subnetMask)
        {
            var ipAdressBytes = address.GetAddressBytes();
            var subnetMaskBytes = subnetMask.GetAddressBytes();

            if (ipAdressBytes.Length != subnetMaskBytes.Length)
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

            var broadcastAddress = new byte[ipAdressBytes.Length];
            for (var i = 0; i < broadcastAddress.Length; i++)
            {
                broadcastAddress[i] = (byte)(ipAdressBytes[i] | (subnetMaskBytes[i] ^ 255));
            }
            return new IPAddress(broadcastAddress);
        }
        public static IPAddress GetNetworkAddress(IPAddress address, IPAddress subnetMask)
        {
            var ipAdressBytes = address.GetAddressBytes();
            var subnetMaskBytes = subnetMask.GetAddressBytes();

            if (ipAdressBytes.Length != subnetMaskBytes.Length)
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

            var broadcastAddress = new byte[ipAdressBytes.Length];
            for (var i = 0; i < broadcastAddress.Length; i++)
            {
                broadcastAddress[i] = (byte)(ipAdressBytes[i] & subnetMaskBytes[i]);
            }
            return new IPAddress(broadcastAddress);
        }

        /// <summary>
        /// ამოწმებს ამ სუბმასკაშია თუ არა.
        /// </summary>
        /// <param name="address1"></param>
        /// <param name="address2"></param>
        /// <param name="subnetMask"></param>
        /// <returns></returns>
        public static bool IsInSameSubnet(IPAddress address1, IPAddress address2, IPAddress subnetMask)
        {
            var network1 = GetNetworkAddress(address1, subnetMask);
            var network2 = GetNetworkAddress(address2, subnetMask);

            return network1.Equals(network2);
        }

        #region IP Address
        public static long IPAddressToInt64(string ipAddress)
        {
            /*
            192.0.0.1
            (first octet * 256³) + (second octet * 256²) + (third octet * 256) + (fourth octet)
            = 	(first octet * 16777216) + (second octet * 65536) + (third octet * 256) + (fourth octet)
            = 	(192 * 16777216) + (0 * 65536) + (0 * 256) + (1)
            = 	3221225473*/
            IPAddress ip;
            if (IPAddress.TryParse(ipAddress, out ip))
            {
                var buffer = ip.GetAddressBytes();
                long l = (buffer[0] << 24) | (buffer[1] << 16) | (buffer[2] << 8) | buffer[3];
                return l > 0 ? l : (uint)unchecked(l);
            }
            return 0;
        }
        public static string Int64ToIPAddress(long ipAddress)
        {
            IPAddress ip;
            return IPAddress.TryParse(ipAddress.ToString(CultureInfo.InvariantCulture), out ip) ? ip.ToString() : "0.0.0.0";
        }

        public static bool IsInRange(string source, string start, string end)
        {
            IPAddress ipSource;
            if (!IPAddress.TryParse(source, out ipSource)) return false;

            IPAddress ipStart;
            if (!IPAddress.TryParse(start, out ipStart)) return false;
            IPAddress ipEnd;
            return IPAddress.TryParse(end, out ipEnd) && IsInRange(ipSource, ipStart, ipEnd);
        }
        public static bool IsInRange(IPAddress source, IPAddress start, IPAddress end)
        {
            if (IPAddress.IsLoopback(source) == false)
            {
                var result = IPAddressToToUInt32(source);
                return result >= IPAddressToToUInt32(start) && result <= IPAddressToToUInt32(end);
            }
            return true;
        }


        public static uint IPAddressToToUInt32(IPAddress ipAddress)
        {
            var ipBytes = ipAddress.GetAddressBytes();
            return (uint)ipBytes[0] << 24 | (uint)ipBytes[1] << 16 | (uint)ipBytes[2] << 8 | ipBytes[3];

            //var ipAddress = IPAddress.Parse("some.ip.address");
            //var ipBytes = ipAddress.GetAddressBytes();
            //var ip = (uint)ipBytes[0] << 24;
            //ip += (uint)ipBytes[1] << 16;
            //ip += (uint)ipBytes[2] << 8;
            //ip += (uint)ipBytes[3];
        }
        public static uint IPAddressToToUInt32(string ipAddress)
        {
            IPAddress ip;
            return IPAddress.TryParse(ipAddress, out ip) ? IPAddressToToUInt32(ip) : 0;
        }

        public static IPAddress TryParse(string ipAddress)
        {
            IPAddress ip;
            IPAddress.TryParse(ipAddress, out ip);
            return ip;
        }

        public static string IPAddressToString(string ipAddress)
        {
            return IPAddressToString(TryParse(ipAddress));
        }
        public static string IPAddressToString(IPAddress ipAddress)
        {
            if (ipAddress == null) return null;
            var ipBytes = ipAddress.GetAddressBytes();
            return $"{ipBytes[0].ToString("000")}.{ipBytes[1].ToString("000")}.{ipBytes[2].ToString("000")}.{ipBytes[3].ToString("000")}";
        }
        #endregion
    }
}
