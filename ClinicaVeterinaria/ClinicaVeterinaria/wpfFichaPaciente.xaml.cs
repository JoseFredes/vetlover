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
    /// Lógica de interacción para wpfFichaPaciente.xaml
    /// </summary>
    public partial class wpfFichaPaciente : Window
    {
        //se setan los objetos
        Cliente cliente = new Cliente();
        Mascota mascota = new Mascota();
        ConeccionBBDD consecionsql = new ConeccionBBDD();
        List<string> listamascotas = new List<string>();
       
        public wpfFichaPaciente()
        { 
            // se inicializan los componentes
            var diademanana = DateTime.Today.AddDays(1);
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.cmbTipoMascotas.Items.Insert(0, "-Seleccione tipo de Mascota-");
            llenarComboanimales(cmbTipoMascotas);
            this.cmbTipoMascotas.SelectedIndex = 0;
            this.fechanacimiento.DisplayDateEnd = diademanana;

        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        // se llenan los combos
        private void llenarComboanimales(ComboBox combo)
        {
            List<string> listamascotas = new List<string>();
            listamascotas.Add("-Seleccione tipo de mascota-");
            listamascotas.Add(TipoMascota.Perro.ToString());
            listamascotas.Add(TipoMascota.Ave.ToString());
            listamascotas.Add(TipoMascota.Gato.ToString());
            listamascotas.Add(TipoMascota.Conejo.ToString());
            listamascotas.Add(TipoMascota.Roedor.ToString());

            combo.DataContext = null;
            combo.Items.Clear();

            foreach (string MASCOTA in listamascotas)
            {
                combo.Items.Add(MASCOTA);
            }

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

        // se valida el email
        bool validaremail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        //botones
        private void botonInicio(object sender, RoutedEventArgs e)
        {
            MainWindow Inicio = new MainWindow();
            Inicio.Show();
            this.Close();
        }

        private void botonVentas(object sender, RoutedEventArgs e)
        {
            wpfventas Venta = new wpfventas();
            Venta.Show();
            this.Close();
        }

        private void botonAgenda(object sender, RoutedEventArgs e)
        {
            wpfAgendarHora agendar = new wpfAgendarHora();
            agendar.Show();
            this.Close();
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

        private void salir(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // se crea el dueño
        private void creardueño(object sender, RoutedEventArgs e)
        {
            
            if(this.txtNombre.Text == string.Empty )
                MessageBox.Show("debe rellenar el nombre");
            else
            {
                if (this.txtDireccion.Text == string.Empty)
                    MessageBox.Show("debe rellenar la dirección");
                else
                {
                    if (this.txtRut.Text == string.Empty)
                        MessageBox.Show("debe rellenar el Rut");
                    else
                    {
                        if (validarRut(this.txtRut.Text) == false)
                            MessageBox.Show("Debe Ingresar un Rut valido");
                        else
                        {
                            if (validaremail(this.txtcorreo.Text) == false)
                                MessageBox.Show("Ingrese un correo valido");
                            else
                            {
                                if (this.txtcorreo.Text == string.Empty)
                                    MessageBox.Show("debe rellenar el correo");
                                else
                                {
                                    var nuevocliente = new Cliente();
                                    nuevocliente.NombreCliente = this.txtNombre.Text;
                                    nuevocliente.Rut = this.txtRut.Text;
                                    nuevocliente.direccion = this.txtDireccion.Text;
                                    nuevocliente.Correo = this.txtcorreo.Text;
                                    nuevocliente.apellido = this.txtapellido.Text;

                                    string rutdevuelto = nuevocliente.crearcliente();

                                    MessageBox.Show("Se ha creado el cliente, Rut: " + rutdevuelto);

                                }
                            }
                            
                        }
                       

                    }
                    
                }
               

            }
            this.txtapellido.Text = string.Empty;
            this.txtcorreo.Text = string.Empty;
            this.txtDireccion.Text = string.Empty;
            this.txtNombre.Text = string.Empty;
            this.txtRut.Text = string.Empty;

        }

        // se busca el dueño
        private void buscardueno(object sender, RoutedEventArgs e)
        {

            for (int i = 0; i < listamascotas.Count; i++)
            {
                if (listamascotas[i].Contains(cmbMascotas.SelectedItem.ToString()))
                {
                    lvFicha.Items.Add(listamascotas[i]);
                }
            }

        }

       
        // se agrega la mascota
        private void agregarmascota(object sender, RoutedEventArgs e)
        {
            if (this.txtRutDuenio.Text == string.Empty)
                MessageBox.Show("debe ingresar el rut");
            else
            {
                if (this.txtNombreMascota.Text == string.Empty)
                    MessageBox.Show("debe ingresar el nombre de la mascota");
                else
                {
                    if (this.cmbTipoMascotas.SelectedItem == null)
                        MessageBox.Show("debe seleccionar un tipo de mascota");
                    else
                    {
                        if (validarRut(this.txtRutDuenio.Text) == false)
                            MessageBox.Show("Debe Ingresar un Rut valido");
                        else
                        {
                            string rut = this.txtRutDuenio.Text;
                            mascota.Nombre = this.txtNombreMascota.Text;
                            mascota.FechaNacimiento = this.fechanacimiento.SelectedDate.Value;

                            if (TipoMascota.Ave.ToString().Equals(cmbTipoMascotas.SelectedValue))
                                mascota.tipoMascota = TipoMascota.Ave;
                            if (TipoMascota.Conejo.ToString().Equals(cmbTipoMascotas.SelectedValue))
                                mascota.tipoMascota = TipoMascota.Conejo;
                            if (TipoMascota.Gato.ToString().Equals(cmbTipoMascotas.SelectedValue))
                                mascota.tipoMascota = TipoMascota.Gato;
                            if (TipoMascota.Perro.ToString().Equals(cmbTipoMascotas.SelectedValue))
                                mascota.tipoMascota = TipoMascota.Perro;
                            if (TipoMascota.Roedor.ToString().Equals(cmbTipoMascotas.SelectedValue))
                                mascota.tipoMascota = TipoMascota.Roedor;
                            cliente.AgregarMascota(rut, mascota);
                        }
                    }
                }
            }

            this.txtRutDuenio.Text = string.Empty;
            this.txtNombreMascota.Text = string.Empty;
            this.cmbTipoMascotas.SelectedIndex = 0;

            MessageBox.Show("se ha añadido la mascota al cliente");
        }

        // se muesta la mascotas por el rut
        private void Mostrarmascotas(object sender, RoutedEventArgs e)
        {
            cmbMascotas.Items.Clear();
            if (this.txtBxRut.Text == string.Empty)
                MessageBox.Show("debe ingresar el rut");
            else
            {
                if (validarRut(this.txtBxRut.Text) == false)
                    MessageBox.Show("Debe Ingresar un Rut valido");
                else
                {
                    string rut = this.txtBxRut.Text;
                    var mascotas = consecionsql.buscarmascotasxrutdueno(rut);

                    for (int i = 0; i < mascotas.Count(); i++)
                    {
                        string linea = mascotas[i].ToString();
                        string[] infomascotas = linea.Split(';');
                        var fecha = infomascotas[3].Split('-');
                        var dia = int.Parse(fecha[0]);
                        var mes = int.Parse(fecha[1]);
                        var ano = int.Parse(fecha[2].Split(' ')[0]);
                        var fechaobtenida = new DateTime(ano, mes, dia);
                        cmbMascotas.Items.Add(infomascotas[1].ToString());
                        cmbMascotas.SelectedIndex = 0;
                        listamascotas.Add("Nombre Mascota: " + infomascotas[1].ToString() + "  Tipo:  " + infomascotas[2].ToString() + "  fecha Nacimiento " + fechaobtenida.ToString("yyyy/MM/dd"));
                    }
                }
            }

        }

        
    }
}
