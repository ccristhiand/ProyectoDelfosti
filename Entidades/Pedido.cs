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

    public class DetallePedido
    {
        public int numeroPedido { get; set; }
        public DateTime fechaPedido { get; set; }
        public DateTime fechaRecepcion { get; set; }
        public DateTime fechaDespacho { get; set; }
        public DateTime fechaEntrega { get; set; }
        public int sku { get; set; }
        public string? NombreProducto { get; set; }
        public int cantidad { get; set; }
        public double precio { get; set; }
        public double total { get; set; }
        public string? vendedor { get; set; }
        public string? repartidor { get; set; }
    }
}
