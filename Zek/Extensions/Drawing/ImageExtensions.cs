﻿using System;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Zek.Extensions.Drawing
{
    [Serializable]
    public enum MimeTypes
    {
        BMP,
        JPEG,
        GIF,
        TIFF,
        PNG
    }

    public static class ImageExtensions
    {
        public static byte[] ToByteArray(this Image image, ImageFormat imageFormat)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, imageFormat);
                return ms.ToArray();
            }
        }

        public static Bitmap CreateThumbnail(this Image image, int thumbnailSize)
        {
            int w;
            int h;
            if (image.Width > image.Height)
            {
                w = thumbnailSize < image.Width ? thumbnailSize : image.Width;
                h = image.Height / (image.Width / w);
            }
            else
            {
                h = thumbnailSize < image.Height ? thumbnailSize : image.Height;
                w = image.Width / (image.Height / h);
            }
            var newImage = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            using (var canvas = Graphics.FromImage(newImage))
            {
                canvas.SmoothingMode = SmoothingMode.AntiAlias;
                canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                canvas.PixelOffsetMode = PixelOffsetMode.HighQuality;
                canvas.DrawImage(image, new Rectangle(0, 0, w, h));
            }
            return newImage;
        }

        public static bool IsValidMimeType(string mimeType)
        {
            string[] mimeTypes = {
                "image/bmp",
                "image/jpg", "image/jpeg", "image/pjpeg",
                "image/gif",
                "image/tiff",
                "image/png", "image/x-png"
            };
            return !string.IsNullOrEmpty(mimeType) && IsStringInArray(mimeType, mimeTypes);
        }
        public static bool IsValidMimeType(byte[] buffer)
        {
            try
            {
                Image.FromStream(new MemoryStream(buffer)).Dispose();
            }
            catch { return false; }

            return true;
        }
        public static bool IsValidImage(string filename)
        {
            try
            {
                Image.FromFile(filename).Dispose();
                //Image newImage = Image.FromFile(filename);
            }
            catch { return false; }
            //catch (OutOfMemoryException ex)
            //{
            //    // Image.FromFile will throw this if file is invalid.
            //    // Don't ask me why.
            //    return false;
            //}
            return true;
        }
        public static bool IsValidImage(Stream stream)
        {
            try
            {
                Image.FromStream(stream).Dispose();
            }
            catch { return false; }

            return true;
        }


        private static bool IsStringInArray(string value, string[] array)
        {
            if (string.IsNullOrEmpty(value)) return false;

            for (var i = 0; i < array.Length; i++)
                if (value.Equals(array[i])) return true;
            //if (array[i].IndexOf(value) >= 0) return true;

            return false;
        }


        public static string GetMimeType(MimeTypes mimeType)
        {
            switch (mimeType)
            {
                case MimeTypes.BMP:
                    return "image/bmp";

                case MimeTypes.JPEG:
                    return "image/jpeg";

                case MimeTypes.GIF:
                    return "image/gif";

                case MimeTypes.TIFF:
                    return "image/tiff";

                case MimeTypes.PNG:
                    return "image/png";
            }

            throw new ArgumentNullException(nameof(mimeType), "ფორმატი არასწორია.");
        }


        public static ImageCodecInfo GetEncoderInfo(MimeTypes mimeType)
        {
            return GetEncoderInfo(GetMimeType(mimeType));
        }
        public static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            int i;
            var encoders = ImageCodecInfo.GetImageEncoders();
            for (i = 0; i < encoders.Length; ++i)
            {
                if (encoders[i].MimeType == mimeType)
                    return encoders[i];
            }
            return null;
        }


        public static void SaveToFile(Bitmap bitmap, string filePath, long qualityPercent, MimeTypes mimeType)
        {
            SaveToFile(bitmap, filePath, qualityPercent, GetMimeType(mimeType));
        }
        public static void SaveToFile(Bitmap bitmap, string filePath, long qualityPercent, string mimeType)
        {
            var imageCodecInfo = GetEncoderInfo(mimeType);
            var encoder = Encoder.Quality;
            using (var encoderParameters = new EncoderParameters(1))
            {
                encoderParameters.Param[0] = new EncoderParameter(encoder, qualityPercent);
                bitmap.Save(filePath, imageCodecInfo, encoderParameters);
            }
        }


        public static Bitmap CreateThumbnail(string filename, int thumbnailSize)
        {
            return Image.FromFile(filename).CreateThumbnail(thumbnailSize);
        }
        public static Bitmap CreateThumbnail(byte[] buffer, int thumbnailSize)
        {
            return Image.FromStream(new MemoryStream(buffer)).CreateThumbnail(thumbnailSize);
        }
        public static Bitmap CreateThumbnail(Stream stream, int thumbnailSize)
        {
            return Image.FromStream(stream).CreateThumbnail(thumbnailSize);
        }

        /*public static Bitmap CreateThumbnail(Image image, int thumbnailSize)
        {
            image = image.GetThumbnailImage(thumbnailSize, thumbnailSize, null, new IntPtr());
            Bitmap newImage = new Bitmap(thumbnailSize, thumbnailSize, PixelFormat.Format24bppRgb);
            using (Graphics canvas = Graphics.FromImage(newImage))
            {
                canvas.SmoothingMode = SmoothingMode.AntiAlias;
                canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                canvas.PixelOffsetMode = PixelOffsetMode.HighQuality;
                canvas.DrawImage(image, new Rectangle(0, 0, w, h));
            }
            return newImage;
        }*/
        /*public static Bitmap CreateThumbnail(Image image, int thumbnailSize)
        {
            int w;
            int h;
            if (image.Width > image.Height)
            {
                h = thumbnailSize;
                w = (int)((image.Width * (float)h) / (float)image.Height);
            }
            else
            {
                w = thumbnailSize;
                h = (int)((image.Height * (float)w) / (float)image.Width);
            }
            Bitmap newImage = new Bitmap(thumbnailSize, thumbnailSize, PixelFormat.Format24bppRgb);
            using (Graphics canvas = Graphics.FromImage(newImage))
            {
                canvas.SmoothingMode = SmoothingMode.AntiAlias;
                canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                canvas.PixelOffsetMode = PixelOffsetMode.HighQuality;
                canvas.DrawImage(image, new Rectangle(0, 0, w, h));
            }
            return newImage;
        }*/



        public static void CreateThumbnailFile(string filePath, int thumbnailSize, string thumbnailFilePath)
        {
            CreateThumbnailFile(filePath, thumbnailSize, thumbnailFilePath, 90L);
        }
        public static void CreateThumbnailFile(string filePath, int thumbnailSize, string thumbnailFilePath, long qualityPercent)
        {
            CreateThumbnailFile(filePath, thumbnailSize, thumbnailFilePath, qualityPercent, MimeTypes.JPEG);
        }
        public static void CreateThumbnailFile(string filePath, int thumbnailSize, string thumbnailFilePath, long qualityPercent, MimeTypes mimeTypes)
        {
            CreateThumbnailFile(filePath, thumbnailSize, thumbnailFilePath, qualityPercent, GetMimeType(mimeTypes));
        }
        public static void CreateThumbnailFile(string filePath, int thumbnailSize, string thumbnailFilePath, long qualityPercent, string mimeType)
        {
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                using (var image = Image.FromStream(stream))
                {
                    if (image.Width == thumbnailSize && image.Height == thumbnailSize)
                    {
                        File.Copy(filePath, thumbnailFilePath, true);
                    }
                    else
                    {
                        using (var bitmap = image.CreateThumbnail(thumbnailSize))
                        {
                            SaveToFile(bitmap, thumbnailFilePath, qualityPercent, mimeType);
                        }
                    }
                }
            }
        }


        public static void CreateThumbnailFile(byte[] buffer, int thumbnailSize, string thumbnailFilePath)
        {
            CreateThumbnailFile(buffer, thumbnailSize, thumbnailFilePath, 90L);
        }
        public static void CreateThumbnailFile(byte[] buffer, int thumbnailSize, string thumbnailFilePath, long qualityPercent)
        {
            CreateThumbnailFile(buffer, thumbnailSize, thumbnailFilePath, qualityPercent, MimeTypes.JPEG);
        }
        public static void CreateThumbnailFile(byte[] buffer, int thumbnailSize, string thumbnailFilePath, long qualityPercent, MimeTypes mimeType)
        {
            CreateThumbnailFile(buffer, thumbnailSize, thumbnailFilePath, qualityPercent, GetMimeType(mimeType));
        }
        public static void CreateThumbnailFile(byte[] buffer, int thumbnailSize, string thumbnailFilePath, long qualityPercent, string mimeType)
        {
            CreateThumbnailFile(new MemoryStream(buffer), thumbnailSize, thumbnailFilePath, qualityPercent, mimeType);
        }


        public static void CreateThumbnailFile(Stream stream, int thumbnailSize, string thumbnailFilePath)
        {
            CreateThumbnailFile(stream, thumbnailSize, thumbnailFilePath, 90L);
        }
        public static void CreateThumbnailFile(Stream stream, int thumbnailSize, string thumbnailFilePath, long qualityPercent)
        {
            CreateThumbnailFile(stream, thumbnailSize, thumbnailFilePath, qualityPercent, MimeTypes.JPEG);
        }
        public static void CreateThumbnailFile(Stream stream, int thumbnailSize, string thumbnailFilePath, long qualityPercent, MimeTypes mimeType)
        {
            CreateThumbnailFile(stream, thumbnailSize, thumbnailFilePath, qualityPercent, GetMimeType(mimeType));
        }
        public static void CreateThumbnailFile(Stream stream, int thumbnailSize, string thumbnailFilePath, long qualityPercent, string mimeType)
        {
            using (var image = Image.FromStream(stream))
            {
                CreateThumbnailFile(image, thumbnailSize, thumbnailFilePath, qualityPercent, mimeType);
            }
        }


        public static void CreateThumbnailFile(this Image image, int thumbnailSize, string thumbnailFilePath)
        {
            CreateThumbnailFile(image, thumbnailSize, thumbnailFilePath, 90L);
        }
        public static void CreateThumbnailFile(this Image image, int thumbnailSize, string thumbnailFilePath, long qualityPercent)
        {
            CreateThumbnailFile(image, thumbnailSize, thumbnailFilePath, qualityPercent, MimeTypes.JPEG);
        }
        public static void CreateThumbnailFile(this Image image, int thumbnailSize, string thumbnailFilePath, long qualityPercent, MimeTypes mimeType)
        {
            CreateThumbnailFile(image, thumbnailSize, thumbnailFilePath, qualityPercent, GetMimeType(mimeType));
        }
        public static void CreateThumbnailFile(this Image image, int thumbnailSize, string thumbnailFilePath, long qualityPercent, string mimeType)
        {
            using (var bitmap = image.CreateThumbnail(thumbnailSize))
            {
                SaveToFile(bitmap, thumbnailFilePath, qualityPercent, mimeType);
            }
        }
    }
}
