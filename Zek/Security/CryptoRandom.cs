using System;
using System.Security.Cryptography;

namespace Zek.Security
{
    public class CryptoRandom : RandomNumberGenerator
    {
        static CryptoRandom()
        {
            Generator = Create();
        }

        private static readonly RandomNumberGenerator Generator;


        public override void GetBytes(byte[] data)
        {
            Generator.GetBytes(data);
        }

        public override void GetNonZeroBytes(byte[] data)
        {
            Generator.GetNonZeroBytes(data);
        }


        ///<summary>
        /// Returns a random number between 0.0 and 1.0.
        ///</summary>
        public double NextDouble()
        {
            var randomBytes = new byte[4];
            Generator.GetBytes(randomBytes);
            return (double)BitConverter.ToUInt32(randomBytes, 0) / uint.MaxValue;
        }

        ///<summary>
        /// Returns a random number within the specified range.
        ///</summary>
        ///<param name="minValue">The inclusive lower bound of the random number returned.</param>
        ///<param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
        public int Next(int minValue = 0, int maxValue = int.MaxValue)
        {
            var range = (long)maxValue - minValue;
            return (int)((long)Math.Floor(NextDouble() * range) + minValue);
        }

        public static uint GetNextUInt32()
        {
            var randomBytes = new byte[4];
            Generator.GetBytes(randomBytes);
            return BitConverter.ToUInt32(randomBytes, 0);
        }

        public static int GetNextInt32()
        {
            var randomBytes = new byte[4];
            Generator.GetBytes(randomBytes);
            return BitConverter.ToInt32(randomBytes, 0);
        }
    }
}
