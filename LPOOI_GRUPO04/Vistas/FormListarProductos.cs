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
    public partial class FormListarProductos : Form
    {
        public FormListarProductos()
        {
            InitializeComponent();
            
        }

        private void rbtDescripción_CheckedChanged(object sender, EventArgs e)
        {
            string orden = "Descripcion";
            dataGridProductos.DataSource = TrabajarProducto.ordenar_Productos(orden);
            dataGridProductos.Columns["ID"].Visible = false;
        }

        private void rbtCategoría_CheckedChanged(object sender, EventArgs e)
        {
            string orden2 = "Categoria";
            dataGridProductos.DataSource = TrabajarProducto.ordenar_Productos(orden2);
            dataGridProductos.Columns["ID"].Visible = false;
        }

        private void FormListarProductos_Load(object sender, EventArgs e)
        {
            load_combo_cliente();
           
            
        }

        private void load_combo_cliente()
        {
            cmbCliente.DisplayMember = "ClienteDisplay";
            cmbCliente.ValueMember = "ID";
            cmbCliente.DataSource = TrabajarCliente.listar_clientes_combobox();

        }

        private void cmbCliente_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int id = (int)(cmbCliente.SelectedValue);
            DataSet ds = TrabajarProducto.lista_productos_vendidos_x_cliente(id);
            dataGridProductos.DataSource = ds.Tables[0];
            dataGridProductos.Refresh();
            dataGridProductos.Columns["NRO VENTA"].Visible = false;

            DataRowView selectedCliente = (DataRowView)cmbCliente.SelectedItem;
            int clienteID = (int)selectedCliente["ID"];
            DataTable ventasCliente = TrabajarVenta.listar_ventas_por_cliente(clienteID);
            dataGridProductos.DataSource = ventasCliente;
            cantProd.Text = Convert.ToString(contar_productos(ventasCliente));

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
            FormMain formMain = new FormMain();
            formMain.Show();
        }
        private int contar_productos(DataTable table)
        {
            int total = 0;
            foreach (DataRow Fila in table.Rows)
            {
                total = total + Convert.ToInt32(Fila["Cantidad"]);
            }
            return total;
        }
    }
}
