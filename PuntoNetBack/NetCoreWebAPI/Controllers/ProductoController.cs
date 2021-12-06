using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Controllers
{
    using AutoMapper;
    using BusinessLayer.IBL;
    using DataAccessLayer.Dtos.Productos;
    using DataAccessLayer.Dtos.Salon;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Shared.ModeloDeDominio;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    namespace NetCoreWebAPI.Controllers
    {
        [Route("api/productos")]
        [ApiController]
        public class ProductoController : ControllerBase
        {
            private readonly IBL_Producto _bl;
            private readonly IMapper _mapper;
            private readonly ILogger<SalonController> _logger;

            public ProductoController(IBL_Producto bl, IMapper mapper, ILogger<SalonController> logger)
            {
                _bl = bl;
                _mapper = mapper;
                _logger = logger;
            }


            //POST api/salon
            [HttpPost]
            public ActionResult<SalonReadDto> CreateProducto(ProductoCreateDto productCreateDto)
            {
                var result = _bl.CreateProduct(productCreateDto);
                if (result)
                {
                    return Ok(new { message="Producto creado con exito" });
                }
                else
                {
                    return BadRequest();
                }
            }

            [HttpGet("{plan_id}")]
            public ActionResult GetProducto(string id)
            {
                return Ok(_bl.GetProducto(id));
            }

            [HttpGet]
            public ActionResult GetProductos()
            {
                return Ok(_bl.GetProductos());
            }

            [HttpPut]
            public ActionResult UpdateProduct(string plan_id, double precio)
            {
                if (_bl.UpdateProductoPrecio(precio, plan_id))
                {
                    return Ok(new { message="Precio actualizado con exito" });
                }
                else
                {
                    return BadRequest();
                }
            }
            [HttpDelete("{plan_id}")]
            public ActionResult DeleteProduct(string plan_id)
            {
                if (_bl.EliminarProducto(plan_id))
                {
                    return Ok(new { message = "Producto eliminado con exito" });
                }
                else
                {
                    return BadRequest();
                }
            }
        }
    }

}
