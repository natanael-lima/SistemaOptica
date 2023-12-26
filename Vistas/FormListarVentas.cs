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
    public partial class FormListarVentas : Form
    {
        public FormListarVentas()
        {
            InitializeComponent();
        }

        private void FormListarVentas_Load(object sender, EventArgs e)
        {
            load_ventas();

        }
        private void load_ventas()
        {
            dataGridVentas.DataSource = TrabajarVenta.list_venta();
            dataGridVentas.Columns["ID"].Visible = false;
            int num = dataGridVentas.RowCount-1;
            txtRegistro.Text = num.ToString();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
             this.Close();
             FormMain formMain = new FormMain();
             formMain.Show();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DataTable dt = TrabajarVenta.getVentasByFecha(dateTimeDesde.Value.Date, dateTimeHasta.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59));
            dataGridVentas.DataSource = dt;
            int num = dataGridVentas.RowCount - 1;
            txtRegistro.Text = num.ToString();
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            load_ventas();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro de que desea continuar?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                Venta venta = new Venta();
                venta.Ven_Nro = Int32.Parse(dataGridVentas.CurrentRow.Cells["Nro Venta"].Value.ToString());

                TrabajarVenta.eliminar_venta(venta);
                MessageBox.Show("Datos eliminados correctamente");

                load_ventas();

            }
        }


    }
}
