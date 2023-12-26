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
    /// Interaction logic for FormClienteNuevo.xaml
    /// </summary>
    public partial class FormClienteNuevo : Window
    {
        public FormClienteNuevo()
        {
            InitializeComponent();
            DataContext = new Cliente();
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
                FormCliente main = new FormCliente();
                main.Show();
                this.Close();
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (txtApellido.Text == "" || txtNombre.Text == "" || txtDNI.Text == "" || txtTelefono.Text == "")
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
                else if (txtDNI.Text.Length <=7)
                {
                    MessageBox.Show("DNI invalido", "Invalido", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (txtTelefono.Text.Length <= 9)
                {
                    MessageBox.Show("Telefono invalido", "Invalido", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("¿Está seguro de que desea guardar los datos?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        Cliente oCliente = new Cliente();
                        oCliente.Cli_DNI = int.Parse(txtDNI.Text);
                        oCliente.Cli_Apellido = txtApellido.Text;
                        oCliente.Cli_Nombre = txtNombre.Text;
                        oCliente.Cli_Telefono = long.Parse(txtTelefono.Text);
                        oCliente.Cli_Ex = 1;

                        string mensaje = "DNI: " + oCliente.Cli_DNI + "\nApellido: " + oCliente.Cli_Apellido + "\nNombre: " + oCliente.Cli_Nombre + "\nTeléfono: " + oCliente.Cli_Telefono;
                        MessageBoxResult result2 = MessageBox.Show(mensaje, "Valores Almacenados", MessageBoxButton.OK, MessageBoxImage.Information);
                        if (result2 == MessageBoxResult.OK)
                        {
                            TrabajarClientes.alta_cliente(oCliente);//Guarda en la bd
                            this.Close();
                        }
                    }
                }
            }
        }
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
