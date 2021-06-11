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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClinicaVeterinaria
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ConeccionBBDD coneccionsql = new ConeccionBBDD();
        Veterinaria vet = new Veterinaria();
        public MainWindow()
        {
            // se inicializan los componentes
            InitializeComponent();
            // falla el inicio en algunos computadores, si tira error al inicio de la app, descomentar.
            //WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            TotalInventario();
            Cantventasdeldía();
            Atencionesdeldia();
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        // botones laterales
        private void botonVentas(object sender, RoutedEventArgs e)
        {
            wpfventas ventas = new wpfventas();
            ventas.Show();
            this.Close();
        }
        private void botonAgenda(object sender, RoutedEventArgs e)
        {
            wpfAgendarHora agenda = new wpfAgendarHora();
            agenda.Show();
            this.Close();

        }

        private void TotalInventario()
        {
            // se le pasa el llamado de la bd
            this.lbltotalinv.Content = string.Empty;
            this.lbltotalinv.Content = vet.TotalInventario();

        }

        private void Cantventasdeldía()
        { // se le pasa el llamado de la bd
            this.lblventasdeldia.Content = string.Empty;
            this.lblventasdeldia.Content = vet.TotalventasDirarias();

        }

        private void Atencionesdeldia()
        {  // se le pasa el llamado de la bd
            this.lblatenciones.Content = string.Empty;
            this.lblatenciones.Content = vet.MostrarReservasDiarias();
        }

        private void botonInventario(object sender, RoutedEventArgs e)
        {
            wpfInventario inventario = new wpfInventario();
            inventario.Show();
            this.Close();
        }

        private void botonReportes(object sender, RoutedEventArgs e)
        {
            wpfReportes reportes = new wpfReportes();
            reportes.Show();
            this.Close();
        }

        private void botonFicha(object sender, RoutedEventArgs e)
        {
            wpfFichaPaciente fichaPaciente = new wpfFichaPaciente();
            fichaPaciente.Show();
            this.Close();
        }

        private void salir(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
