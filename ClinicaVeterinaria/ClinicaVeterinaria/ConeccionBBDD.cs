using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ClinicaVeterinaria
{
    public class ConeccionBBDD
    {
        // se hace la coneccion a la base de datos ventas, (es el nombre de la bases de datos general)
        // algunos metodos no pueden tener catch ya que al recivir valores estos no los toman

        public static string Connect = "Server = localhost; Database = ventas; Uid=root; pwd=;";
        MySqlConnection mySql = new MySqlConnection(Connect);
        
      
        // se cierra la coneccion
        public void Cerrar()
        {
            try
            {
                mySql.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // se agrega el producto al inventario
        public  void AgregarProductoInventario( string nombre, int cantidad, int precio, string info ,TipoProducto tipoProducto)
        {
            try
            {
                mySql.Open();
                String query = "INSERT INTO producto (nombre_producto,cantidad,precio,info,tipoproducto) VALUES ('" + nombre + "','" + cantidad + "','" + precio + "','" + info + "','" + tipoProducto.ToString() + "');";
                MySqlCommand command = new MySqlCommand(query, mySql);
                command.ExecuteNonQuery();
                Cerrar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

        // se elimina la cantidad de procuto ( se actualiza) 
        public void eliminarCantidadproducto(int id, int cantidad)
        {
            try
            {
                mySql.Open();
                String query = "UPDATE `producto` SET `cantidad` = " + cantidad + " WHERE `id_producto`= " + id + ";";
                MySqlCommand command = new MySqlCommand(query, mySql);
                command.ExecuteNonQuery();
                Cerrar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

        // se muestra el iventario
        public List<string>  mostrarInventario()
        {
            List<string> productos = new List<string>();
            mySql.Open();
            String query = "SELECT * FROM  producto ;";
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                productos.Add(reader["id_producto"].ToString() + ";" + reader["nombre_producto"].ToString() + ";" + reader["cantidad"].ToString() + ";" + reader["precio"].ToString() + ";" + reader["info"].ToString() + ";" + reader["tipoproducto"].ToString());
            }
            Cerrar();
            return productos;

        }


        // trae el inventario ( es el mismo metodo, pero se uso ya que en un momento la consulta era distinta, por lo que no se cambio por el uso que tiene de pueba)
        public List<string> traerinventario()
        {
            List<string> productos = new List<string>();
            mySql.Open();
            String query = "SELECT * FROM  producto ;";
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                productos.Add(reader["id_producto"].ToString()+ ";" + reader["nombre_producto"].ToString() + ";" + reader["cantidad"].ToString() + ";" + reader["precio"].ToString() + ";" + reader["info"].ToString() + ";" + reader["tipoproducto"].ToString());
            }
            Cerrar();
            return productos;

        }

        // trae todos los productos del inventario, solo la cantidad con el fin de que el metodo al que pase este cuente la cantidad ( podria hacerse con un count en el query pero en el momento generaba problemas al traer solo el count, por lo que se decicio hacer esto)
        public List<int> mostrarTotalinventario()
        {
            List<int> productos = new List<int>();
            mySql.Open();
            String query = "SELECT cantidad FROM  producto ;";
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                productos.Add(int.Parse(reader["cantidad"].ToString()));
            }
            Cerrar();
            return productos;

        }

        // se muestran los productos del inventario por el tipo de producto
        public List<string> mostrarinventarioportipo(TipoProducto tipoProducto)
        {
            List<string> productos = new List<string>();
            mySql.Open();
            String query = "SELECT `id_producto`, `nombre_producto`, `cantidad`, `precio`, `info`, `tipoproducto` FROM `producto` WHERE `tipoproducto` = '" + tipoProducto.ToString()+ "' ;";
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                productos.Add(reader["id_producto"].ToString() + ";" + reader["nombre_producto"].ToString() + ";" + reader["cantidad"].ToString() + ";" + reader["precio"].ToString() + ";" + reader["info"].ToString() + ";" + reader["tipoproducto"].ToString());
            }
            Cerrar();
            return productos;

        }

        // trae el id de los clientes
        public List<string> trearidcliente()
        {
            List<string> productos = new List<string>();
            mySql.Open();
            String query = "SELECT id_cliente , rut FROM  cliente ;";
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                productos.Add(reader["id_cliente"].ToString() + ";" + reader["rut"].ToString());
            }
            Cerrar();
            return productos;

        }

        // trae el id de las ventas
        public List<int> trearidventas()
        {
            List<int> idsventa = new List<int>();
            mySql.Open();
            String query = "SELECT id_venta FROM  venta ;";
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                idsventa.Add(int.Parse(reader["id_venta"].ToString()));
            }
            Cerrar();
            return idsventa;

        }
        // Genera las ventas
        public void generarventas(int monto, DateTime dateTime)
        {
            try
            {
                mySql.Open();
                String query = "INSERT INTO venta (`monto`, `fecha_venta`) VALUES ('" + monto + "','" + dateTime.ToString("yyyy/MM/dd") + "');";
                MySqlCommand command = new MySqlCommand(query, mySql);
                command.ExecuteNonQuery();
                Cerrar();

            }
            catch ( Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

        // genera un detalle por pedido de ventas
        public void generardetallepedido(int idventa,int idcliente , int idproducto)
        {
            try
            {
                mySql.Open();
                String query = "INSERT INTO detalledeventa (`id_venta`, `id_producto`, `id_cliente`) VALUES ('" + idventa + "','" + idproducto + "','" + idcliente + "');";
                MySqlCommand command = new MySqlCommand(query, mySql);
                command.ExecuteNonQuery();
                Cerrar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }
        //trae la cantidad del producto
        public int taercantidadproducto(int id)
        {
            int valorcantidad = 0;
            mySql.Open(); 
            String query = "SELECT  `cantidad` FROM `producto` WHERE `id_producto` = " + id + ";";
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                valorcantidad = (int.Parse(reader["cantidad"].ToString()));
            }
            Cerrar();
            return valorcantidad;
        }

        //trae las vtnras del dia
        public List<string> listadeventas()
        {
            List<string> ventas = new List<string>();
            mySql.Open(); 
            String query = "SELECT `id_venta`,`monto` FROM venta where `fecha_venta` = CURRENT_DATE(); ";
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ventas.Add(reader["id_venta"].ToString()+ ';'+ reader["monto"].ToString() );
            }
            
            Cerrar();
            return ventas;
        }

        //trae la ventas del dia, dependiendo el dia que se le ingrese
        public List<string> listadeventaspordia(DateTime date)
        {
            var fecha = date.ToString("yyyyMMdd");
            List<string> ventas = new List<string>();
            mySql.Open(); 
            String query = "SELECT v.id_venta, v.fecha_venta, v.monto, p.nombre_producto, p.precio FROM `venta` v INNER JOIN `detalledeventa` d ON v.id_venta = d.id_venta " +
                "INNER JOIN `producto` p ON d.id_producto = p.id_producto WHERE v.fecha_venta = '" + fecha+"' ORDER BY v.id_venta ASC ";
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ventas.Add(reader["id_venta"].ToString() + ';'+ reader["fecha_venta"].ToString() + ';'+reader["monto"].ToString() + ';' + reader["nombre_producto"].ToString() + ';' + reader["precio"].ToString());
            }

            Cerrar();
            return ventas;
        }

        // se agrega un cliente
        public string AgregarCliente(string nombre,string apellido, string rut, string direccion, string correo)
        {
            mySql.Open();
            String query = "INSERT INTO `cliente`(`nombre`, `apellido`, `direccion`, `rut`, `email`) VALUES ('" + nombre + "','" + apellido + "','" + direccion + "','" + rut + "','" + correo + "');";
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            Cerrar();
            return rut;
        }

        // se agrega una mascota
        public void  agregarmascota(string nombre, DateTime fechanacimiento, TipoMascota tipo)
        {
            mySql.Open();
            String query = "INSERT INTO `mascota` ( `nombre_mascota`, `tipo_mascota`, `fecha_nacimiento`) VALUES ('" + nombre + "','" + tipo.ToString() + "','" + fechanacimiento.ToString("yyyy/MM/dd") + "');";
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            Cerrar();
        }

        // se busca una mascota
        public int buscarmascota()
        {
            List<int> id = new List<int>();
            mySql.Open(); 
            String query = "SELECT `id_mascota` FROM `mascota` ; ";
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                id.Add( int.Parse(reader["id_mascota"].ToString()));
            }
            Cerrar();
            return id.Last();
        }
        // se agrega un paciente
        public void agregarpaciente(int id, int idcliente)
        {
            try
            {
                mySql.Open();
                String query = "INSERT INTO `pacientes`(`id_cliente`, `id_mascota`)VALUES ('" + idcliente + "','" + id + "');";
                MySqlCommand command = new MySqlCommand(query, mySql);
                command.ExecuteNonQuery();
                Cerrar();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        
        // se buscan las mascotas por el rut del dueño
        public List<string> buscarmascotasxrutdueno(string rut)
        {
            List<string> mascotas = new List<string>();
            mySql.Open();
            String query = ("SELECT mascota.id_mascota, mascota.nombre_mascota, mascota.tipo_mascota, mascota.fecha_nacimiento ,cliente.id_cliente , cliente.nombre , cliente.apellido , cliente.rut FROM `mascota` INNER JOIN `pacientes` ON mascota.id_mascota = pacientes.id_mascota INNER JOIN `cliente` on pacientes.id_cliente = cliente.id_cliente WHERE cliente.rut = " + rut + ";");
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                mascotas.Add(reader["id_mascota"].ToString() + ";" + reader["nombre_mascota"] + ";" + reader["tipo_mascota"] + ";"+ reader["fecha_nacimiento"].ToString()+ ";" + reader["id_cliente"].ToString());
            }
            Cerrar();
            return mascotas;
        }
       
        // trae el total de ventas del dia 
        public int  totalpordia (DateTime date)
        {
            int total = 0;
            var fecha = date.ToString("yyyyMMdd");
            mySql.Open(); 
            String query = "SELECT sum(monto) FROM `venta` WHERE `fecha_venta` = "+ fecha+"" ;
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
       
               total = int.Parse(reader.GetString(0));

            }

            Cerrar();
            return total;
        }

        // busca paciente por el id del dueño y de la mascota (trae el id paciente para poder hacer la toma de  hora)
        public string buscarpaciente(string idduenio, string idmascota)
        {
            
            mySql.Open();
            String query = ("SELECT id_paciente FROM `pacientes` WHERE id_cliente = "+ idduenio + "&&" + " id_mascota =" + idmascota +  "; ");
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            MySqlDataReader reader = command.ExecuteReader();
            string idpaciente = "";
            while (reader.Read())
            {
               idpaciente = reader["id_paciente"].ToString();
            }
            Cerrar();
            return idpaciente;
        }

        // trae los datos del medico segun la especialidad
        public List<string> datosmedico(string especialidad)
        {
            
            List<string> datos = new List<string>();
            mySql.Open();
            String query = ("SELECT `id_medico`,`nombre`,`rut` FROM `medico` INNER JOIN `especialidad` on medico.id_especialidad = especialidad.id_especialidad WHERE especialidad.nombre_especialidad = '" + especialidad.ToString() + "' ;");
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                datos.Add(reader["id_medico"].ToString() + ";" + reader["nombre"] + ";" + reader["rut"]);
            }
            Cerrar();
            return datos;
        }

        // se genera la hora
        public void Generarhora(string idpaciente, int idmedico, DateTime fecha , string hora)
        {
            try
            {
                mySql.Open();
                String query = "INSERT INTO `consulta`(`id_paciente`, `id_medico`, `fecha`, `hora`) VALUES ('" + idpaciente + "','" + idmedico.ToString() + "','" + fecha.ToString("yyyy/MM/dd") + "','" + hora + "');";
                MySqlCommand command = new MySqlCommand(query, mySql);
                command.ExecuteNonQuery();
                Cerrar();
            }
            catch (Exception)
            {

                throw;
            }
        }

        //se traen las horas segun la fecha
        public List<String> HoraPorFecha(DateTime date)
        { 
            List<string> horas = new List<string>();
            var fecha = date.ToString("yyyyMMdd");
            mySql.Open(); 
            String query = "SELECT `hora` FROM `consulta` WHERE `fecha` = " + fecha + "";
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                horas.Add(reader["hora"].ToString());
            }

            Cerrar();
            return horas;
        }

        //trae las atenciones de dia (cantidad)
        public int cantidaddeatencionesxdia(DateTime date)
        {
            int total = 0;
            var fecha = date.ToString("yyyyMMdd");
            mySql.Open();
            String query = "SELECT COUNT(*) FROM `consulta` WHERE `fecha` = " + fecha + "";
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                total = int.Parse(reader.GetString(0));

            }

            Cerrar();
            return total;
        }

        // se elimina la consulta
        public void eliminarconsulta(string idpaciente  , DateTime date , string hora)
        {
      
            try
            {
                mySql.Open();
                var fecha = date.ToString("yyyyMMdd");
                String query = "DELETE FROM `consulta` WHERE `id_paciente` = '" + idpaciente + "' and `fecha` = '" + fecha + "' and `hora` = '" + hora + "');";
                MySqlCommand command = new MySqlCommand(query, mySql);
                command.ExecuteNonQuery();
                Cerrar();
            }
            catch(Exception ex)
            {
                Console.WriteLine("OPPS! HAY UN PROBLEMA CON LOS DATOS" + ex);
            }
        }


       // se traen  las consultas para el calendario
        public List<string> traerdatosparacalendario(DateTime date)
        {
            List<string> horas = new List<string>();
            var fecha = date.ToString("yyyyMMdd");
            mySql.Open(); 
            String query = "SELECT `id_paciente`,`id_medico`,`hora` FROM `consulta` WHERE `fecha` = " + fecha + "";
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                horas.Add(reader["id_paciente"]+";"+reader["id_medico"]+";"+reader["hora"].ToString());
            }

            Cerrar();
            return horas;
        }

        //trae la lista de pacientes
        public List<string> listapacientes()
        {
            List<string> mascotas = new List<string>();
            mySql.Open();
            String query = "SELECT pacientes.id_paciente, mascota.nombre_mascota, mascota.tipo_mascota FROM `pacientes` INNER JOIN `mascota` on pacientes.id_mascota = mascota.id_mascota";
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                mascotas.Add(reader["id_paciente"] + ";" + reader["nombre_mascota"] + ";" + reader["tipo_mascota"].ToString());
            }
            Cerrar();
            return mascotas;
        }

       
    } 

}
