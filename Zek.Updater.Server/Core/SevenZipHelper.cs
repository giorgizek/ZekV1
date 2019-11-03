using System;
using System.IO;
using SevenZip;
using SevenZip.Compression.LZMA;

namespace Zek
{
    public static class SevenZipHelper
    {
        /*
        static int dictionary = 1 << 23;

        // static Int32 posStateBits = 2;
        // static  Int32 litContextBits = 3; // for normal files
        // UInt32 litContextBits = 0; // for 32-bit data
        // static  Int32 litPosBits = 0;
        // UInt32 litPosBits = 2; // for 32-bit data
        // static   Int32 algorithm = 2;
        // static    Int32 numFastBytes = 128;

        static bool eos = false;





        static CoderPropID[] propIDs = 
                                {
                                        CoderPropID.DictionarySize,
                                        CoderPropID.PosStateBits,
                                        CoderPropID.LitContextBits,
                                        CoderPropID.LitPosBits,
                                        CoderPropID.Algorithm,
                                        CoderPropID.NumFastBytes,
                                        CoderPropID.MatchFinder,
                                        CoderPropID.EndMarker
                                };

        // these are the default properties, keeping it simple for now:
        static object[] properties = 
                                {
                                        (Int32)(dictionary),
                                        (Int32)(2),
                                        (Int32)(3),
                                        (Int32)(0),
                                        (Int32)(2),
                                        (Int32)(128),
                                        "bt4",
                                        eos
                                };


        public static byte[] Compress(byte[] inputBytes)
        {
            MemoryStream inStream = new MemoryStream(inputBytes);
            MemoryStream outStream = new MemoryStream();
            var encoder = new Encoder();
            encoder.SetCoderProperties(propIDs, properties);
            encoder.WriteCoderProperties(outStream);
            long fileSize = inStream.Length;
            for (int i = 0; i < 8; i++)
                outStream.WriteByte((Byte)(fileSize >> (8 * i)));
            encoder.Code(inStream, outStream, -1, -1, null);
            return outStream.ToArray();
        }

        public static byte[] Decompress(byte[] inputBytes)
        {
            MemoryStream inStream = new MemoryStream(inputBytes);
            var decoder = new Decoder();

            inStream.Seek(0, 0);
            MemoryStream newOutStream = new MemoryStream();

            byte[] properties2 = new byte[5];
            if (inStream.Read(properties2, 0, 5) != 5)
                throw (new Exception("input .lzma is too short"));
            long outSize = 0;
            for (int i = 0; i < 8; i++)
            {
                int v = inStream.ReadByte();
                if (v < 0)
                    throw (new Exception("Can't Read 1"));
                outSize |= ((long)(byte)v) << (8 * i);
            }
            decoder.SetDecoderProperties(properties2);

            long compressedSize = inStream.Length - inStream.Position;
            decoder.Code(inStream, newOutStream, compressedSize, outSize, null);

            return newOutStream.ToArray();
        }

        */

        public static void CompressFileLZMA(string sourceFile, string destinationFile)
        {
            // make sure the source file is there
            if (File.Exists(sourceFile) == false)
                throw new FileNotFoundException("Unable to find the specified file.", sourceFile);

            // Create the streams and byte arrays needed
            //byte[] buffer = null;
            FileStream sourceStream = null;
            FileStream destinationStream = null;
            try
            {
                var coder = new Encoder();
                // Read the bytes from the source file into a byte array
                sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);

                // Read the source stream values into the buffer
                //buffer = new byte[sourceStream.Length];
                //int checkCounter = sourceStream.Read(buffer, 0, buffer.Length);

                //if (checkCounter != buffer.Length)
                //    throw new ApplicationException();

                destinationStream = new FileStream(destinationFile, FileMode.OpenOrCreate, FileAccess.Write);

                // Write the encoder properties
                coder.WriteCoderProperties(destinationStream);

                // Write the decompressed file size.//BitConverter.GetBytes(buffer.Length)
                destinationStream.Write(BitConverter.GetBytes(sourceStream.Length), 0, 8);

                // Encode the file.
                coder.Code(sourceStream, destinationStream, sourceStream.Length, -1, null);
                destinationStream.Flush();
            }
            finally
            {
                // Make sure we allways close all streams
                if (sourceStream != null)
                    sourceStream.Close();

                //if (compressedStream != null)
                //    compressedStream.Close();

                if (destinationStream != null)
                    destinationStream.Close();
            }
        }
        public static void DecompressFileLZMA(string sourceFile, string destinationFile)
        {
            // make sure the source file is there
            if (File.Exists(sourceFile) == false)
                throw new FileNotFoundException("Unable to find the specified file.", sourceFile);

            FileStream sourceStream = null;
            FileStream destinationStream = null;
            try
            {
                var coder = new SevenZip.Compression.LZMA.Decoder();
                sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read);
                destinationStream = new FileStream(destinationFile, FileMode.OpenOrCreate, FileAccess.Write);

                // Read the decoder properties
                byte[] properties = new byte[5];
                sourceStream.Read(properties, 0, 5);

                // Read in the decompress file size.
                byte[] fileLengthBytes = new byte[8];
                sourceStream.Read(fileLengthBytes, 0, 8);
                long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);

                coder.SetDecoderProperties(properties);
                coder.Code(sourceStream, destinationStream, sourceStream.Length, fileLength, null);
                destinationStream.Flush();
            }
            finally
            {
                // Make sure we allways close all streams
                if (sourceStream != null)
                    sourceStream.Close();

                //if (compressedStream != null)
                //    compressedStream.Close();

                if (destinationStream != null)
                    destinationStream.Close();
            }
        }
    }
}
