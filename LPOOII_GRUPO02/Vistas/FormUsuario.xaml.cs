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
    /// Interaction logic for FormUsuario.xaml
    /// </summary>
    public partial class FormUsuario : Window
    {
        public FormUsuario()
        {
            InitializeComponent();
        }
        CollectionView Vista;
        ObservableCollection<Usuario> listUsuario;
        private bool agregar = false;
        private int indice = 0;
        private Usuario band;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGridUsuarios.DataContext = TrabajarUsuario.traerUsuariosASC();

            actualizarLista();
            txtUsername.IsEnabled = false;
            txtPassword.IsEnabled = false;
            txtNombre.IsEnabled = false;
            txtApellido.IsEnabled = false;
            txtRol.IsEnabled = false;

            btnGuardar.IsEnabled = false;
            

            // Recargar el diccionario de recursos
            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("/LibreriaRecursos;component/FormStyle.xaml", UriKind.Relative)
            });
        }
        private void actualizarLista()
        {
            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["List_Users"];
            listUsuario = odp.Data as ObservableCollection<Usuario>;

            Vista = (CollectionView)CollectionViewSource.GetDefaultView(canvas_content.DataContext);
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            FormMain fMain = new FormMain();
            fMain.Show();
            this.Close();
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

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            FormUsuarioNuevo form = new FormUsuarioNuevo();
            form.Show();
            
            /* (version maxi)
            Usuario newUser = new Usuario();
            listUsuario.Add(newUser);

            Vista.MoveCurrentToLast();

            txtUsername.IsEnabled = true;
            txtPassword.IsEnabled = true;
            txtNombre.IsEnabled = true;
            txtApellido.IsEnabled = true;
            txtRol.IsEnabled = true;

            btnGuardar.IsEnabled = true;
            btnCancelar.IsEnabled = true;

            btnEditar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            btnAgregar.IsEnabled = false;

            agregar = true;*/

            
        }

        private void btnNavegacionAntFinal_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToFirst();
            indice = 0;
        }

        private void btnNavegacionSigFinal_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToLast();
            indice = listUsuario.Count - 1;
        }

        private void btnNavegacionAnt_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToPrevious();
            indice = indice - 1;
            if (Vista.IsCurrentBeforeFirst)
            {
                Vista.MoveCurrentToLast();
                indice = listUsuario.Count - 1;
                
            }
        }

        private void btnNavegacionSig_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToNext();
            indice = indice + 1;
            if (Vista.IsCurrentAfterLast)
            {
                Vista.MoveCurrentToFirst();
                indice = 0;
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Usuario newUser = new Usuario();
            if (agregar == true)
            {
                newUser.User_Name = txtUsername.Text;
                newUser.User_Password = txtPassword.Text;
                newUser.User_Nombre = txtNombre.Text;
                newUser.User_Apellido = txtApellido.Text;
                string rolSeleccionado = (txtRol.SelectedItem as ComboBoxItem).Content.ToString();
                newUser.User_Rol = rolSeleccionado;

                TrabajarUsuario.altaUsuario(newUser);
                MessageBox.Show("Usuario Guardado correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                actualizarLista();
                deCero();
            }
            else
            {
                newUser.User_Id = listUsuario[indice].User_Id;
                newUser.User_Name = txtUsername.Text;
                newUser.User_Password = txtPassword.Text;
                newUser.User_Nombre = txtNombre.Text;
                newUser.User_Apellido = txtApellido.Text;
                string rolSeleccionado = (txtRol.SelectedItem as ComboBoxItem).Content.ToString();
                newUser.User_Rol = rolSeleccionado;
                TrabajarUsuario.editarUsuario(newUser);
                MessageBox.Show("Usuario Editado correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);

                listUsuario[indice].User_Name = txtUsername.Text;
                listUsuario[indice].User_Password = txtPassword.Text;
                listUsuario[indice].User_Nombre = txtNombre.Text;
                listUsuario[indice].User_Apellido = txtApellido.Text;
                listUsuario[indice].User_Rol = rolSeleccionado;
                actualizarLista();
                deCero();
            }


            agregar = false;
        }
        private void deCero(){
            txtUsername.IsEnabled = false;
            txtPassword.IsEnabled = false;
            txtNombre.IsEnabled = false;
            txtApellido.IsEnabled = false;
            txtRol.IsEnabled = false;

            btnAgregar.IsEnabled = true;
            btnEditar.IsEnabled = true;
            btnEliminar.IsEnabled = true;
            btnGuardar.IsEnabled = false;
            btnCancelar.IsEnabled = false;
        }


        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            btnEditar.IsEnabled = true;
            btnEliminar.IsEnabled = true;
            btnAgregar.IsEnabled = true;

            if (agregar == true)
            {
                listUsuario.RemoveAt(listUsuario.Count - 1);
                actualizarLista();
                deCero();
            }
            else
            {
                listUsuario.RemoveAt(indice);
                listUsuario.Add(band);
                listUsuario.Move(indice, listUsuario.Count - 1);
                actualizarLista();
                deCero();
            }



            agregar = false;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

            TrabajarUsuario.eliminarUsuario(listUsuario[indice].User_Id);
            listUsuario.RemoveAt(indice);
            MessageBox.Show("Usuario Eliminado correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            actualizarLista();
            Vista.MoveCurrentToFirst();
            indice = 0;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            txtUsername.IsEnabled = true;
            txtPassword.IsEnabled = true;
            txtNombre.IsEnabled = true;
            txtApellido.IsEnabled = true;
            txtRol.IsEnabled = true;

            btnGuardar.IsEnabled = true;
            btnCancelar.IsEnabled = true;

            btnEditar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            btnAgregar.IsEnabled = false;
            band = new Usuario();
            band.User_Id = listUsuario[indice].User_Id;
            band.User_Name = txtUsername.Text;
            band.User_Password = txtPassword.Text;
            band.User_Nombre = txtNombre.Text;
            band.User_Apellido = txtApellido.Text;
            band.User_Rol = txtRol.Text;
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
           dataGridUsuarios.DataContext = TrabajarUsuario.traerUsuariosFiltro(txtBuscar.Text);
        }

        private void btnVistaImpr_Click(object sender, RoutedEventArgs e)
        {
            
            ObservableCollection<Usuario> list = new ObservableCollection<Usuario>();
            foreach (Usuario usuario in dataGridUsuarios.Items)
            {
                list.Add(usuario);
            }
            ImprimirFlow impForm = new ImprimirFlow(list);
            impForm.Show();
            
            
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            dataGridUsuarios.DataContext = TrabajarUsuario.traerUsuariosASC();
        }


    }
}
