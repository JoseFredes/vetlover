using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaVeterinaria
{
    public class Mascota
    {
        public int Id_Mascota { get; set; }
        public string Nombre { get; set; }
        public TipoMascota tipoMascota { get; set; }
        public DateTime FechaNacimiento { get; set; }
        
          
    }
}
