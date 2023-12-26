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
    public partial class FormVenta : Form
    {

        private DataTable DetalleProductos;

        public FormVenta()
        {
            InitializeComponent();
        }

        private void FrmVenta_Load(object sender, EventArgs e)
        {
            load_combo_cliente();
            load_combo_productos();
            IniciarDetalleProductos();
            btnRegistrar.Enabled = false;
            dwvListaDetalles.Columns["ID_PROD"].Visible = false;
            numCantidad.Minimum = 1;
            
        }

        private void load_combo_cliente()
        {
            cmbCli.DisplayMember = "ClienteDisplay";
            cmbCli.ValueMember = "ID";
            cmbCli.DataSource = TrabajarCliente.listar_clientes_combobox();
        }

        private void load_combo_productos()
        {
            cmbProd.DisplayMember = "prod_Descripcion";
            cmbProd.ValueMember = "prod_Id";
            cmbProd.DataSource = TrabajarVenta.list_productos();
            
        }

        private void IniciarDetalleProductos()
        {
            DetalleProductos = new DataTable();
            DetalleProductos.Clear();
            DetalleProductos.Columns.Add("ID_PROD", typeof(int));
            DetalleProductos.Columns.Add("Descripcion", typeof(string));
            DetalleProductos.Columns.Add("Precio Unitario", typeof(decimal));
            DetalleProductos.Columns.Add("Cantidad", typeof(decimal));
            DetalleProductos.Columns.Add("Total", typeof(decimal));
            dwvListaDetalles.DataSource = DetalleProductos;
        }

        private void AgregarProducto()
        {
            
            if (EstaEnTabla(cmbProd.Text))
            {
                ActualizarRow();
            }
            else
            {
                
                DataRow detalle = DetalleProductos.NewRow();
                detalle["ID_PROD"] = Int32.Parse(cmbProd.SelectedValue.ToString());
                detalle["Descripcion"] = cmbProd.Text;

                detalle["Precio Unitario"] = Decimal.Parse(((DataRowView)cmbProd.SelectedItem)["Prod_Precio"].ToString());
                //detalle["Precio Unitario"] = Decimal.Parse(cmbProd.SelectedValue.ToString());
                
                detalle["Cantidad"] = numCantidad.Value;
                detalle["Total"] = numCantidad.Value * Decimal.Parse(((DataRowView)cmbProd.SelectedItem)["Prod_Precio"].ToString());
                DetalleProductos.Rows.Add(detalle);
                btnRegistrar.Enabled = true;

            }
        }

        private bool EstaEnTabla(string pCodigo)
        {
            bool bFound = false;
            foreach (DataRow row in DetalleProductos.Rows)
            {
                if (row["Descripcion"] == (pCodigo))
                    bFound = true;
            }
            return bFound;
        }

        private void ActualizarRow()
        {
            foreach (DataRow row in DetalleProductos.Rows)
            {
                if (row["Descripcion"] == cmbProd.Text)
                {
                    row["ID_PROD"] = row["ID_PROD"];
                    decimal dCantidad = Convert.ToDecimal(row["Cantidad"].ToString()) + numCantidad.Value;
                    row["Cantidad"] = dCantidad;
                    decimal dTotal = Convert.ToDecimal(row["Total"].ToString()) + Convert.ToDecimal(row["Precio Unitario"].ToString()) * numCantidad.Value;
                    row["Total"] = dTotal.ToString();
                }
            }
        }
        

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            AgregarProducto();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Desea registrar la venta?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                //Alta Venta
                Venta ven = new Venta();

                ven.Ven_Fecha = dateTimeVenta.Value;
                string id = cmbCli.SelectedValue.ToString();
                int x = Int32.Parse(id);
                ven.Cli_Id = x;
                
                TrabajarVenta.alta_venta(ven);

                //Alta VentaDetalle
                foreach (DataRow row in DetalleProductos.Rows)
                {
                    VentaDetalle venDet = new VentaDetalle();
    
                    venDet.Ven_Nro = TrabajarVenta.ultima_venta();
                    venDet.Prod_Id = Int32.Parse(row[0].ToString());
                    venDet.Det_Precio = Double.Parse(row[2].ToString());
                    venDet.Det_Cantidad = Double.Parse(row[3].ToString());
                    venDet.Det_Total = Double.Parse(row[4].ToString());
                    TrabajarVenta.alta_ventaDetalle(venDet);

                }

                MessageBox.Show("Datos guardados correctamente");

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir del formulario VENTA?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
                FormMain formMain = new FormMain();
                formMain.Show();
            }
        }

        private void btnVaciar_Click(object sender, EventArgs e)
        {
            btnRegistrar.Enabled = false;
            load_combo_cliente();
            load_combo_productos();
            IniciarDetalleProductos();
        }

        
    }
}
