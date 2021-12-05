using DataAccessLayer.Dtos.Productos;
using DataAccessLayer.Helpers;
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

        List<ProductoReadDto> GetProductos();

        ProductoReadDto GetProducto(string id);

        bool UpdateProductoPrecio(double precio, string plan_id);

        bool SaveChanges();
    }
}
