using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.Productos
{
    public class ProductoReadDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public string status { get; set; }
    }
}
