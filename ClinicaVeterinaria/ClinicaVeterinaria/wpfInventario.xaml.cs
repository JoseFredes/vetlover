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
    /// Lógica de interacción para wpfInventario.xaml
    /// </summary>
    public partial class wpfInventario : Window
    {
        string tipo;
        Venta venta = new Venta();
        Producto producto = new Producto();
        ConeccionBBDD coneccionbbdd = new ConeccionBBDD();
       
        public wpfInventario()
        {
            // se inicializan los componentes, como los combo box.
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.cmbProducto.Items.Insert(0, "-Seleccione tipo de producto-");
            this.cmbBuscarporTipo.Items.Insert(0, "-Seleccione tipo de producto para Filtrar-");
            llenarCombo(cmbProducto, cmbBuscarporTipo); 
            this.cmbProducto.SelectedIndex = 0;
            this.cmbBuscarporTipo.SelectedIndex = 0;
            
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        // se programan los botones de cada una de las ventanas
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

        private void botonFicha(object sender, RoutedEventArgs e)
        {
            wpfFichaPaciente fichaPaciente = new wpfFichaPaciente();
            fichaPaciente.Show();
            this.Close();
        }

        private void botonReportes(object sender, RoutedEventArgs e)
        {
            wpfReportes reportes = new wpfReportes();
            reportes.Show();
            this.Close();
        }
        private void llenarCombo(ComboBox combo, ComboBox comboBoxBuscar)
        {
            //se llena el combo de tipo de producto
            List<string> listaProductos = new List<string>();
            listaProductos.Add("-Seleccione tipo de producto-");
            listaProductos.Add(TipoProducto.Alimento.ToString());
            listaProductos.Add(TipoProducto.Juguete.ToString());
            listaProductos.Add(TipoProducto.Insumo.ToString());
            listaProductos.Add(TipoProducto.RopaAnimal.ToString());
            listaProductos.Add(TipoProducto.Varios.ToString());

            combo.DataContext = null;
            combo.Items.Clear();
            comboBoxBuscar.Items.Clear();
            
            foreach (string producto in listaProductos)
            {
                combo.Items.Add(producto);
                comboBoxBuscar.Items.Add(producto);
            }

        }

        private void cmbProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (cmbProducto.SelectedIndex >= 0)
            {
                tipo = (cmbProducto.SelectedIndex + 1).ToString();
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            // se validan los datos y luego se agregan los productos a la tabla inventario. 


            if (this.txtNombre.Text == string.Empty)
                MessageBox.Show("Por favor Ingresar el nombre del producto");

            if (this.txtInformacion.Text == string.Empty)
                MessageBox.Show("Debe ingresar la información del producto");

            if (this.txtCantidad.Text == string.Empty)
                MessageBox.Show("Debe ingresar una cantidad");

            if (this.txtPrecio.Text == string.Empty)
                MessageBox.Show("Debe ingresar un precio");


            producto.NombreProducto = this.txtNombre.Text;
            producto.InfoProducto = this.txtInformacion.Text;
            producto.PrecioProducto = int.Parse(this.txtPrecio.Text);
            producto.CantidadProducto = int.Parse(this.txtCantidad.Text);
       
            if (TipoProducto.Alimento.ToString().Equals(cmbProducto.SelectedValue))
                producto.Tipoproducto = TipoProducto.Alimento;
            if (TipoProducto.Juguete.ToString().Equals(cmbProducto.SelectedValue))
                producto.Tipoproducto = TipoProducto.Juguete;
            if (TipoProducto.Insumo.ToString().Equals(cmbProducto.SelectedValue))            
                producto.Tipoproducto = TipoProducto.Insumo;      
            if (TipoProducto.Varios.ToString().Equals(cmbProducto.SelectedValue))            
                producto.Tipoproducto = TipoProducto.Varios;                               
            if (TipoProducto.RopaAnimal.ToString().Equals(cmbProducto.SelectedValue))
                producto.Tipoproducto = TipoProducto.RopaAnimal;
                
            coneccionbbdd.AgregarProductoInventario(producto.NombreProducto,producto.CantidadProducto , producto.PrecioProducto, producto.InfoProducto, producto.Tipoproducto);

            venta.AgregarProducto(producto);
            MessageBox.Show("Producto Agregado!");
            this.txtNombre.Text = String.Empty;
            this.txtInformacion.Text = String.Empty;
            this.txtPrecio.Text = String.Empty;
            this.txtCantidad.Text = String.Empty;
            this.cmbProducto.SelectedIndex = 0;

        }

      

        private void MostrarInventario(object sender, RoutedEventArgs e)
        {
            // al momento de apretar el boton, se muestra el inventario
            String[] losproductos = new String[coneccionbbdd.mostrarInventario().Count];
            lvinventario.Items.Clear();
            foreach (var item in coneccionbbdd.mostrarInventario().ToList())
            {
                string strproductos = item.ToString();
                losproductos = strproductos.Split(';');
                lvinventario.Items.Add("ID:" + losproductos[0].ToString() + " Nombre Producto: " + losproductos[1].ToString()+ " Cantidad: " + losproductos[2].ToString()+" Precio: " + losproductos[3].ToString()+" Info: " + losproductos[4].ToString() + " Tipo de Producto: " + losproductos[5].ToString()+ "\n");
            }
             
        }

        private void BxCodigo(object sender, RoutedEventArgs e)
        {
            // se busca el producto por condigo y luego este se muestra

            if (this.txtBxCodigo.Text == string.Empty)
                MessageBox.Show("Debes ingresar un Codigo");

            string codigo = this.txtBxCodigo.Text;
            int id = int.Parse(codigo);
            String[] arrayproductos = new String[coneccionbbdd.mostrarInventario().Count];


            foreach (var producto in coneccionbbdd.mostrarInventario().ToList())
            {
                string strproductos = producto.ToString();
                arrayproductos = strproductos.Split(';');

                if (arrayproductos[0].Equals(id.ToString()))
                {
                    MessageBox.Show("ID:" + arrayproductos[0].ToString() + " Nombre Producto: " + arrayproductos[1].ToString() + " Cantidad: " + arrayproductos[2].ToString() + " Precio: " + arrayproductos[3].ToString() + " Info: " + arrayproductos[4].ToString() + " Tipo de Producto: " + arrayproductos[5].ToString());
                }
            }
        }

        public void mostrarproductoactualizado(int cantidad, int id, int cantidadaeliminar)
        {
            // se muestran los productos luego de eliminarse un producto.
            String[] arrayproductos = new String[coneccionbbdd.mostrarInventario().Count];

            foreach (var producto in coneccionbbdd.mostrarInventario().ToList())
            {
                string strproductos = producto.ToString();
                arrayproductos = strproductos.Split(';');

                if (arrayproductos[0].Equals(id.ToString()))
                {
                    MessageBox.Show("Se han eliminado : " + cantidadaeliminar + "\nDe : " + arrayproductos[1].ToString() + "\nCantidad Anterior" + cantidad );
                }
            }
        }


        private void Eliminar(object sender, RoutedEventArgs e)
        {
            // se elimina un producto del inventario y su cantidad, el producto en si no se elimina,si no que se elimina su cantidad. 

            if (this.txtExCodigo.Text == string.Empty)
                MessageBox.Show("Debes ingresar el codigo");
            else
            {
                if (this.txtEliminarCantidad.Text == string.Empty)
                    MessageBox.Show("La Cantidad ingresada no es valida");
                else
                {
                    //se toman las variables desde los txt y luego se convierten a entero
                    string codigo = this.txtExCodigo.Text;
                    string cantidad = this.txtEliminarCantidad.Text;
                    int id = int.Parse(codigo);
                    int cantidadint = int.Parse(cantidad);
                    String[] arrayproductos = new String[coneccionbbdd.traerinventario().Count];
                    int resultado = 0;
                    int intproductoinv = 0;


                    foreach (var producto in coneccionbbdd.mostrarInventario())
                    {
                        string strproductos = producto.ToString();
                        arrayproductos = strproductos.Split(';');

                        if (arrayproductos[0].Equals((id.ToString())))
                        {
                            string cadena = arrayproductos[2];
                            string filtrado = string.Concat(cadena.Where(c => Char.IsDigit(c)));
                            intproductoinv = int.Parse(filtrado);

                            if (intproductoinv >= cantidadint)
                            {

                                resultado = venta.EliminarProductoInventario(cantidadint, intproductoinv);
                            }
                            else
                            {
                                MessageBox.Show("el valor que desea eliminar" + cantidad + "es mayor a la del intentario" + arrayproductos[2] + "intente con otro valor");
                            }
                        }
                    }

                    coneccionbbdd.eliminarCantidadproducto(id, resultado);
                    mostrarproductoactualizado(intproductoinv, id, cantidadint);
                }

            }
            

            this.txtExCodigo.Text = String.Empty;
            this.txtEliminarCantidad.Text = String.Empty;
        }

        private void salir(object sender, RoutedEventArgs e)
        {
            // boton salir.
            this.Close();
        }

        private void cmbBuscarporTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // no requiere ni un metodo, ya que es el cmb
        }

        private void BxTipo(object sender, RoutedEventArgs e)
        { 
            // en este boton se buscan los intvbentarios por el tipo de producto que haya, o sea se filtran
            // se crea un producto el cual tiene como objetivo pasar solo el atributo TipoProducto
            Producto productoabuscar = new Producto();            
            lvinventario.Items.Clear();
        
            if (TipoProducto.Alimento.ToString().Equals(cmbBuscarporTipo.SelectedValue))
                productoabuscar.Tipoproducto = TipoProducto.Alimento;
            if (TipoProducto.Juguete.ToString().Equals(cmbBuscarporTipo.SelectedValue))
                productoabuscar.Tipoproducto = TipoProducto.Juguete;
            if (TipoProducto.Insumo.ToString().Equals(cmbBuscarporTipo.SelectedValue))
                productoabuscar.Tipoproducto = TipoProducto.Insumo;
            if (TipoProducto.Varios.ToString().Equals(cmbBuscarporTipo.SelectedValue))
                productoabuscar.Tipoproducto = TipoProducto.Varios;
            if (TipoProducto.RopaAnimal.ToString().Equals(cmbBuscarporTipo.SelectedValue))
                productoabuscar.Tipoproducto = TipoProducto.RopaAnimal;

            String[] productosportpo = new String[coneccionbbdd.mostrarinventarioportipo(productoabuscar.Tipoproducto).Count];
            foreach (var item in coneccionbbdd.mostrarinventarioportipo(productoabuscar.Tipoproducto).ToList())
            {
                string strproductos = item.ToString();
                productosportpo = strproductos.Split(';');
                lvinventario.Items.Add("ID:" + productosportpo[0].ToString() + " Nombre Producto: " + productosportpo[1].ToString() + " Cantidad: " + productosportpo[2].ToString() + " Precio: " + productosportpo[3].ToString() + " Info: " + productosportpo[4].ToString() + " Tipo de Producto: " + productosportpo[5].ToString() + "\n");
            }
            this.cmbBuscarporTipo.SelectedIndex = 0; 

        }
    }
}
