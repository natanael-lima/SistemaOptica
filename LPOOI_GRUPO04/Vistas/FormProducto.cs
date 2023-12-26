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
    public partial class FormProducto : Form
    {
        public FormProducto()
        {
            InitializeComponent();
            btnModificar.Enabled = false;
            btn_Eliminar.Enabled = false;
        }

        private void FormProducto_Load(object sender, EventArgs e)
        {
            load_productos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿¿Desea salir del formulario PRODUCTO?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
                FormMain formMain = new FormMain();
                formMain.Show();
            }
        }

        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "" || txtCategoria.Text == "" || txtDescripcion.Text == "" || txtPrecio.Text == "")
            {
                MessageBox.Show("Debe llenar todos los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }else
                if (txtCodigo.Text.Length != 4)
                    {
                        MessageBox.Show("Codigo invalido, debe ser de 4 digitos", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                else if (txtCategoria.Text.Length < 3)
                    {
                        MessageBox.Show("Categoria invalida", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                else if (txtDescripcion.Text.Length < 5)
                  {
                      MessageBox.Show("Descripcion invalida", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  }
                else if (txtPrecio.Text.Length < 2)
                  {
                      MessageBox.Show("Precio invalido", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  }
            else
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro que desea continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    Producto prod = new Producto();

                    prod.Prod_Codigo = txtCodigo.Text;
                    prod.Prod_Categoria = txtCategoria.Text;
                    prod.Prod_Descripcion = txtDescripcion.Text;
                    prod.Prod_Precio = Convert.ToDecimal(txtPrecio.Text);

                    TrabajarProducto.alta_producto(prod);

                    MessageBox.Show("Datos guardados correctamente");

                    vaciar_campos();

                    load_productos();
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "" || txtCategoria.Text == "" || txtDescripcion.Text == "" || txtPrecio.Text == "")
            {
                MessageBox.Show("Debe llenar todos los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }else
                if (txtCodigo.Text.Length != 4)
                    {
                        MessageBox.Show("Codigo invalido, debe ser de 4 digitos", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                else if (txtCategoria.Text.Length < 3)
                    {
                        MessageBox.Show("Categoria invalida", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                else if (txtDescripcion.Text.Length < 5)
                  {
                      MessageBox.Show("Descripcion invalida", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  }
                else if (txtPrecio.Text.Length < 2)
                {
                    MessageBox.Show("Precio invalido", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult resultado = MessageBox.Show("¿Está seguro de que desea continuar?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        Producto prod = new Producto();

                        string id = dgvProductos.CurrentRow.Cells["ID"].Value.ToString();
                        int x = Int32.Parse(id);

                        prod.Prod_Id = x;
                        prod.Prod_Codigo = txtCodigo.Text;
                        prod.Prod_Categoria = txtCategoria.Text;
                        prod.Prod_Descripcion = txtDescripcion.Text;
                        prod.Prod_Precio = decimal.Parse(txtPrecio.Text);

                        TrabajarProducto.actualizat_producto(prod);
                        MessageBox.Show("Datos modificados correctamente");

                        vaciar_campos();

                        load_productos();
                        btnModificar.Enabled = false;
                        btn_Eliminar.Enabled = false;
                        btn_Guardar.Enabled = true;
                    }
                }

            
        }

        private void btn_Eliminar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro de que desea continuar?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                Producto prod = new Producto();
                prod.Prod_Id = Int32.Parse(dgvProductos.CurrentRow.Cells["ID"].Value.ToString());
                prod.Prod_Codigo = txtCodigo.Text;
                prod.Prod_Categoria = txtCategoria.Text;
                prod.Prod_Descripcion = txtDescripcion.Text;
                prod.Prod_Precio = decimal.Parse(txtPrecio.Text);

                TrabajarProducto.eliminar_porducto(prod);
                MessageBox.Show("Datos eliminados correctamente");

                vaciar_campos();

                load_productos();
                btnModificar.Enabled = false;
                btn_Eliminar.Enabled = false;
                btn_Guardar.Enabled = true;
            }
        }

        private void load_productos()
        {
            
            dgvProductos.DataSource = TrabajarProducto.listar_productos();
            dgvProductos.Columns["ID"].Visible = false;
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
            if (dgvProductos.CurrentCell != null || dgvProductos.CurrentCell.Value != null)
            {
                txtCodigo.Text = dgvProductos.CurrentRow.Cells["Codigo"].Value.ToString();
                txtCategoria.Text = dgvProductos.CurrentRow.Cells["Categoria"].Value.ToString();
                txtDescripcion.Text = dgvProductos.CurrentRow.Cells["Descripcion"].Value.ToString();
                txtPrecio.Text = dgvProductos.CurrentRow.Cells["Precio"].Value.ToString();
                btnModificar.Enabled = true;
                btn_Eliminar.Enabled = true;
                btn_Guardar.Enabled = false;
            }
        }

        private void vaciar_campos()
        {
            txtCodigo.Clear();
            txtCategoria.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
        }

        private void deshabilitar_campos()
        {
            txtCodigo.Enabled = false;
            txtCategoria.Enabled = false;
            txtDescripcion.Enabled = false;
            txtPrecio.Enabled = false;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtCodigo.Clear();
            txtCategoria.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            btnModificar.Enabled = false;
            btn_Eliminar.Enabled = false;
            btn_Guardar.Enabled = true;
        }
        //Validacion campo CODIGO
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.solo_numeros(e);
        }
        //Validacion campo CATEGORIA
        private void txtCategoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.solo_letras(e);
        }
        //Validacion campo DESCRIPCION
        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.solo_letras(e);
        }
        //Validacion campo PRECIO
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.solo_numeros(e);
        }
       
    }
}
