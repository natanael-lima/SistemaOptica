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
namespace Vistas
{
    /// <summary>
    /// Interaction logic for ImprimirFijo.xaml
    /// </summary>
    public partial class ImprimirFlow : Window
    {
        public ImprimirFlow(ObservableCollection<Usuario> lista)
        {
            InitializeComponent();
            listaUsuarios.DataContext = lista;
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
