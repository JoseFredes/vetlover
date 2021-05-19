using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaVeterinaria
{
    public class Venta
    {
        ConeccionBBDD basededatos = new ConeccionBBDD();
        
        public Venta()
        {
            ListaProductos = new List<Producto>();
            ProductosSeleccionados = new List<Producto>();

        }

        public List<Producto> ListaProductos { get; set; }

        public List<Producto> ProductosSeleccionados { get; set; }

        public DateTime FechadeVenta { get; set; }
        public void AgregarProducto(Producto producto)
        {

        }
        public List<Producto> AgregarProductosAlCarrito(int id)
        {
            String[] ProductosBBDD = new String[basededatos.mostrarInventario().Count];
            foreach (var producto in basededatos.mostrarInventario().ToList())
            {
                string linea = producto.ToString();
                ProductosBBDD = linea.Split(';');
                if (id.Equals(int.Parse(ProductosBBDD[0])))
                {
                    Producto ProductosEnCarrito = new Producto();
                    ProductosEnCarrito.IdProducto = int.Parse(ProductosBBDD[0]);
                    ProductosEnCarrito.NombreProducto = ProductosBBDD[1];
                    ProductosEnCarrito.CantidadProducto = 1; //la cantidad se deja en uno ya que es la cantidad que el cliente se lleva.
                    ProductosEnCarrito.PrecioProducto = int.Parse(ProductosBBDD[3]);
                    ProductosEnCarrito.InfoProducto = ProductosBBDD[4];

                    if (TipoProducto.Alimento.ToString().Equals(ProductosBBDD[5]))
                        ProductosEnCarrito.Tipoproducto = TipoProducto.Alimento;
                    if (TipoProducto.Juguete.ToString().Equals(ProductosBBDD[5]))
                        ProductosEnCarrito.Tipoproducto = TipoProducto.Juguete;
                    if (TipoProducto.Insumo.ToString().Equals(ProductosBBDD[5]))
                        ProductosEnCarrito.Tipoproducto = TipoProducto.Insumo;
                    if (TipoProducto.Varios.ToString().Equals(ProductosBBDD[5]))
                        ProductosEnCarrito.Tipoproducto = TipoProducto.Varios;
                    if (TipoProducto.RopaAnimal.ToString().Equals(ProductosBBDD[5]))
                        ProductosEnCarrito.Tipoproducto = TipoProducto.RopaAnimal;

                    ProductosSeleccionados.Add(ProductosEnCarrito);
                }
            }

            return ProductosSeleccionados;
        }

        public void GenerarVenta(int montoTotal, DateTime dateTime,int idcliente, List<int> idproductos)
        {
            basededatos.generarventas( montoTotal, dateTime);
            int idventa = basededatos.trearidventas().ToList().Last(); 
            for (int i = 0; i < idproductos.Count; i++)
            {
                basededatos.generardetallepedido(idventa,idcliente, idproductos[i]);
            }
           
        }

        public int TotalBoleta(List<int> listatotal)
        {
            return listatotal.Sum();
        }

        public int EliminarProductoInventario( int cantidadint , int cantExitente)
        {
            int resultado = cantExitente - cantidadint;
            return resultado;
        }

        public List<Producto> MostrarInventario()
        {
            return ListaProductos;
        }

        public void eliminarProductoCarrito(int id)
        {
            for (int i = 0; i < ProductosSeleccionados.Count; i++)
            {
                if (ProductosSeleccionados[i].IdProducto == id)
                {
                    ProductosSeleccionados.Remove(ProductosSeleccionados[i]);
                }
            }

        }
      
    }
}
