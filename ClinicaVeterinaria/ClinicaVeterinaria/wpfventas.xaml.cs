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
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InventarioCargado();
        }

        private void InventarioCargado()
        {
            String[] losproductos = new String[coneccionsql.mostrarInventario().Count];
            lvInventario.Items.Clear();
            foreach (var item in coneccionsql.mostrarInventario().ToList())
            {
                string strproductos = item.ToString();
                losproductos = strproductos.Split(';');
                lvInventario.Items.Add("ID:" + losproductos[0].ToString() + " Nombre Producto: " + losproductos[1].ToString() + " Cantidad: " + losproductos[2].ToString() + " Precio: " + losproductos[3].ToString() + " Info: " + losproductos[4].ToString() + " Tipo de Producto: " + losproductos[5].ToString() + "\n");
            }
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void botonInicio(object sender, RoutedEventArgs e)
        {
            MainWindow Inicio = new MainWindow();
            Inicio.Show();
            this.Close();

        }

        private void botonAgenda(object sender, RoutedEventArgs e)
        {
           
        }

        private void botonFicha(object sender, RoutedEventArgs e)
        {

        }

        private void botonReportes(object sender, RoutedEventArgs e)
        {

        }

        private void botonInventario(object sender, RoutedEventArgs e)
        {
            wpfInventario Inventario = new wpfInventario();
            Inventario.Show();
            this.Close();

        }
        
        
       
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
        private void mostrarTotal(object sender, RoutedEventArgs e)
        {
            
            this.lblMostrarTotal.Content = "$ " + mostrarTotalBoleta();
        }

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

            

            Venta2.GenerarVenta(montototal, Venta2.FechadeVenta, idcliente, listaidproductos);
           
            String[] arrayproductos = new String[coneccionsql.mostrarInventario().Count];

            for (int i = 0; i < listaidproductos.Count; i++)
            {
                int cantidadproducto = coneccionsql.taercantidadproducto(listaidproductos[i]);

                if (cantidadproducto>=1)
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

            Venta2.ProductosSeleccionados.Clear();
            this.txtrut.Text = string.Empty;
            this.lblMostrarTotal.Content = string.Empty;
            lvCarrito.Items.Clear();
            MessageBox.Show("se ha generado la venta por : " + montototal + " El dia de : "+ Venta2.FechadeVenta);
        }
    }
}
