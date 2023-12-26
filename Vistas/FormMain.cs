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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            
        }
        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCliente formCliente = new FormCliente();
            formCliente.Show();
            this.Hide();
        }

        private void nuevaVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormVenta formVenta = new FormVenta();
            formVenta.Show();
            this.Hide();
        }


        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que quiere Cerrar Sesion?", "Cerrar Sesion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
                FormLogin formLogin = new FormLogin();
                formLogin.Show();
            }
        }

        private void listarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormListarVentas FormListarVentas = new FormListarVentas();
            FormListarVentas.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        
            if (Program.UserRolGlobal == "Administrador")
            {
                clientesToolStripMenuItem.Enabled = false;
                ventasToolStripMenuItem.Enabled = false;
                
                lblUser.Text = Program.UserRolGlobal;
            }
            else
            {
                if (Program.UserRolGlobal == "Operador")
                {
                    usuariosToolStripMenuItem.Enabled = false;
                    
                    productosToolStripMenuItem.Enabled = false;
                    lblUser.Text = Program.UserRolGlobal;
                }
                else {
                    lblUser.Text = Program.UserRolGlobal;
                }
            }
        }

        private void verProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormListarProductos FormListarProductos = new FormListarProductos();
            FormListarProductos.Show();
            this.Hide();
        
        }

        private void horaFecha_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
            lblFecha.Text = DateTime.Now.ToString("dddd, dd MMMM yyy");
        }

        private void listadoPorClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormListarVentasFiltros formVentasCliente = new FormListarVentasFiltros();
            formVentasCliente.Show();
            this.Hide();
        }

        private void verClientesPorObraSocialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormClientesPorObraSocial formClientesOS = new FormClientesPorObraSocial();
            formClientesOS.Show();
            this.Hide();
        }

        private void aBMProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormProducto formProducto = new FormProducto();
            formProducto.Show();
            this.Hide();
        }

        private void aBMUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUsuario formUsuario = new FormUsuario();
            formUsuario.Show();
            this.Hide();
        }

        private void aBMObraSocialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormObraSocial formOS = new FormObraSocial();
            formOS.Show();
            this.Hide();
        }

    }
}
