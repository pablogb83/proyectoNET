using DataAccessLayer.Dtos.Productos;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBL
{
    public interface IBL_Producto
    {
        bool CreateProduct(ProductoCreateDto producto);
        void SaveChanges();
    }
}
