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
using System.Windows.Controls;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for FormAcercaDe.xaml
    /// </summary>
    public partial class FormAcercaDe : Window
    {
        public FormAcercaDe()
        {
            InitializeComponent();
        }

        private void ReproducirVideo(object sender, RoutedEventArgs e)
        {
            // Establece la fuente del MediaElement con la ruta del archivo de video
            string ruta = @"C:\Users\admin\Documents\Visual Studio 2010\Projects\LPOOII_GRUPO02\Vistas\recursos\video.mp4";
            mediaPlayer.Source = new Uri(ruta);

            // Reproduce el video
            mediaPlayer.Play();
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
    }
}
