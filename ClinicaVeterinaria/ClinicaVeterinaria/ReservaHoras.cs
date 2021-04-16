using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaVeterinaria
{
    public class ReservaHoras
    {
        public ReservaHoras()
        {
            Medicos = new List<Medico>();
        }
        public Cliente cliente { get; set; }
        public TipoAtencion tipoAtencion { get; set; }

        public List<Medico> Medicos { get; set; }

        public DateTime Calendario { get; set; }

        public void GenerarReserva(Cliente cliente, TipoAtencion tipoAtencion, Medico medico, DateTime hora)
        {

        }

        public void CancelarHora(Cliente cliente, DateTime hora)
        {// puede entrar el rut de cliente en vez de entrar un objeto cliente

        }
    }
}
