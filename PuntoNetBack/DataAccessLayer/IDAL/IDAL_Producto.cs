using DataAccessLayer.Dtos.Productos;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDAL
{
    public interface IDAL_Producto
    {
        bool CreateProducto(ProductoCreateDto producto);

        bool SaveChanges();
    }
}
