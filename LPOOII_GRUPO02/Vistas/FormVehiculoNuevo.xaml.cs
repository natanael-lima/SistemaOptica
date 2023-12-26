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
using System.Data;
using Microsoft.Win32;
using System.IO;


namespace Vistas
{
    /// <summary>
    /// Interaction logic for FormVehiculoNuevo.xaml
    /// </summary>
    public partial class FormVehiculoNuevo : Window
    {
        public FormVehiculoNuevo()
        {
            InitializeComponent();
            DataContext = new TipoVehiculo();
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

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
             if (txtDescripcion.Text == "" || txtTarifa.Text == "" || txtUrl.Text == "")
            {
                MessageBox.Show("Debe llenar todos los campos", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }else
                 if (txtDescripcion.Text.Length < 4)
                {
                    MessageBox.Show("Descripcion invalido", "Invalido", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                 else if (txtTarifa.Text.Length < 3)
                {
                    MessageBox.Show("Tarifa invalido", "Invalido", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                 else if (txtUrl.Text.Length < 5)
                {
                    MessageBox.Show("Url Imagen invalido", "Invalido", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else 
                {
                    MessageBoxResult result = MessageBox.Show("¿Está seguro de que desea guardar los datos?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                   
                    if (result == MessageBoxResult.Yes)
                    {
                    TipoVehiculo oTipoVehiculo = new TipoVehiculo();

                    oTipoVehiculo.Tv_Descripcion = txtDescripcion.Text;
                    oTipoVehiculo.Tv_Tarifa = decimal.Parse(txtTarifa.Text);
                    oTipoVehiculo.Tv_Imagen = txtUrl.Text;
                    oTipoVehiculo.Tv_Ex = 1;
                    string mensaje = "Tarifa: " + oTipoVehiculo.Tv_Tarifa + "\nDescripción: " + oTipoVehiculo.Tv_Descripcion;

                    // Obtenemos la ruta de acceso absoluta de la carpeta raíz del proyecto
                    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                    // Obtenemos la ruta de acceso relativa de la carpeta de las fotos de los usuarios
                    string destino = System.IO.Path.Combine(baseDirectory, "recursos", "vehiculos");

                    // Combinar la ruta de destino con el nombre de archivo proporcionado en txtUrl.Text
                    string rutaArchivoDestino = System.IO.Path.Combine(destino, txtUrl.Text);

                    string recurso = imgFoto.Source.ToString().Replace("file:///", "");

                    // Copiar el archivo de la imagen al destino
                    File.Copy(recurso, rutaArchivoDestino, true);

                    MessageBoxResult result2 = MessageBox.Show(mensaje, "Valores Almacenados", MessageBoxButton.OK, MessageBoxImage.Information);
                        if (result2 == MessageBoxResult.OK)
                        {
                            TrabajarTipoVehiculos.guardar_tipo_vehiculo(oTipoVehiculo);//Guarda en la bd
                            this.Close();
                        }
                    }
                }
        }

        private void btnCargarFoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Todos los archivos|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    BitmapImage foto = new BitmapImage();
                    foto.BeginInit();
                    foto.UriSource = new Uri(openFileDialog.FileName);
                    foto.EndInit();
                    foto.Freeze();

                    imgFoto.Source = foto;
                    txtUrl.Text = txtDescripcion.Text + "imagen-vehiculo" + ".jpg";
                }
                catch
                {
                }
            }
        }
    }
}
