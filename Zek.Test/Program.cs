using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Zek.Core;
using Zek.Security;
using Zek.Service.SMS;
using Zek.Test.CC;

namespace Zek.Test
{

    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            var clientHandler = new HttpClientHandler();
            var clientCertificate = new X509Certificate2(File.ReadAllBytes(@"d:\securepay.ufc.ge_5300559_merchant_wp.p12"), "VDj65rfkMHGDyj56m"/*, X509KeyStorageFlags.MachineKeySet*/);

            //ServicePointManager.ServerCertificateValidationCallback += delegate
            //{
            //    return true;
            //};

            //clientHandler.ClientCertificates.Add(clientCertificate);
            //clientHandler.ServerCertificateCustomValidationCallback += (message, certificate2, x509Chain, sslPolicyErrors) => true;


            //using (var client = new HttpClient(clientHandler))
            //{
            //    var response = await client.PostAsync(_serverUrl + "?" + parameters, new StringContent(string.Empty));
            //    response.EnsureSuccessStatusCode();
            //    return await response.Content.ReadAsStringAsync();
            //}


            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new Form3());
        }

    }

}
