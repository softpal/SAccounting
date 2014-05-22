using System;
using System.Net;
using System.Net.Mail;
using System.Text;

// ReSharper disable once CheckNamespace
namespace StratusAccounting.Models
{
    internal class SendMails
    {
        private readonly string _smtp = string.Empty;
        private readonly int _portNo;
        private readonly string _from = string.Empty;
        private readonly string _userCredentials = string.Empty;

        public SendMails()
        {
            _smtp = Helper.Helper.GetApplicationParamValue("RegSMTP");
            _portNo = Convert.ToInt32(Helper.Helper.GetApplicationParamValue("RegPort"));
            _from = Helper.Helper.GetApplicationParamValue("RegMailId");
            _userCredentials = Helper.Helper.GetApplicationParamValue("RegMailPwd");
        }

        public string ToMailId { private get; set; }

        public string Subject { private get; set; }

        public string Url { get; set; }

        public void SendRegistrationMails()
        {
            var body = new StringBuilder();
            body.Append("Hi <br/>Thanks for choosing Status for your accounting. <br/>Your registration went sucessfull. Please go through below link to activate the your account.");
            body.Append("<br/>");
            body.Append(Url);
            //
            using (var mes = new MailMessage())
            {
                var from = new MailAddress(_from, Helper.Helper.GetApplicationParamValue("RegMaildisplayName"));
                var to = new MailAddress(ToMailId);
                mes.From = from;
                mes.To.Add(to);
                mes.Subject = Subject;
                mes.IsBodyHtml = true;
                mes.Priority = MailPriority.High;
                mes.Body = body.ToString();
                var client = new SmtpClient(_smtp, _portNo)
                {
                    EnableSsl = Convert.ToBoolean(Helper.Helper.GetApplicationParamValue("RegEnableSSI")),
                    UseDefaultCredentials = Convert.ToBoolean(Helper.Helper.GetApplicationParamValue("RegDefaultCrd")),
                    Credentials = new NetworkCredential(_from, _userCredentials),
                    Port = _portNo
                };
                client.Send(mes);
            }
        }
    }
}