using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaVeterinaria
{
    public class ReservaHoras
    {
        ConeccionBBDD Coneccion = new ConeccionBBDD();
        public ReservaHoras()
        {
            Medicos = new List<Medico>();
        }
        public Cliente cliente { get; set; }
        public TipoAtencion tipoAtencion { get; set; }

        public List<Medico> Medicos { get; set; }

        public DateTime Calendario { get; set; }
        
        public string hora { get; set; }

        public DateTime Fecha { get; set; }

        public void GenerarReserva(string rut, TipoAtencion tipoAtencion, DateTime fecha, string hora , string nombremascota)
        {
            var mascota = new Mascota();
            var medico = new Medico();
            var listamascotasxdueno= Coneccion.buscarmascotasxrutdueno(rut);

            string idduenio = string.Empty;

            for (int i = 0; i < listamascotasxdueno.Count; i++)
            {
                string linea = listamascotasxdueno[i].ToString();
                var info = linea.Split(';');
                idduenio = info[4];

                if (info[1].Contains(nombremascota))
                {
                    mascota.Id_Mascota = int.Parse(info[0]);
                }
            }

            string idpaciente  = Coneccion.buscarpaciente(idduenio, mascota.Id_Mascota.ToString());

            var datosmedico = Coneccion.datosmedico(tipoAtencion.ToString());

            for (int i = 0; i < datosmedico.Count; i++)
            {
                string linea = datosmedico[i].ToString();
                var datos = linea.Split(';');
                medico.Id_Medico = int.Parse(datos[0]);
                medico.NombreMedico = datos[1];
                medico.rut = datos[2];
            }

            Coneccion.Generarhora(idpaciente, medico.Id_Medico, fecha, hora);
        }

        public void CancelarHora(string id, DateTime fecha, string hora)
        {
            Coneccion.eliminarconsulta(id, fecha, hora);
        }

        public List<string> Consultarhorapaciente(string rut, DateTime date, string nombremascota)
        {
            var paciente = Coneccion.Consultarhoradepaciente(rut,date,nombremascota);

            return paciente;
        }
    }
}
