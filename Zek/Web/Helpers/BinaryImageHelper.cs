using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Image = System.Drawing.Image;

namespace Zek.Web
{
    public class BinaryImageHelper
    {
        public static void PostImage(HttpResponse response, string id)
        {
            var image = FindImage(id);
            WriteBinaryImage(response, image);
        }

        private static void WriteBinaryImage(HttpResponse response, byte[] image)
        {
            if (image != null)
            {
                response.ContentType = "image/jpeg";
                using (var ms = new MemoryStream(image))
                {
                    using (var bmp = (Bitmap)Image.FromStream(ms))
                    {
                        bmp.Save(response.OutputStream, ImageFormat.Jpeg);
                    }
                }
            }
            else
            {
                response.ContentType = "image/gif";
            }
            response.End();
        }

        private static byte[] FindImage(string id)
        {
            var ds = new AccessDataSource("~/App_Data/nwind.mdb", $"select Photo from [Employees] where employeeid={id}");
            var view = (DataView)ds.Select(DataSourceSelectArguments.Empty);
            if (view != null && view.Count > 0) return view[0][0] as byte[];
            return null;
        }
    }

}
