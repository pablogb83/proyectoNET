using BusinessLayer.IBL;
using DataAccessLayer.Dtos.Productos;
using DataAccessLayer.Helpers;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BL
{
    public class BL_Producto : IBL_Producto
    {

        private readonly DataAccessLayer.IDAL.IDAL_Producto _dal;

        public BL_Producto(DataAccessLayer.IDAL.IDAL_Producto dal)
        {
            _dal = dal;
        }

        public void SaveChanges()
        {
            _dal.SaveChanges();
        }

        public bool CreateProduct(ProductoCreateDto producto)
        {
            return _dal.CreateProducto(producto);
        }

        public List<ProductoReadDto> GetProductos()
        {
            return _dal.GetProductos();
        }

        public ProductoReadDto GetProducto(string id)
        {
            return _dal.GetProducto(id);
        }

        public bool UpdateProductoPrecio(double precio, string plan_id)
        {
            return _dal.UpdateProductoPrecio(precio, plan_id);
        }

        public bool EliminarProducto(string plan_id)
        {
            return _dal.EliminarProducto(plan_id);
        }

    }
}
