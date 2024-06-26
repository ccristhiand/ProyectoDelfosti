﻿using Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace ProyectoDelfosti.Controllers
{
    [ApiController]
    [Route("pedido")]
    public class PedidoController : ControllerBase
    {
        readonly IConfiguration _configuration;
        public PedidoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [Authorize(Roles ="Encargado,Vendedor")]
        [HttpPost]
        public async Task<IActionResult> Post(Entidades.Pedido pedido)
        {
            try
            {
                return Ok(await new Model.Pedido(_configuration).Post(pedido));
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [Authorize(Roles = "Encargado,Delivery,Repartidor")]
        [HttpPatch]
        public async Task<IActionResult> Patch(int Estado, int numeroPedido)
        {
            try
            {
                return Ok(await new Model.Pedido(_configuration).Patch(Estado, numeroPedido));
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        //[Authorize(Roles = "Encargado,Vendedor,Delivery,Repartidor")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get(int pedido)
        {
            try
            {
                return Ok(await new Model.Pedido(_configuration).Get(pedido));
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
