using DataAccessLayer.IDAL;
using Shared.ModeloDeDominio;
using DataAccessLayer.Helpers;
using System.Net.Http;
using DataAccessLayer.Dtos.Productos;
using Shared.Enum;

namespace DataAccessLayer.DAL
{
    public class DAL_Producto : IDAL_Producto

    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly MultiTenantStoreDbContext _context;

        public DAL_Producto(IHttpClientFactory clientFactory,MultiTenantStoreDbContext context)
        {
            _clientFactory = clientFactory;
            _context = context;
        }
        public bool CreateProducto(ProductoCreateDto producto)
        {
            var paypalTools = new PaypalUtil(_clientFactory);
            string token = paypalTools.getPayPalAccessToken();
            string id = paypalTools.createProduct(token, ProductNameEnum.CHICO);
            return true;
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
