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
using System.Collections.ObjectModel;
using ClaseBase;
using System.Windows.Controls.Primitives;
namespace Vistas
{
    /// <summary>
    /// Interaction logic for FormSectores.xaml
    /// </summary>
    public partial class FormSectores : Window
    {
        ObservableCollection<Ticket> listTiket = new ObservableCollection<Ticket>();
        public FormSectores()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Sector> lSec = TrabajarSector.traerSectores();
            


            foreach (Sector sec in lSec)
            {
                if (sec.Sec_Habilitado == 2)
                {
                    Ticket ticket = TrabajarTicket.traerTicket(sec);
                    if (ticket != null)
                    {
                        listTiket.Add(ticket);

                    }
                }
            }

            dataGridSectores.ItemsSource = listTiket;
            // Asignar contenido a cada celda de la columna
            int c = 0;
            foreach (Ticket ticket in listTiket)
            {
                DateTime ahora = DateTime.Now;
                TimeSpan tiempoTranscurrido = ahora - ticket.T_FechaHoraEnt;
                // Ejemplo: Asignar un valor fijo para demostrar
                DataGridCell cell = GetCell(dataGridSectores, c, 7);
                if (cell != null)
                {
                    cell.Content = tiempoTranscurrido.ToString(@"hh\:mm\:ss");

                }
                c++;

            }



        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Estacionamiento form = new Estacionamiento();
            form.Show();
            this.Close();
        }

        public static DataGridCell GetCell(DataGrid dataGrid, int rowIndex, int columnIndex)
        {
            if (dataGrid == null || rowIndex < 0 || rowIndex >= dataGrid.Items.Count || columnIndex < 0 || columnIndex >= dataGrid.Columns.Count)
            {
                return null;
            }

            var row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            if (row == null)
            {
                // Si la fila aún no está visible, asegurémonos de que se genere
                dataGrid.UpdateLayout();
                dataGrid.ScrollIntoView(dataGrid.Items[rowIndex]);
                row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            }

            if (row != null)
            {
                var presenter = GetVisualChild<DataGridCellsPresenter>(row);
                if (presenter != null)
                {
                    var cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);
                    return cell;
                }
            }

            return null;
        }

        // Método auxiliar para obtener el visual child dentro de un control
        private static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ImprimirSectoresOcup imp = new ImprimirSectoresOcup(listTiket);
            imp.Show();
        }


    }
}
