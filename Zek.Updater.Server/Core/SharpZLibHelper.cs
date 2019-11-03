using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Zek.Updater
{
    public class SharpZLibHelper
    {
        static SharpZLibHelper()
        {
            ZipConstants.DefaultCodePage = 850;
        }
        public static void CompressFile(string sourceFile, string destinationFile)
        {
            FileStream fsOut = File.Create(destinationFile);
            ZipOutputStream zipStream = new ZipOutputStream(fsOut);
            zipStream.SetLevel(9);

            int folderOffset = Path.GetDirectoryName(sourceFile).Length + 1;
            

            FileInfo fi = new FileInfo(sourceFile);
            string entryName = sourceFile.Substring(folderOffset); // Makes the name in zip based on the folder
            entryName = ZipEntry.CleanName(entryName); // Removes drive from name and fixes slash direction
            ZipEntry newEntry = new ZipEntry(entryName);
            newEntry.DateTime = fi.LastWriteTime; // Note the zip format stores 2 second granularity
            newEntry.Size = fi.Length;
            zipStream.PutNextEntry(newEntry);
            byte[] buffer = new byte[4096];
            using (FileStream streamReader = File.OpenRead(sourceFile))
            {
                StreamUtils.Copy(streamReader, zipStream, buffer);
            }
            zipStream.CloseEntry();

            zipStream.IsStreamOwner = true; // Makes the Close also Close the underlying stream
            zipStream.Close();
        }

        public static void DecompressFile(string sourceFile, string destinationFile)
        {
            ZipFile zf = null;
            try
            {
                FileStream fs = File.OpenRead(sourceFile);
                zf = new ZipFile(fs);
                //if (!string.IsNullOrEmpty(password))
                //{
                //    zf.Password = password;     // AES encrypted entries are handled automatically
                //}
                
                foreach (ZipEntry zipEntry in zf)
                {
                    if (!zipEntry.IsFile)
                    {
                        continue;           // Ignore directories
                    }
                    //String entryFileName = zipEntry.Name;
                    // to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
                    // Optionally match entrynames against a selection list here to skip as desired.
                    // The unpacked length is available in the zipEntry.Size property.

                    byte[] buffer = new byte[4096];     // 4K is optimum
                    Stream zipStream = zf.GetInputStream(zipEntry);

                    // Manipulate the output filename here as desired.
                    //String fullZipToPath = Path.Combine(destinationFile, entryFileName);
                    string directoryName = Path.GetDirectoryName(destinationFile);
                    if (directoryName.Length > 0)
                        Directory.CreateDirectory(directoryName);

                    // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                    // of the file, but does not waste memory.
                    // The "using" will close the stream even if an exception occurs.
                    using (var streamWriter = File.Create(destinationFile))//File.Create(fullZipToPath)
                    {
                        StreamUtils.Copy(zipStream, streamWriter, buffer);
                    }
                }
            }
            finally
            {
                if (zf != null)
                {
                    zf.IsStreamOwner = true; // Makes close also shut the underlying stream
                    zf.Close(); // Ensure we release resources
                }
            }
        }
    }
}
