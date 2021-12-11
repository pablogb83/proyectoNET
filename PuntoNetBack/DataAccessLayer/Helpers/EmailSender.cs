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
using DataAccessLayer.Dtos.Productos;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Helpers
{
    public class EmailSender
    {
        public static void sendEmail(string emailTo, string name, ProductoReadDto producto)
        {
            if (!Configuration.Default.ApiKey.ContainsKey("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"))
            {
                Configuration.Default.ApiKey.Add("api-key", "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");
            }
            var apiInstance = new TransactionalEmailsApi();
            string SenderName = "Pablo Gaione";
            string SenderEmail = "pablo.gaione@estudiantes.utec.edu.uy";
            SendSmtpEmailSender Email = new SendSmtpEmailSender(SenderName, SenderEmail);
            string ToEmail = emailTo;
            string ToName = name;
            SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(ToEmail, ToName);
            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(smtpEmailTo);
            List<SendSmtpEmailBcc> Bcc = null;
            List<SendSmtpEmailCc> Cc = null;
            string HtmlContent = "<html><body><h1>Suscripción realizada exitosamente a {{params.parameter}}</h1><br><h4>Producto {{params.productoNombre}}</h4><br><h4>Precio {{params.precio}}</h4><br><h4>Descripción {{params.productoDescrip}}</h4></body></html>";
            string TextContent = "Hola esto es una prueba";
            string Subject = "{{params.subject}}";
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
            Params.Add("parameter", "Puertan");
            Params.Add("subject", "Suscripción exitosa");
            Params.Add("precio", producto.price);
            Params.Add("productoNombre", producto.name);
            Params.Add("productoDescrip", producto.description);
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
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Console.WriteLine(e.Message);
            }
        }
    }
}

