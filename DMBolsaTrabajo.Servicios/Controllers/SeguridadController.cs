using DMBolsaTrabajo.Aplicacion;
using DMBolsaTrabajo.Dto.Seguridad;
using DMBolsaTrabajo.IAplicacion;
using DMBolsaTrabajo.Utilitarios;
using DMBolsaTrabajo.Utilitarios.EstadoRespuesta;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DMBolsaTrabajo.Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SeguridadController : ControllerBase
    {

        protected readonly ISeguridadAplicacion _SeguridadAplicacion;
        private readonly IConfiguration _configuration;

        public SeguridadController(ISeguridadAplicacion seguridadAplicacion, IConfiguration configuration)
        {
            _SeguridadAplicacion = seguridadAplicacion;
            _configuration = configuration;

        }

        [HttpPost("iniciarSesion")]
        [AllowAnonymous]
        public async Task<ActionResult<Respuesta>> IniciarSesion([FromBody] SeguridadRequestDto request)
        {
            request.Password = Contrasenias.HashPassword(request.Password);

            try
            {
                var respuesta = new Respuesta();

                respuesta = await _SeguridadAplicacion.IniciarSesion(request);

                if (respuesta.success)
                {
                    SeguridadResponseDto objRpt = (SeguridadResponseDto)respuesta.data;

                    var claims = new ClaimsIdentity(new[] {
                    new Claim("Id", objRpt.Id.ToString()),
                    //new Claim(ClaimTypes.Name, request.Codigo),
                    new Claim(ClaimTypes.Name,objRpt.Id.ToString()),
                    new Claim("Usuario", request.Usuario),
                    new Claim("Nombres", objRpt.Nombres.ToString()),
                    new Claim("ApellidoPaterno", objRpt.ApellidoPaterno.ToString()),
                    new Claim("ApellidoMaterno", objRpt.ApellidoMaterno.ToString()),
                    new Claim("RolId", objRpt.IdRol.ToString()),
                    new Claim("Email", objRpt.Email.ToString().Split("|")[0]),
                    new Claim(ClaimTypes.Role, objRpt.IdRol.ToString())
                });
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claims,
                        Expires = DateTime.UtcNow.AddMinutes(600),
                        SigningCredentials = credentials,
                        Audience = _configuration["Jwt:Audience"],
                        Issuer = _configuration["Jwt:Issuer"],
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                    respuesta.data = tokenHandler.WriteToken(createdToken);
                    return respuesta;
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("solicitarCambioContrasena")]
        [AllowAnonymous]
        public async Task<ActionResult<Respuesta>> EnviarCorreoRecuperacion(string email, string url)
        {
            return Ok(await _SeguridadAplicacion.EnviarCorreoRecuperacion(email, url));
        }

        [HttpGet("validarCredencialesRestablecer")]
        [AllowAnonymous]
        public async Task<ActionResult<Respuesta>> validarCredencialesRestablecer(int IdUsuario, string Token)
        {
            return Ok(await _SeguridadAplicacion.ValidarCredencialesRestablecer(IdUsuario, Token));
        }

        [HttpPatch("cambiarClave")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(RespuestaGen<Int32>))]
        public async Task<ActionResult<Respuesta>> CambiarClave([FromBody] ClaveRequestDto request)
        {
            request.Password1 = Contrasenias.HashPassword(request.Password1);
            request.Password2 = Contrasenias.HashPassword(request.Password2);
            return await _SeguridadAplicacion.CambiarClave(request);
        }

        [HttpPatch("cambiarClaveNoAuth")]
        [AllowAnonymous]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(RespuestaGen<Int32>))]
        public async Task<ActionResult<Respuesta>> CambiarClaveNoAuth([FromBody] ClaveRequestDto request)
        {
            request.Password1 = Contrasenias.HashPassword(request.Password1);
            return await _SeguridadAplicacion.CambiarClaveNoAuth(request);
        }
    }
}
