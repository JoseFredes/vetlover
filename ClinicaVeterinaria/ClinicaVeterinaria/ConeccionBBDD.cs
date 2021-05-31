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
        public static string Connect = "Server = localhost; Database = ventas; Uid=root; pwd=;";
        MySqlConnection mySql = new MySqlConnection(Connect);
        
      
        public void Cerrar()
        {
            mySql.Close();
        }
        public  void AgregarProductoInventario( string nombre, int cantidad, int precio, string info ,TipoProducto tipoProducto)
        {
            mySql.Open();
            String query = "INSERT INTO producto (nombre_producto,cantidad,precio,info,tipoproducto) VALUES ('" + nombre + "','" + cantidad + "','" + precio + "','" + info + "','" + tipoProducto.ToString() + "');";
            MySqlCommand command = new MySqlCommand(query,mySql);
            command.ExecuteNonQuery();
            Cerrar();
        }

        public void eliminarCantidadproducto(int id, int cantidad)
        {
            mySql.Open();
            String query = "UPDATE `producto` SET `cantidad` = "+ cantidad + " WHERE `id_producto`= "+ id +";";
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            Cerrar();
        }
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

        public void generarventas(int monto, DateTime dateTime)
        {
            mySql.Open();
            String query = "INSERT INTO venta (`monto`, `fecha_venta`) VALUES ('" + monto + "','" + dateTime.ToString("yyyy/MM/dd") + "');";
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            Cerrar();
        }

        public void generardetallepedido(int idventa,int idcliente , int idproducto)
        {
            mySql.Open();
            String query = "INSERT INTO detalledeventa (`id_venta`, `id_producto`, `id_cliente`) VALUES ('" + idventa + "','" + idproducto + "','" + idcliente + "');";
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            Cerrar();
        }

        public int taercantidadproducto(int id)
        {
            int valorcantidad = 0;
            mySql.Open(); //SELECT  `cantidad` FROM `producto` WHERE `id_producto` = 1;
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

        public List<string> listadeventas()
        {
            List<string> ventas = new List<string>();
            mySql.Open(); //SELECT COUNT(*) FROM `venta` WHERE `fecha_venta` = "20210517"
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
        public List<string> listadeventaspordia(DateTime date)
        {
            var fecha = date.ToString("yyyyMMdd");
            List<string> ventas = new List<string>();
            mySql.Open(); //SELECT COUNT(*) FROM `venta` WHERE `fecha_venta` = "20210517"
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


        public string AgregarCliente(string nombre,string apellido, string rut, string direccion, string correo)
        {
            mySql.Open();
            String query = "INSERT INTO `cliente`(`nombre`, `apellido`, `direccion`, `rut`, `email`) VALUES ('" + nombre + "','" + apellido + "','" + direccion + "','" + rut + "','" + correo + "');";
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            Cerrar();

            return rut;
        }

        public void  agregarmascota(string nombre, DateTime fechanacimiento, TipoMascota tipo)
        {
            //INSERT INTO `mascota`( `nombre_mascota`, `tipo_mascota`, `fecha_nacimiento`) VALUES('[value-1]', '[value-2]', '[value-3]')
            mySql.Open();
            String query = "INSERT INTO `mascota` ( `nombre_mascota`, `tipo_mascota`, `fecha_nacimiento`) VALUES ('" + nombre + "','" + tipo.ToString() + "','" + fechanacimiento.ToString("yyyy/MM/dd") + "');";
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            Cerrar();
        }

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

        public void agregarpaciente(int id, int idcliente)
        {
            mySql.Open(); //INSERT INTO `pacientes`(`id_paciente`, `id_cliente`, `id_mascota`) VALUES ('[value-1]','[value-2]','[value-3]')
            String query = "INSERT INTO `pacientes`(`id_cliente`, `id_mascota`)VALUES ('" + idcliente + "','" + id  + "');";
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            Cerrar();

        }
        //SELECT * FROM `pacientes` INNER JOIN`cliente`  on pacientes.id_cliente = cliente.id_cliente WHERE cliente.rut = "182625957"

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
        //SELECT sum(`monto`) FROM `venta` WHERE `fecha_venta` = "2021/05/17"

        public int  totalpordia (DateTime date)
        {
            int total = 0;
            var fecha = date.ToString("yyyyMMdd");
            mySql.Open(); //SELECT COUNT(*) FROM `venta` WHERE `fecha_venta` = "20210517"
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
        public void Generarhora(string idpaciente, int idmedico, DateTime fecha , string hora)
        {
           
            mySql.Open();
            String query = "INSERT INTO `consulta`(`id_paciente`, `id_medico`, `fecha`, `hora`) VALUES ('" + idpaciente + "','" + idmedico.ToString() + "','" + fecha.ToString("yyyy/MM/dd") + "','" + hora + "');";
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            Cerrar();
        }

        public List<String> HoraPorFecha(DateTime date)
        { 
            List<string> horas = new List<string>();
            var fecha = date.ToString("yyyyMMdd");
            mySql.Open(); //SELECT `hora` FROM `consulta` WHERE `fecha` = "20210530"
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


        public void eliminarconsulta(string idpaciente  , DateTime date , string hora)
        {
            //DELETE FROM `consulta` WHERE `id_paciente` = "1" and `id_medico` = "2" and `fecha` = "2021/05/30" and `hora` = "9:00 AM" 
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

       
        public List<string> traerdatosparacalendario(DateTime date)
        {
            List<string> horas = new List<string>();
            var fecha = date.ToString("yyyyMMdd");
            mySql.Open(); //SELECT `hora` FROM `consulta` WHERE `fecha` = "20210530"
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

        public List<string> listapacientes()
        {
            List<string> mascotas = new List<string>();
            mySql.Open(); //SELECT `hora` FROM `consulta` WHERE `fecha` = "20210530"
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

        public List<string> Consultarhoradepaciente(string rut, DateTime date, string nombremasctoa)
        {
            mySql.Open();
            List<string> listapacientes = new List<string>();
            var fecha = date.ToString("yyyyMMdd");
            String query = ("SELECT mascota.nombre_mascota, mascota.tipo_mascota, consulta.hora FROM `cliente` INNER JOIN `pacientes` ON cliente.id_cliente = pacientes.id_cliente INNER JOIN `mascota` on pacientes.id_mascota = mascota.id_mascota INNER JOIN `consulta` on consulta.id_paciente = pacientes.id_paciente WHERE cliente.rut = '" + rut + "' AND mascota.nombre_mascota = '" + nombremasctoa +"' AND consulta.fecha = '"+ fecha +"' ; ");
            MySqlCommand command = new MySqlCommand(query, mySql);
            command.ExecuteNonQuery();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                listapacientes.Add(reader["nombre_mascota"].ToString() + reader["tipo_mascota"].ToString()+ reader["hora"].ToString());
            }
            Cerrar();
            return listapacientes;
        }

    } 

}
