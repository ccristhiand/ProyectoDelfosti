using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Producto
    {
        public int sku { get; set; }
        public string? nombre { get; set; }
        public int stock { get; set; }
        public int? idTipoProducto { get; set; }
        public string? etiquetas { get; set; }
        public double precio { get; set; }
        public int idUnidadMedida { get; set; }
    }
}
