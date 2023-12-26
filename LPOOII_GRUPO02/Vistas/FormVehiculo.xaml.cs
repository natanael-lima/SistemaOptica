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

namespace Vistas
{
    /// <summary>
    /// Interaction logic for FormVehiculo.xaml
    /// </summary>
    public partial class FormVehiculo : Window
    {
        public FormVehiculo()
        {
            InitializeComponent();
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
                FormMain main = new FormMain();
                main.Show();
                this.Close();
            }
        }


        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            FormMain fMain = new FormMain();
            fMain.Show();
            this.Close();
        }

        private void dgTiposVehiculos_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgTiposVehiculos.DataContext = TrabajarTipoVehiculos.traer_tipos_vehiculos();
            txtDescripcion.IsReadOnly=true;
            txtTarifa.IsReadOnly = true;
            txtImagen.IsReadOnly = true;
            btnEditar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            FormVehiculoNuevo form = new FormVehiculoNuevo();
            form.Show();
            
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClaseBase.TipoVehiculo selectedTipoVehiculo = dgTiposVehiculos.SelectedItem as ClaseBase.TipoVehiculo;
            if (selectedTipoVehiculo != null)
            {
                // Accede a las propiedades de selectedTipoVehiculo directamente
                int id = selectedTipoVehiculo.Tv_Id;
                string descripcion = selectedTipoVehiculo.Tv_Descripcion;
                decimal tarifa = selectedTipoVehiculo.Tv_Tarifa;
                string imagen = selectedTipoVehiculo.Tv_Imagen;

                // Pasa los valores al formulario o realiza otras acciones según tus necesidades
                txtId.Text = id.ToString();
                txtDescripcion.Text = descripcion;
                txtTarifa.Text = tarifa.ToString();
                txtImagen.Text = imagen;
                // campos
                txtDescripcion.IsReadOnly = false;
                txtTarifa.IsReadOnly = false;
                txtImagen.IsReadOnly = true;
                //botones
                btnEditar.IsEnabled = true;
                btnEliminar.IsEnabled = true;
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Está seguro de que desea modificar los datos?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                TipoVehiculo oVeh = new TipoVehiculo();
                oVeh.Tv_Id = int.Parse(txtId.Text);
                oVeh.Tv_Descripcion = txtDescripcion.Text;
                oVeh.Tv_Tarifa = decimal.Parse(txtTarifa.Text);
                oVeh.Tv_Imagen = txtImagen.Text;
                oVeh.Tv_Ex = 1;

                TrabajarTipoVehiculos.editarTipoVehiculo(oVeh);

                string mensaje = "ID: " + oVeh.Tv_Id + "\nDescripcion: " + oVeh.Tv_Descripcion + "\nTarfica: " + oVeh.Tv_Tarifa;
                MessageBoxResult result2 = MessageBox.Show(mensaje, "Valores Almacenados", MessageBoxButton.OK, MessageBoxImage.Information);
                if (result2 == MessageBoxResult.OK)
                {
                    dgTiposVehiculos.DataContext = TrabajarTipoVehiculos.traer_tipos_vehiculos();
                }
            }

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Está seguro de que desea eliminar los datos?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                TipoVehiculo oVeh = new TipoVehiculo();
                oVeh.Tv_Id = int.Parse(txtId.Text);
                oVeh.Tv_Descripcion = txtImagen.Text;
                oVeh.Tv_Tarifa = decimal.Parse(txtTarifa.Text);
                oVeh.Tv_Imagen = txtImagen.Text;
                oVeh.Tv_Ex = 0;

                TrabajarTipoVehiculos.editarTipoVehiculo(oVeh);

                string mensaje = "ID: " + oVeh.Tv_Id + "\nDescripcion: " + oVeh.Tv_Descripcion + "\nTarfica: " + oVeh.Tv_Tarifa;
                MessageBoxResult result2 = MessageBox.Show(mensaje, "Valores Eliminados", MessageBoxButton.OK, MessageBoxImage.Information);
                if (result2 == MessageBoxResult.OK)
                {
                    dgTiposVehiculos.DataContext = TrabajarTipoVehiculos.traer_tipos_vehiculos();
                }
            }
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            dgTiposVehiculos.DataContext = TrabajarTipoVehiculos.traer_tipos_vehiculos();
        }
    }
}
