using MySql.Data.MySqlClient;
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
    /// Lógica de interacción para wpfventas.xaml
    /// </summary>
    public partial class wpfventas : Window
    {
        

        Venta Venta2 = new Venta();
        ConeccionBBDD coneccionsql = new ConeccionBBDD();
        public wpfventas()
        {
            // se inicializan las funciones y los componentes
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InventarioCargado();
        }

        private void InventarioCargado()
        { // el inventario se carga
            String[] losproductos = new String[coneccionsql.mostrarInventario().Count];
            lvInventario.Items.Clear();
            foreach (var item in coneccionsql.mostrarInventario().ToList())
            {
                string strproductos = item.ToString();
                losproductos = strproductos.Split(';');
                lvInventario.Items.Add("ID:" + losproductos[0].ToString() + " Nombre Producto: " + losproductos[1].ToString() + " Cantidad: " + losproductos[2].ToString() + " Precio: " + losproductos[3].ToString() + " Info: " + losproductos[4].ToString() + " Tipo de Producto: " + losproductos[5].ToString() + "\n");
            }
        }

        public bool validarRut(string rut)
        {
            //funcion que valida el rut, se agregan los datos del rut con puntos para validarlo de igual forma, pero se prefiere idealmente que no se agregen rut con puntos ni guion 
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
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        // se asignan los botones laterales
        private void botonInicio(object sender, RoutedEventArgs e)
        {
            MainWindow Inicio = new MainWindow();
            Inicio.Show();
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

        private void botonReportes(object sender, RoutedEventArgs e)
        {
            wpfReportes reportes = new wpfReportes();
            reportes.Show();
            this.Close();
        }

        private void botonInventario(object sender, RoutedEventArgs e)
        {
            wpfInventario Inventario = new wpfInventario();
            Inventario.Show();
            this.Close();

        }
        
        
       // se agregan los productos al inventario
        private void AgregarProducto(object sender, RoutedEventArgs e)
        {
            int id = 0;
            List<string> listacarrito = new List<string>();
            foreach (var item in lvInventario.Items)
            {
                if(item.Equals(lvInventario.SelectedValue.ToString()))
                {
                    listacarrito.Add(item.ToString());
                }
            }

            String[] Productosdelcarrito = new String[lvCarrito.Items.Count];
            foreach (var producto in listacarrito)
            {
                Productosdelcarrito = producto.ToString().Split(':');
                string cadena = Productosdelcarrito[1];
                string filtrado = string.Concat(cadena.Where(c => Char.IsDigit(c)));
                id = int.Parse(filtrado);
                Venta2.AgregarProductosAlCarrito(id);
            }

            foreach (var productocarrito in Venta2.ProductosSeleccionados)
            {
                if (productocarrito.IdProducto == id)
                {
                    lvCarrito.Items.Add("ID:" + productocarrito.IdProducto + " Nombre Producto: " + productocarrito.NombreProducto + " Cantidad: " + productocarrito.CantidadProducto + " Precio: " + productocarrito.PrecioProducto + " Tipo de Producto: " + productocarrito.Tipoproducto);
                    break;
                }
               
            }

            listacarrito.Clear();
            lvInventario.SelectedValue = string.Empty;
         
        }

        private void lvInventario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void salir(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // elimina un un poducto
        private void eliminarProducto(object sender, RoutedEventArgs e)
        {
            String[] arraycarrito = new String[lvCarrito.Items.Count];
            foreach (var item in lvCarrito.Items)
            {
                if (item.Equals(lvCarrito.SelectedItem))
                {
                    arraycarrito = item.ToString().Split(':');
                    string cadena = arraycarrito[1];
                    string filtrado = string.Concat(cadena.Where(c => Char.IsDigit(c)));
                    int id = int.Parse(filtrado);
                    Venta2.eliminarProductoCarrito(id);
                    
                }
            }
            lvCarrito.Items.Remove(lvCarrito.SelectedItem);
        }

        // muestra el total de la boleta

        public int mostrarTotalBoleta()
        {
            String[] Productosdelcarrito = new String[lvCarrito.Items.Count];
            int compra = 0;
            List<int> total = new List<int>();
            foreach (var item in lvCarrito.Items)
            {
                Productosdelcarrito = item.ToString().Split(':');
                string cadena = Productosdelcarrito[4];
                string filtrado = string.Concat(cadena.Where(c => Char.IsDigit(c)));
                compra = int.Parse(filtrado);
                total.Add(compra);
            }
            return Venta2.TotalBoleta(total);
        }

        // funcion que muestra el total
        private void mostrarTotal(object sender, RoutedEventArgs e)
        {
            
            this.lblMostrarTotal.Content = "$ " + mostrarTotalBoleta();
        }

        // se confirma la compra y se realiza en la base de datos
        private void confirmarCompra(object sender, RoutedEventArgs e)
        {
            String[] datoscliente = new String[coneccionsql.trearidcliente().Count];
            int montototal = mostrarTotalBoleta();
            Venta2.FechadeVenta = DateTime.Now;
            string rut = this.txtrut.Text;
            int idcliente = 0;
            List<int> listaidproductos = new List<int>();

            foreach (var dato in coneccionsql.trearidcliente().ToList())
            {
                string linea = dato.ToString();
                datoscliente = linea.Split(';');

                if (rut.Equals(datoscliente[1]))
                {
                    idcliente = int.Parse(datoscliente[0]);
                }
            }
            foreach (var ids in Venta2.ProductosSeleccionados)
            {
                listaidproductos.Add(ids.IdProducto);
            }


            if (this.txtrut.Text == string.Empty)
                MessageBox.Show("Debes ingresar un rut para poder efectuar la venta");
            {
                if (validarRut(rut) == false)
                    MessageBox.Show("Debes ingresar un rut valido");
                else
                {
                    Venta2.GenerarVenta(montototal, Venta2.FechadeVenta, idcliente, listaidproductos);

                    String[] arrayproductos = new String[coneccionsql.mostrarInventario().Count];

                    for (int i = 0; i < listaidproductos.Count; i++)
                    {
                        int cantidadproducto = coneccionsql.taercantidadproducto(listaidproductos[i]);

                        if (cantidadproducto >= 1)
                        {
                            int total = Venta2.EliminarProductoInventario(1, cantidadproducto);
                            coneccionsql.eliminarCantidadproducto(listaidproductos[i], total);
                        }
                        else
                        {
                            coneccionsql.eliminarCantidadproducto(listaidproductos[i], 0);
                            MessageBox.Show("Stock agotado de producto, Numero ID: " + listaidproductos[i]);
                            break;
                        }
                    }
                }

            }
            

            Venta2.ProductosSeleccionados.Clear();
            this.txtrut.Text = string.Empty;
            this.lblMostrarTotal.Content = string.Empty;
            lvCarrito.Items.Clear();
            MessageBox.Show("se ha generado la venta por : " + montototal + " El dia de : "+ Venta2.FechadeVenta);
        }
    }
}
