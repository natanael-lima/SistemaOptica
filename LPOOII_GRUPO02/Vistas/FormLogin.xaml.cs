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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClaseBase;
using System.Data;
using System.Collections.ObjectModel;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class FormLogin : Window
    {
        
        public FormLogin()
        {
            InitializeComponent();
            userControlLoginn.botonClick += UserControlLogin_botonClick;
            
        }
        // metodo para mover la ventana con el mouse, se agrega en el inicio de Windows Form
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton==MouseButtonState.Pressed){
                DragMove();
            }
        }

        //nuevo evento agregado en vez del click del boton, ya que este maneja el evento del userControl
        private void UserControlLogin_botonClick(object sender,EventArgs e)
        {

            string usuario = userControlLoginn.txtUser.Text;
            string password = userControlLoginn.txtPassword.Password;

            // Realizar la verificación de credenciales
            ObservableCollection<Usuario> usuarios = TrabajarUsuario.search_usuarios(usuario);

            if (usuarios.Count > 0 && usuarios[0].User_Password == password)
            {
                // Usuario autenticado correctamente
                App.UserGlobal = usuarios[0];
                MessageBox.Show("Bienvenido/a: " + usuario, "Mensaje", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                // Mostrar la ventana principal u otra acción que desees realizar
                FormMain mainWindow = new FormMain();
                mainWindow.Show();

                // Cerrar la ventana actual
                this.Close();
            }
            else
            {
                // Credenciales incorrectas
                MessageBox.Show("Nombre de usuario o contraseña incorrectos", "Error de Inicio de Sesión", MessageBoxButton.OK, MessageBoxImage.Error);
            }

       
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void irMenuPrincipal(){
            FormMain fMain = new FormMain();
            fMain.Show();
            this.Close();
        }

        private void userControlLoginn_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
