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
    /// Interaction logic for FormUsuarioNuevo.xaml
    /// </summary>
    public partial class FormUsuarioNuevo : Window
    {
        public FormUsuarioNuevo()
        {
            InitializeComponent();
            DataContext = new Usuario();
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
                FormUsuario form = new FormUsuario();
                form.Show();
                this.Close();
            }
        }
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (txtApellido.Text == "" || txtNombre.Text == "" || txtUsername.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Debe llenar todos los campos", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (txtApellido.Text.Length < 3)
                {
                    MessageBox.Show("Apellido invalido", "Invalido", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (txtNombre.Text.Length < 3)
                {
                    MessageBox.Show("Nombre invalido", "Invalido", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (txtUsername.Text.Length <= 3)
                {
                    MessageBox.Show("Username invalido", "Invalido", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (txtPassword.Text.Length <= 3)
                {
                    MessageBox.Show("Password invalido", "Invalido", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("¿Está seguro de que desea guardar los datos?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        Usuario newUser = new Usuario();
                        newUser.User_Name = txtUsername.Text;
                        newUser.User_Password = txtPassword.Text;
                        newUser.User_Nombre = txtNombre.Text;
                        newUser.User_Apellido = txtApellido.Text;
                        string rolSeleccionado = (txtRol.SelectedItem as ComboBoxItem).Content.ToString();
                        newUser.User_Rol = rolSeleccionado;
                        newUser.User_Foto = txtUrl.Text;

                        // Obtenemos la ruta de acceso absoluta de la carpeta raíz del proyecto
                        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                        // Obtenemos la ruta de acceso relativa de la carpeta de las fotos de los usuarios
                        string destino = System.IO.Path.Combine(baseDirectory, "recursos", "usuarios");

                        // Combinar la ruta de destino con el nombre de archivo proporcionado en txtUrl.Text
                        string rutaArchivoDestino = System.IO.Path.Combine(destino, txtUrl.Text);

                        string recurso = imgFoto.Source.ToString().Replace("file:///", "");
                        // Copiar el archivo de la imagen al destino
                        File.Copy(recurso, rutaArchivoDestino, true);


                        MessageBoxResult result2 = MessageBox.Show("Usuario Guardado correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);

                        if (result2 == MessageBoxResult.OK)
                        {
                            TrabajarUsuario.altaUsuario(newUser);//Guarda en la bd
                            this.Close();
                        }

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
                    txtUrl.Text = txtNombre.Text + txtApellido.Text + ".jpg";
                }
                catch
                {
                }
            }
        }

        private void txtRol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }
}
