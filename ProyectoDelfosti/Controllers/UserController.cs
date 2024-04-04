using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Entidades;
using Microsoft.AspNetCore.Authorization;
using Entidades.request;

namespace ProyectoDelfosti.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        readonly IConfiguration _configuration;
        private string _secret;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
            _secret = configuration.GetValue<string>("ApiSettings:secret");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(login login)
        {
            try
            {
                Usuario usuario = new Usuario();
                usuario = await new User(_configuration).Login(login.correo, login.password);

                if (usuario.result == "Inicio de sesion Exitoso")
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_secret);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim(ClaimTypes.Name, usuario.nombre.ToString()),
                        new Claim(ClaimTypes.Role, usuario.rol,ToString())
                        }),

                        Expires = DateTime.UtcNow.AddHours(10),
                        SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    usuario.token = tokenHandler.WriteToken(token);
                }

                return Ok(usuario);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //[Authorize(Roles =("Encargado"))]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult>Get(string? rol)
        {
            try
            {
                rol= rol ?? string.Empty;
                return Ok(await new User(_configuration).Get(rol)) ;
            }
            catch (Exception)
            {
               return  BadRequest();
            }
        }
    }
}
