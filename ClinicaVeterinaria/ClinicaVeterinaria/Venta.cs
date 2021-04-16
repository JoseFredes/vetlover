using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaVeterinaria
{
    public class Venta
    {
        public Venta()
        {
            ListaProductos = new List<Producto>();
            ProductosSeleccionados = new List<Producto>();

        }

        public List<Producto> ListaProductos { get; set; }

        public List<Producto> ProductosSeleccionados { get; set; }

        public List<Producto> AgregarProducto(List<Producto> listaProductos)
        { //retorna la lista con los productos seleccionados
            return listaProductos;
        }

        public void GenerarVenta()
        {

        }

        public int TotalBoleta()
        { //se saca la suma
            int totaldelaboleta = 0;
            for (int i = 0; i < ProductosSeleccionados.Count(); i++)
            {
                totaldelaboleta =+ ProductosSeleccionados[i].PrecioProducto;
            }

            return totaldelaboleta;
        }

        public void EliminarProducto(Producto producto)
        { // no se si debe entrar una lista de productos o un objeto producto, según yo es un objeto produucto

        }
    }
}
