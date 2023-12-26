using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace ClasesBase
{
    public class TrabajarCliente
    {
        public static void alta_cliente(Cliente cliente) {

            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.opticaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Cliente(cli_DNI,cli_Apellido,cli_Nombre,cli_Direccion,os_CUIT,cli_NroCarnet,cli_Activo) values(@dni,@ape,@nom,@dir,@cuit,@nrocarnet,@activo)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@dni",cliente.Cli_DNI);
            cmd.Parameters.AddWithValue("@ape",cliente.Cli_Apellido);
            cmd.Parameters.AddWithValue("@nom",cliente.Cli_Nombre);
            cmd.Parameters.AddWithValue("@dir",cliente.Cli_Direccion);
            cmd.Parameters.AddWithValue("@cuit",cliente.Os_CUIT);
            cmd.Parameters.AddWithValue("@nrocarnet",cliente.Cli_NroCarnet);
            cmd.Parameters.AddWithValue("@activo", cliente.Cli_Activo);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static DataTable listar_Clientes() {

            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.opticaConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "listarClientes";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            //Ejecuta la consulta
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //Llena los datos de la consulta en el DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);


            return dt;
        }
        public static void actualizar_cliente(Cliente cliente){
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.opticaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE Cliente SET cli_DNI=@dni,cli_Apellido=@ape,cli_Nombre=@nom,cli_Direccion=@dir,os_CUIT=@cuit,cli_NroCarnet=@nrocarnet WHERE cli_Id=" + cliente.Cli_Id;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@dni",cliente.Cli_DNI);
            cmd.Parameters.AddWithValue("@ape",cliente.Cli_Apellido);
            cmd.Parameters.AddWithValue("@nom",cliente.Cli_Nombre);
            cmd.Parameters.AddWithValue("@dir",cliente.Cli_Direccion);
            cmd.Parameters.AddWithValue("@cuit",cliente.Os_CUIT);
            cmd.Parameters.AddWithValue("@nrocarnet",cliente.Cli_NroCarnet);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }


        public static void eliminar_cliente(Cliente cliente)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.opticaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE Cliente SET cli_Activo=@activo WHERE cli_Id=" + cliente.Cli_Id;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@activo", cliente.Cli_Activo);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }    
        public static DataTable buscar_Clientes(string parametro) {

            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.opticaConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "buscarClientes";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@parametro", "%"+parametro+"%");

            //Ejecuta la consulta
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //Llena los datos de la consulta en el DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public static DataTable ordenar_Clientes() {

            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.opticaConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "ordenarClientes";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            //Ejecuta la consulta
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //Llena los datos de la consulta en el DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public static DataTable list_OS() {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.opticaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM ObraSocial";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public static DataTable listar_clientes_combobox()
        {
            DataTable dt = new DataTable();
            dt = listar_Clientes();

            // Columna para la visualización personalizada en el ComboBox
            dt.Columns.Add("ClienteDisplay", typeof(string), "DNI + ' - ' + Apellido + ' ' + Nombre");

            return dt;
        }

        public static DataTable listarClientesPorObraSocial(string os_CUIT)
        {

            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.opticaConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "listarClientesPorObraSocial";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            //Ejecuta la consulta
            cmd.Parameters.AddWithValue("@os_CUIT", os_CUIT);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //Llena los datos de la consulta en el DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
