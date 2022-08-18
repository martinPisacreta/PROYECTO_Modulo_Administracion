using System;
using System.Data;
using System.Windows.Forms;

namespace Modulo_Administracion
{
    public partial class frmBuscar : Form
    {

        DataTable AuxDt; // Aca tengo q usarlo para filtrar cantidad de datos
        string header_form;
        bool btnNuevo_enabled;
        string nombre_fantasia; //ES UNA VARIABLE SOLAMENTE PARA frmFactura

        public frmBuscar(string header_form, bool btnNuevo_enabled)
        {

            try
            {
                InitializeComponent();
                this.header_form = header_form;
                this.btnNuevo_enabled = btnNuevo_enabled;


            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public frmBuscar(string header_form, bool btnNuevo_enabled, string nombre_fantasia)
        {

            try
            {
                InitializeComponent();
                this.header_form = header_form;
                this.btnNuevo_enabled = btnNuevo_enabled;
                this.nombre_fantasia = nombre_fantasia;


            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }


        public void IniciarForm(DataTable dt, int de_donde_vengo)
        {

            try
            {
                this.Text = header_form;
                bool existe_cliente_en_dt = false;
                AuxDt = dt;
                dgvResultados.DataSource = AuxDt;
                dgvResultados.Columns[0].Visible = false;

                if (de_donde_vengo == 1) //si vengo aca desde frmFactura
                {
                    if (dgvResultados.Columns.Count > 2)
                        dgvResultados.Columns[2].Visible = false; //oculto la columna 2
                }
                else //si vengo desde otro formulario
                {
                    if (dgvResultados.Columns.Count > 2)
                        dgvResultados.Columns[2].Visible = true; //muestro la columna 2
                }


                lblRegistros.Text = "Registros: " + dgvResultados.RowCount;
                if (dgvResultados.RowCount < 1) // si no hay resultados , bloqueo el aceptar y el nuevo
                {
                    btnNuevo.Enabled = false;
                    btnAceptar.Enabled = false;
                }
                else //si hay resultados
                {
                    if (de_donde_vengo == 1) //y vengo aca desde frmFactura
                    {
                        //recorro el dgvResultados para ver si alguno de los registros es igual a nombre_fantasia
                        //esto lo hago para no dar de alta un cliente con un nombre_fantasia existente
                        foreach (DataGridViewRow row in dgvResultados.Rows)
                        {
                            if (row.Cells[1].Value.ToString() == nombre_fantasia)
                            {
                                existe_cliente_en_dt = true;

                            }
                        }


                        if (existe_cliente_en_dt == true) //en caso de que si-> el boton NUEVO lo deshabilito
                        {
                            btnNuevo_enabled = false;
                            btnNuevo.Enabled = btnNuevo_enabled;
                        }
                        else //en caso de que no-> el boton NUEVO lo habilito
                        {
                            btnNuevo_enabled = true;
                            btnNuevo.Enabled = btnNuevo_enabled;
                        }

                    }
                    else //y vengo aca desde otro form
                    {
                        btnNuevo.Enabled = btnNuevo_enabled;
                        if (dgvResultados.RowCount == 1)  // y si hay un solo resultado , no muestro la grilla 
                        {
                            dgvResultados.Rows[0].Selected = true;

                            this.DialogResult = DialogResult.OK;
                            return;
                        }

                    }
                }


                this.ShowDialog();
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        private void btnAceptar_Click(System.Object sender, System.EventArgs e)
        {

            try
            {

                this.DialogResult = DialogResult.OK;

            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
        private void btnCancelar_Click(System.Object sender, System.EventArgs e)
        {

            try
            {

                this.DialogResult = DialogResult.Cancel;

            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
        private void btnNuevo_Click(System.Object sender, System.EventArgs e)
        {

            try
            {
                this.DialogResult = DialogResult.Yes;

            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void dgvResultados_CellDoubleClick(System.Object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {

            try
            {
                this.DialogResult = DialogResult.OK;

            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }


        private void dgvResultados_KeyDown(System.Object sender, System.Windows.Forms.KeyEventArgs e)
        {

            try
            {
                if (e.KeyCode == Keys.Enter)
                    this.DialogResult = DialogResult.OK;

            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }


    }
}