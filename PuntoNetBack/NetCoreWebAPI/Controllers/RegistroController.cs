using AutoMapper;
using BusinessLayer.IBL;
using DataAccessLayer.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using NetCoreWebAPI.Helpers;
using Shared.ModeloDeDominio;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Controllers
{
    [Route("api/registro")]
    [ApiController]
    public class RegistroController : ControllerBase
    {
        private readonly IBL_Registro _bl;
        private readonly IMapper _mapper;

        public RegistroController(IBL_Registro bl, IMapper mapper)
        {
            _bl = bl;
            _mapper = mapper;
        }


        [HttpPost]
        [Route("confirm")]
        public ActionResult webHookTest(PaypalSuscriptionActivated suscription)
        {
            Ok();
            _bl.AuthorizePayment(Request.Headers,suscription);
            return Ok(new { data = suscription.event_type });
        }

    }
}
