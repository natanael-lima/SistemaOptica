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
    public class TrabajarTicket
    {
        //Ontengo la ruta absoluta en donde se instalo la aplicacion
        private static string nuevoDir = AppDomain.CurrentDomain.BaseDirectory;

        public static Ticket altaTicket(Ticket ticket)
        {
            //Modifico la cadena de conexcion con la ruta en donde se guardo la aplicacion
            AppDomain.CurrentDomain.SetData("DataDirectory", nuevoDir);

            using (SqlConnection cn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Ticket(cli_Id, tv_Id, sec_Id, t_Duracion,t_FechaHoraEnt, t_Patente, t_Tarifa,t_Total) values (@cli_Id,@tv_Id,@sec_Id,@t_Duracion,@t_FechaHoraEntrada, @t_Patente, @t_Tarifa,@t_Total); SELECT SCOPE_IDENTITY();", cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@cli_Id", ticket.Cli_Id.Cli_Id);
                    cmd.Parameters.AddWithValue("@tv_Id", ticket.Tv_Id.Tv_Id);
                    cmd.Parameters.AddWithValue("@sec_Id", ticket.Sec_Id.Sec_Codigo);
                    cmd.Parameters.AddWithValue("@t_Duracion", ticket.T_Duracion);
                    cmd.Parameters.AddWithValue("@t_FechaHoraEntrada", ticket.T_FechaHoraEnt);
                    cmd.Parameters.AddWithValue("@t_Patente", ticket.T_Patente);
                    cmd.Parameters.AddWithValue("@t_Tarifa", ticket.T_Tarifa);
                    cmd.Parameters.AddWithValue("@t_Total", ticket.T_Total);

                    // Ejecutar la inserción y recuperar el ID generado
                    int idGenerado = Convert.ToInt32(cmd.ExecuteScalar());

                    // Asignar el ID generado al objeto Ticket antes de retornarlo
                    ticket.T_Id = idGenerado;
                }
            }

            // Retornar el objeto Ticket con el ID generado
            return ticket;
        }
        public static Ticket traerTicket(Sector sector)
        {
            Ticket ticket = null;

            //Modifico la cadena de conexcion con la ruta en donde se guardo la aplicacion
            AppDomain.CurrentDomain.SetData("DataDirectory", nuevoDir);

            using (SqlConnection cn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString))
            {
                cn.Open();
                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT TOP 1 Ticket.* FROM Ticket INNER JOIN Sector ON Ticket.sec_Id = " + sector.Sec_Codigo + " WHERE Sector.sec_Identificador = '" + sector.Sec_Identificador + "' ORDER BY t_Id DESC";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ticket = new Ticket();
                            // Mapear las columnas según la estructura de tu tabla Sector
                            ticket.T_Id = Convert.ToInt32(reader["t_Id"]);
                            Cliente cli = new Cliente();
                            cli.Cli_Id = Convert.ToInt32(reader["cli_Id"]);
                            foreach (Cliente clii in TrabajarClientes.traer_clientes())
                            {
                                if (cli.Cli_Id == clii.Cli_Id)
                                {
                                    cli = clii;
                                    break;
                                }
                            }
                            ticket.Cli_Id = cli;
                            TipoVehiculo tp = new TipoVehiculo();
                            tp.Tv_Id = Convert.ToInt32(reader["tv_Id"]);
                            foreach (TipoVehiculo tpp in TrabajarTipoVehiculos.traer_tipos_vehiculos())
                            {
                                if (tp.Tv_Id == tpp.Tv_Id)
                                {
                                    tp = tpp;
                                    break;
                                }
                            }
                            ticket.Tv_Id = tp;
                            Sector sec = new Sector();
                            sec.Sec_Codigo = Convert.ToInt32(reader["sec_Id"]);
                            foreach (Sector secc in TrabajarSector.traerSectores())
                            {
                                if (sec.Sec_Codigo == secc.Sec_Codigo)
                                {
                                    sec = secc;
                                    break;
                                }
                            }
                            ticket.Sec_Id = sec;
                            ticket.T_Duracion = Convert.ToDouble(reader["t_Duracion"]);
                            ticket.T_FechaHoraEnt = Convert.ToDateTime(reader["t_FechaHoraEnt"]);
                            ticket.T_Patente = Convert.ToString(reader["t_Patente"]);
                            ticket.T_Tarifa = Convert.ToDecimal(reader["t_Tarifa"]);
                            ticket.T_Total = Convert.ToDecimal(reader["t_Total"]);
                            // Otros atributos
                        }
                    }
                }
            }
            return ticket;
        }
        public static void updateTiket(Ticket tiket)
        {
            //Modifico la cadena de conexcion con la ruta en donde se guardo la aplicacion
            AppDomain.CurrentDomain.SetData("DataDirectory", nuevoDir);

            using (SqlConnection cn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE Ticket SET t_FechaHoraSal = @fecha WHERE t_Id = @id", cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@fecha", tiket.T_FechaHoraSal);
                    cmd.Parameters.AddWithValue("@id", tiket.T_Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static ObservableCollection<Ticket> traerTickets()
        {
            ObservableCollection<Ticket> listaTicket = new ObservableCollection<Ticket>();

            //Modifico la cadena de conexcion con la ruta en donde se guardo la aplicacion
            AppDomain.CurrentDomain.SetData("DataDirectory", nuevoDir);

            using (SqlConnection cn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString))
            {
                cn.Open();
                using (var cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM Ticket";

                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                if (dr["t_FechaHoraSal"] != DBNull.Value)
                                {
                                    Ticket ticket = new Ticket();
                                    ticket.T_Id = int.Parse(dr["t_Id"].ToString());
                                    Cliente cli = new Cliente();
                                    cli.Cli_Id = int.Parse(dr["cli_Id"].ToString());
                                    foreach (Cliente clii in TrabajarClientes.traer_clientes())
                                    {
                                        if (cli.Cli_Id == clii.Cli_Id)
                                        {
                                            cli = clii;
                                            break;
                                        }
                                    }
                                    ticket.Cli_Id = cli;
                                    TipoVehiculo tp = new TipoVehiculo();
                                    tp.Tv_Id = int.Parse(dr["tv_Id"].ToString());
                                    foreach (TipoVehiculo tpp in TrabajarTipoVehiculos.traer_tipos_vehiculos())
                                    {
                                        if (tp.Tv_Id == tpp.Tv_Id)
                                        {
                                            tp = tpp;
                                            break;
                                        }
                                    }
                                    ticket.Tv_Id = tp;
                                    Sector sec = new Sector();
                                    sec.Sec_Codigo = int.Parse(dr["sec_Id"].ToString());
                                    foreach (Sector secc in TrabajarSector.traerSectores())
                                    {
                                        if (sec.Sec_Codigo == secc.Sec_Codigo)
                                        {
                                            sec = secc;
                                            break;
                                        }
                                    }
                                    ticket.Sec_Id = sec;
                                    ticket.T_Duracion = Double.Parse(dr["t_Duracion"].ToString().ToString());
                                    ticket.T_FechaHoraEnt = Convert.ToDateTime(dr["t_FechaHoraEnt"].ToString());
                                    ticket.T_FechaHoraSal = Convert.ToDateTime(dr["t_FechaHoraSal"].ToString());
                                    ticket.T_Patente = Convert.ToString(dr["t_Patente"].ToString());
                                    ticket.T_Tarifa = Convert.ToDecimal(dr["t_Tarifa"].ToString());
                                    ticket.T_Total = Convert.ToDecimal(dr["t_Total"].ToString());
                                    listaTicket.Add(ticket);
                                }
                            }
                        }
                    }
                }
            }
            return listaTicket;
        }
    }
}