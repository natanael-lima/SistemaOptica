using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClasesBase;

namespace Vistas
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            this.AcceptButton = btnIngresar;
        }

        private void btnIngresar_Click_1(object sender, EventArgs e)
        {
            FormMain oFormMain = new FormMain();

            DataTable usuario = TrabajarUsuario.search_usuarios(txt_usu.Text);

            if (usuario.Rows.Count > 0)
            {
                if (usuario.Rows[0].Field<string>("Usuario") == txt_usu.Text && usuario.Rows[0].Field<string>("Contraseña") == txtPassword.Text)
                {
                    Program.UserRolGlobal = usuario.Rows[0].Field<string>("Rol");
                    MessageBox.Show("Bienvenido/a: " + txt_usu.Text, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    oFormMain.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Datos de acceso erróneos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Usuario no existente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnIngresar_MouseHover(object sender, EventArgs e)
        {
            //btnIngresar.ForeColor = Color.Black;
        }

        private void btnIngresar_MouseLeave(object sender, EventArgs e)
        {
            //btnIngresar.ForeColor = Color.Black;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
