using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;

namespace Zek.Web
{
    /// <summary>
    /// Summary description for Old
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Old : WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string Pay(int amount)
        {
            //Task.Factory.StartNew(() =>
            //{
            //    var ws = new WSTest.TestServiceClient();
            //    ws.Echo(amount.ToString());
            //});



            

            return "Payed";
        }
    }
}
