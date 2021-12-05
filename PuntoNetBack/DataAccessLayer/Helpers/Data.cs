
using Shared.Enum;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Helpers
{
    public class TokenInfo
    {
        public string scope { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string app_id { get; set; }
        public int expires_in { get; set; }
        public string nonce { get; set; }
    }

    public class ProductInfo
    {
        public ProductInfo(ProductNameEnum nombreProducto)
        {
            name = nombreProducto;
            description = "Servicio de gestion de puertas y accesos nivel: " + nombreProducto;
            type = "SERVICE";
            category = "SOFTWARE";
        }

        public ProductNameEnum name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string category { get; set; }

    }

    public class PaymentMethod
    {
        public PaymentMethod()
        {
            payer_selected = "PAYPAL";
            payee_preferred = "IMMEDIATE_PAYMENT_REQUIRED";
        }

        public string payer_selected { get; set; }
        public string payee_preferred { get; set; }
    }

    public class ApplicationContext
    {
        public ApplicationContext()
        {
            brand_name = "NetProject";
            locale = "en-US";
            user_action = "SUBSCRIBE_NOW";
            payment_method = new PaymentMethod();
            return_url = "http://127.0.0.1:4200/component/login";
            cancel_url = "http://127.0.0.1:4200/component/pago";
        }

        public static string brand_name { get; set; }
        public string locale { get; set; }
        public string user_action { get; set; }
        public PaymentMethod payment_method { get; set; }
        public string return_url { get; set; }
        public string cancel_url { get; set; }
    }

    public class SuscriptionInfo
    {
        public SuscriptionInfo(string custom)
        {
            plan_id = "P-97818393X7850501NMFRB3JQ";
            application_context = new ApplicationContext();
            custom_id = custom;
        }

        public string plan_id { get; set; }

        public string custom_id { get; set; }
        public ApplicationContext application_context { get; set; }
    }

    public class SuscriptionResponse
    {
        public string status { get; set; }
        public string id { get; set; }
        public DateTime create_time { get; set; }
        public List<Link> links { get; set; }
    }

    public class Name
    {
        public string given_name { get; set; }
        public string surname { get; set; }
    }

    public class Address
    {
        public string address_line_1 { get; set; }
        public string admin_area_2 { get; set; }
        public string admin_area_1 { get; set; }
        public string postal_code { get; set; }
        public string country_code { get; set; }
    }

    public class ShippingAddress
    {
        public Address address { get; set; }
    }

    public class Subscriber
    {
        public string email_address { get; set; }
        public string payer_id { get; set; }
        public Name name { get; set; }
        public ShippingAddress shipping_address { get; set; }
    }

    public class ShippingAmount
    {
        public string currency_code { get; set; }
        public string value { get; set; }
    }

    public class OutstandingBalance
    {
        public string currency_code { get; set; }
        public string value { get; set; }
    }

    public class CycleExecution
    {
        public string tenure_type { get; set; }
        public int sequence { get; set; }
        public int cycles_completed { get; set; }
        public int cycles_remaining { get; set; }
        public int current_pricing_scheme_version { get; set; }
        public int total_cycles { get; set; }
    }

    public class Amount
    {
        public string currency_code { get; set; }
        public string value { get; set; }
    }

    public class LastPayment
    {
        public Amount amount { get; set; }
        public DateTime time { get; set; }
    }

    public class BillingInfo
    {
        public OutstandingBalance outstanding_balance { get; set; }
        public List<CycleExecution> cycle_executions { get; set; }
        public LastPayment last_payment { get; set; }
        public DateTime next_billing_time { get; set; }
        public int failed_payments_count { get; set; }
    }

    public class LinkEncType
    {
        public string href { get; set; }
        public string rel { get; set; }
        public string method { get; set; }
        public string encType { get; set; }
    }

    public class Link
    {
        public string href { get; set; }
        public string rel { get; set; }
        public string method { get; set; }
    }

    public class Resource
    {
        public string quantity { get; set; }
        public Subscriber subscriber { get; set; }
        public DateTime create_time { get; set; }

        public string custom_id { get; set; }
        public bool plan_overridden { get; set; }
        public ShippingAmount shipping_amount { get; set; }
        public DateTime start_time { get; set; }
        public DateTime update_time { get; set; }
        public BillingInfo billing_info { get; set; }
        public List<LinkEncType> links { get; set; }
        public string id { get; set; }
        public string plan_id { get; set; }
        public string status { get; set; }
        public DateTime status_update_time { get; set; }
    }

    public class PaypalSuscriptionActivated
    {
        public string id { get; set; }
        public string event_version { get; set; }
        public string create_time { get; set; }
        public string resource_type { get; set; }
        public string resource_version { get; set; }
        public string event_type { get; set; }
        public string summary { get; set; }
        public Resource resource { get; set; }
        public List<Link> links { get; set; }
    }

    public class VerificationBody
    {
        public VerificationBody(string auth_algo, string cert_url, string transmission_id, string transmission_sig, string transmission_time,PaypalSuscriptionActivated webhook_event)
        {
            this.auth_algo = auth_algo;
            this.cert_url = cert_url;
            this.transmission_id = transmission_id;
            this.transmission_sig = transmission_sig;
            this.transmission_time = transmission_time;
            this.webhook_id = "7RS944148F691061T";
            this.webhook_event = webhook_event;
        }

        public string auth_algo { get; set; }
        public string cert_url { get; set; }
        public string transmission_id { get; set; }
        public string transmission_sig { get; set; }
        public string transmission_time { get; set; }
        public string webhook_id { get; set; }
        public PaypalSuscriptionActivated webhook_event { get; set; }
    }

    public class ImageInfo
    {
        public string FilePath { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string Metadata { get; set; } = string.Empty;
        public string Attributes { get; set; } = string.Empty;
        public string Caption { get; set; } = string.Empty;
        public string OcrResult { get; set; } = string.Empty;
        public string ThumbUrl { get; set; } = string.Empty; //"Assets/FaceFinder.jpg";
        public string Confidence { get; set; } = string.Empty;
    }

    //HASTA ACA TODO MAL

    public class Frequency
    {
        public Frequency()
        {
            interval_unit = "MONTH";
            interval_count = 1;
        }
        public string interval_unit { get; set; }
        public int interval_count { get; set; }
    }

    public class FixedPrice
    {
        public FixedPrice(double precio)
        {
            currency_code = "USD";
            value = precio.ToString();
        }
        public string value { get; set; }
        public string currency_code { get; set; }

    }

    public class PricingScheme
    {
        public PricingScheme(double precio)
        {
            fixed_price = new FixedPrice(precio);
            create_time = DateTime.Now;
            update_time = DateTime.Now;
        }
        public FixedPrice fixed_price { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
    }

    public class PricingSchemeUpdate
    {
        public PricingSchemeUpdate(double precio)
        {
            fixed_price = new FixedPrice(precio);
            roll_out_strategy = new RollOutStrategy();
        }
        public FixedPrice fixed_price { get; set; }
        public RollOutStrategy roll_out_strategy { get; set; }

    }

    public class RollOutStrategy
    {
        public RollOutStrategy()
        {
            effective_time = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            process_change_from = "NEXT_PAYMENT";
        }
        public string effective_time { get; set; }
        public string process_change_from { get; set; }
    }

    public class BillingCycle
    {
        public BillingCycle(double precio)
        {
            frequency = new Frequency();
            pricing_scheme = new PricingScheme(precio);
            tenure_type = "REGULAR";
            sequence = 1;
            total_cycles = 12;
        }
        public Frequency frequency { get; set; }
        public PricingScheme pricing_scheme { get; set; }
        public string tenure_type { get; set; }
        public int sequence { get; set; }
        public int total_cycles { get; set; }
    }

    public class PaymentPreferences
    {
        public PaymentPreferences()
        {
            auto_bill_outstanding = true;
        }
        public bool auto_bill_outstanding { get; set; }
    }

    public class PlanCreationBody
    {
        public  PlanCreationBody(string nombre, string descripcion, string estado, double precio, string product_id)
        {
            this.product_id = product_id;
            name = nombre;
            description = descripcion;
            status = estado;
            billing_cycles = new List<BillingCycle>();
            billing_cycles.Add(new BillingCycle(precio));
            payment_preferences = new PaymentPreferences();
        }

        public string product_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public List<BillingCycle> billing_cycles { get; set; }
        public PaymentPreferences payment_preferences { get; set; }
    }

    public class Plan
    {
        public string id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public string usage_type { get; set; }
        public DateTime create_time { get; set; }
        public List<Link> links { get; set; }
    }

    public class PlanSuscriptionInfo
    {
        public List<Plan> plans { get; set; }
        public List<Link> links { get; set; }
    }

    public class SetupFee
    {
        public string currency_code { get; set; }
        public string value { get; set; }
    }

    public class DetailedPlanInfo
    {
        public string id { get; set; }
        public string product_id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public string usage_type { get; set; }
        public List<BillingCycle> billing_cycles { get; set; }
        public PaymentPreferences payment_preferences { get; set; }
        public bool quantity_supported { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
        public List<Link> links { get; set; }
    }

    public class UpdateOptions
    {
        public string op { get; set; }
        public string path { get; set; }
        public string value { get; set; }
    }

    public class PricingUpdate
    {
        public PricingUpdate(PricingSchemeUpdate scheme)
        {
            billing_cycle_sequence = 1;
            pricing_scheme = scheme;
        }
        public int billing_cycle_sequence { get; set; }
        public PricingSchemeUpdate pricing_scheme { get; set; }
    }

    public class PricingUpdateList
    {
        public List<PricingUpdate> pricing_schemes { get; set; }
    }

}
