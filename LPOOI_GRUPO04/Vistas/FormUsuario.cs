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
    public partial class FormUsuario : Form
    {
        public FormUsuario()
        {
            InitializeComponent();
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            load_combo_roles();
            load_usuarios();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "" || txtApellidoNombre.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Debe llenar todos campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                if (txtUsuario.Text.Length < 2)
                {
                    MessageBox.Show("Usuario invalido", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtApellidoNombre.Text.Length < 5)
                {
                    MessageBox.Show("Apellido y Nombre invalido", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtPassword.Text.Length < 2)
                {
                    MessageBox.Show("Password invalido", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            else
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro de que desea continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    Usuario usuario = new Usuario();
                    usuario.Rol_Codigo = (int)cmbRol.SelectedValue;
                    usuario.Usu_ApellidoNombre = txtApellidoNombre.Text;
                    usuario.Usu_NombreUsuario = txtUsuario.Text;
                    usuario.Usu_Password = txtPassword.Text;
                    TrabajarUsuario.insert_usuario(usuario);
                    load_usuarios();
                    MessageBox.Show("Usuario agregado correctamente");
                }
                
            }
           
        }

        private void load_combo_roles()
        {
            cmbRol.DisplayMember = "rol_Descripcion";
            cmbRol.ValueMember = "rol_Codigo";
            cmbRol.DataSource = TrabajarUsuario.list_roles();
        }

        private void load_usuarios()
        {
            dgwUsuarios.DataSource = TrabajarUsuario.list_usuarios();
            dgwUsuarios.Columns["ID"].Visible = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir del formulario USUARIO?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
                FormMain formMain = new FormMain();
                formMain.Show();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            
            if (txtBuscado.Text != "")
            {
                dgwUsuarios.DataSource = TrabajarUsuario.search_usuarios(txtBuscado.Text);
            }
            else
            {
                load_usuarios();
            }
        }

        private void dgwUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgwUsuarios.SelectedRows.Count == 0)
            {
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
                btnGuardar.Enabled = true;
                txtApellidoNombre.Text = string.Empty;
                txtUsuario.Text = string.Empty;
                txtPassword.Text = string.Empty;
            }
            else
            {
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
                btnGuardar.Enabled = false;
                string rol = dgwUsuarios.CurrentRow.Cells["Rol"].Value.ToString();
                if (rol == "Administrador")
                    cmbRol.SelectedValue = 1;
                if (rol == "Operador")
                    cmbRol.SelectedValue = 2;
                if (rol == "Auditor")
                    cmbRol.SelectedValue = 3;
                txtApellidoNombre.Text = dgwUsuarios.CurrentRow.Cells["Nombre y Apellido"].Value.ToString();
                txtUsuario.Text = dgwUsuarios.CurrentRow.Cells["Usuario"].Value.ToString();
                txtPassword.Text = dgwUsuarios.CurrentRow.Cells["Contraseña"].Value.ToString();
            }
        }


        private void btnModificar_Click(object sender, EventArgs e)
        {   
            if (txtUsuario.Text == "" || txtApellidoNombre.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Debe llenar todos campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                if (txtUsuario.Text.Length < 2)
                {
                    MessageBox.Show("Usuario invalido", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtApellidoNombre.Text.Length < 5)
                {
                    MessageBox.Show("Apellido y Nombre invalido", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtPassword.Text.Length < 2)
                {
                    MessageBox.Show("Password invalido", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult resultado = MessageBox.Show("¿Esta seguro de que desea modificar este usuario?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        Usuario usuario = new Usuario();
                        usuario.Rol_Codigo = (int)cmbRol.SelectedValue;
                        usuario.Usu_ApellidoNombre = txtApellidoNombre.Text;
                        usuario.Usu_NombreUsuario = txtUsuario.Text;
                        usuario.Usu_Password = txtPassword.Text;
                        usuario.Usu_ID = Int32.Parse(dgwUsuarios.CurrentRow.Cells["ID"].Value.ToString());
                        TrabajarUsuario.update_usuario(usuario);
                        MessageBox.Show("Usuario modificado correctamente");
                        load_usuarios();
                    }
                }
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro de que desea eliminar este usuario?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                Usuario usuario = new Usuario();
                usuario.Rol_Codigo = (int)cmbRol.SelectedValue;
                usuario.Usu_ApellidoNombre = txtApellidoNombre.Text;
                usuario.Usu_NombreUsuario = txtUsuario.Text;
                usuario.Usu_Password = txtPassword.Text;
                usuario.Usu_ID = Int32.Parse(dgwUsuarios.CurrentRow.Cells["ID"].Value.ToString());
                TrabajarUsuario.delete_usuario(usuario);
                MessageBox.Show("Usuario eliminado correctamente");
                load_usuarios();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtApellidoNombre.Clear();
            txtUsuario.Clear();
            txtPassword.Clear();
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = true;
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            load_usuarios();
            txtBuscado.Text = "";
        }

        private void txtApellidoNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.solo_letras(e);
        }

    }
}
