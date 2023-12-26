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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for FormVentas.xaml
    /// </summary>
    public partial class FormVentas : Window
    {
        ObservableCollection<Ticket> listTiket = new ObservableCollection<Ticket>();
        Decimal total = 0;

        public FormVentas()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listTiket = TrabajarTicket.traerTickets();
            dataGridVentas.ItemsSource = listTiket;
            calcularTotal();
        }


        public void filtrar()
        {
            ICollectionView vista = CollectionViewSource.GetDefaultView(listTiket);
            DateTime fechaInicio = Convert.ToDateTime(fecha1.Text);
            DateTime fechaSalida = Convert.ToDateTime(fecha2.Text);

            vista.Filter = item =>
            {
                Ticket ticket = item as Ticket;
                if (ticket != null)
                {
                    // Suponiendo que 'Fecha' es el nombre de la propiedad que contiene la fecha en tu clase de datos
                    return ticket.T_FechaHoraEnt >= fechaInicio && ticket.T_FechaHoraSal <= fechaSalida;
                }
                return false;
            };

            // Asignar la vista filtrada al DataGrid
            dataGridVentas.ItemsSource = vista;
        }

        private void buscar_Click(object sender, RoutedEventArgs e)
        {


            if (fecha1.ToString() != "" && fecha2.ToString() != "")
            {
                filtrar();
                calcularTotal();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fecha valida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }
        private void calcularTotal()
        {
            total = 0;
            foreach (Ticket item in dataGridVentas.Items)
            {
                // Obtener el valor de la columna T_Total de cada fila
                var valorCelda = item.T_Total; // Suponiendo que T_Total es la propiedad que contiene los valores numéricos

                // Verificar si el valor es numérico y agregarlo al total
                if (valorCelda != null && valorCelda is decimal)
                {
                    total += (decimal)valorCelda;
                }
            }
            txtTotal.Text = total.ToString();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            FormMain menuPrincipal = new FormMain();
            menuPrincipal.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtTotal.Text != "0")
            {
                ImprimirVentas imp = new ImprimirVentas(listTiket);
                imp.Show();
            }
            else 
            {
                MessageBox.Show("No hay datos necesarios para la impresion.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
