using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace ClaseBase
{
    public class TrabajarClientes
    {
        //Ontengo la ruta absoluta en donde se instalo la aplicacion
        private static string nuevoDir = AppDomain.CurrentDomain.BaseDirectory;

        //Metodo para listar todos los clientes de la base de datos
        public static ObservableCollection<Cliente> traer_clientes()
        {
            ObservableCollection<Cliente> clientes = new ObservableCollection<Cliente>();
            //Modifico la cadena de conexcion con la ruta en donde se guardo la aplicacion
            AppDomain.CurrentDomain.SetData("DataDirectory", nuevoDir);
            using (SqlConnection cn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString))
            {
                cn.Open();
                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "listar_clientes";

                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Cliente oCliente = new Cliente();
                                oCliente.Cli_Id = int.Parse(dr["cli_Id"].ToString());
                                oCliente.Cli_Apellido = dr["cli_Apellido"].ToString();
                                oCliente.Cli_Nombre = dr["cli_Nombre"].ToString();
                                oCliente.Cli_DNI = long.Parse(dr["cli_DNI"].ToString());
                                oCliente.Cli_Telefono = long.Parse(dr["cli_Telefono"].ToString());
                                clientes.Add(oCliente);
                            }
                        }

                    }

                }

            }

            return clientes;
        }
        // otra forma v2 de listar datos
        public DataTable TraerClientes()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", nuevoDir);
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection cn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("listar_clientes", cn)) // Nombre del procedimiento almacenado
                    {
                        cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                return dt;
            }
        }

        public static void alta_cliente(Cliente cliente)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", nuevoDir);
            using (SqlConnection cn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("alta_cliente", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", cliente.Cli_Nombre);
                    cmd.Parameters.AddWithValue("@apellido", cliente.Cli_Apellido);
                    cmd.Parameters.AddWithValue("@dni", cliente.Cli_DNI);
                    cmd.Parameters.AddWithValue("@telefono", cliente.Cli_Telefono);
                    cmd.Parameters.AddWithValue("@ex", cliente.Cli_Ex);
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public static void modificar_cliente(Cliente cliente)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", nuevoDir);
            using (SqlConnection cn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("modificar_cliente", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", cliente.Cli_Id);
                    cmd.Parameters.AddWithValue("@NuevoApellido", cliente.Cli_Apellido);
                    cmd.Parameters.AddWithValue("@NuevoNombre", cliente.Cli_Nombre);
                    cmd.Parameters.AddWithValue("@NuevoDNI", cliente.Cli_DNI);
                    cmd.Parameters.AddWithValue("@NuevoTelefono", cliente.Cli_Telefono);
                    cmd.Parameters.AddWithValue("@Ex", cliente.Cli_Ex);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void eliminar_cliente(Cliente cliente)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", nuevoDir);
            using (SqlConnection cn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("eliminar_cliente", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", cliente.Cli_Id);
                    cmd.ExecuteNonQuery();

                }
                cn.Close();
            }
        }
        public static Cliente traer_cliente_por_dni(int dni)
        {
            Cliente cliente = null;
            AppDomain.CurrentDomain.SetData("DataDirectory", nuevoDir);
            using (SqlConnection cn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("traer_cliente_dni", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@dni", SqlDbType.Int, 20));
                    cmd.Parameters["@dni"].Value = dni;
                    cn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cliente = new Cliente
                            {
                                Cli_Id = int.Parse(reader["cli_Id"].ToString()),
                                Cli_Apellido = reader["cli_Apellido"].ToString(),
                                Cli_Nombre = reader["cli_Nombre"].ToString(),
                                Cli_DNI = long.Parse(reader["cli_DNI"].ToString()),
                                Cli_Telefono = long.Parse(reader["cli_Telefono"].ToString())
                            };
                        }
                    }
                }
            }

            return cliente;
        }

    }
}
