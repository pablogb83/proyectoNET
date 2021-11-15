using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;
using System.Threading.Tasks;using System.Text;
using Newtonsoft.Json.Linq;
using Shared.ModeloDeDominio;
using Shared.Enum;

namespace DataAccessLayer.Helpers
{
    public class PaypalUtil
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string client_id = "ASHw5T-b50JuZ31FXCp1mp3ddJKDC_EPIJMm_kaznk89IU5ikuCDHgKoSuhnUCBHzT_lJIYASn0wD3Gs";
        private readonly string client_secret = "EJXh9R3GuYyHQXrmVS85Sc1-RsD-EIjYF0hDjRT6lOtnTnUUkWm2Oh0vzYkXofO_kAP0LJKE93uqq5K9";
        public PaypalUtil(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public string getPayPalAccessToken()
        {
            var client = new RestClient("https://api-m.sandbox.paypal.com/v1/oauth2/token") { Encoding = Encoding.UTF8 };
            var authRequest = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
            client.Authenticator = new HttpBasicAuthenticator(client_id, client_secret);
            authRequest.AddParameter("grant_type", "client_credentials");
            var authResponse = client.Execute(authRequest);
            string content = authResponse.Content;
            TokenInfo obj = JsonConvert.DeserializeObject<TokenInfo>(content);
            return obj.access_token;
        }
        public string createProduct(string token, ProductNameEnum nombreProducto)
        {
            var client = new RestClient("https://api-m.sandbox.paypal.com/v1/catalogs/products") { Encoding = Encoding.UTF8 };
            client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", token));
            ProductInfo body = new ProductInfo(nombreProducto);
            var authRequest = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
            authRequest.RequestFormat = DataFormat.Json;
            authRequest.AddJsonBody(body);
            var authResponse = client.Execute(authRequest);
            string content = authResponse.Content;
            SuscriptionResponse obj = JsonConvert.DeserializeObject<SuscriptionResponse>(content);
            return obj.links[0].href;
        }
        public bool authorizePayment(IHeaderDictionary headers, PaypalSuscriptionActivated body, string token)
        {
            var client = new RestClient("https://api-m.sandbox.paypal.com/v1/notifications/verify-webhook-signature") { Encoding = Encoding.UTF8 };
            client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", token));
            var authRequest = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
            authRequest.RequestFormat = DataFormat.Json;
            string auth_algo = headers["paypal-auth-algo"];
            string cert_url = headers["paypal-cert-url"];
            string transmission_id = headers["paypal-transmission-id"];
            string transmission_sig = headers["paypal-transmission-sig"];
            string transmission_time = headers["paypal-transmission-time"];
            string codyJson = Newtonsoft.Json.JsonConvert.SerializeObject(body);
            VerificationBody verBody = new VerificationBody(auth_algo, cert_url, transmission_id, transmission_sig, transmission_time, body);
            authRequest.AddJsonBody(verBody);
            string jsonString = System.Text.Json.JsonSerializer.Serialize(body);
            var authResponse = client.Execute(authRequest);
            JObject status = JObject.Parse(authResponse.Content) as JObject;
            var verification_status = status["verification_status"];
            if (verification_status.ToString().Equals("SUCCESS"))
            {
                return true;
            }
            return false;
        }

    }
}
