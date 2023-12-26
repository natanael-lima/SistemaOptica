using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace ClaseBase
{
    public class TrabajarTipoVehiculos
    {
        //Ontengo la ruta absoluta en donde se instalo la aplicacion
        private static string nuevoDir = AppDomain.CurrentDomain.BaseDirectory;

        // Guarda información de un nuevo tipo de vehiculo
        public static void guardar_tipo_vehiculo(TipoVehiculo tipo_vehiculo)
        {
            //Modifico la cadena de conexcion con la ruta en donde se guardo la aplicacion
            AppDomain.CurrentDomain.SetData("DataDirectory", nuevoDir);

            SqlConnection cnn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "INSERT INTO TipoVehiculo(tv_Descripcion,tv_Tarifa,tv_Imagen, tv_Ex) values (@descripcion,@tarifa,@imagen,@ex)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cmd.Parameters.AddWithValue("@descripcion", tipo_vehiculo.Tv_Descripcion);
            cmd.Parameters.AddWithValue("@tarifa", tipo_vehiculo.Tv_Tarifa);
            cmd.Parameters.AddWithValue("@imagen", tipo_vehiculo.Tv_Imagen);
            cmd.Parameters.AddWithValue("@ex", tipo_vehiculo.Tv_Ex);
            cnn.Open();
            cmd.ExecuteNonQuery();
        }

        //Muestra contenido de la bs de la tabla tipo vehiculo en el datagrid
        public static List<TipoVehiculo> traer_tipos_vehiculos()
        {
           
            List<TipoVehiculo> lista_tv = new List<TipoVehiculo>(); //lista de tipo de vehiculos

            //Modifico la cadena de conexcion con la ruta en donde se guardo la aplicacion
            AppDomain.CurrentDomain.SetData("DataDirectory", nuevoDir);

            using (SqlConnection conexion = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString))
            {
                string consulta = "SELECT * FROM TipoVehiculo WHERE tv_Ex = 1";
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    conexion.Open();
                    //Recorre la base de datos y los carga a la lista_tv
                    using (DbDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                TipoVehiculo oTv = new TipoVehiculo();
                                oTv.Tv_Id = int.Parse(dr["tv_Id"].ToString());
                                oTv.Tv_Descripcion = dr["tv_Descripcion"].ToString();
                                oTv.Tv_Tarifa = decimal.Parse(dr["tv_Tarifa"].ToString());
                                oTv.Tv_Imagen = dr["tv_Imagen"].ToString();
                                lista_tv.Add(oTv);
                            }
                        }

                    }
                }
            }
            return lista_tv;
        }

        public static void eliminarTipoVehiculo(int id)
        {
            //Modifico la cadena de conexcion con la ruta en donde se guardo la aplicacion
            AppDomain.CurrentDomain.SetData("DataDirectory", nuevoDir);

            using (SqlConnection cn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("eliminar_vehiculo", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();

                }
                cn.Close();
            }
        }

        public static void editarTipoVehiculo(TipoVehiculo veh)
        {
            //Modifico la cadena de conexcion con la ruta en donde se guardo la aplicacion
            AppDomain.CurrentDomain.SetData("DataDirectory", nuevoDir);

            using (SqlConnection cn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("modificar_vehiculo", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", veh.Tv_Id);
                    cmd.Parameters.AddWithValue("@desc", veh.Tv_Descripcion);
                    cmd.Parameters.AddWithValue("@tarifa", veh.Tv_Tarifa);
                    cmd.Parameters.AddWithValue("@imagen", veh.Tv_Imagen);
                    cmd.Parameters.AddWithValue("@ex", veh.Tv_Ex);
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
