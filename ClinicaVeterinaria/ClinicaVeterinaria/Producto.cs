using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaVeterinaria
{
    public class Producto
    {
        public int IdProducto { get; set; }

        public string NombreProducto { get; set; }

        public int CantidadProducto { get; set; }

        public int PrecioProducto { get; set; }

        public string InfoProducto { get; set; }

        public TipoProducto Tipoproducto { get; set; }


        public void Eliminarproducto(Producto producto)
        {
            // debe eliminar entrando un objeto producto
        }
        
        public void AgregarProducto(Producto producto)
        {
            // debe agregar un objeto producto
        }


    }
}
