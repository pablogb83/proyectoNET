using AutoMapper;
using BusinessLayer.IBL;
using DataAccessLayer.DAL;
using DataAccessLayer.Dtos.Persona;
using DataAccessLayer.Dtos.Usuarios;
using DataAccessLayer.Helpers;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Controllers
{
    //api/Usuario
    [Route("api/usuarios")]
    [ApiController]
    [Authorize(Roles = "ADMIN, SUPERADMIN")]

    public class UsuarioController : ControllerBase
    {
        private readonly IBL_Usuario _bl;
        private readonly IBL_Persona _blPersona;
        private readonly IBL_Institucion _blInst;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public UsuarioController(IBL_Usuario bl, IMapper mapper, IOptions<AppSettings> appSettings, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager,IBL_Institucion blInst, IBL_Persona blPersona)
        {
            _bl = bl;
            _blInst = blInst;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _userManager = userManager;
            _signInManager = signInManager;
            _blPersona = blPersona; 
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateAsync([FromBody] UsuarioAutenticateDto model)
        {
            var user = await _bl.Autenticar(model.Email, model.Password);

            if (user == null)
                return BadRequest(new { message = "Usuario o password incorrectos" });

            var role = await _bl.GetRolUsuario(user);
            if (role == null)
               return BadRequest(new { message = "Usuario sin rol asignado, contacte a su administrador" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            IdentityOptions _options = new IdentityOptions();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("TenantId", user.TenantId),
                    new Claim(_options.ClaimsIdentity.RoleClaimType, role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new
            {
                Id = user.Id,
                Email = user.Email,
                Token = tokenString,
                TenantId = user.TenantId,
                Role = role
            });
        }

        //GET api/usuarios

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioReadDto>>> GetAllUsuariosAsync()
        {
            var Usuario = await _bl.GetAllUsuariosAsync();
            return Ok(_mapper.Map<IEnumerable<UsuarioReadDto>>(Usuario));
        }

        [HttpGet("admin")]
        public async Task<ActionResult<IEnumerable<UsuarioReadDto>>> GetUsuariosAdmin()
        {
            var Usuario = await _bl.GetUsuariosAdmin();
            return Ok(_mapper.Map<IEnumerable<UsuarioReadDto>>(Usuario));
        }


        //GET api/usuarios/{id}
        [HttpGet("{id}", Name = "GetUsuarioById")]
        public ActionResult<UsuarioReadDto> GetUsuarioById(int id)
        {
            var Usuario = _bl.GetUsuarioByIdAsync(id);
            if (Usuario != null)
            {
                return Ok(_mapper.Map<UsuarioReadDto>(Usuario));
            }
            return NotFound();
        }

        //POST api/usuarios
        [HttpPost]
        public async Task<ActionResult<UsuarioReadDto>> CreateUsuarioAsync([FromBody] UsuarioCreateDto usuarioCreateDto)
        {
            var UsuarioModel = _mapper.Map<Usuario>(usuarioCreateDto);
            try
            {
                await _bl.CreateUsuarioAsync(UsuarioModel, usuarioCreateDto.PasswordPlano);
                _bl.SaveChanges();

                var UsuarioReadDto = _mapper.Map<UsuarioReadDto>(UsuarioModel);

                return CreatedAtRoute(nameof(GetUsuarioById), new { Id = UsuarioReadDto.Id }, UsuarioReadDto);
                //return Ok(commandReadDto);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("admin")]
        public async Task<ActionResult> CreateAdminAsync(AdminCreateDto usuarioCreateDto)
        {
            Institucion falsoTenant = new Institucion { Name ="FalsaInstitucion", Id="12131",Identifier="12131",Activa=true,PlanId="121212",Direccion="BENGOA", Suscripcion=new Suscripcion(),Telefono="098776123" };
            HttpContext.TrySetTenantInfo(falsoTenant, true);
            var tenantActual = HttpContext.GetMultiTenantContext<Institucion>();
            var UsuarioModel = _mapper.Map<Usuario>(usuarioCreateDto);
            try
            {
      
                await _bl.CreateAdminAsync(UsuarioModel, usuarioCreateDto.PasswordPlano);

                var UsuarioReadDto = _mapper.Map<UsuarioReadDto>(UsuarioModel);

                return CreatedAtRoute(nameof(GetUsuarioById), new { Id = UsuarioReadDto.Id }, UsuarioReadDto);
                //return Ok(commandReadDto);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //PUT api/usuarios/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUsuarioAsync(int id, UsuarioUpdateDto UsuarioUpdateDto)
        {
            var UsuarioModelFromRepo = await _bl.GetUsuarioByIdAsync(id);
            if (UsuarioModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(UsuarioUpdateDto, UsuarioModelFromRepo);
            _bl.UpdateUsuario(UsuarioModelFromRepo);
            _bl.SaveChanges();
            return Ok();
        }

        //PATCH api/usuarios/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUsuarioUpdtateAsync(int id, JsonPatchDocument<UsuarioUpdateDto> patchDoc)
        {
            var UsuarioModelFromRepo = await _bl.GetUsuarioByIdAsync(id);
            if (UsuarioModelFromRepo == null)
            {
                return NotFound();
            }

            var UsuarioToPatch = _mapper.Map<UsuarioUpdateDto>(UsuarioModelFromRepo);
            patchDoc.ApplyTo(UsuarioToPatch, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);
            if (!TryValidateModel(UsuarioToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(UsuarioToPatch, UsuarioModelFromRepo);
            _bl.UpdateUsuario(UsuarioModelFromRepo);
            _bl.SaveChanges();
            return NoContent();
        }

        //DELETE api/usuarios/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUsuarioAsync(int id)
        {
            var UsuarioModelFromRepo = await _bl.GetUsuarioByIdAsync(id);
            if (UsuarioModelFromRepo == null)
            {
                return NotFound();
            }
            _bl.DeleteUsuario(UsuarioModelFromRepo);
            _bl.SaveChanges();
            return NoContent();
        }

        //api/roles/addRoletoUser/
        [HttpPost("addRoletoUser")]
        public async Task<ActionResult> AddRoleUserAsync([FromBody] UserIdRolId parametros)
        {
            try
            {
                await _bl.AddRoleToUserAsync(parametros.RolId, parametros.UserId);
                return Ok();
            }
            catch (Exception)
            {
                return NoContent();

            }
        }

        [HttpPost("compareFaces")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> compareFaces()
        {
            try
            {
                var tenant = HttpContext.GetMultiTenantContext<Institucion>();
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var result = await DAL_FaceApi.ReconocimientoFacial(postedFile.OpenReadStream(), tenant.TenantInfo.Name);
                if (result!=null)
                {
                    Persona coincidencia = _blPersona.GetPersonaByDocumento(result.Name);
                    var personaReadDto = _mapper.Map<PersonaReadDto>(coincidencia);
                    return Ok(personaReadDto);
                }
                else
                {
                    return BadRequest(new { message = "No se encontro la persona"});
                }
            }
            catch (Exception)
            {
                return new JsonResult("No se pudo subir el archivo");
            }
        }
    }
}

