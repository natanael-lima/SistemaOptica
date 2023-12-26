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

namespace Vistas
{
    /// <summary>
    /// Interaction logic for ImprimirVentas.xaml
    /// </summary>
    public partial class ImprimirVentas : Window
    {
        public ImprimirVentas(ObservableCollection<Ticket> lista)
        {
            InitializeComponent();
            listaVentas.DataContext = lista;
        }

        private void imprimir_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pdlg = new PrintDialog();
            if (pdlg.ShowDialog() == true)
            {
                pdlg.PrintDocument(((IDocumentPaginatorSource)DocMain).DocumentPaginator, "Imprimir");
            }
        }
    }
}
