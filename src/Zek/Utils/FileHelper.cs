using System;
using System.IO;

namespace Zek.Utils
{

    public static class FileHelper
    {
        public static string ToComputerSize(string fileName, bool useAbbreviations = false)
        {
            return ToComputerSize(new FileInfo(fileName).Length, useAbbreviations);
        }

        public static string ToComputerSize(long value, bool useAbbreviations = false)
        {
            double valor = value;
            long i;
            var names = useAbbreviations
                ? new[] {"Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"}
                : new[] {"Bytes", "KBytes", "MBytes", "GBytes", "TBytes", "PBytes", "EBytes", "ZBytes", "YBytes"};
            for (i = 0; i < names.Length && valor >= 1024; i++)
                valor /= 1024.0;
            return $"{valor:#,###.00} {names[i]}";
        }

        public static string GetAvailableFileName(string fileName)
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


        public static void CreateDirectoryIfNotExists(string file)
        {
            var dir = Path.GetDirectoryName(file);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        //public static bool CreateFile(string path)
        //{
        //    var dir = Path.GetDirectoryName(path);
        //    if (!Directory.Exists(dir))
        //        Directory.CreateDirectory(dir);

        //    if (!File.Exists(path))
        //    {
        //        File.Create(path);
        //        return true;
        //    }

        //    return false;
        //}
    }
}
