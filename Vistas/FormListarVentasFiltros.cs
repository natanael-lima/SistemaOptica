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
    public partial class FormListarVentasFiltros : Form
    {
        public FormListarVentasFiltros()
        {
            InitializeComponent();
        }

        private void FormVentasCliente_Load(object sender, EventArgs e)
        {
            load_combo_clientes();
        }

        private void load_combo_clientes()
        {
            cmbClientes.DisplayMember = "ClienteDisplay";
            cmbClientes.ValueMember = "ID";
            cmbClientes.DataSource = TrabajarCliente.listar_clientes_combobox();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
            FormMain formMain = new FormMain();
            formMain.Show();
        }

        private void cmbClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView selectedCliente = (DataRowView)cmbClientes.SelectedItem;
            int clienteID = (int)selectedCliente["ID"];
            DataTable ventasCliente = TrabajarVenta.listar_ventas_por_cliente(clienteID);
            dgwVentasCliente.DataSource = ventasCliente;
            cantProd.Text = Convert.ToString(contar_productos(ventasCliente));
            int num = dgwVentasCliente.RowCount - 1;
            txtRegistros.Text = num.ToString();

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (dtPicker1.Text != "" && dtPicker2.Text != "")
            {
                //var parseDate1 = DateTime.Parse(dtPicker1.Text);
                //var parseDate2 = DateTime.Parse(dtPicker2.Text);
                DataTable ventasPorFecha = TrabajarProducto.exec_listar_productos(dtPicker1.Value.Date, dtPicker2.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59));
                dgwVentasCliente.DataSource = ventasPorFecha;
                cantProd.Text = Convert.ToString(contar_productos(ventasPorFecha));
                int num = dgwVentasCliente.RowCount - 1;
                txtRegistros.Text = num.ToString();
            }
            else
            {

            }
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
