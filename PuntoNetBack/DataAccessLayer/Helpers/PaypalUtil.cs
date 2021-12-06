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
using System.Diagnostics;

namespace DataAccessLayer.Helpers
{
    public class PaypalUtil
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string client_id = "AVmQaL-kdm-qDBwhTK8Mqjdyj0y813I8CLceNgUdUOIo7tQosCLHOuKSa8SO_FeUR40eV1W_Rvu8GEHG";
        private readonly string client_secret = "EGMCKHHljcAp_jqwvXyqgqwG1RKEU124AWTa5h66Y7pOn1VkT9rQt_rIsGUV9mW6xKKxlszm5pBlbFdg";
        public PaypalUtil(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public string getPayPalAccessToken()
        {
            try
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
            catch (Exception e)
            {
                Debug.WriteLine("Error en la conexion con paypal");
                return null;
            }
        }
        public string createProduct(string token, ProductNameEnum nombreProducto)
        {
            try
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
            catch (Exception e)
            {
                Debug.WriteLine("Error en la conexion con paypal");
                return null;
            }
        }

        public bool createSuscriptionPlan(string nombre, string descripcion, double precio, string token)
        {
            try
            {
                string product_id = "PROD-6A298268V69316926";
                var client = new RestClient("https://api-m.sandbox.paypal.com/v1/billing/plans") { Encoding = Encoding.UTF8 };
                client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", token));
                PlanCreationBody body = new PlanCreationBody(nombre, descripcion, "ACTIVE", precio, product_id);
                var authRequest = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
                authRequest.RequestFormat = DataFormat.Json;
                authRequest.AddJsonBody(body);
                var authResponse = client.Execute(authRequest);
                string content = authResponse.Content;
                SuscriptionResponse obj = JsonConvert.DeserializeObject<SuscriptionResponse>(content);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error en la conexion con paypal");
                return false;
            }
        }

        public bool UpdatePlanPrice(double precio, string token, string plan_id)
        {
            try
            {
                var client = new RestClient("https://api-m.sandbox.paypal.com/v1/billing/plans/" + plan_id + "/update-pricing-schemes") { Encoding = Encoding.UTF8 };
                client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", token));
                var update = new PricingSchemeUpdate(precio);
                var precioActualizado = new List<PricingUpdate>();
                precioActualizado.Add(new PricingUpdate(update));
                var body = new PricingUpdateList();
                body.pricing_schemes = precioActualizado;
                var jsonBody = JsonConvert.SerializeObject(body);
                var authRequest = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
                authRequest.RequestFormat = DataFormat.Json;
                authRequest.AddJsonBody(body);
                var authResponse = client.Execute(authRequest);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error en la conexion con paypal");
                return false;
            }
        }

        public bool DeactivatePlan(string plan_id, string token)
        {
            try
            {
                var client = new RestClient("https://api-m.sandbox.paypal.com/v1/billing/plans/" + plan_id + "/deactivate") { Encoding = Encoding.UTF8 };
                client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", token));
                var authRequest = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
                var authResponse = client.Execute(authRequest);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error en la conexion con paypal");
                return false;
            }
        }

        public List<Plan> getSuscriptionPlans(string token)
        {
            try
            {
                var client = new RestClient("https://api-m.sandbox.paypal.com/v1/billing/plans") { Encoding = Encoding.UTF8 };
                client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", token));
                client.AddDefaultHeader("Prefer", "return=representation");
                var authRequest = new RestRequest(Method.GET) { RequestFormat = DataFormat.Json };
                authRequest.RequestFormat = DataFormat.Json;
                var authResponse = client.Execute(authRequest);
                string content = authResponse.Content;
                PlanSuscriptionInfo obj = JsonConvert.DeserializeObject<PlanSuscriptionInfo>(content);
                return obj.plans;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error en la conexion con paypal");
                return null;
            }
        }

        public List<Transaction> getFacturasSuscripcion(string token, string subid, DateTime fechainicio, DateTime fechafin)
        {
            try
            {
                var client = new RestClient("https://api-m.sandbox.paypal.com/v1/billing/subscriptions/" + subid + "/transactions?start_time="+fechainicio.ToString("yyyy-MM-ddTHH:mm:ss") +"z&end_time="+ fechafin.ToString("yyyy-MM-ddTHH:mm:ss")+"z") { Encoding = Encoding.UTF8 };
                client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", token));
                var authRequest = new RestRequest(Method.GET) { RequestFormat = DataFormat.Json };
                authRequest.RequestFormat = DataFormat.Json;
                var authResponse = client.Execute(authRequest);
                string content = authResponse.Content;
                FacturasSuscripcion obj = JsonConvert.DeserializeObject<FacturasSuscripcion>(content);
                return obj.transactions;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error en la conexion con paypal");
                return null;
            }
        }

        public DetailedPlanInfo getSuscriptionPlan(string token, string id)
        {
            try
            {
                var client = new RestClient("https://api-m.sandbox.paypal.com/v1/billing/plans/" + id) { Encoding = Encoding.UTF8 };
                client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", token));
                var authRequest = new RestRequest(Method.GET) { RequestFormat = DataFormat.Json };
                authRequest.RequestFormat = DataFormat.Json;
                var authResponse = client.Execute(authRequest);
                string content = authResponse.Content;
                DetailedPlanInfo obj = JsonConvert.DeserializeObject<DetailedPlanInfo>(content);
                return obj;
            }
            catch(Exception e)
            {
                Debug.WriteLine("Error en la conexion con paypal");
                return null;
            }
           
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
