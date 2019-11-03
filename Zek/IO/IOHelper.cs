using System;
using System.Globalization;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Forms;

namespace Zek.IO
{
    public class IOHelper
    {
        private static readonly char[] PathSeparatorChars = { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar, Path.VolumeSeparatorChar };
        public static char[] GetPathSeparatorChars()
        {
            return PathSeparatorChars;
        }
        public static string GetFileName(string path)
        {
            var startIndex = path.LastIndexOfAny(GetPathSeparatorChars());
            if (startIndex < 0)
            {
                return path;
            }
            return path.Substring(startIndex);
        }

        /// <summary>
        /// იღებს ფაილიდან OpenDialog, SaveDialog-ის ფილტრს.
        /// </summary>
        /// <param name="fileName">ფაილი რის მიხევითაც ამოიღოს ფილტრი.</param>
        /// <returns>აბრუნებს ფილტრის ტექსტს.</returns>
        public static string GetFilter(string fileName)
        {
            return GetFilter(fileName, true);
        }
        /// <summary>
        /// იღებს ფაილიდან OpenDialog, SaveDialog-ის ფილტრს.
        /// </summary>
        /// <param name="fileName">ფაილი რის მიხევითაც ამოიღოს ფილტრი.</param>
        /// <param name="addAllFilesFilter"></param>
        /// <returns>აბრუნებს ფილტრის ტექსტს.</returns>
        public static string GetFilter(string fileName, bool addAllFilesFilter)
        {
            var filter = string.Empty;
            try
            {
                var file = new FileInfo(fileName);

                // Create a registry key object to represent the HKEY_CLASSES_ROOT registry section
                var rkRoot = Registry.ClassesRoot;

                // Attempt to retrieve the registry key for the .blackwasp file type
                var rkFileType = rkRoot.OpenSubKey(file.Extension);

                // Was the file type found?
                if (rkFileType != null)
                {
                    var type = rkFileType.GetValue(string.Empty).ToString();

                    rkFileType = rkRoot.OpenSubKey(type);

                    // Was the file type found?
                    if (rkFileType != null)
                    {
                        filter += $"{rkFileType.GetValue(string.Empty)}|*{file.Extension}";
                    }
                }
            }
            catch { }


            if (addAllFilesFilter)
            {
                if (filter.Length > 0)
                    filter += "|";

                filter += @"All files|*.*";
            }

            return filter;
        }

        /// <summary>
        /// აგენერირებს დროებითი ფაილის სახელს.
        /// </summary>
        /// <param name="fileName">ორიგინალი ფაილის სახელი.</param>
        /// <param name="ext">დაბოლოება.</param>
        /// <returns>აბრუნებს დროებითი ფაილის მისამართს.</returns>
        public static string GetTempFileName(string fileName, string ext)
        {
            return GetTempFileName(fileName + ext);
        }
        /// <summary>
        /// აგენერირებს დროებითი ფაილის სახელს.
        /// </summary>
        /// <param name="fileName">ორიგინალი ფაილის სახელი (თავისი დაბოლოებით).</param>
        /// <returns>აბრუნებს დროებითი ფაილის მისამართს.</returns>
        public static string GetTempFileName(string fileName)
        {
            //string filename = Path.GetTempFileName();
            //filename = Path.ChangeExtension(filename, ext);
            //File.Delete(filename);
            //return filename;

            return $"{Path.GetTempPath()}{DateTime.Now.Ticks}_{RemoveInvalidFileNameChars(fileName)}";
        }

        /// <summary>
        /// იღებს ფაილიდან არა ვალიდურ სიმბოლოებს.
        /// </summary>
        /// <param name="fileName">ფაილის დასახელება.</param>
        /// <returns>აბრუნებს ფაილის დასახელებას.</returns>
        public static string RemoveInvalidFileNameChars(string fileName)
        {
            foreach (var ch in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(ch.ToString(CultureInfo.InvariantCulture), string.Empty);
            }

            //fileName = fileName.Replace("\\", "");
            //fileName = fileName.Replace("/", "");
            //fileName = fileName.Replace(":", "");
            //fileName = fileName.Replace("?", "");
            //fileName = fileName.Replace("\"", "");
            //fileName = fileName.Replace("<", "");
            //fileName = fileName.Replace(">", "");
            //fileName = fileName.Replace("|", "");
            return fileName;
        }

        /// <summary>
        /// იღებს დირექტორიიდან არა ვალიდურ სიმბოლოებს.
        /// </summary>
        /// <param name="path">დირექტორიის დასახელება.</param>
        /// <returns>აბრუნებს ფაილის დასახელებას.</returns>
        public static string RemoveInvalidPathChars(string path)
        {
            foreach (var ch in Path.GetInvalidPathChars())
            {
                path = path.Replace(ch.ToString(CultureInfo.InvariantCulture), string.Empty);
            }
            return path;
        }




        /// <summary>
        /// ინახავს ფაილს და შემდეგ უშვებს მას.
        /// </summary>
        /// <param name="path">ფაილის მისამართი (მაგ: c:\file1.txt).</param>
        /// <param name="buffer">ფაილის კონტენტი</param>
        public void WriteAllBytesAndRunFile(string path, byte[] buffer)
        {
            File.WriteAllBytes(path, buffer);
            RunFile(path);
        }
        /// <summary>
        /// უშვებს ფაილს.
        /// </summary>
        /// <param name="fileName">ფაილის დასახელება (მაგ: file1.txt).</param>
        public static void RunFile(string fileName)
        {
            //try
            //{
                if (!File.Exists(fileName))
                    return;

                Process.Start(fileName);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("Can't open file.\n" + ex.Message);
            //}
        }
        /// <summary>
        /// ქმნის დროებით ფაილს დაშემდეგ უშვებს.
        /// </summary>
        /// <param name="fileName">ფაილის დასახელება (მაგ: file1.txt).</param>
        /// <param name="buffer">ფაილის კონტენტი.</param>
        public static void RunTmpFile(string fileName, byte[] buffer)
        {
            var path = GetTempFileName(fileName);
            File.WriteAllBytes(path, buffer);
            //TmpFileList.Add(path);
            RunFile(path);
        }

        public static void DeletFiles(params string[] files)
        {
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }

        /// <summary>
        /// აბრუნებს ფაილის ზომას (მაგ: 15 Gb, 248 KB).
        /// </summary>
        /// <param name="fileSize">ფაილის ზომა.</param>
        /// <returns>ფაილის ფორმატირებული ზომა.</returns>
        public static string FormatSize(int fileSize)
        {
            return FormatSize((decimal)fileSize);
        }
        /// <summary>
        /// აბრუნებს ფაილის ზომას (მაგ: 15 Gb, 248 KB).
        /// </summary>
        /// <param name="fileSize">ფაილის ზომა.</param>
        /// <returns>ფაილის ფორმატირებული ზომა.</returns>
        public static string FormatSize(decimal fileSize)
        {
            if (fileSize >= 1073741824)
                return Math.Round(fileSize / 1073741824 * 100) / 100 + " Gb";

            if (fileSize >= 1048576)
                return Math.Round(fileSize / 1048576 * 100) / 100 + " Mb";

            if (fileSize >= 1024)
                return Math.Round(fileSize / 1024 * 100) / 100 + " Kb";

            return fileSize + " b";
        }

        /// <summary>
        /// იღებს ფაილის დაბოლოებას.
        /// </summary>
        /// <param name="fileName">ფაილის დასახელება.</param>
        /// <returns>ფაილის დაბოლოება.</returns>
        public static string GetFileExtension(string fileName)
        {
            return new FileInfo(fileName).Extension;
        }
        public static string GetDirectory(string fileName)
        {
            return new FileInfo(fileName).DirectoryName;
        }


        /// <summary>
        /// Returns the names of files in the specified directory that match the specified searchpatterns.
        /// </summary>
        /// <param name="path">the directory to search.</param>
        /// <param name="searchPatterns">the search strings to match against the names of files in the path deliminated by a ';'. For example:"*.gif;*.xl?;my*.txt"</param>
        /// <returns></returns>
        public static string[] GetFiles(string path, string searchPatterns)
        {
            //declare the return array
            var returnArray = new string[0];

            if (Directory.Exists(path))
            {
                //loop throuht the givven searchpatterns
                foreach (var ext in searchPatterns.Split(';'))
                {

                    var tmpArray = Directory.GetFiles(path, ext);
                    if (tmpArray.Length > 0)
                    {
                        var newArray = new string[returnArray.Length + tmpArray.Length];
                        returnArray.CopyTo(newArray, 0);
                        tmpArray.CopyTo(newArray, returnArray.Length);
                        returnArray = newArray;
                    }
                }
            }
            return returnArray;
        }

        /// <summary>
        /// Creates all directories and subdirectories as specified and return false in case of an error.
        /// </summary>
        /// <param name="physicalPath"></param>
        /// <returns></returns>
        public static bool CreateDirectory(string physicalPath)
        {
            var returnValue = true;

            if (!Directory.Exists(physicalPath))
            {
                try { Directory.CreateDirectory(physicalPath); }
                catch { returnValue = false; }
            }
            return returnValue;
        }

        /// <summary>
        /// Usage: 
        /// Copy Recursive with Overwrite if exists. 
        /// RecursiveDirectoryCopy("C:\Data", "D:\Data", True, True) 
        /// Copy Recursive without Overwriting. 
        /// RecursiveDirectoryCopy("C:\Data", "D:\Data", True, False) 
        /// Copy this directory Only. Overwrite if exists. 
        /// RecursiveDirectoryCopy("C:\Data", "D:\Data", False, True) 
        /// Copy this directory only without overwriting. 
        /// RecursiveDirectoryCopy("C:\Data", "D:\Data", False, False) 
        /// Recursively copy all files and subdirectories from the specified source to the specified 
        /// destination. 
        /// </summary>
        /// <param name="sourceDir"></param>
        /// <param name="destDir"></param>
        /// <param name="recursive"></param>
        /// <param name="overWrite"></param>
        public static void RecursiveDirectoryCopy(string sourceDir, string destDir, bool recursive, bool overWrite)
        {
            //string sDir;
            // string sFile;
            // Add trailing separators to the supplied paths if they don't exist. 
            if (!sourceDir.EndsWith(Path.DirectorySeparatorChar.ToString(CultureInfo.InvariantCulture)))
            {
                sourceDir += Path.DirectorySeparatorChar;
            }
            if (!destDir.EndsWith(Path.DirectorySeparatorChar.ToString(CultureInfo.InvariantCulture)))
            {
                destDir += Path.DirectorySeparatorChar;
            }
            //If destination directory does not exist, create it. 
            var dDirInfo = new DirectoryInfo(destDir);
            if (dDirInfo.Exists == false)
                dDirInfo.Create();
            // Recursive switch to continue drilling down into directory structure. 
            if (recursive)
            {
                // Get a list of directories from the current parent. 
                foreach (var sDir in Directory.GetDirectories(sourceDir))
                {
                    var sDirInfo = new DirectoryInfo(sDir);
                    dDirInfo = new DirectoryInfo(destDir + sDirInfo.Name);
                    // Create the directory if it does not exist. 
                    if (dDirInfo.Exists == false)
                        dDirInfo.Create();
                    // Since we are in recursive mode, copy the children also 
                    RecursiveDirectoryCopy(sDirInfo.FullName, dDirInfo.FullName, true, overWrite);
                }
            }
            // Get the files from the current parent. 
            foreach (var sourceFile in Directory.GetFiles(sourceDir))
            {
                var sourceFileInfo = new FileInfo(sourceFile);
                var destFileInfo = new FileInfo(sourceFile.Replace(sourceDir, destDir));
                //If File does not exist. Copy. 
                if (destFileInfo.Exists == false)
                {
                    sourceFileInfo.CopyTo(destFileInfo.FullName, overWrite);
                }
                else
                {
                    //If file exists and is the same length (size). Skip. 
                    //If file exists and is of different Length (size) and overwrite = True. Copy 
                    if (sourceFileInfo.Length != destFileInfo.Length && overWrite)
                    {
                        sourceFileInfo.CopyTo(destFileInfo.FullName, true);
                    }
                    //If file exists and is of different Length (size) and overwrite = False. Skip 
                    else if (sourceFileInfo.Length != destFileInfo.Length && !overWrite)
                    {

                    }
                }
            }
        }

        /// <summary>
        /// Retrievs the parent directory of the working directory.
        /// </summary>
        /// <returns></returns>
        public static string GetWorkingParentDirectory()
        {
            return GetParentDirectory(Directory.GetCurrentDirectory());
        }
        /// <summary>
        /// Retrievs the parent directory of the speciﬁed path, including both absolute and relative paths.
        /// </summary>
        /// <param name="path">The path for which to retrieve the parent directory.</param>
        /// <returns></returns>
        public static string GetParentDirectory(string path)
        {
            return Directory.GetParent(path).FullName;
        }

        /// <summary>
        /// Retrievs the directory of the exe.
        /// </summary>
        /// <returns></returns>
        public static string ExecutableDirectory()
        {
            return Path.GetDirectoryName(Application.ExecutablePath);
        }
        /// <summary>
        /// Retrievs the parent directory of the exe.
        /// </summary>
        /// <returns></returns>
        public static string ExecutableParentDirectory()
        {
            return GetParentDirectory(ExecutableDirectory());
        }



        public static string AvailableFileName(string fileName)
        {
            if (!File.Exists(fileName))
                return fileName;
            var path = Path.Combine(Path.GetDirectoryName(fileName), Path.GetFileNameWithoutExtension(fileName));
            var extension = Path.GetExtension(fileName);
            for (var i = 1; ; i++)
            {
                var fullPath = $"{path}({i}){extension}";
                if (!File.Exists(fullPath))
                    return fullPath;
            }
        }


        /// <summary>
        /// Creates empty file.
        /// </summary>
        /// <param name="path">Path of file.</param>
        /// <returns>Returns true if new file. Otherwise returns false.</returns>
        public static bool CreateFile(string path)
        {
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (!File.Exists(path))
            {
                File.Create(path).Close();
                return true;
            }

            return false;
        }
    }
}