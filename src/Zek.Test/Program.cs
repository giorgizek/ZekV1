using System;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;
using PolicyService;
using Zek.Cryptography;
using Zek.Utils;

namespace Zek.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(PasswordHelper.Generate(1, 4, 1, 1));
            }

            //var sw1 = new Stopwatch();
            //sw1.Start();
            //for (var i = 0; i < 1000000; i++)
            //{
            //    //Console.WriteLine(PasswordHelper.Generate(5, 5, 1, 1));
            //    StringHelper.FindCount("Hey jon, this is sort of a offtopic question but you seem to know a lot: In what situation would one not use ", "giou");
            //}
            //sw1.Stop();
            //Console.WriteLine(StringHelper.FindCount("Hey jon, this is sort of a offtopic question but you seem to know a lot: In what situation would one not use ", "giou"));
            //Console.WriteLine("Method 1: " + sw1.ElapsedMilliseconds);



            //var sw2 = new Stopwatch();
            //sw2.Start();
            //for (var i = 0; i < 1000000; i++)
            //{
            //    //Console.WriteLine(PasswordHelper.Generate(5, 5, 1, 1));
            //    StringHelper.FindCount2("Hey jon, this is sort of a offtopic question but you seem to know a lot: In what situation would one not use ", "giou");
            //}
            //sw1.Stop();
            //Console.WriteLine(StringHelper.FindCount2("Hey jon, this is sort of a offtopic question but you seem to know a lot: In what situation would one not use ", "giou"));
            //Console.WriteLine("Method 2: " + sw2.ElapsedMilliseconds);



            Console.ReadKey();


            //var ws = new PolicyService.PolicyClient();
            //var result = ws.PayPolicyAsync(new RequestOfNewPolicyPaymentRequest
            //{
            //    ApplicationKey = "GPIH",
            //    RequestID = "1",
            //    Value = new NewPolicyPaymentRequest
            //    {
            //        PolicyNumber = "PPMED 0000001",
            //        Amount = 10,
            //        PaymentDateTime = DateTime.Now,
            //        TransactionID = "TRAN 0000001"
            //    }

            //}).Result;


            //var json = JsonConvert.SerializeObject(result);
            //Console.WriteLine(json);

            //Console.ReadKey();
            //ძ







            return;
            var texts = new string[]
            {
                "1234567890",
                "2015-05-25 12:12:12",
                "Giorgi",
                "Zekalashvili",
                "Zekalashvili",
                "Zekalashvili",
                "Zekalashvili",
                "Zekalashvili"
            };


            var key = "key";
            var count = 1000000;
            var idlinkSha1 = IdLinkHelper.Encode(texts, key);
            var idlinkMd5 = IdLinkHelper.Encode(texts, key, IdLinkMode.MD5);
            var idlinkAes = IdLinkHelper.Encode(texts, key, IdLinkMode.Aes);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                //var idlink = IdLinkHelper.Encode(texts, key);
                IdLinkHelper.Decode(idlinkSha1, key);
            }
            stopwatch.Start();
            Console.WriteLine("Sha1 " + stopwatch.ElapsedMilliseconds);

            stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                //var idlink = IdLinkHelper.Encode(texts, key, IdLinkMode.MD5);
                IdLinkHelper.Decode(idlinkMd5, key, IdLinkMode.MD5);
            }
            stopwatch.Start();
            Console.WriteLine("Md5 " + stopwatch.ElapsedMilliseconds);


            stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                //var idlink = IdLinkHelper.Encode(texts, key, IdLinkMode.Aes);
                IdLinkHelper.Decode(idlinkAes, key, IdLinkMode.Aes);
            }
            stopwatch.Start();
            Console.WriteLine("Aes " + stopwatch.ElapsedMilliseconds);
















            stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                //var idlink = IdLinkHelper.Encode(texts, key);
                IdLinkHelper.Decode(idlinkSha1, key);
            }
            stopwatch.Start();
            Console.WriteLine("Sha1 " + stopwatch.ElapsedMilliseconds);

            stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                //var idlink = IdLinkHelper.Encode(texts, key, IdLinkMode.MD5);
                IdLinkHelper.Decode(idlinkMd5, key, IdLinkMode.MD5);
            }
            stopwatch.Start();
            Console.WriteLine("Md5 " + stopwatch.ElapsedMilliseconds);


            stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < count; i++)
            {
                //var idlink = IdLinkHelper.Encode(texts, key, IdLinkMode.Aes);
                IdLinkHelper.Decode(idlinkAes, key, IdLinkMode.Aes);
            }
            stopwatch.Start();
            Console.WriteLine("Aes " + stopwatch.ElapsedMilliseconds);








            Console.ReadKey();

            //var select = new SelectStatement("T_Policy", "Policy", "p");
            ////select.Top = 100;

            //select.Count = true;
            //select.Paging = new Paging(2);




            //File.WriteAllText(@"D:\sql.txt", select.ToSql());
        }
    }
}