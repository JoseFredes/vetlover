using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClinicaVeterinaria
{
    /// <summary>
    /// Lógica de interacción para ventanaagenda.xaml
    /// </summary>
    public partial class ventanaagenda : Window
    {
        public ventanaagenda()
        {
            InitializeComponent();
           
        }

        public void llenarcalendario(DateTime fecha)
        { 
            var coneccion = new ConeccionBBDD();
            this.lblfechadia.Content = fecha.ToString("yyyy/MM/dd");
            var listahorarios = coneccion.traerdatosparacalendario(fecha);
            var listamascotas = coneccion.listapacientes();

            for (int i = 0; i < listahorarios.Count; i++)
            {
                var hora = listahorarios[i].ToString().Split(';');
                for (int x = 0; x < listamascotas.Count; x++)
                {
                    var paciente = listamascotas[x].ToString().Split(';');

                    if (hora[2].Equals("9:00 AM"))
                    {
                        if (paciente[0].Equals(hora[0]))
                        {
                            this.lbl9.Content = "Nombre: " + paciente[1] + " Tipo:" + paciente[2];
                        }
                       
                    }
                    if (hora[2].Equals("9:30 AM"))
                    {
                        if (paciente[0].Equals(hora[0]))
                        {
                            this.lbl930.Content = "Nombre: " + paciente[1] + " Tipo:" + paciente[2];
                        }
                        
                    }
                    if (hora[2].Equals("10:00 AM"))
                    {
                        if (paciente[0].Equals(hora[0]))
                        {
                            this.lbl10.Content = "Nombre: " + paciente[1] + " Tipo:" + paciente[2];
                        }
                       
                    }
                    if (hora[2].Equals("10:30 AM"))
                    {
                        if (paciente[0].Equals(hora[0]))
                        {
                            this.lbl1030.Content = "Nombre: " + paciente[1] + " Tipo:" + paciente[2];
                        }
                        
                    }
                    if (hora[2].Equals("11:00 AM"))
                    {
                        if (paciente[0].Equals(hora[0]))
                        {
                            this.lbl11.Content = "Nombre: " + paciente[1] + " Tipo:" + paciente[2];
                        }
                        
                    }
                    if (hora[2].Equals("11:30 AM"))
                    {
                        if (paciente[0].Equals(hora[0]))
                        {
                            this.lbl1130.Content = "Nombre: " + paciente[1] + " Tipo:" + paciente[2];
                        }
                        
                    }
                    if (hora[2].Equals("12:00 PM"))
                    {
                        if (paciente[0].Equals(hora[0]))
                        {
                            this.lbl12.Content = "Nombre: " + paciente[1] + " Tipo:" + paciente[2];
                        }
                        
                    }
                    if (hora[2].Equals("12:30 PM"))
                    {
                        if (paciente[0].Equals(hora[0]))
                        {
                            this.lbl1230.Content = "Nombre: " + paciente[1] + " Tipo:" + paciente[2];
                        }
                        
                    }
                    if (hora[2].Equals("13:00 PM"))
                    {
                        if (paciente[0].Equals(hora[0]))
                        {
                            this.lbl13.Content = "Nombre: " + paciente[1] + " Tipo:" + paciente[2];
                        }
                        
                    }
                    if (hora[2].Equals("13:30 PM"))
                    {
                        if (paciente[0].Equals(hora[0]))
                        {
                            this.lbl1330.Content = "Nombre: " + paciente[1] + " Tipo:" + paciente[2];
                        }
                        
                    }
                    if (hora[2].Equals("14:00 PM"))
                    {
                        if (paciente[0].Equals(hora[0]))
                        {
                            this.lbl14.Content = "Nombre: " + paciente[1] + " Tipo:" + paciente[2];
                        }
                        
                    }
                    if (hora[2].Equals("14:30 PM"))
                    {
                        if (paciente[0].Equals(hora[0]))
                        {
                            this.lbl1430.Content = "Nombre: " + paciente[1] + " Tipo:" + paciente[2];
                        }
                        
                    }
                    if (hora[2].Equals("15:00 PM"))
                    {
                        if (paciente[0].Equals(hora[0]))
                        {
                            this.lbl15.Content = "Nombre: " + paciente[1] + " Tipo:" + paciente[2];
                        }
                        
                    }
                    if (hora[2].Equals("15:30 PM"))
                    {
                        if (paciente[0].Equals(hora[0]))
                        {
                            this.lbl1530.Content = "Nombre: " + paciente[1] + " Tipo:" + paciente[2];
                        }
                        
                    }
                    if (hora[2].Equals("16:00 PM"))
                    {
                        if (paciente[0].Equals(hora[0]))
                        {
                            this.lbl16.Content = "Nombre: " + paciente[1] + " Tipo:" + paciente[2];
                        }
                       
                    }
                    if (hora[2].Equals("16:30 PM"))
                    {
                        if (paciente[0].Equals(hora[0]))
                        {
                            this.lbl1630.Content = "Nombre: " + paciente[1] + " Tipo:" + paciente[2];
                        }
                     
                    }
                    if (hora[2].Equals("17:00 PM"))
                    {
                        if (paciente[0].Equals(hora[0]))
                        {
                            this.lbl17.Content = "Nombre: " + paciente[1] + " Tipo:" + paciente[2];
                        }
                      
                    }
                    if (hora[2].Equals("17:30 PM"))
                    {
                        if (paciente[0].Equals(hora[0]))
                        {
                            this.lbl1730.Content = "Nombre: " + paciente[1] + " Tipo:" + paciente[2];
                        }
                      
                    }
                    if (hora[2].Equals("18:00 PM"))
                    {
                        if (paciente[0].Equals(hora[0]))
                        {
                            this.lbl18.Content = "Nombre: " + paciente[1] + " Tipo:" + paciente[2];
                        }
                      
                    }
                    if (hora[2].Equals("18:30 PM"))
                    {
                        if (paciente[0].Equals(hora[0]))
                        {
                            this.lbl1830.Content = "Nombre: " + paciente[1] + " Tipo:" + paciente[2];
                        }
                       
                    }
                    if (hora[2].Equals("19:00 PM"))
                    {
                        if (paciente[0].Equals(hora[0]))
                        {
                            this.lbl19.Content = "Nombre: " + paciente[1] + " Tipo:" + paciente[2];
                        }
                        
                    }
                    if (hora[2].Equals("19:30 PM"))
                    {
                        if (paciente[0].Equals(hora[0]))
                        {
                            this.lbl1930.Content = "Nombre: " + paciente[1] + " Tipo:" + paciente[2];
                        }
                       
                    }
                    if (hora[2].Equals("20:00 PM"))
                    {
                        if (paciente[0].Equals(hora[0]))
                        {
                            this.lbl20.Content = "Nombre: " + paciente[1] + " Tipo:" + paciente[2];
                        }
                        
                    }
                    if (hora[2].Equals("20:30 PM"))
                    {
                        if (paciente[0].Equals(hora[0]))
                        {
                            this.lbl2030.Content = "Nombre: " + paciente[1] + " Tipo:" + paciente[2];
                        }
                        
                    }
                    if (hora[2].Equals("21:00 PM"))
                    {
                        if (paciente[0].Equals(hora[0]))
                        {
                            this.lbl21.Content = "Nombre: " + paciente[1] + " Tipo:" + paciente[2];
                        }
                        
                    }

                }
 
            }

        }
    }
}
