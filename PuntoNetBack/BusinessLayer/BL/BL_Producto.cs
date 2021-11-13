using BusinessLayer.IBL;
using DataAccessLayer.Dtos.Productos;
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
    }
}
