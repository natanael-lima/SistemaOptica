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
    public partial class FormClientesPorObraSocial : Form
    {
        public FormClientesPorObraSocial()
        {
            InitializeComponent();
        }

        private void FormClientesPorObraSocial_Load(object sender, EventArgs e)
        {
            loadComboClientes();
        }
        
        private void loadComboClientes()
        {
            cmbObraSocial.DisplayMember = "os_RazonSocial";
            cmbObraSocial.ValueMember = "os_CUIT";
            cmbObraSocial.DataSource = TrabajarCliente.list_OS();
            dgwClientesOS.Columns["ID"].Visible = false;
            dgwClientesOS.Columns["cli_Activo"].Visible = false;
        }

        private void cmbClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView selectedObraSocial = (DataRowView)cmbObraSocial.SelectedItem;
            string obraSocialCUIT = (string)selectedObraSocial["os_CUIT"];
            DataTable ventasCliente = TrabajarCliente.listarClientesPorObraSocial(obraSocialCUIT);
            dgwClientesOS.DataSource = ventasCliente;
            int total = dgwClientesOS.RowCount - 1;
            txtTotalClientes.Text = total.ToString();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
            FormMain formMain = new FormMain();
            formMain.Show();
        }
    }
}
