using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;

namespace Travel_Request_System_EF.Mail
{

    public class SendMail
    {
        public List<string> ToAddresses;
        public string MailBody;
        public string MailSubject;
        public List<MailAttachment> MailAttachments;

        public void Send()
        {
            string host = ConfigurationManager.AppSettings["SMTPHost"];
            string fromAddress = ConfigurationManager.AppSettings["SMTPFrom"];
            string smtpUname = ConfigurationManager.AppSettings["SMTPUname"];
            string smtpPass = ConfigurationManager.AppSettings["SMTPPass"];
            try
            {
                MailMessage mail = new MailMessage
                {
                    Body = MailBody,
                    IsBodyHtml = true,
                    SubjectEncoding = Encoding.UTF8,
                    Priority = MailPriority.Normal
                };

                foreach (var item in ToAddresses)
                {
                    mail.To.Add(new MailAddress(item));

                }
                mail.From = new MailAddress(fromAddress);
                mail.Subject = MailSubject;

                foreach (MailAttachment ma in MailAttachments)
                {
                    mail.Attachments.Add(ma.File);
                }

                SmtpClient smtp = new SmtpClient
                {
                    Host = host,
                    Credentials = new NetworkCredential(smtpUname, smtpPass),
                    EnableSsl = false
                };
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder(1024);
                sb.Append("\nTo:" + string.Join(", ", ToAddresses.ToArray()));
                sb.Append("\nbody:" + MailBody);
                sb.Append("\nsubject:" + MailSubject);
                sb.Append("\nfromAddress:" + fromAddress);
                sb.Append("\nHosting:" + host);
                throw new Exception(sb.ToString(), ex);
            }
        }

        public string ListToString<T>(List<T> inputList, params Expression<Func<T, object>>[] fxns)
        {
            return GetMyTable(inputList, fxns);
        }

        public static string GetMyTable<T>(IEnumerable<T> list, params Expression<Func<T, object>>[] fxns)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("<TABLE>\n");

            sb.Append("<TR>\n");
            foreach (var fxn in fxns)
            {
                sb.Append("<TH>");
                sb.Append(GetName(fxn));
                sb.Append("</TH>");
            }
            sb.Append("</TR> <!-- HEADER -->\n");


            foreach (var item in list)
            {
                sb.Append("<TR>\n");
                foreach (var fxn in fxns)
                {
                    sb.Append("<TD>");
                    sb.Append(fxn.Compile()(item));
                    sb.Append("</TD>");
                }
                sb.Append("</TR>\n");
            }
            sb.Append("</TABLE>");

            return sb.ToString();
        }

        private static string GetName<T>(Expression<Func<T, object>> expr)
        {
            var member = expr.Body as MemberExpression;
            if (member != null)
            {
                return GetName2(member);
            }

            var unary = expr.Body as UnaryExpression;
            if (unary != null)
            {
                return GetName2((MemberExpression)unary.Operand);
            }

            return "?+?";
        }

        private static string GetName2(MemberExpression member)
        {
            var fieldInfo = member.Member as FieldInfo;
            if (fieldInfo != null)
            {
                var d = fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (d != null)
                {
                    return d.Description;
                }

                return fieldInfo.Name;
            }

            var propertInfo = member.Member as PropertyInfo;
            if (propertInfo != null)
            {
                var d = propertInfo.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (d != null)
                {
                    return d.Description;
                }

                return propertInfo.Name;
            }

            return "?-?";
        }
    }
}