using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClaseBase;
namespace Vistas
{
    /// <summary>
    /// Interaction logic for ImprimirSalida.xaml
    /// </summary>
    public partial class ImprimirSalida : Window
    {
        public ImprimirSalida(Ticket tiket)
        {
            InitializeComponent();
            txtTicketID.Text = "Ticket ID: " + tiket.T_Id;
            txtSector.Text = "Sector: " + tiket.Sec_Id.Sec_Identificador;
            txtCliente.Text = "Cliente: " + tiket.Cli_Id.Cli_Nombre + tiket.Cli_Id.Cli_Apellido;
            txtPatente.Text = "Patente: " + tiket.T_Patente;
            txtIngreso.Text = "Ingreso: " + tiket.T_FechaHoraEnt;
            txtSalida.Text = "Salida: " + tiket.T_FechaHoraSal;
            txtTV.Text = "Tipo de Vehiculo: " + tiket.Tv_Id.Tv_Descripcion;
            txtTarifa.Text = "Tarifa: " + tiket.T_Tarifa.ToString();
            txtTotal.Text = "Total: " + tiket.T_Total.ToString();
            txtUsuario.Text = "Usuario: " + App.UserGlobal.User_Nombre;
            TimeSpan tiempoTranscurrido = DateTime.Now - tiket.T_FechaHoraEnt;
            txtTiempoTrans.Text = "Tiempo Transcurrido: " + tiempoTranscurrido.ToString(@"hh\:mm\:ss");
        }
    }
}
