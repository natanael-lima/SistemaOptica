using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Collections.ObjectModel;
namespace ClaseBase
{
    public class TrabajarSector
    {
        //Ontengo la ruta absoluta en donde se instalo la aplicacion
        private static string nuevoDir = AppDomain.CurrentDomain.BaseDirectory;

        public static Sector traerSector(string cod)
        {
            Sector sec = new Sector();

            //Modifico la cadena de conexcion con la ruta en donde se guardo la aplicacion
            AppDomain.CurrentDomain.SetData("DataDirectory", nuevoDir);

            using (SqlConnection cn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString))
            {
                cn.Open();
                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM Sector WHERE (sec_Identificador LIKE '" + cod + "')";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            // Mapear las columnas según la estructura de tu tabla Sector
                            sec.Sec_Codigo = Convert.ToInt32(reader["sec_Id"]);
                            sec.Sec_Descripcion = Convert.ToString(reader["sec_Descripcion"]);
                            sec.Sec_Habilitado = Convert.ToInt32(reader["sec_Habilitado"]);
                            sec.Sec_Identificador = Convert.ToString(reader["sec_Identificador"]);
                            sec.Zona_Codigo = Convert.ToInt32(reader["zona_Codigo"]);
                            // Otros atributos

                        }
                    }
                }
            }
            return sec;
        }
        public static void estadoSector(Sector sector)
        {
            //Modifico la cadena de conexcion con la ruta en donde se guardo la aplicacion
            AppDomain.CurrentDomain.SetData("DataDirectory", nuevoDir);

            using (SqlConnection cn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE Sector SET sec_Habilitado = " + sector.Sec_Habilitado + " WHERE sec_Id = " + sector.Sec_Codigo, cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static ObservableCollection<Sector> traerSectores()
        {
            ObservableCollection<Sector> listaSectores = new ObservableCollection<Sector>();

            //Modifico la cadena de conexcion con la ruta en donde se guardo la aplicacion
            AppDomain.CurrentDomain.SetData("DataDirectory", nuevoDir);

            using (SqlConnection cn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString))
            {
                cn.Open();
                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM Sector";

                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Sector sector = new Sector();
                                sector.Sec_Codigo = int.Parse(dr["sec_Id"].ToString());
                                sector.Sec_Descripcion = dr["sec_Descripcion"].ToString();
                                sector.Sec_Habilitado = int.Parse(dr["sec_Habilitado"].ToString());
                                sector.Sec_Identificador = dr["sec_Identificador"].ToString();
                                sector.Zona_Codigo = int.Parse(dr["zona_Codigo"].ToString());
                                listaSectores.Add(sector);
                            }
                        }
                    }
                }
            }
            return listaSectores;
        }
    }
}