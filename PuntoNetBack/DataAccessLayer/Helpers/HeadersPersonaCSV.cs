using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Helpers
{
    public class HeadersPersonaCSV
    {
        public static List<string> HeaderCSV()
        {
            List<string> headers = new List<string>();
            headers.Add("Nombres");
            headers.Add("Apellidos");
            headers.Add("Telefono");
            headers.Add("Email");
            headers.Add("tipo_doc");
            headers.Add("nro_doc");
            headers.Add("PhotoFileName");
            return headers;
        }
    }
}
