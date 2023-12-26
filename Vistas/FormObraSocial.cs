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
    public partial class FormObraSocial : Form
    {
        public FormObraSocial()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtCUIT.Text == "" || txtRazonSocial.Text == "" || txtDireccion.Text == "" || txtTelefono.Text == "")
            {
                MessageBox.Show("Debe llenar todos campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro de que desea continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    // El usuario ha hecho clic en "Sí"

                    ObraSocial os = new ObraSocial();
                    os.Os_CUIT = txtCUIT.Text;
                    os.Os_RazonSocial = txtRazonSocial.Text;
                    os.Os_Direccion = txtDireccion.Text;
                    os.Os_Telefono = txtTelefono.Text;

                    MessageBox.Show("Datos guardados correctamente: CUIT: " + os.Os_CUIT + " , Razon Social: " + os.Os_RazonSocial + " , Direccion: " + os.Os_Direccion + " , Telefono: " + os.Os_Telefono);
                }
                else
                {
                    // El usuario ha hecho clic en "No"

                }
            }



           
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar el ALTA de obra social?", "Cancelar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
                FormMain formMain = new FormMain();
                formMain.Show();
            }
        }
    }
}
