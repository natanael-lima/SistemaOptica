using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using ClasesBase;

namespace Vistas
{
    public partial class FormCliente : Form
    {
        public FormCliente()
        {
            InitializeComponent();
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }
        // Evento boton para guardar un cliente
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            
            if (txtNombre.Text == "" || txtApellido.Text == "" || txtDNI.Text == "" || txtDireccion.Text == "" || txtNroCarnet.Text == "")
            {
                MessageBox.Show("Debe llenar todos campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }else
                  if (txtDNI.Text.Length != 8)
                    {
                        MessageBox.Show("DNI invalido", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                  else if (txtNombre.Text.Length < 2)
                    {
                        MessageBox.Show("Nombre invalido", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                  else if (txtApellido.Text.Length < 2)
                  {
                      MessageBox.Show("Apellido invalido", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  }
                  else if (txtDireccion.Text.Length < 12)
                  {
                      MessageBox.Show("Direccion invalida", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  }
                  else if (txtNroCarnet.Text.Length !=4)
                  {
                      MessageBox.Show("Numero de Carnet invalido", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  }
            
            else
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro de que desea continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    // El usuario ha hecho clic en "Sí"

                    Cliente cli1 = new Cliente();

                    cli1.Cli_Nombre = txtNombre.Text;
                    cli1.Cli_Apellido = txtApellido.Text;
                    cli1.Cli_DNI = txtDNI.Text;
                    cli1.Cli_Direccion = txtDireccion.Text;
                    cli1.Cli_NroCarnet = txtNroCarnet.Text;
                    cli1.Os_CUIT = comboOS.SelectedValue.ToString();
                    cli1.Cli_Activo = 1;

                    TrabajarCliente.alta_cliente(cli1);

                    MessageBox.Show("Datos guardados correctamente");

                    txtNombre.Clear();
                    txtApellido.Clear();
                    txtDNI.Clear();
                    txtDireccion.Clear();
                    txtNroCarnet.Clear();
                    load_clientes();
                }
                else
                {
                    // El usuario ha hecho clic en "No"
                }
            }
        }
        // Evento load
        private void FormCliente_Load(object sender, EventArgs e)
        {
            load_clientes();
        }
        // Evento boton para cancelar la carga del formulario cliente
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir del formulario CLIENTE?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
                FormMain formMain = new FormMain();
                formMain.Show();
            }
        }
        // Funcion para load cliente
        private void load_clientes()
        {
            grillaClientes.DataSource = TrabajarCliente.listar_Clientes();
            grillaClientes.Columns["CUIT"].Visible = false;
            grillaClientes.Columns["ID"].Visible = false;
            grillaClientes.Columns["cli_Activo"].Visible = false;
            comboOS.DisplayMember = "os_RazonSocial";
            comboOS.ValueMember = "os_CUIT";
            comboOS.DataSource = TrabajarCliente.list_OS();

        }
        // Evento boton para modificar un cliente
        private void btnModificar_Click(object sender, EventArgs e)
        {

            if (txtNombre.Text == "" || txtApellido.Text == "" || txtDNI.Text == "" || txtDireccion.Text == "" || txtNroCarnet.Text == "")
            {
                MessageBox.Show("Debe llenar todos campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }else
                  if (txtDNI.Text.Length != 8)
                    {
                        MessageBox.Show("DNI invalido", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                  else if (txtNombre.Text.Length < 2)
                    {
                        MessageBox.Show("Nombre invalido", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                  else if (txtApellido.Text.Length < 2)
                  {
                      MessageBox.Show("Apellido invalido", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  }
                  else if (txtDireccion.Text.Length < 12)
                  {
                      MessageBox.Show("Direccion invalida", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  }
                  else if (txtNroCarnet.Text.Length != 4)
                  {
                      MessageBox.Show("Numero de Carnet invalido", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  }

                  else
                  {
                      DialogResult resultado = MessageBox.Show("¿Está seguro de que desea continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                      if (resultado == DialogResult.Yes)
                      {
                          // El usuario ha hecho clic en "Sí"

                          Cliente cli = new Cliente();

                          cli.Cli_Nombre = txtNombre.Text;
                          cli.Cli_Apellido = txtApellido.Text;
                          cli.Cli_DNI = txtDNI.Text;
                          cli.Cli_Direccion = txtDireccion.Text;
                          cli.Cli_NroCarnet = txtNroCarnet.Text;
                          cli.Os_CUIT = comboOS.SelectedValue.ToString();
                          //int idd = (int)grillaClientes.CurrentRow.Cells["ID"].Value;
                          cli.Cli_Id = Int32.Parse(grillaClientes.CurrentRow.Cells["ID"].Value.ToString());
                          //string id = grillaClientes.CurrentRow.Cells["ID"].Value.ToString();
                          //int x = Int32.Parse(id);
                          //cli.Cli_Id = x;
                          TrabajarCliente.actualizar_cliente(cli);
                          MessageBox.Show("Datos modificados correctamente");
                          txtNombre.Clear();
                          txtApellido.Clear();
                          txtDNI.Clear();
                          txtDireccion.Clear();
                          txtNroCarnet.Clear();

                          load_clientes();
                          btnModificar.Enabled = false;
                          btnEliminar.Enabled = false;
                          btnGuardar.Enabled = true;

                      }
                      else
                      {
                          // El usuario ha hecho clic en "No"
                      }
                  }

            

        }
        // Evento boton para eliminar un cliente
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            
                      DialogResult resultado = MessageBox.Show("¿Está seguro de que desea continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                      if (resultado == DialogResult.Yes)
                      {
                          // El usuario ha hecho clic en "Sí"
                          Cliente cli = new Cliente();
                          cli.Cli_Nombre = txtNombre.Text;
                          cli.Cli_Apellido = txtApellido.Text;
                          cli.Cli_DNI = txtDNI.Text;
                          cli.Cli_Direccion = txtDireccion.Text;
                          cli.Cli_NroCarnet = txtNroCarnet.Text;
                          cli.Os_CUIT = comboOS.Text;
                          cli.Cli_Activo = 0;

                          string id = grillaClientes.CurrentRow.Cells["ID"].Value.ToString();
                          int x = Int32.Parse(id);
                          cli.Cli_Id = x;
                          TrabajarCliente.eliminar_cliente(cli);
                          MessageBox.Show("Datos eliminados correctamente");
                          txtNombre.Clear();
                          txtApellido.Clear();
                          txtDNI.Clear();
                          txtDireccion.Clear();
                          txtNroCarnet.Clear();
                          //txtCUIT.Clear();

                          load_clientes();
                          btnModificar.Enabled = false;
                          btnEliminar.Enabled = false;
                          btnGuardar.Enabled = true;
                      }
                      else
                      {
                          // El usuario ha hecho clic en "No"
                      }
            

        }
        // Evento para seleccionar un cliente de la lista (grilla)
        private void grillaClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;
            if (grillaClientes.CurrentCell != null || grillaClientes.CurrentCell.Value != null)
            {
                comboOS.SelectedValue = grillaClientes.CurrentRow.Cells["CUIT"].Value.ToString();
                txtNombre.Text = grillaClientes.CurrentRow.Cells["Nombre"].Value.ToString();
                txtApellido.Text = grillaClientes.CurrentRow.Cells["Apellido"].Value.ToString();
                txtDNI.Text = grillaClientes.CurrentRow.Cells["DNI"].Value.ToString();
                txtDireccion.Text = grillaClientes.CurrentRow.Cells["Direccion"].Value.ToString();
                txtNroCarnet.Text = grillaClientes.CurrentRow.Cells["Nro Carnet"].Value.ToString();

                btnGuardar.Enabled = false;
            }

        }
        // Evento boton para buscar un cliente de la lista
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text != ""){
                grillaClientes.DataSource = TrabajarCliente.buscar_Clientes(txtBuscar.Text);
            }
            else{
                load_clientes();
            }
        }
        // Evento boton para refrescar la lista
        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            load_clientes();
            txtBuscar.Text = "";
        }
        // Evento boton para ordenar la lista
        private void btnOrdenar_Click(object sender, EventArgs e)
        {
            grillaClientes.DataSource = TrabajarCliente.ordenar_Clientes();
        }
        // Evento boton para vaciar los campos y poder ingresar un nuevo cliente
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtDNI.Clear();
            txtDireccion.Clear();
            txtNroCarnet.Clear();
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = true;
        }
        //Validacion campo NOMBRE
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.solo_letras(e);
        }
        //Validacion campo APELLIDO
        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.solo_letras(e);
        }
        //Validacion campo DNI
        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.solo_numeros(e);
        }
        
        //Validacion campo CARNET
        private void txtNroCarnet_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.solo_numeros(e);
        }
        
    }
}
