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
using System.Windows.Threading;
using ClaseBase;
using System.Xml;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for RegistrarEntrada.xaml
    /// </summary>
    public partial class RegistrarEntrada : Window
    {
        private Estacionamiento formulario1;
        private string codigo, fechaYHora;
        private DispatcherTimer timer;


        public RegistrarEntrada(Estacionamiento form1, string cod)
        {
            InitializeComponent();
            formulario1 = form1;
            codigo = cod;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Actualiza el TextBlock con la fecha y hora actual
            fechaYHora = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            txtFecha.Text = fechaYHora;
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtTitle.Text = "Registro: " + codigo;
            Ticket tiket = new Ticket();
            tiket = chekear(codigo);
            //cargar combobox clientes
            cmbClientes.SelectedValuePath = "Cli_Id";
            cmbClientes.DisplayMemberPath = "Cli_Nombre";
            cmbClientes.ItemsSource = TrabajarClientes.traer_clientes();

            //cargar combobox tipoVehiculos
            cmbTVehiculos.SelectedValuePath = "Tv_Id";
            cmbTVehiculos.DisplayMemberPath = "Tv_Descripcion";
            cmbTVehiculos.ItemsSource = TrabajarTipoVehiculos.traer_tipos_vehiculos();

            //cargar datos si es que ya esta ocupado ese sector
            if (tiket != null && TrabajarSector.traerSector(codigo).Sec_Habilitado == 2)
            {
                cmbClientes.SelectedValue = tiket.Cli_Id.Cli_Id;
                cmbClientes.IsEnabled = false;
                txtPatente.Text = tiket.T_Patente;
                txtPatente.IsEnabled = false;
                cmbTVehiculos.SelectedValue = tiket.Tv_Id.Tv_Id;
                cmbTVehiculos.IsEnabled = false;
                txtTarifa.Text = tiket.T_Tarifa.ToString();
                txtTarifa.IsEnabled = false;

                comboTime(tiket.T_Duracion);
                cmbTime.IsEnabled = false;
                txtTotal.Text = tiket.T_Total.ToString();
                txtTotal.IsEnabled = false;
                txtFecha.Text = tiket.T_FechaHoraEnt.ToString();
                txtFecha.IsEnabled = false;
                TimeSpan dif = DateTime.Now - tiket.T_FechaHoraEnt;
                DateTime fechaDiferencia = DateTime.MinValue.Add(dif);
                txtTiempoTrans.Text = string.Format("{0:D2}:{1:D2}:{2:D2}",
                        (int)dif.TotalHours, dif.Minutes, dif.Seconds);
                txtTiempoTrans.IsEnabled = false;

                btn.Content = "Registrar Salida";

            }
            else
            {

                txtBTotal.IsEnabled = false;
                txtTotal.IsEnabled = false;



                //inicializar temporizador
                // Inicializar el temporizador
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1); // Actualizar cada segundo
                timer.Tick += Timer_Tick;

                txtTarifa.IsEnabled = false;
                txtTiempoTrans.IsEnabled = false;
                txtTiempoTrans.Visibility = Visibility.Collapsed;
                txtBTiempoTrans.Visibility = Visibility.Collapsed;
                txtTotal.Visibility = Visibility.Collapsed;
                txtBTotal.Visibility = Visibility.Collapsed;
                // Iniciar el temporizador
                timer.Start();

                btn.Content = "Registrar Entrada";
            }






        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Está seguro de que desea salir?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {

                this.Close();
            }
        }

        //imprimir el tiket
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Ticket tiket = new Ticket();
            tiket = chekear(codigo);

            if (tiket != null && TrabajarSector.traerSector(codigo).Sec_Habilitado == 2)
            {
                Sector sec = TrabajarSector.traerSector(codigo);
                sec.Sec_Habilitado = 1;
                TrabajarSector.estadoSector(sec);
                tiket.T_FechaHoraSal = DateTime.Now;
                TrabajarTicket.updateTiket(tiket);
                ImprimirSalida impF = new ImprimirSalida(tiket);
                impF.Show();

                formulario1.pintar(0, codigo);
            }
            else
            {
                tiket = new Ticket();
                Cliente cliSelected = (Cliente)cmbClientes.SelectedItem;
                TipoVehiculo tp = (TipoVehiculo)cmbTVehiculos.SelectedItem;
                Sector sec = TrabajarSector.traerSector(codigo);

                tiket.Cli_Id = cliSelected;
                tiket.Tv_Id = tp;
                tiket.T_Patente = txtPatente.Text;
                tiket.T_Tarifa = tp.Tv_Tarifa;
                tiket.Sec_Id = sec;

                tiket.T_Duracion = double.Parse(cmbTime.SelectedValue.ToString());
                tiket.T_FechaHoraEnt = DateTime.Now;
                tiket.T_Total = (tiket.T_Tarifa * Convert.ToDecimal(tiket.T_Duracion)) / 60;

                tiket = TrabajarTicket.altaTicket(tiket);
                sec.Sec_Habilitado = 0;
                TrabajarSector.estadoSector(sec);
                ImprimirFixed imp = new ImprimirFixed(tiket);
                imp.Show();



                sec.Sec_Habilitado = 2;
                TrabajarSector.estadoSector(sec);

                formulario1.pintar(int.Parse(cmbTime.SelectedValue.ToString()), codigo);
            }

            this.Close();


        }
        //para chekear si el sector esta ocupado
        private Ticket chekear(string cod)
        {

            Ticket tiket = new Ticket();
            tiket = TrabajarTicket.traerTicket(TrabajarSector.traerSector(cod));
            if (tiket != null)
            {
                return tiket;
            }
            return tiket;
        }
        //chekear el tiempo seleccionado
        private void comboTime(double comp)
        {
            // Obtenemos la ruta de acceso absoluta de la carpeta raíz del proyecto
            string origen = AppDomain.CurrentDomain.BaseDirectory;

            // Obtenemos la ruta de acceso relativa de la carpeta FOTOS
            //string fotosDirectory = System.IO.Path.Combine(origen, "recursos/usuarios");

            XmlDocument doc = new XmlDocument();
            doc.Load(origen + "/Tiempos.xml"); // Reemplaza con la ruta correcta de tu archivo XML

            XmlNodeList nodeList = doc.SelectNodes("//minutos");

            for (int i = 0; i < nodeList.Count; i++)
            {
                double var = Double.Parse(nodeList[i].InnerText);
                if (var == comp)
                {
                    cmbTime.SelectedIndex = i;
                    break;
                }
            }

        }

        private void cmbTVehiculos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TipoVehiculo tp = (TipoVehiculo)cmbTVehiculos.SelectedItem;
            txtTarifa.Text = tp.Tv_Tarifa.ToString();
        }
    }
}
