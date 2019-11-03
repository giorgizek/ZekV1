using System;
using System.Text;
using System.IO;
using System.IO.Compression;
using Zek.Extensions;

namespace Zek.IO
{
    /// <summary>
    /// კომპრესიის დამხმარე კლასი.
    /// </summary>
    public class GZipHelper
    {
        #region Const
        //internal static int CompressionLevel = 6;
        #endregion


        /// <summary>
        /// ფაილის კომპრესია
        /// </summary>
        /// <param name="sourceFile">ორიგინალი ფაილის მისამართი</param>
        /// <param name="destinationFile">დაკომპრესებული ფაილის მისამართი</param>
        public static void CompressFile(string sourceFile, string destinationFile)
        {
            // make sure the source file is there
            if (File.Exists(sourceFile) == false)
                throw new FileNotFoundException("Unable to find the specified file.", sourceFile);

            // Create the streams and byte arrays needed
            FileStream sourceStream = null;
            FileStream destinationStream = null;
            GZipStream compressedStream = null;

            try
            {
                // Read the bytes from the source file into a byte array
                sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);

                // Read the source stream values into the buffer
                var buffer = new byte[sourceStream.Length];
                var checkCounter = sourceStream.Read(buffer, 0, buffer.Length);

                if (checkCounter != buffer.Length)
                    throw new ApplicationException();

                // Open the FileStream to write to
                destinationStream = new FileStream(destinationFile, FileMode.OpenOrCreate, FileAccess.Write);

                // Create a compression stream pointing to the destiantion stream
                compressedStream = new GZipStream(destinationStream, CompressionMode.Compress, true);

                // Now write the compressed data to the destination file
                compressedStream.Write(buffer, 0, buffer.Length);
            }
            finally
            {
                // Make sure we allways close all streams
                if (sourceStream != null)
                    sourceStream.Close();

                if (compressedStream != null)
                    compressedStream.Close();

                if (destinationStream != null)
                    destinationStream.Close();
            }
        }

        /// <summary>
        /// დაკომპრესებული ფაილის ორიგინალში გადაყვანა
        /// </summary>
        /// <param name="sourceFile">დაკომპრესებული ფაილის მისამართი</param>
        /// <param name="destinationFile">ორიგინალი ფაილის მისამართი</param>
        public static void DecompressFile(string sourceFile, string destinationFile)
        {
            // make sure the source file is there
            if (File.Exists(sourceFile) == false)
                throw new FileNotFoundException("Unable to find the specified file.", sourceFile);

            // Create the streams and byte arrays needed
            FileStream sourceStream = null;
            FileStream destinationStream = null;
            GZipStream decompressedStream = null;

            try
            {
                // Read in the compressed source stream
                sourceStream = new FileStream(sourceFile, FileMode.Open);

                // Create a compression stream pointing to the destiantion stream
                decompressedStream = new GZipStream(sourceStream, CompressionMode.Decompress, true);

                // Read the footer to determine the length of the destiantion file
                var quartetBuffer = new byte[4];
                var position = (int)sourceStream.Length - 4;
                sourceStream.Position = position;
                sourceStream.Read(quartetBuffer, 0, 4);
                sourceStream.Position = 0;
                var checkLength = BitConverter.ToInt32(quartetBuffer, 0);

                const int size = 4096;
                var buffer = new byte[checkLength + size];

                var offset = 0;
                var total = 0;

                // Read the compressed data into the buffer
                while (true)
                {
                    var bytesRead = decompressedStream.Read(buffer, offset, size);

                    if (bytesRead == 0)
                        break;

                    offset += bytesRead;
                    total += bytesRead;
                }

                // Now write everything to the destination file
                destinationStream = new FileStream(destinationFile, FileMode.Create);
                destinationStream.Write(buffer, 0, total);

                // and flush everyhting to clean out the buffer
                destinationStream.Flush();
            }
            finally
            {
                // Make sure we allways close all streams
                if (sourceStream != null)
                    sourceStream.Close();

                if (decompressedStream != null)
                    decompressedStream.Close();

                if (destinationStream != null)
                    destinationStream.Close();
            }

        }


        public static string CompressText(string text)
        {
            if (text == null) return null;
            return Convert.ToBase64String(Compress(Encoding.UTF8.GetBytes(text)));
        }
        public static string DecompressText(string compressedBase64Text)
        {
            if (compressedBase64Text == null) return null;
            return Decompress(Convert.FromBase64String(compressedBase64Text)).UTF8ArrayToString();
        }


        //private const int BufferSize = 1 << 12; // Pow(2, 12) == 4096


        public static byte[] Compress(byte[] buffer)
        {
            if (buffer == null) return null;

            //Stream stream = null;
            //try
            //{
            //    stream = new MemoryStream();
            //    using (var gzipStream = new GZipStream(stream, CompressionMode.Compress, true))
            //    {
            //        stream = null;
            //        gzipStream.Write(buffer, 0, buffer.Length);
            //    }
            //}
            //finally
            //{
            //    stream?.Dispose();
            //}

            using (var memStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(memStream, CompressionMode.Compress, true))
                {
                    gzipStream.Write(buffer, 0, buffer.Length);
                }
                return memStream.ToArray();
            }

        }
        public static byte[] Decompress(byte[] gzip)
        {
            if (gzip == null) return null;

            using (var memStream = new MemoryStream(gzip))
            using (var gzipStream = new GZipStream(memStream, CompressionMode.Decompress))
            {
                const int size = 4096;
                var buffer = new byte[size];
                using (var decompressedStream = new MemoryStream())
                {
                    int count;
                    do
                    {
                        count = gzipStream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            decompressedStream.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return decompressedStream.ToArray();
                }
            }

            //var memStream = new MemoryStream(gzip);
            //var gzipStream = new GZipStream(memStream, CompressionMode.Decompress);
            //var decompressedStream = new MemoryStream();

            //try
            //{
            //    const int size = 4096;
            //    byte[] buffer = new byte[size];


            //    int count = 0;
            //    do
            //    {
            //        count = gzipStream.Read(buffer, 0, size);
            //        if (count > 0)
            //            decompressedStream.Write(buffer, 0, count);
            //    }
            //    while (count > 0);
            //    var result = decompressedStream.ToArray();
            //    return result;
            //}
            //finally
            //{
            //    if (gzipStream != null)
            //        gzipStream.Close();

            //    if (memStream != null)
            //        memStream.Close();

            //    if (decompressedStream != null)
            //        decompressedStream.Close();
            //}
        }


        //public static void CompressToFile(string inputFile, string outputFile)
        //{
        //    byte[] buffer;
        //    using (var inputStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
        //    {
        //        buffer = new byte[inputStream.Length];
        //        inputStream.Read(buffer, 0, (int)inputStream.Length);
        //    }
        //    CompressToFile(buffer, outputFile);
        //}
        //public static void CompressToFile(byte[] buffer, string outputFile)
        //{
        //    using (var outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
        //    using (var gzip = new GZipStream(outputStream, CompressionMode.Compress, true))
        //    {
        //        gzip.Write(buffer, 0, buffer.Length);
        //    }
        //}
    }
}
