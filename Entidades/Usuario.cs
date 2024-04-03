using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Entidades
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public int idRol { get; set; }
        public string? nombre { get; set; }
        public string? rol { get; set; }
        public string? correo { get; set; }
        public string? telefono { get; set; }
        public string? puesto { get; set; }
        public string? result { get; set; }
        public string? token { get; set; }  

    }
    public class UsuarioGet
    {
        public int idPersona { get; set; }
        public string? Nombre { get; set; }
        public string? tepefono { get; set; }
        public string? puesto { get; set; }
        public int idRol { get; set; }
        public string? Rol { get; set; }
    }
}
