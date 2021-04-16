using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaVeterinaria
{
    public class Cliente
    {
        public Cliente()
        {
            Mascotas = new List<Mascota>();
        }

        public List<Mascota> Mascotas { get; set; }
        public string Rut { get; set; }
        public string NombreCliente { get; set; }
        public string direccion { get; set; }
        public string InfoAdicional { get; set; }
        public string Correo { get; set; }

        public void AgregarMascota(int Rut, string Nombrecliente)
        {
            // no se si para agregar una mascota debe ingresar el rut del cliente a la función

        }
    }
}
