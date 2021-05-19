using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaVeterinaria
{
    public class Cliente
    {
        ConeccionBBDD coneccionsql = new ConeccionBBDD();
        public Cliente()
        {
            Mascotas = new List<Mascota>();
        }

        public List<Mascota> Mascotas { get; set; }
        public string Rut { get; set; }
        public string NombreCliente { get; set; }
        public string direccion { get; set; }
        public string Correo { get; set; }

        public string apellido { get; set; }

        public void AgregarMascota(string Rut,  Mascota mascota)
        {
            coneccionsql.agregarmascota(mascota.Nombre, mascota.FechaNacimiento, mascota.tipoMascota);
            int id = coneccionsql.buscarmascota();

            String[] listaclientes = new String[coneccionsql.trearidcliente().Count];
            for (int i = 0; i < coneccionsql.trearidcliente().Count; i++)
            {
                string linea = coneccionsql.trearidcliente()[i].ToString();
                listaclientes = linea.Split(';');

                if (listaclientes[1].Equals(Rut))
                {
                    coneccionsql.agregarpaciente(id, int.Parse(listaclientes[0]));
                }

            }
        }

        public string crearcliente()
        {
           string rutdevuelto =  coneccionsql.AgregarCliente(this.NombreCliente, this.apellido,this.Rut, this.direccion, this.Correo);
            return rutdevuelto;
        }

        
    }
}
