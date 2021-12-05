using DataAccessLayer.IDAL;
using Shared.ModeloDeDominio;
using DataAccessLayer.Helpers;
using System.Net.Http;
using DataAccessLayer.Dtos.Productos;
using Shared.Enum;
using AutoMapper;
using System.Collections.Generic;
using System.Globalization;

namespace DataAccessLayer.DAL
{
    public class DAL_Producto : IDAL_Producto

    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly MultiTenantStoreDbContext _context;
        private readonly IMapper _mapper;


        public DAL_Producto(IHttpClientFactory clientFactory,MultiTenantStoreDbContext context, IMapper mapper)
        {
            _clientFactory = clientFactory;
            _context = context;
            _mapper = mapper;
        }
        public bool CreateProducto(ProductoCreateDto producto)
        {
            var paypalTools = new PaypalUtil(_clientFactory);
            string token = paypalTools.getPayPalAccessToken();
            return paypalTools.createSuscriptionPlan(producto.Nombre, producto.Descripcion, producto.Precio, token);
        }

        public bool UpdateProductoPrecio(double precio, string plan_id)
        {
            var paypalTools = new PaypalUtil(_clientFactory);
            string token = paypalTools.getPayPalAccessToken();
            return paypalTools.UpdatePlanPrice(precio, token, plan_id);
        }

        public List<ProductoReadDto> GetProductos()
        {
            var paypalTools = new PaypalUtil(_clientFactory);
            string token = paypalTools.getPayPalAccessToken();
            var data = paypalTools.getSuscriptionPlans(token);
            return _mapper.Map<List<ProductoReadDto>>(data);
        }

        public ProductoReadDto GetProducto(string id)
        {
            var paypalTools = new PaypalUtil(_clientFactory);
            string token = paypalTools.getPayPalAccessToken();
            var planData = paypalTools.getSuscriptionPlan(token, id);
            var result = new ProductoReadDto();
            result.id = planData.id;
            result.name = planData.name;
            result.description = planData.description;
            result.price = double.Parse(planData.billing_cycles[0].pricing_scheme.fixed_price.value, CultureInfo.InvariantCulture); 
            return result;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}
