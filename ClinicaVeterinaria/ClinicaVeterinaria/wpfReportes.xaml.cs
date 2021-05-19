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
    /// Lógica de interacción para wpfReportes.xaml
    /// </summary>
    public partial class wpfReportes : Window
    {
        public wpfReportes()
        {
            InitializeComponent();
            clrpordia.SelectedDate = DateTime.Now;
        }

        private void botonInicio(object sender, RoutedEventArgs e)
        {
            MainWindow inicio = new MainWindow();
            inicio.Show();
            this.Close();
        }

        private void botonVentas(object sender, RoutedEventArgs e)
        {
            wpfventas venta = new wpfventas();
            venta.Show();
            this.Close();
        }

        private void botonAgenda(object sender, RoutedEventArgs e)
        {
            wpfAgendarHora agenda = new wpfAgendarHora();
            agenda.Show();
            this.Close();
        }

        private void botonFicha(object sender, RoutedEventArgs e)
        {
            wpfFichaPaciente ficha = new wpfFichaPaciente();
            ficha.Show();
            this.Close();
        }

        private void botonInventario(object sender, RoutedEventArgs e)
        {
            wpfInventario inventario = new wpfInventario();
            inventario.Show();
            this.Close();
        }

        private void MostrarVentas(object sender, RoutedEventArgs e)
        {
            var conexionBBDD = new ConeccionBBDD();
            String fecha = clrpordia.SelectedDate.Value.ToString();
            string[] fechaseleccionada = fecha.Split('-');
            var dia = int.Parse(fechaseleccionada[0]);
            var mes = int.Parse(fechaseleccionada[1]);
            var ano = int.Parse(fechaseleccionada[2].Split(' ')[0]);
            var fechap = new DateTime(ano, mes, dia);
            List<string> reportes = new List<string>();
            foreach (var venta in conexionBBDD.listadeventaspordia(fechap))
            {
                lsvventaspordia.Items.Add(venta);
                
            }

            this.lbltotalventas.Content = "$ "+ conexionBBDD.totalpordia(fechap);
 
        }

        private void GenerarReporte(object sender, RoutedEventArgs e)
        {

        }

        private void salir(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
