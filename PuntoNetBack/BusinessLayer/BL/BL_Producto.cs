using BusinessLayer.IBL;
using DataAccessLayer.Dtos.Productos;
using DataAccessLayer.Helpers;
using DataAccessLayer.IDAL;
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

        private readonly IDAL_Producto _dal;
        private readonly IDAL_Institucion _dalInst;

        public BL_Producto(IDAL_Producto dal, IDAL_Institucion dalInst)
        {
            _dal = dal;
            _dalInst = dalInst;
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
            var inst = _dalInst.GetInstitucionesProducto(plan_id);
            if (inst.Any())
            {
                throw new AppException("No se puede eliminar el producto, tiene instituciones asignadas");
            }
            return _dal.EliminarProducto(plan_id);
        }

    }
}
