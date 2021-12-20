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
using BusinessLayer.IBL;
using Shared.ModeloDeDominio;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Authorization;

namespace NetCoreWebAPI.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        private readonly IBL_Usuario _bl;
        private readonly IBL_Producto _blProd;
        private readonly IBL_Institucion _blInst;

        public EmailController(IBL_Usuario bl, IBL_Producto blProd, IBL_Institucion blInst)
        {
            _bl = bl;
            _blProd = blProd;
            _blInst = blInst;
        }     


        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ActionResult SendEmail()
        {
            Configuration.Default.ApiKey.Add("api-key", "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");

            var apiInstance = new TransactionalEmailsApi();
            string SenderName = "Pablo Gaione";
            string SenderEmail = "pablo.gaione@estudiantes.utec.edu.uy";
            SendSmtpEmailSender Email = new SendSmtpEmailSender(SenderName, SenderEmail);
            string ToEmail = "gaionepablo@gmail.com";
            string ToName = "Pablo Gaione";
            SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(ToEmail, ToName);
            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(smtpEmailTo);
            string BccName = "Janice Doe";
            string BccEmail = "pablogaione@hotmail.com";
            SendSmtpEmailBcc BccData = new SendSmtpEmailBcc(BccEmail, BccName);
            List<SendSmtpEmailBcc> Bcc = new List<SendSmtpEmailBcc>();
            Bcc.Add(BccData);
            string CcName = "John Doe";
            string CcEmail = "example3@example2.com";
            SendSmtpEmailCc CcData = new SendSmtpEmailCc(CcEmail, CcName);
            List<SendSmtpEmailCc> Cc = new List<SendSmtpEmailCc>();
            Cc.Add(CcData);
            string HtmlContent = "<html><body><h1>This is my first transactional email {{params.parameter}}</h1></body></html>";
            string TextContent = "Hola esto es una prueba";
            string Subject = "My {{params.subject}}";
            string ReplyToName = "John Doe";
            string ReplyToEmail = "replyto@domain.com";
            SendSmtpEmailReplyTo ReplyTo = new SendSmtpEmailReplyTo(ReplyToEmail, ReplyToName);
            string AttachmentUrl = null;
            string stringInBase64 = "aGVsbG8gdGhpcyBpcyB0ZXN0";
            byte[] Content = System.Convert.FromBase64String(stringInBase64);
            string AttachmentName = "test.txt";
            SendSmtpEmailAttachment AttachmentContent = new SendSmtpEmailAttachment(AttachmentUrl, Content, AttachmentName);
            List<SendSmtpEmailAttachment> Attachment = new List<SendSmtpEmailAttachment>();
            Attachment.Add(AttachmentContent);
            JObject Headers = new JObject();
            Headers.Add("Some-Custom-Name", "unique-id-1234");
            long? TemplateId = 1;
            JObject Params = new JObject();
            Params.Add("parameter", "My param value");
            Params.Add("subject", "Probando");
            List<string> Tags = new List<string>();
            Tags.Add("mytag");
            SendSmtpEmailTo1 smtpEmailTo1 = new SendSmtpEmailTo1(ToEmail, ToName);
            List<SendSmtpEmailTo1> To1 = new List<SendSmtpEmailTo1>();
            To1.Add(smtpEmailTo1);
            Dictionary<string, object> _parmas = new Dictionary<string, object>();
            _parmas.Add("params", Params);
            SendSmtpEmailReplyTo1 ReplyTo1 = new SendSmtpEmailReplyTo1(ReplyToEmail, ReplyToName);
            SendSmtpEmailMessageVersions messageVersion = new SendSmtpEmailMessageVersions(To1, _parmas, Bcc, Cc, ReplyTo1, Subject);
            List<SendSmtpEmailMessageVersions> messageVersiopns = new List<SendSmtpEmailMessageVersions>();
            messageVersiopns.Add(messageVersion);
            try
            {
                var sendSmtpEmail = new SendSmtpEmail(Email, To, Bcc, Cc, HtmlContent, TextContent, Subject, ReplyTo, Attachment, Headers, TemplateId, Params, messageVersiopns, Tags);
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                Debug.WriteLine(result.ToJson());
                Console.WriteLine(result.ToJson());
                return Ok("Email enviado");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Console.WriteLine(e.Message);
            }
            return NoContent();

        }

        [HttpPost ("simple/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> SendSimpleEmail(string id)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault().Value);
                var tenant = HttpContext.GetMultiTenantContext<Institucion>();
                //provisorio para la demo de la defensa, se necesita internet libre
                tenant.TenantInfo.Activa = true;
                var susc = new Suscripcion();
                susc.Id = id;
                susc.estado = "ACTIVE";
                tenant.TenantInfo.Suscripcion = susc; 
                _blInst.SaveChanges();
                //******************************//
                string planId = tenant.TenantInfo.PlanId;
                var producto = _blProd.GetProducto(planId);
                var user = await _bl.GetUsuarioByIdAsync(userId);
                EmailSender.sendEmail(user.Email, user.UserName, producto);
                return Ok(new { message = "Email enviado" });
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Console.WriteLine(e.Message);
                return BadRequest();
            }
        }
    }
}
