using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace ClasesBase
{
    public class TrabajarProducto
    {

        public static void alta_producto(Producto producto)
        {

            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.opticaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Producto(prod_Codigo,prod_Categoria,prod_Descripcion,prod_Precio) values(@cod,@cat,@des,@pre)";
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "alta_producto_sp";
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;


            cmd.Parameters.AddWithValue("@cod", producto.Prod_Codigo);
            cmd.Parameters.AddWithValue("@cat", producto.Prod_Categoria);
            cmd.Parameters.AddWithValue("@des", producto.Prod_Descripcion);
            cmd.Parameters.AddWithValue("@pre", producto.Prod_Precio);


            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();


        }

        public static void actualizat_producto(Producto producto)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.opticaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE Producto SET prod_Codigo=@cod,prod_Categoria=@cat,prod_Descripcion=@des,prod_Precio=@pre WHERE prod_Id=" + producto.Prod_Id;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cmd.Parameters.AddWithValue("@cod", producto.Prod_Codigo);
            cmd.Parameters.AddWithValue("@cat",producto.Prod_Categoria);
            cmd.Parameters.AddWithValue("@des", producto.Prod_Descripcion);
            cmd.Parameters.AddWithValue("@pre", producto.Prod_Precio);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static void eliminar_porducto(Producto producto)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.opticaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "DELETE FROM Producto WHERE prod_Id=" + producto.Prod_Id;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();


        }

        public static DataTable listar_productos()
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.opticaConnectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT prod_Id as 'ID', prod_Codigo as 'Codigo', prod_Categoria 'Categoria', prod_Descripcion as 'Descripcion', prod_Precio as 'Precio' FROM Producto";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        


        public static DataTable ordenar_Productos(string orden)
        {

            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.opticaConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "ordenar_productos_sp";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@orden", orden);

            //Ejecuta la consulta
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //Llena los datos de la consulta en el DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }


        public static DataTable exec_listar_productos(DateTime param1, DateTime param2)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.opticaConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "listar_productos_sp";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            //Parametro de entrada
            cmd.Parameters.AddWithValue("@param1", param1);
            cmd.Parameters.AddWithValue("@param2", param2);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public static DataSet lista_productos_vendidos_x_cliente(int id)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.opticaConnectionString);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("listar_productos_x_cliente", cnn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("@ID", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = id;
            da.SelectCommand.Parameters.Add(param);

            DataSet ds = new DataSet();
            da.Fill(ds, "vw_listar_producto_cliente");

            return ds;
        }
    }
}
