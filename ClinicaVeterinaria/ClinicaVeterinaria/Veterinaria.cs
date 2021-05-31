using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaVeterinaria
{
    public class Veterinaria
    {
        ConeccionBBDD coneccionsql = new ConeccionBBDD();
        public Veterinaria()
        {
            ListaClientes = new List<Cliente>();
            ListaReservaHoras = new List<ReservaHoras>();
            ListaVentas = new List<Venta>();
        }

        public List<ReservaHoras> ListaReservaHoras { get; set; }
        public List<Venta> ListaVentas { get; set; }
        public List<Cliente> ListaClientes { get; set; }

        public List<Cliente> ObtenerFichaClientes()
        {

            return ListaClientes.ToList();
        }

        public int TotalventasDirarias()
        {
            
            int totalventas = 0;

            totalventas = coneccionsql.listadeventas().Count();

            return totalventas;
        }

        public string TotalInventario()
        {
            string totalinventario;
            totalinventario=  coneccionsql.mostrarTotalinventario().Sum().ToString();
            return totalinventario;
        }

        public int MostrarReservasDiarias()
        {
            DateTime fecha = DateTime.Now;
            return coneccionsql.cantidaddeatencionesxdia(fecha); 
        }
      

        public Cliente ConsultarfichaPacienteporNombre(String Nombre, string  rut)
        {
            var Clienteconsulta = new Cliente();

            foreach (var Cliente in ListaClientes)
            {
                if(Cliente.NombreCliente.Equals(Nombre) && Cliente.Rut.Equals(rut))
                {
                   Clienteconsulta = Cliente;
                }
            }
            return Clienteconsulta;

        }


                
    }
}
