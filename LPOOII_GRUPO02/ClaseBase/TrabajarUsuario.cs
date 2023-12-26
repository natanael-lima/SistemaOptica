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
    public class TrabajarUsuario
    {
        static string nuevoDir = AppDomain.CurrentDomain.BaseDirectory;

        public static ObservableCollection<Usuario> traerUsuarios()
        {
            //Modifico la cadena de conexcion con la ruta en donde se guardo la aplicacion
            AppDomain.CurrentDomain.SetData("DataDirectory", nuevoDir);
            
            ObservableCollection<Usuario> listaUsuario = new ObservableCollection<Usuario>();

            using (SqlConnection cn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString))
            {
                cn.Open();
                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "listar_usuarios";

                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Usuario user = new Usuario();
                                user.User_Id = int.Parse(dr["user_Id"].ToString());
                                user.User_Name = dr["user_Name"].ToString();
                                user.User_Password = dr["user_Password"].ToString();
                                user.User_Nombre = dr["user_Nombre"].ToString();
                                user.User_Apellido = dr["user_Apellido"].ToString();
                                user.User_Rol = dr["user_Rol"].ToString();
                                user.User_Foto = dr["user_Foto"].ToString();
                                listaUsuario.Add(user);
                            }
                        }
                    }
                }
            }
            return listaUsuario;
        }

        public static void altaUsuario(Usuario user)
        {
            //Modifico la cadena de conexcion con la ruta en donde se guardo la aplicacion
            AppDomain.CurrentDomain.SetData("DataDirectory", nuevoDir);

            using (SqlConnection cn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("alta_usuario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", user.User_Name);
                    cmd.Parameters.AddWithValue("@password", user.User_Password);
                    cmd.Parameters.AddWithValue("@nombre", user.User_Nombre);
                    cmd.Parameters.AddWithValue("@apellido", user.User_Apellido);
                    cmd.Parameters.AddWithValue("@rol", user.User_Rol);
                    cmd.Parameters.AddWithValue("@foto", user.User_Foto);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void eliminarUsuario(int id)
        {
            //Modifico la cadena de conexcion con la ruta en donde se guardo la aplicacion
            AppDomain.CurrentDomain.SetData("DataDirectory", nuevoDir);
            
            using (SqlConnection cn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("eliminar_usuario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();

                }
                cn.Close();
            }
        }

        public static void editarUsuario(Usuario user)
        {
            //Modifico la cadena de conexcion con la ruta en donde se guardo la aplicacion
            AppDomain.CurrentDomain.SetData("DataDirectory", nuevoDir);
            
            using (SqlConnection cn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("modificar_usuario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", user.User_Id);
                    cmd.Parameters.AddWithValue("@username", user.User_Name);
                    cmd.Parameters.AddWithValue("@password", user.User_Password);
                    cmd.Parameters.AddWithValue("@name", user.User_Nombre);
                    cmd.Parameters.AddWithValue("@apellido", user.User_Apellido);
                    cmd.Parameters.AddWithValue("@rol", user.User_Rol);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static ObservableCollection<Usuario> traerUsuariosASC()
        {
            ObservableCollection<Usuario> listaUsuario = new ObservableCollection<Usuario>();

            //Modifico la cadena de conexcion con la ruta en donde se guardo la aplicacion
            AppDomain.CurrentDomain.SetData("DataDirectory", nuevoDir);
            
            using (SqlConnection cn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString))
            {
                cn.Open();
                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "listar_usuariosASC";
                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Usuario user = new Usuario();
                                user.User_Id = int.Parse(dr["user_Id"].ToString());
                                user.User_Name = dr["user_Name"].ToString();
                                user.User_Password = dr["user_Password"].ToString();
                                user.User_Nombre = dr["user_Nombre"].ToString();
                                user.User_Apellido = dr["user_Apellido"].ToString();
                                user.User_Rol = dr["user_Rol"].ToString();
                                listaUsuario.Add(user);
                            }
                        }
                    }
                }
            }
            return listaUsuario;
        }
        public static ObservableCollection<Usuario> traerUsuariosFiltro(string filtro)
        {
            //Modifico la cadena de conexcion con la ruta en donde se guardo la aplicacion
            AppDomain.CurrentDomain.SetData("DataDirectory", nuevoDir);
            
            ObservableCollection<Usuario> listaUsuario = new ObservableCollection<Usuario>();

            using (SqlConnection cn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString))
            {
                cn.Open();
                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "listar_usuariosFiltro";
                    cmd.Parameters.AddWithValue("@pattern", filtro + "%");
                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Usuario user = new Usuario();
                                user.User_Id = int.Parse(dr["user_Id"].ToString());
                                user.User_Name = dr["user_Name"].ToString();
                                user.User_Password = dr["user_Password"].ToString();
                                user.User_Nombre = dr["user_Nombre"].ToString();
                                user.User_Apellido = dr["user_Apellido"].ToString();
                                user.User_Rol = dr["user_Rol"].ToString();
                                listaUsuario.Add(user);
                            }
                        }
                    }
                }
            }
            return listaUsuario;
        }

        public static ObservableCollection<Usuario> search_usuarios(string sPattern)
        {
            //Modifico la cadena de conexcion con la ruta en donde se guardo la aplicacion
            AppDomain.CurrentDomain.SetData("DataDirectory", nuevoDir);

            ObservableCollection<Usuario> listaUsuario = new ObservableCollection<Usuario>();

            using (SqlConnection cn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString))
            {
                cn.Open();
                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "traer_usuario";  // Cambiado a "traer_usuarios" si es el procedimiento almacenado correcto
                    cmd.Parameters.AddWithValue("@username", "%" + sPattern + "%");
                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Usuario user = new Usuario();
                                user.User_Id = int.Parse(dr["user_Id"].ToString());
                                user.User_Name = dr["user_Name"].ToString();
                                user.User_Password = dr["user_Password"].ToString();
                                user.User_Nombre = dr["user_Nombre"].ToString();
                                user.User_Apellido = dr["user_Apellido"].ToString();
                                user.User_Rol = dr["user_Rol"].ToString();
                                user.User_Foto = dr["user_Foto"].ToString();
                                listaUsuario.Add(user);
                            }
                        }
                    }
                }
            }
            return listaUsuario;
        }
    }
}
