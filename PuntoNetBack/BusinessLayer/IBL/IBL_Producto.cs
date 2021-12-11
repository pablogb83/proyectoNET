using DataAccessLayer.Dtos.Productos;
using DataAccessLayer.Helpers;
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
        List<ProductoReadDto> GetProductos();
        ProductoReadDto GetProducto(string id);
        bool UpdateProductoPrecio(double precio, string plan_id);
        bool EliminarProducto(string plan_id);

    }
}
