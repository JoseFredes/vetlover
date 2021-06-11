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
    /// Lógica de interacción para wpfAgendarHora.xaml
    /// </summary>
    public partial class wpfAgendarHora : Window
    {
        // se crean algunos objetos
        Cliente cliente = new Cliente();
        ConeccionBBDD coneccion = new ConeccionBBDD();
        public wpfAgendarHora()
        {
            // se inicializan los componentes y se limitan las fechas de los calendarios
            InitializeComponent();
            llenarcombos();
            cmbtipoatencion.SelectedItem = 0;
            this.calendario.SelectedDate = DateTime.Now;
            this.calendario.DisplayDateStart = DateTime.Today.AddDays(-30); // estan disponibles 30 dias previos al dia de hoy
            this.calendario.DisplayDateEnd = DateTime.Today.AddDays(200); // se pueden tomar horas hasta 200 dias más del dia de hoy
            this.cmbhoraeliminar.SelectedIndex = 0;
        }

        // se llenan  los combos
        public void llenarcombos()
        {
            List<string> listaatencion = new List<string>();
            listaatencion.Add("-Seleccione tipo de Atención-");
            listaatencion.Add(TipoAtencion.Control.ToString());
            listaatencion.Add(TipoAtencion.Especialidad.ToString());
            listaatencion.Add(TipoAtencion.urgencia.ToString());
            listaatencion.Add(TipoAtencion.cirugia.ToString());

            cmbtipoatencion.DataContext = null;
            cmbtipoatencion.Items.Clear();

            foreach (string tipoatencion in listaatencion)
            {
                cmbtipoatencion.Items.Add(tipoatencion);
            }

            List<string> listadehorasaliminar = new List<string>();
            listadehorasaliminar.Add("9:00 AM");
            listadehorasaliminar.Add("9:30 AM");
            listadehorasaliminar.Add("10:00 AM");
            listadehorasaliminar.Add("10:30 AM");
            listadehorasaliminar.Add("11:00 AM");
            listadehorasaliminar.Add("11:30 AM");
            listadehorasaliminar.Add("11:30 AM");
            listadehorasaliminar.Add("12:00 PM");
            listadehorasaliminar.Add("12:30 PM");
            listadehorasaliminar.Add("13:00 PM");
            listadehorasaliminar.Add("13:30 PM");
            listadehorasaliminar.Add("14:00 PM");
            listadehorasaliminar.Add("14:30 PM");
            listadehorasaliminar.Add("15:00 PM");
            listadehorasaliminar.Add("15:30 PM");
            listadehorasaliminar.Add("16:00 PM");
            listadehorasaliminar.Add("16:30 PM");
            listadehorasaliminar.Add("17:00 PM");
            listadehorasaliminar.Add("17:30 PM");
            listadehorasaliminar.Add("18:00 PM");
            listadehorasaliminar.Add("18:30 PM");
            listadehorasaliminar.Add("19:00 PM");
            listadehorasaliminar.Add("19:30 PM");
            listadehorasaliminar.Add("20:00 PM");
            listadehorasaliminar.Add("20:30 PM");
            listadehorasaliminar.Add("21:00 PM");

            cmbhoraeliminar.Items.Clear();
            cmbhoraeliminar.DataContext = null;

            foreach (var hora in listadehorasaliminar)
            {
                cmbhoraeliminar.Items.Add(hora);
            }
        }

        public void llenarcombohorario(List<string> horas)
        {
            List<string> listadehoras = new List<string>();
            listadehoras.Add("9:00 AM");
            listadehoras.Add("9:30 AM");
            listadehoras.Add("10:00 AM");
            listadehoras.Add("10:30 AM");
            listadehoras.Add("11:00 AM");
            listadehoras.Add("11:30 AM");
            listadehoras.Add("11:30 AM");
            listadehoras.Add("12:00 PM");
            listadehoras.Add("12:30 PM");
            listadehoras.Add("13:00 PM");
            listadehoras.Add("13:30 PM");
            listadehoras.Add("14:00 PM");
            listadehoras.Add("14:30 PM");
            listadehoras.Add("15:00 PM");
            listadehoras.Add("15:30 PM");
            listadehoras.Add("16:00 PM");
            listadehoras.Add("16:30 PM");
            listadehoras.Add("17:00 PM");
            listadehoras.Add("17:30 PM");
            listadehoras.Add("18:00 PM");
            listadehoras.Add("18:30 PM");
            listadehoras.Add("19:00 PM");
            listadehoras.Add("19:30 PM");
            listadehoras.Add("20:00 PM");
            listadehoras.Add("20:30 PM");
            listadehoras.Add("21:00 PM");
            listadehoras.Add("Fin de las horas, revisar el Proximo día");
            cmbhora.Items.Clear();
            cmbhora.DataContext = null;

            for (int i = 0; i < listadehoras.Count; i++)
            {
                foreach (var horabd in horas)
                {
                    if (listadehoras[i].Contains(horabd))
                    {
                        listadehoras.Remove(listadehoras[i]);
                    }

                }
                cmbhora.Items.Add(listadehoras[i]);
            }
            cmbhora.SelectedIndex = 0;
            cmbtipoatencion.SelectedItem = 0;

        }

        // se valida el rut
        public bool validarRut(string rut)
        {
            bool validacion = false;
            try
            {
                rut = rut.ToUpper();
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    validacion = true;
                }
            }
            catch (Exception)
            {
            }
            return validacion;
        }

        // botones laterales
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

        private void botonReportes(object sender, RoutedEventArgs e)
        {
            wpfReportes reporte = new wpfReportes();
            reporte.Show();
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

        // se agenda la hora
        private void AgendarHora(object sender, RoutedEventArgs e)
        {
            ReservaHoras reserva = new ReservaHoras();
            Mascota mascota = new Mascota();
           // validaciones
            if (this.txtRutCliente.Text == string.Empty)
                MessageBox.Show("Debe ingresar el rut");
            else
            {
                if (validarRut(this.txtRutCliente.Text) == false)
                    MessageBox.Show("Debes ingresar un Rut valido");
                else
                {
                    // se pasan los datos y se pasan a la clase
                    cliente.Rut = this.txtRutCliente.Text;
                    mascota.Nombre = cmbnombremascotas.SelectedValue.ToString();
                    cliente.Mascotas.Add(mascota);
                    reserva.Calendario = this.calendario.SelectedDate.Value;
                    reserva.hora = this.cmbhora.SelectedValue.ToString();

                    if (TipoAtencion.cirugia.ToString().Equals(cmbtipoatencion.SelectedValue))
                        reserva.tipoAtencion = TipoAtencion.cirugia;
                    if (TipoAtencion.Control.ToString().Equals(cmbtipoatencion.SelectedValue))
                        reserva.tipoAtencion = TipoAtencion.Control;
                    if (TipoAtencion.Especialidad.ToString().Equals(cmbtipoatencion.SelectedValue))
                        reserva.tipoAtencion = TipoAtencion.Especialidad;
                    if (TipoAtencion.urgencia.Equals(cmbtipoatencion.SelectedValue))
                        reserva.tipoAtencion = TipoAtencion.urgencia;


                    reserva.GenerarReserva(cliente.Rut, reserva.tipoAtencion, reserva.Calendario, reserva.hora, mascota.Nombre);
                    MessageBox.Show("Se ha generado la hora para el paciente " + mascota.Nombre + reserva.hora + reserva.Calendario.ToString("yyyy/MM/dd"));
                }

            }
            

            this.txtRutCliente.Text = string.Empty;
            this.cmbnombremascotas.Items.Clear();
            this.calendario.SelectedDate = DateTime.Now;
            this.cmbhora.Items.Clear();
            
        }

        private void Eliminar(object sender, RoutedEventArgs e)
        {
            // elimina la hora del cliente.
            var client = new Cliente();
            var re = new ReservaHoras();

            if (this.txtRutClienteeliminar.Text == string.Empty)
                MessageBox.Show("Debe ingresar el Rut");
            if (validarRut(this.txtRutClienteeliminar.Text) == false)
                MessageBox.Show("Debes ingresar un Rut valido");
            else
            {
                client.Rut = this.txtRutClienteeliminar.Text;
                re.Fecha = this.calendario.SelectedDate.Value;
                re.hora = this.cmbhoraeliminar.SelectedItem.ToString();

                var ids = coneccion.trearidcliente();
                string idcliente = string.Empty;
                for (int i = 0; i < ids.Count; i++)
                {
                    var linea = ids[i].ToString().Split(';');
                    if (linea[1].Equals(client.Rut))
                    {
                        idcliente = linea[0];
                    }
                }

                re.CancelarHora(idcliente, re.Fecha, re.hora);
            }

            this.txtRutClienteeliminar.Text = string.Empty;
            this.cmbhoraeliminar.Items.Clear();
            this.cmbhoraeliminar.SelectedIndex = 0;
            this.calendario.SelectedDate = DateTime.Now;

        }

        private void Calendario(object sender, RoutedEventArgs e)
        {
            // muestra un calendario con las horas seleccionadas
            var reserva = new ReservaHoras();
            ventanaagenda ventanaagenda = new ventanaagenda();
            ventanaagenda.Show();
            ventanaagenda.llenarcalendario(this.calendario.SelectedDate.Value);
            
        }

        private void salir(object sender, RoutedEventArgs e)
        {
            // salir
            this.Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void mostrarnombres(object sender, RoutedEventArgs e)
        {
            //muestra los nombres de las mascotas por el rut
            cmbnombremascotas.Items.Clear();
            if (this.txtRutCliente.Text == string.Empty)
                MessageBox.Show("Debe ingresar el Rut");
            else
            {
                if (validarRut(this.txtRutCliente.Text) == false)
                    MessageBox.Show("Debe Ingresar el Rut");
                else
                {
                    string rut = this.txtRutCliente.Text;
                    var mascotas = coneccion.buscarmascotasxrutdueno(rut);
                    for (int i = 0; i < mascotas.Count(); i++)
                    {
                        string linea = mascotas[i].ToString();
                        string[] infomascotas = linea.Split(';');
                        cmbnombremascotas.Items.Add(infomascotas[1].ToString());
                        cmbnombremascotas.SelectedIndex = 0;
                    }
                }
             
            }
          
        }

        private void btnhorasdisponibles(object sender, RoutedEventArgs e)
        {
            // se muestran las horas disponibles para el dia seleccionado
            String fecha = this.calendario.SelectedDate.Value.ToString();
            string[] fechaseleccionada = fecha.Split('-');
            var dia = int.Parse(fechaseleccionada[0]);
            var mes = int.Parse(fechaseleccionada[1]);
            var ano = int.Parse(fechaseleccionada[2].Split(' ')[0]);
            var fechap = new DateTime(ano, mes, dia);
            var horas = coneccion.HoraPorFecha(fechap);
            llenarcombohorario(horas);

        }
    }
}
