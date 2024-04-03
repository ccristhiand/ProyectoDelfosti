using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace ProyectoDelfosti.Controllers
{
    [ApiController]
    [Route("producto")]
    public class ProductoController : ControllerBase
    {

        readonly IConfiguration _configuration;
        public ProductoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Authorize(Roles = ("Encargado"))]
        [HttpGet]
        public async Task<IActionResult> Get(int sku)
        {
            try
            {
                return Ok(await new Producto(_configuration).Get(sku));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
