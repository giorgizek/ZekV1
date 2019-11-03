using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace Zek.Extensions.IO
{
    public static class FileExtensions
    {
        public static string ToComputerSize(this long value, bool useAbbreviations = false)
        {
            double valor = value;
            long i;
            var names = useAbbreviations ? new[] { "Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" } : new[] { "Bytes", "KBytes", "MBytes", "GBytes", "TBytes", "PBytes", "EBytes", "ZBytes", "YBytes" };
            for (i = 0; i < names.Length && valor >= 1024; i++)
                valor /= 1024.0;
            return $"{valor:#,###.00} {names[i]}";
        }



        /// <summary>
        /// Opens the with default program.
        /// </summary>
        /// <param name="file">The file to open.</param>
        /// <returns></returns>
        public static Process OpenWithDefaultProgram(this FileInfo file)
        {
            if (!file.Exists)
                throw new FileNotFoundException("File does not exist");

            var process = new Process { StartInfo = { FileName = file.FullName, Verb = "Open" } };
            process.Start();
            return process;
        }

        /// <summary>
        /// Prints the specified file.
        /// </summary>
        /// <param name="file">The file to be printed.</param>
        /// <returns></returns>
        public static Process Print(this FileInfo file)
        {
            if (!file.Exists)
                throw new FileNotFoundException("File does not exist");

            var process = new Process { StartInfo = { FileName = file.FullName, Verb = "Print" } };
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            return process;

        }

        /// <summary>
        /// Compares the files to see if they are different. 
        /// First checks file size
        /// Then modified if the file is larger than the specified size
        /// Then compares the bytes
        /// </summary>
        /// <param name="file1">The source file</param>
        /// <param name="file2">The destination file</param>
        /// <param name="mb">Skip the smart check if the file is larger than this many megabytes. Default is 10.</param>
        /// <returns></returns>
        public static bool IsDifferentThan(this FileInfo file1, FileInfo file2, int mb = 10)
        {
            var ret = false;

            // different size is a different file
            if (file1.Length != file2.Length) return true;

            // if the file times are different and the file is bigger than 10mb flag it for updating
            if (file1.LastWriteTimeUtc > file2.LastWriteTimeUtc && file1.Length > mb * 1024 * 1024) return true;

            var f1 = File.ReadAllBytes(file1.FullName);
            var f2 = File.ReadAllBytes(file2.FullName);

            // loop through backwards because if they are different
            // it is more likely that the last few bytes will be different
            // than the first few
            for (var i = file1.Length - 1; i > 0; i--)
            {
                if (f1[i] != f2[i])
                {
                    ret = true;
                    break;
                }
            }

            return ret;
        }


        //[DllImport("kernel32.dll", SetLastError = true)]
        //private static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        //[DllImport("kernel32.dll", SetLastError = true)]
        //private static extern bool GetFileInformationByHandle(IntPtr hFile, out ByHandleFileInformation lpFileInformation);

        //public static bool IsSameFileAs(this FileSystemInfo file, string path)
        //{
        //    ByHandleFileInformation fileInfo1, fileInfo2;
        //    var ptr1 = CreateFile(file.FullName, GENERIC_READ, FILE_SHARE_READ, IntPtr.Zero, OPEN_EXISTING, FILE_FLAG_BACKUP_SEMANTICS, IntPtr.Zero);
        //    if ((int)ptr1 == -1)
        //    {
        //        var e = new Win32Exception(Marshal.GetLastWin32Error());
        //        throw e;
        //    }
        //    var ptr2 = CreateFile(path, GENERIC_READ, FILE_SHARE_READ, IntPtr.Zero, OPEN_EXISTING,
        //                             FILE_FLAG_BACKUP_SEMANTICS, IntPtr.Zero);
        //    if ((int)ptr2 == -1)
        //    {
        //        var e = new Win32Exception(Marshal.GetLastWin32Error());
        //        throw e;
        //    }
        //    GetFileInformationByHandle(ptr1, out fileInfo1);
        //    GetFileInformationByHandle(ptr2, out fileInfo2);

        //    return (fileInfo1.FileIndexHigh == fileInfo2.FileIndexHigh) &&
        //           (fileInfo1.FileIndexLow == fileInfo2.FileIndexLow);
        //}

        /// <summary>
        /// 	Renames a file.
        /// </summary>
        /// <param name = "file">The file.</param>
        /// <param name = "newName">The new name.</param>
        /// <returns>The renamed file</returns>
        /// <example>
        /// 	<code>
        /// 		var file = new FileInfo(@"c:\test.txt");
        /// 		file.Rename("test2.txt");
        /// 	</code>
        /// </example>
        public static FileInfo Rename(this FileInfo file, string newName)
        {
            var filePath = Path.Combine(Path.GetDirectoryName(file.FullName), newName);
            file.MoveTo(filePath);
            return file;
        }

        /// <summary>
        /// 	Renames a without changing its extension.
        /// </summary>
        /// <param name = "file">The file.</param>
        /// <param name = "newName">The new name.</param>
        /// <returns>The renamed file</returns>
        /// <example>
        /// 	<code>
        /// 		var file = new FileInfo(@"c:\test.txt");
        /// 		file.RenameFileWithoutExtension("test3");
        /// 	</code>
        /// </example>
        public static FileInfo RenameFileWithoutExtension(this FileInfo file, string newName)
        {
            var fileName = string.Concat(newName, file.Extension);
            file.Rename(fileName);
            return file;
        }

        /// <summary>
        /// 	Changes the files extension.
        /// </summary>
        /// <param name = "file">The file.</param>
        /// <param name = "newExtension">The new extension.</param>
        /// <returns>The renamed file</returns>
        /// <example>
        /// 	<code>
        /// 		var file = new FileInfo(@"c:\test.txt");
        /// 		file.ChangeExtension("xml");
        /// 	</code>
        /// </example>
        public static FileInfo ChangeExtension(this FileInfo file, string newExtension)
        {
            newExtension = newExtension.EnsureStartsWith(".");
            var fileName = string.Concat(Path.GetFileNameWithoutExtension(file.FullName), newExtension);
            file.Rename(fileName);
            return file;
        }

        /// <summary>
        /// 	Changes the extensions of several files at once.
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <param name = "newExtension">The new extension.</param>
        /// <returns>The renamed files</returns>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.ChangeExtensions("tmp");
        /// 	</code>
        /// </example>
        public static FileInfo[] ChangeExtensions(this FileInfo[] files, string newExtension)
        {
            var result = new FileInfo[files.Length];
            for (var i = 0; i < files.Length; i++)
            {
                result[i] = ChangeExtension(files[i], newExtension);
            }
            return result;
        }

        /// <summary>
        /// 	Deletes several files at once and consolidates any exceptions.
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.Delete()
        /// 	</code>
        /// </example>
        public static void Delete(this FileInfo[] files)
        {
            files.Delete(true);
        }

        /// <summary>
        /// 	Deletes several files at once and optionally consolidates any exceptions.
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <param name = "consolidateExceptions">if set to <c>true</c> exceptions are consolidated and the processing is not interrupted.</param>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.Delete()
        /// 	</code>
        /// </example>
        public static void Delete(this FileInfo[] files, bool consolidateExceptions)
        {

            if (consolidateExceptions)
            {
                var exceptions = new List<Exception>();

                foreach (var file in files)
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (Exception e)
                    {
                        exceptions.Add(e);
                    }
                }
                if (exceptions.Any())
                    throw CombinedException.Combine(
                        "Error while deleting one or several files, see InnerExceptions array for details.", exceptions);
            }
            else
            {
                foreach (var file in files)
                {
                    file.Delete();
                }
            }




        }

        /// <summary>
        /// 	Copies several files to a new folder at once and consolidates any exceptions.
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <param name = "targetPath">The target path.</param>
        /// <returns>The newly created file copies</returns>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		var copiedFiles = files.CopyTo(@"c:\temp\");
        /// 	</code>
        /// </example>
        public static FileInfo[] CopyTo(this FileInfo[] files, string targetPath)
        {
            return files.CopyTo(targetPath, true);
        }

        /// <summary>
        /// 	Copies several files to a new folder at once and optionally consolidates any exceptions.
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <param name = "targetPath">The target path.</param>
        /// <param name = "consolidateExceptions">if set to <c>true</c> exceptions are consolidated and the processing is not interrupted.</param>
        /// <returns>The newly created file copies</returns>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		var copiedFiles = files.CopyTo(@"c:\temp\");
        /// 	</code>
        /// </example>
        public static FileInfo[] CopyTo(this FileInfo[] files, string targetPath, bool consolidateExceptions)
        {
            var copiedfiles = new List<FileInfo>();
            List<Exception> exceptions = null;

            foreach (var file in files)
            {
                try
                {
                    var fileName = Path.Combine(targetPath, file.Name);
                    copiedfiles.Add(file.CopyTo(fileName));
                }
                catch (Exception e)
                {
                    if (consolidateExceptions)
                    {
                        if (exceptions == null)
                            exceptions = new List<Exception>();
                        exceptions.Add(e);
                    }
                    else
                        throw;
                }
            }

            if ((exceptions != null) && (exceptions.Count > 0))
                throw new CombinedException("Error while copying one or several files, see InnerExceptions array for details.", exceptions.ToArray());

            return copiedfiles.ToArray();
        }

        /// <summary>
        /// 	Moves several files to a new folder at once and optionally consolidates any exceptions.
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <param name = "targetPath">The target path.</param>
        /// <returns>The moved files</returns>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.MoveTo(@"c:\temp\");
        /// 	</code>
        /// </example>
        public static FileInfo[] MoveTo(this FileInfo[] files, string targetPath)
        {
            return files.MoveTo(targetPath, true);
        }

        /// <summary>
        /// 	Movies several files to a new folder at once and optionally consolidates any exceptions.
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <param name = "targetPath">The target path.</param>
        /// <param name = "consolidateExceptions">if set to <c>true</c> exceptions are consolidated and the processing is not interrupted.</param>
        /// <returns>The moved files</returns>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.MoveTo(@"c:\temp\");
        /// 	</code>
        /// </example>
        public static FileInfo[] MoveTo(this FileInfo[] files, string targetPath, bool consolidateExceptions)
        {
            List<Exception> exceptions = null;

            foreach (var file in files)
            {
                try
                {
                    var fileName = Path.Combine(targetPath, file.Name);
                    file.MoveTo(fileName);
                }
                catch (Exception e)
                {
                    if (consolidateExceptions)
                    {
                        if (exceptions == null)
                            exceptions = new List<Exception>();
                        exceptions.Add(e);
                    }
                    else
                        throw;
                }
            }

            if ((exceptions != null) && (exceptions.Count > 0))
                throw new CombinedException(
                    "Error while moving one or several files, see InnerExceptions array for details.",
                    exceptions.ToArray());

            return files;
        }

        /// <summary>
        /// 	Sets file attributes for several files at once
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <param name = "attributes">The attributes to be set.</param>
        /// <returns>The changed files</returns>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.SetAttributes(FileAttributes.Archive);
        /// 	</code>
        /// </example>
        public static FileInfo[] SetAttributes(this FileInfo[] files, FileAttributes attributes)
        {
            foreach (var file in files)
                file.Attributes = attributes;
            return files;
        }

        /// <summary>
        /// 	Appends file attributes for several files at once (additive to any existing attributes)
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <param name = "attributes">The attributes to be set.</param>
        /// <returns>The changed files</returns>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.SetAttributesAdditive(FileAttributes.Archive);
        /// 	</code>
        /// </example>
        public static FileInfo[] SetAttributesAdditive(this FileInfo[] files, FileAttributes attributes)
        {
            foreach (var file in files)
                file.Attributes = file.Attributes | attributes;
            return files;
        }
    }


    /// <summary>
    /// 	Generic exception for combining several other exceptions
    /// </summary>
    [Serializable]
    public class CombinedException : Exception
    {
        /// <summary>
        /// 	Initializes a new instance of the <see cref = "CombinedException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <param name = "innerExceptions">The inner exceptions.</param>
        public CombinedException(string message, Exception[] innerExceptions)
            : base(message)
        {
            InnerExceptions = innerExceptions;
        }

        /// <summary>
        /// 	Gets the inner exceptions.
        /// </summary>
        /// <value>The inner exceptions.</value>
        public Exception[] InnerExceptions { get; protected set; }

        public static Exception Combine(string message, params Exception[] innerExceptions)
        {
            if (innerExceptions.Length == 1)
                return innerExceptions[0];

            return new CombinedException(message, innerExceptions);
        }
        /// <summary>
        /// Combines the specified exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerExceptions">The inner exceptions.</param>
        /// <returns></returns>
        public static Exception Combine(string message, IEnumerable<Exception> innerExceptions)
        {
            return Combine(message, innerExceptions.ToArray());
        }


        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}