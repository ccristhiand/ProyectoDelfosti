using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Pedido
    {
        public Pedido()
        {
            this.productosPedidos = new List<ProductosPedido>();
        }
        public int idVendedor {  get; set; }
        public int idRepartidor { get; set; }
        public List<ProductosPedido>? productosPedidos { get; set; }
    }
    public class ProductosPedido
    {
        public int sku { get; set; }
        public int cant { get; set; }
    }
}
