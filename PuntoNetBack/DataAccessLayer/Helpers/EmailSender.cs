using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using DataAccessLayer.Helpers;

namespace DataAccessLayer.Helpers
{
    public class EmailSender
    {
        public static void sendEmail()
        {
            if (!Configuration.Default.ApiKey.ContainsKey("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"))
            {
                Configuration.Default.ApiKey.Add("api-key", "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");
            }
            var apiInstance = new TransactionalEmailsApi();
            string SenderName = "Pablo Gaione";
            string SenderEmail = "pablo.gaione@estudiantes.utec.edu.uy";
            SendSmtpEmailSender Email = new SendSmtpEmailSender(SenderName, SenderEmail);
            string ToEmail = "gaionepablo@gmail.com";
            string ToName = "Pablo Gaione";
            SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(ToEmail, ToName);
            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(smtpEmailTo);
            List<SendSmtpEmailBcc> Bcc = null;
            List<SendSmtpEmailCc> Cc = null;
            string HtmlContent = "<html><body><h1>This is my first transactional email {{params.parameter}}</h1></body></html>";
            string TextContent = "Hola esto es una prueba";
            string Subject = "My {{params.subject}}";
            SendSmtpEmailReplyTo ReplyTo = null;
            string AttachmentUrl = null;
            string stringInBase64 = "aGVsbG8gdGhpcyBpcyB0ZXN0";
            byte[] Content = System.Convert.FromBase64String(stringInBase64);
            string AttachmentName = "test.txt";
            SendSmtpEmailAttachment AttachmentContent = new SendSmtpEmailAttachment(AttachmentUrl, Content, AttachmentName);
            List<SendSmtpEmailAttachment> Attachment = null;
            JObject Headers = new JObject();
            Headers.Add("Some-Custom-Name", "unique-id-1234");
            long? TemplateId = null;
            JObject Params = new JObject();
            Params.Add("parameter", "My param value");
            Params.Add("subject", "Probando");
            List<string> Tags = new List<string>();
            Tags.Add("mytag");
            Dictionary<string, object> _parmas = new Dictionary<string, object>();
            _parmas.Add("params", Params);
            List<SendSmtpEmailMessageVersions> messageVersiopns = null;
            try
            {
                var sendSmtpEmail = new SendSmtpEmail(Email, To, Bcc, Cc, HtmlContent, TextContent, Subject, ReplyTo, Attachment, Headers, TemplateId, Params, messageVersiopns, Tags);
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                Debug.WriteLine(result.ToJson());
                Console.WriteLine(result.ToJson());
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}

