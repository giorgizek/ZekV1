using System.Text;
using System.Net.Mail;

namespace Zek.Net
{
    /// <summary>
    /// ელ.ფოსტის დამხარე კლასი (გაგზავნა, ცონვერტაცია).
    /// </summary>
    public class MailHelper
    {
        public static void Send(string from, string to)
        {
            Send(from, to, null, null);
        }
        public static void Send(string from, string to, string subject, string body)
        {
            Send(null, from, to, subject, body);
        }

        public static void Send(string smtpHost, string from, string to, string subject, string body)
        {
            Send(smtpHost, 0, from, to, subject, body);
        }
        public static void Send(string smtpHost, string from, string to, string subject, string body, bool isBodyHtml)
        {
            Send(smtpHost, 0, from, to, body, body, isBodyHtml);
        }
        public static void Send(string smtpHost, string from, string to, string subject, string body, bool isBodyHtml, Encoding bodyEncoding)
        {
            Send(smtpHost, 0, from, to, body, body, isBodyHtml, bodyEncoding);
        }
        public static void Send(string smtpHost, string from, string to, string subject, string body, bool isBodyHtml, Encoding bodyEncoding, Encoding subjectEncoding)
        {
            Send(smtpHost, 0, from, to, body, body, isBodyHtml, bodyEncoding, subjectEncoding);
        }
        public static void Send(string smtpHost, string from, string to, string subject, string body, bool isBodyHtml, Encoding bodyEncoding, Encoding subjectEncoding, MailPriority priority)
        {
            Send(smtpHost, 0, from, to, body, body, isBodyHtml, bodyEncoding, subjectEncoding, priority);
        }

        public static void Send(string smtpHost, int smtpPort, string from, string to, string subject, string body)
        {
            Send(smtpHost, smtpPort, from, to, subject, body, false);
        }
        public static void Send(string smtpHost, int smtpPort, string from, string to, string subject, string body, bool isBodyHtml)
        {
            Send(smtpHost, smtpPort, from, to, subject, body, isBodyHtml, null);
        }
        public static void Send(string smtpHost, int smtpPort, string from, string to, string subject, string body, bool isBodyHtml, Encoding bodyEncoding)
        {
            Send(smtpHost, smtpPort, from, to, subject, body, isBodyHtml, bodyEncoding, null);
        }
        public static void Send(string smtpHost, int smtpPort, string from, string to, string subject, string body, bool isBodyHtml, Encoding bodyEncoding, Encoding subjectEncoding)
        {
            Send(smtpHost, smtpPort, from, to, subject, body, isBodyHtml, bodyEncoding, subjectEncoding, MailPriority.Normal);
        }
        public static void Send(string smtpHost, int smtpPort, string from, string to, string subject, string body, bool isBodyHtml, Encoding bodyEncoding, Encoding subjectEncoding, MailPriority priority)
        {
            using (var message = new MailMessage())
            {
                if (!string.IsNullOrEmpty(from))
                    message.From = new MailAddress(from);

                message.To.Add(to);

                if (!string.IsNullOrEmpty(subject))
                    message.Subject = subject;

                if (!string.IsNullOrEmpty(body))
                    message.Body = body;

                if (bodyEncoding != null)
                    message.BodyEncoding = bodyEncoding;
                if (subjectEncoding != null)
                    message.SubjectEncoding = subjectEncoding;

                message.IsBodyHtml = isBodyHtml;
                message.Priority = priority;

                Send(smtpHost, smtpPort, message);
            }
        }

        public static void Send(string smtpHost, MailMessage message)
        {
            Send(smtpHost, 0, message);
        }
        public static void Send(string smtpHost, int smtpPort, MailMessage message)
        {
            var client = new SmtpClient();
            if (!string.IsNullOrEmpty(smtpHost))
                client.Host = smtpHost;
            if (smtpPort != 0)
                client.Port = smtpPort;

            client.Send(message);
        }

        /// <summary>
        /// გადაყავს ელ. ფოსტა ბოტებისთვის დაცულ ტექსტად.
        /// მაგ: mymail@mydomain.com => mymail at mydomain dot com
        /// </summary>
        /// <param name="email">ელ. ფოსტა.</param>
        /// <returns>აბრუნებს დაკონვერტებულ ელ. ფოსტას.</returns>
        public static string EmailToCaptcha(string email)
        {
            return EmailToCaptcha(email, " at ");
        }
        /// <summary>
        /// გადაყავს ელ. ფოსტა ბოტებისთვის დაცულ ტექსტად.
        /// მაგ: mymail@mydomain.com => mymail at mydomain dot com
        /// </summary>
        /// <param name="email">ელ. ფოსტა.</param>
        /// <param name="at">@-ის ნიშანი რა ტექსტად გადაკეთდეს.</param>
        /// <returns>აბრუნებს დაკონვერტებულ ელ. ფოსტას.</returns>
        public static string EmailToCaptcha(string email, string at)
        {
            return EmailToCaptcha(email, at, " dot ");
        }
        /// <summary>
        /// გადაყავს ელ. ფოსტა ბოტებისთვის დაცულ ტექსტად.
        /// მაგ: mymail@mydomain.com => mymail at mydomain dot com
        /// </summary>
        /// <param name="email">ელ. ფოსტა.</param>
        /// <param name="at">@-ის ნიშანი რა ტექსტად გადაკეთდეს.</param>
        /// <param name="dot">წერტილი რა ტექსტად გადაკეთდეს.</param>
        /// <returns>აბრუნებს დაკონვერტებულ ელ. ფოსტას.</returns>
        public static string EmailToCaptcha(string email, string at, string dot)
        {
            return email.Replace("@", at).Replace(".", dot);
        }

        /// <summary>
        /// გადაყავს ელ. ფოსტა ბოტებისთვის დაცულ ტექსტად.
        /// მაგ: mymail at mydomain dot com => mymail@mydomain.com
        /// </summary>
        /// <param name="captcha">კაპჩა.</param>
        /// <returns>აბრუნებს ელ. ფოსტას.</returns>
        public static string CaptchaToEmail(string captcha)
        {
            return CaptchaToEmail(captcha, " at ");
        }
        /// <summary>
        /// გადაყავს ელ. ფოსტა ბოტებისთვის დაცულ ტექსტად.
        /// მაგ: mymail at mydomain dot com => mymail@mydomain.com
        /// </summary>
        /// <param name="captcha">კაპჩა.</param>
        /// <param name="at">@-ის ნიშანი რა ტექსტად არის გადაკეთებული.</param>
        /// <returns>აბრუნებს ელ. ფოსტას.</returns>
        public static string CaptchaToEmail(string captcha, string at)
        {
            return CaptchaToEmail(captcha, at, " dot ");
        }
        /// <summary>
        /// გადაყავს ელ. ფოსტა ბოტებისთვის დაცულ ტექსტად.
        /// მაგ: mymail at mydomain dot com => mymail@mydomain.com
        /// </summary>
        /// <param name="captcha">კაპჩა.</param>
        /// <param name="at">@-ის ნიშანი რა ტექსტად არის გადაკეთებული.</param>
        /// <param name="dot">წერტილი რა ტექსტად არის გადაკეთებული.</param>
        /// <returns>აბრუნებს ელ. ფოსტას.</returns>
        public static string CaptchaToEmail(string captcha, string at, string dot)
        {
            return captcha.Replace(at, "@").Replace(dot, ".");
        }
    }
}
