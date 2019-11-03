using System;
using System.Security.Cryptography;

namespace Zek.Security
{
    public class RandomHelper
    {
        /// <summary>
        /// Gets a random object with a real random seed
        /// </summary>
        /// <returns></returns>
        public static Random GetRandom()
        {
            // Use a 4-byte array to fill it with random bytes and convert it then
            // to an integer value.
            var randomBytes = new byte[4];

            // Generate 4 random bytes.
            new RNGCryptoServiceProvider().GetBytes(randomBytes);

            // Convert 4 bytes into a 32-bit integer value.
            var seed = (randomBytes[0] & 0x7f) << 24 |
                        randomBytes[1] << 16 |
                        randomBytes[2] << 8 |
                        randomBytes[3];

            // Now, this is real randomization.
            return new Random(seed);
        }
    }
}
