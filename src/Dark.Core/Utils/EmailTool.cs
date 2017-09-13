using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 邮箱帮助类
/// </summary>
namespace Dark.Core.Utils
{
    public class EmailTool
    {
        //private static log
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="toEmailAddress">发送地址</param>
        /// <param name="subject">主题</param>
        /// <param name="content">内容</param>
        /// <param name="attachMents">附件</param>
        /// <param name="ccList">cc的集合</param>
        /// <returns></returns>
        public static string SendEmail(string toEmailAddress, string subject, string content, 
            Dictionary<string, Stream> attachMents = null, List<string> ccList = null)
        {
            string result = "";
            SmtpClient Client = new SmtpClient();
            Client.Host = ConfigurationManager.AppSettings["SmtpHost"];//指定SMTP服务器发送电子邮件
            Client.UseDefaultCredentials = false;
            //Client.EnableSsl = true;
            Client.Port = 25;
            Client.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            Client.Credentials = new NetworkCredential("support@medicalsystem.cn", "Liuzixin2008");
            MailMessage message = new MailMessage("support@medicalsystem.cn", toEmailAddress);
            message.Subject = subject;//主题
            message.Body = content; //内容
            message.IsBodyHtml = true;//设置为HTML格式
            message.BodyEncoding = System.Text.Encoding.UTF8;//正文编码
            message.Priority = MailPriority.High;//优先级
            //检查是否有附件发送
            if (attachMents != null)
            {
                foreach (KeyValuePair<string, Stream> item in attachMents)
                {
                    message.Attachments.Add(new Attachment(item.Value, item.Key));
                }
            }
            //检查是否有CC的地址
            if (ccList != null)
            {
                ccList.ForEach(u =>
                {
                    message.CC.Add(u);
                });
            }

            try
            {
                Client.Send(message);
                return result;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return result;
            }
            finally
            {
                message.Dispose();
            }
        }
    }
}
