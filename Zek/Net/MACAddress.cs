using System;
using System.Globalization;

namespace Zek.Net
{
    public class MacAddress
    {
        private byte[] _Bytes;
        public byte[] Bytes
        {
            get { return _Bytes; }
            set { _Bytes = value; }
        }

        public MacAddress(string macAddress): this(ParseMacAddress(macAddress))
        {
        }
        public MacAddress(byte[] bytes)
        {
            if (bytes.Length != 6)
                throw new ArgumentException("MAC address must have 6 bytes");
            _Bytes = bytes;
        }

        public byte this[int i]
        {
            get { return _Bytes[i]; }
            set { _Bytes[i] = value; }
        }

        public override string ToString()
        {
            return BitConverter.ToString(_Bytes, 0, 6);
        }

        public static byte[] ParseMacAddress(string macAddress)
        {
            macAddress = macAddress.Replace("-", string.Empty).Replace(":", string.Empty).Replace(".", string.Empty).Replace("_", string.Empty).Replace(" ", string.Empty).Replace(",", string.Empty).Replace(";", string.Empty);
            if (macAddress.Length != 12)
                throw new ArgumentException("Incorrect MAC Address.", nameof(macAddress));

            var mac = new byte[6];
            for (var k = 0; k < 6; k++)
                mac[k] = Byte.Parse(macAddress.Substring(k * 2, 2), NumberStyles.HexNumber);

            return mac;
        }
    }
}
