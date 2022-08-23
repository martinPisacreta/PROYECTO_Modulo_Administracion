using Modulo_Administracion.Clases;
using Modulo_Administracion.Clases.Custom;
using Modulo_Administracion.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace Modulo_Administracion.Vista
{
    public partial class frmCliente_Cuenta_Corriente : Form
    {
        cliente cliente;
        string[] tiposFactura_Todos;
        string[] tiposFactura_de_un_Nuevo_Movimiento;

        public frmCliente_Cuenta_Corriente(cliente _cliente)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                InitializeComponent();
                cliente = _cliente;

                this.Text = cliente.nombre_fantasia;

                iniciar();

                DataTable dt = buscar_movimientos_CCC();

                tiposFactura_Todos = Logica_Tipo_Factura.loadComboBox_cbTipoFactura_relacionado_a_CCC(false);
                tiposFactura_de_un_Nuevo_Movimiento = Logica_Tipo_Factura.loadComboBox_cbTipoFactura_relacionado_a_CCC(true);

                cargar_dgv(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }




        private void iniciar()
        {
            try
            {

                dgvClienteCuentaCorriente.Focus();
                rdDeuda.Checked = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            frmEspere frm_Espere = new frmEspere();
            int tipo;
            try
            {




                if (rdDeuda.Checked == true)
                {
                    tipo = 1;
                }
                else
                {
                    tipo = 2;
                }


                Cursor.Current = Cursors.WaitCursor;

                frm_Espere.Show();


                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + "CUENTA CORRIENTE " + cliente.nombre_fantasia + ".pdf";

                DataTable dt = (DataTable)dgvClienteCuentaCorriente.DataSource;
                Logica_Funciones_Generales.generar_reporteClienteCuentaCorriente(dt, path);

                frm_Espere.Hide();
                Cursor.Current = Cursors.Default;

                MessageBox.Show("PDF generado en " + path, "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                frm_Espere.Hide();
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                frm_Espere.Hide();
                Cursor.Current = Cursors.Default;
            }
        }



        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private int convert_tipoFactura_de_STRING_a_INT(string valor_seleccionado_combo)
        {
            try
            {
                int cod_tipo_factura = ttipo_factura_constantes.i_valor_vacio;
                if (valor_seleccionado_combo == ttipo_factura_constantes.s_valor_remito)
                {
                    cod_tipo_factura = ttipo_factura_constantes.i_valor_remito;
                }
                else if (valor_seleccionado_combo == ttipo_factura_constantes.s_valor_nota_credito)
                {
                    cod_tipo_factura = ttipo_factura_constantes.i_valor_nota_credito;
                }
                else if (valor_seleccionado_combo == ttipo_factura_constantes.s_valor_nota_debito)
                {
                    cod_tipo_factura = ttipo_factura_constantes.i_valor_nota_debito;
                }
                else if (valor_seleccionado_combo == ttipo_factura_constantes.s_valor_factura_a_sin_comprobante)
                {
                    cod_tipo_factura = ttipo_factura_constantes.i_valor_factura_a_sin_comprobante;
                }
                else if (valor_seleccionado_combo == ttipo_factura_constantes.s_valor_factura_b_sin_comprobante)
                {
                    cod_tipo_factura = ttipo_factura_constantes.i_valor_factura_b_sin_comprobante;
                }
                else if (valor_seleccionado_combo == ttipo_factura_constantes.s_valor_remito_sin_comprobante)
                {
                    cod_tipo_factura = ttipo_factura_constantes.i_valor_remito_sin_comprobante;
                }
                else if (valor_seleccionado_combo == ttipo_factura_constantes.s_valor_nota_credito_sin_comprobante)
                {
                    cod_tipo_factura = ttipo_factura_constantes.i_valor_nota_credito_sin_comprobante;
                }
                else if (valor_seleccionado_combo == ttipo_factura_constantes.s_valor_nota_debito_sin_comprobante)
                {
                    cod_tipo_factura = ttipo_factura_constantes.i_valor_nota_debito_sin_comprobante;
                }

                return cod_tipo_factura;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void dgvClienteCuentaCorriente_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                string saldo = "0,0000";
                decimal imp_factura = 0.0000M;
                decimal pago1 = 0.0000M;
                decimal pago2 = 0.0000M;
                decimal pago3 = 0.0000M;
                decimal pago4 = 0.0000M;
                Int64 nro_factura;
                int cod_tipo_factura = ttipo_factura_constantes.i_valor_vacio;

                if (dgvClienteCuentaCorriente.Columns[e.ColumnIndex].Name == "Tipo_Factura") //si la columna de la que estoy saliendo es Tipo_Factura
                {
                    if (dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Tipo_Factura"].Value.ToString() != "") //si hay algo escrito en Tipo_Factura
                    {
                        cod_tipo_factura = convert_tipoFactura_de_STRING_a_INT(dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Tipo_Factura"].Value.ToString());

                        Int64 ultimo_nro_factura_vieja = Logica_Factura.ult_nro_factura_no_usado(cod_tipo_factura, false); //el FALSE indica que voy a buscar los datos a la tabla cliente_cuenta_corriente
                        dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Nro_Factura"].Value = ultimo_nro_factura_vieja;
                    }
                }
                else if (dgvClienteCuentaCorriente.Columns[e.ColumnIndex].Name == "Nro_Factura") //si la columna de la que estoy saliendo es Nro_Factura
                {
                    if (dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Nro_Factura"].Value.ToString() != "") //si hay algo escrito en Nro_Factura
                    {
                        nro_factura = Convert.ToInt64(dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Nro_Factura"].Value.ToString()); //convierto Nro_Factura en INT64
                        if (dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Tipo_Factura"].Value.ToString() != "") //si hay algo escrito en Tipo_Factura , cargo cod_tipo_factura
                        {
                            cod_tipo_factura = convert_tipoFactura_de_STRING_a_INT(dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Tipo_Factura"].Value.ToString());
                        }
                        if (cod_tipo_factura == ttipo_factura_constantes.i_valor_vacio) //si cod_tipo_factura es 0 , hubo un error
                        {
                            throw new Exception("Error en el tipo de factura");
                        }
                    }
                }
                else if (dgvClienteCuentaCorriente.Columns[e.ColumnIndex].Name == "Imp_Factura" || dgvClienteCuentaCorriente.Columns[e.ColumnIndex].Name == "Pago_1" || dgvClienteCuentaCorriente.Columns[e.ColumnIndex].Name == "Pago_2" || dgvClienteCuentaCorriente.Columns[e.ColumnIndex].Name == "Pago_3" || dgvClienteCuentaCorriente.Columns[e.ColumnIndex].Name == "Pago_4")
                {


                    if (dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Imp_Factura"].Value.ToString() != "")
                    {
                        imp_factura = Convert.ToDecimal(dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Imp_Factura"].Value.ToString());
                        dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Imp_Factura"].Value = imp_factura.ToString("0.00");
                    }

                    if (dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Pago_1"].Value.ToString() != "")
                    {
                        pago1 = Convert.ToDecimal(dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Pago_1"].Value.ToString());
                        dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Pago_1"].Value = pago1.ToString("0.00");
                    }

                    if (dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Pago_2"].Value.ToString() != "")
                    {
                        pago2 = Convert.ToDecimal(dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Pago_2"].Value.ToString());
                        dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Pago_2"].Value = pago2.ToString("0.00");
                    }

                    if (dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Pago_3"].Value.ToString() != "")
                    {
                        pago3 = Convert.ToDecimal(dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Pago_3"].Value.ToString());
                        dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Pago_3"].Value = pago3.ToString("0.00");
                    }

                    if (dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Pago_4"].Value.ToString() != "")
                    {
                        pago4 = Convert.ToDecimal(dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Pago_4"].Value.ToString());
                        dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Pago_4"].Value = pago4.ToString("0.00");
                    }

                    saldo = (imp_factura - pago1 - pago2 - pago3 - pago4).ToString("0.00");

                    dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Saldo"].Value = saldo;
                }
                if (dgvClienteCuentaCorriente.Columns[e.ColumnIndex].Name == "Fecha_Pago_1")
                {
                    DateTime fecha;
                    if (dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Fecha_Pago_1"].Value.ToString() != "")
                    {
                        fecha = Convert.ToDateTime(dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Fecha_Pago_1"].Value.ToString());
                        if (fecha.Date > DateTime.Now.Date)
                        {
                            dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Fecha_Pago_1"].Value = DBNull.Value;
                            throw new Exception("La fecha del pago 1 no puede ser mayor a hoy");
                        }
                    }
                }
                if (dgvClienteCuentaCorriente.Columns[e.ColumnIndex].Name == "Fecha_Pago_2")
                {
                    DateTime fecha;
                    if (dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Fecha_Pago_2"].Value.ToString() != "")
                    {
                        fecha = Convert.ToDateTime(dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Fecha_Pago_2"].Value.ToString());
                        if (fecha.Date > DateTime.Now.Date)
                        {
                            dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Fecha_Pago_2"].Value = DBNull.Value;
                            throw new Exception("La fecha del pago 2 no puede ser mayor a hoy");
                        }
                    }
                }
                if (dgvClienteCuentaCorriente.Columns[e.ColumnIndex].Name == "Fecha_Pago_3")
                {
                    DateTime fecha;
                    if (dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Fecha_Pago_3"].Value.ToString() != "")
                    {
                        fecha = Convert.ToDateTime(dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Fecha_Pago_3"].Value.ToString());
                        if (fecha.Date > DateTime.Now.Date)
                        {
                            dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Fecha_Pago_3"].Value = DBNull.Value;
                            throw new Exception("La fecha del pago 3 no puede ser mayor a hoy");
                        }
                    }
                }
                if (dgvClienteCuentaCorriente.Columns[e.ColumnIndex].Name == "Fecha_Pago_4")
                {
                    DateTime fecha;
                    if (dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Fecha_Pago_4"].Value.ToString() != "")
                    {
                        fecha = Convert.ToDateTime(dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Fecha_Pago_4"].Value.ToString());
                        if (fecha.Date > DateTime.Now.Date)
                        {
                            dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Fecha_Pago_4"].Value = DBNull.Value;
                            throw new Exception("La fecha del pago 4 no puede ser mayor a hoy");
                        }
                    }
                }
                if (dgvClienteCuentaCorriente.Columns[e.ColumnIndex].Name == "Observacion_Movimiento_Cta_Cte")
                {
                    string observacion = "";
                    observacion = dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Observacion_Movimiento_Cta_Cte"].Value.ToString();
                    if (observacion.Length > 50)
                    {

                        dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Observacion_Movimiento_Cta_Cte"].Value = "";
                        throw new Exception("La observacion del movimiento de cta cte tiene mas de 50 caracteres");
                    }
                }



            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }



        private void Valido(DataGridView gridView)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {

                //validacion
                for (int i = 0; i < gridView.RowCount; i++)
                {


                    if (dgvClienteCuentaCorriente.Rows[i].Cells["Id_factura"].Value.ToString() == "") //si es una factura de las viejas
                    {
                        if (dgvClienteCuentaCorriente.Rows[i].Cells["Fecha"].Value.ToString() == "") //y la fecha esta vacia...
                        {
                            dgvClienteCuentaCorriente.Rows[i].Cells["Fecha"].Selected = true;
                            throw new Exception("Debe cargar la fecha de la factura");
                        }


                        if (dgvClienteCuentaCorriente.Rows[i].Cells["Tipo_Factura"].Value.ToString() == "") //y el tipo de factura esta vacio...
                        {
                            dgvClienteCuentaCorriente.Rows[i].Cells["Tipo_Factura"].Selected = true;
                            throw new Exception("Debe cargar el tipo de factura");
                        }

                        if (dgvClienteCuentaCorriente.Rows[i].Cells["Nro_Factura"].Value.ToString() == "") //y el nro de factura esta vacio...
                        {
                            dgvClienteCuentaCorriente.Rows[i].Cells["Nro_Factura"].Selected = true;
                            throw new Exception("Debe cargar el nro de factura");
                        }


                    }

                    if (dgvClienteCuentaCorriente.Rows[i].Cells["Imp_Factura"].Value.ToString() == "")
                    {
                        dgvClienteCuentaCorriente.Rows[i].Cells["Imp_Factura"].Selected = true;
                        throw new Exception("Debe cargar el imp factura");
                    }

                    if (dgvClienteCuentaCorriente.Rows[i].Cells["Pago_1"].Value.ToString() != "" && dgvClienteCuentaCorriente.Rows[i].Cells["Fecha_Pago_1"].Value.ToString() == "")
                    {
                        dgvClienteCuentaCorriente.Rows[i].Cells["Fecha_Pago_1"].Selected = true;
                        throw new Exception("Debe cargar la fecha del pago 1");
                    }

                    if (dgvClienteCuentaCorriente.Rows[i].Cells["Pago_2"].Value.ToString() != "" && dgvClienteCuentaCorriente.Rows[i].Cells["Fecha_Pago_2"].Value.ToString() == "")
                    {
                        dgvClienteCuentaCorriente.Rows[i].Cells["Fecha_Pago_2"].Selected = true;
                        throw new Exception("Debe cargar la fecha del pago 2");
                    }

                    if (dgvClienteCuentaCorriente.Rows[i].Cells["Pago_3"].Value.ToString() != "" && dgvClienteCuentaCorriente.Rows[i].Cells["Fecha_Pago_3"].Value.ToString() == "")
                    {
                        dgvClienteCuentaCorriente.Rows[i].Cells["Fecha_Pago_3"].Selected = true;
                        throw new Exception("Debe cargar la fecha del pago 3");
                    }

                    if (dgvClienteCuentaCorriente.Rows[i].Cells["Pago_4"].Value.ToString() != "" && dgvClienteCuentaCorriente.Rows[i].Cells["Fecha_Pago_4"].Value.ToString() == "")
                    {
                        dgvClienteCuentaCorriente.Rows[i].Cells["Fecha_Pago_4"].Selected = true;
                        throw new Exception("Debe cargar la fecha del pago 4");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                List<cliente_cuenta_corriente> lista_cliente_cuenta_corriente = new List<cliente_cuenta_corriente>() { };
                cliente_cuenta_corriente cliente_cuenta_corriente = null;


                Valido(dgvClienteCuentaCorriente);


                //grabacion
                for (int i = 0; i < dgvClienteCuentaCorriente.RowCount; i++)
                {

                    cliente_cuenta_corriente = new cliente_cuenta_corriente();

                    if (dgvClienteCuentaCorriente.Rows[i].Cells["Id_factura"].Value.ToString() == "") //si es una factura de las viejas
                    {

                        if (dgvClienteCuentaCorriente.Rows[i].Cells["Id"].Value.ToString() != "")
                        {
                            cliente_cuenta_corriente.id_cliente_cuenta_corriente = Convert.ToInt32(dgvClienteCuentaCorriente.Rows[i].Cells["Id"].Value.ToString());
                        }

                        cliente_cuenta_corriente.id_cliente = cliente.id_cliente;

                        cliente_cuenta_corriente.id_factura = null;
                        if (dgvClienteCuentaCorriente.Rows[i].Cells["Id_factura"].Value.ToString() != "")
                        {
                            cliente_cuenta_corriente.id_factura = Convert.ToInt32(dgvClienteCuentaCorriente.Rows[i].Cells["Id_factura"].Value.ToString());
                        }

                        cliente_cuenta_corriente.fecha_factura_vieja = null;
                        if (dgvClienteCuentaCorriente.Rows[i].Cells["Fecha"].Value.ToString() != "")
                        {
                            cliente_cuenta_corriente.fecha_factura_vieja = Convert.ToDateTime(dgvClienteCuentaCorriente.Rows[i].Cells["Fecha"].Value.ToString());
                        }


                        cliente_cuenta_corriente.nro_factura_vieja = null;
                        if (dgvClienteCuentaCorriente.Rows[i].Cells["Nro_Factura"].Value.ToString() != "")
                        {
                            cliente_cuenta_corriente.nro_factura_vieja = Convert.ToInt64(dgvClienteCuentaCorriente.Rows[i].Cells["Nro_Factura"].Value.ToString());
                        }

                        cliente_cuenta_corriente.cod_tipo_factura_vieja = null;
                        if (dgvClienteCuentaCorriente.Rows[i].Cells["Tipo_Factura"].Value.ToString() != "")
                        {
                            cliente_cuenta_corriente.cod_tipo_factura_vieja = convert_tipoFactura_de_STRING_a_INT(dgvClienteCuentaCorriente.Rows[i].Cells["Tipo_Factura"].Value.ToString());
                        }

                    }
                    else
                    {
                        cliente_cuenta_corriente.id_cliente_cuenta_corriente = Convert.ToInt32(dgvClienteCuentaCorriente.Rows[i].Cells["Id"].Value.ToString());
                        cliente_cuenta_corriente.id_cliente = cliente.id_cliente;
                        cliente_cuenta_corriente.id_factura = Convert.ToInt32(dgvClienteCuentaCorriente.Rows[i].Cells["Id_factura"].Value.ToString());

                        cliente_cuenta_corriente.fecha_factura_vieja = null;
                        cliente_cuenta_corriente.nro_factura_vieja = null;
                        cliente_cuenta_corriente.cod_tipo_factura_vieja = null;
                    }

                    cliente_cuenta_corriente.imp_factura = null;
                    if (dgvClienteCuentaCorriente.Rows[i].Cells["Imp_Factura"].Value.ToString() != "")
                    {
                        cliente_cuenta_corriente.imp_factura = Convert.ToDecimal(dgvClienteCuentaCorriente.Rows[i].Cells["Imp_Factura"].Value.ToString());
                    }

                    cliente_cuenta_corriente.pago_1 = null;
                    if (dgvClienteCuentaCorriente.Rows[i].Cells["Pago_1"].Value.ToString() != "")
                    {
                        cliente_cuenta_corriente.pago_1 = Convert.ToDecimal(dgvClienteCuentaCorriente.Rows[i].Cells["Pago_1"].Value.ToString());
                    }

                    cliente_cuenta_corriente.pago_1_fecha = null;
                    if (dgvClienteCuentaCorriente.Rows[i].Cells["Fecha_Pago_1"].Value.ToString() != "")
                    {
                        cliente_cuenta_corriente.pago_1_fecha = Convert.ToDateTime(dgvClienteCuentaCorriente.Rows[i].Cells["Fecha_Pago_1"].Value.ToString());
                    }


                    cliente_cuenta_corriente.pago_2 = null;
                    if (dgvClienteCuentaCorriente.Rows[i].Cells["Pago_2"].Value.ToString() != "")
                    {
                        cliente_cuenta_corriente.pago_2 = Convert.ToDecimal(dgvClienteCuentaCorriente.Rows[i].Cells["Pago_2"].Value.ToString());
                    }

                    cliente_cuenta_corriente.pago_2_fecha = null;
                    if (dgvClienteCuentaCorriente.Rows[i].Cells["Fecha_Pago_2"].Value.ToString() != "")
                    {
                        cliente_cuenta_corriente.pago_2_fecha = Convert.ToDateTime(dgvClienteCuentaCorriente.Rows[i].Cells["Fecha_Pago_2"].Value.ToString());
                    }

                    cliente_cuenta_corriente.pago_3 = null;
                    if (dgvClienteCuentaCorriente.Rows[i].Cells["Pago_3"].Value.ToString() != "")
                    {
                        cliente_cuenta_corriente.pago_3 = Convert.ToDecimal(dgvClienteCuentaCorriente.Rows[i].Cells["Pago_3"].Value.ToString());
                    }

                    cliente_cuenta_corriente.pago_3_fecha = null;
                    if (dgvClienteCuentaCorriente.Rows[i].Cells["Fecha_Pago_3"].Value.ToString() != "")
                    {
                        cliente_cuenta_corriente.pago_3_fecha = Convert.ToDateTime(dgvClienteCuentaCorriente.Rows[i].Cells["Fecha_Pago_3"].Value.ToString());
                    }


                    cliente_cuenta_corriente.pago_4 = null;
                    if (dgvClienteCuentaCorriente.Rows[i].Cells["Pago_4"].Value.ToString() != "")
                    {
                        cliente_cuenta_corriente.pago_4 = Convert.ToDecimal(dgvClienteCuentaCorriente.Rows[i].Cells["Pago_4"].Value.ToString());
                    }

                    cliente_cuenta_corriente.pago_4_fecha = null;
                    if (dgvClienteCuentaCorriente.Rows[i].Cells["Fecha_Pago_4"].Value.ToString() != "")
                    {
                        cliente_cuenta_corriente.pago_4_fecha = Convert.ToDateTime(dgvClienteCuentaCorriente.Rows[i].Cells["Fecha_Pago_4"].Value.ToString());
                    }

                    cliente_cuenta_corriente.observacion = dgvClienteCuentaCorriente.Rows[i].Cells["Observacion_Movimiento_Cta_Cte"].Value.ToString();

                    lista_cliente_cuenta_corriente.Add(cliente_cuenta_corriente);

                }


                if (Logica_Cliente_Cuenta_Corriente.modificar_movimientos_CCC(lista_cliente_cuenta_corriente) == false)
                {
                    throw new Exception("Error al actualizar cuenta corriente");
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Actualizacíon correcta", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DataTable dt = buscar_movimientos_CCC();
                    cargar_dgv(dt);
                }


            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }



        private void btnAgregarMovimiento_Click(object sender, EventArgs e)
        {

            try
            {

                DataTable dt = (DataTable)dgvClienteCuentaCorriente.DataSource;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (snFilaVacia(i) == true)
                    {
                        throw new Exception("Ya existe una fila vacia en la grilla , utilícela");
                    }
                }

                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
                cargar_dgv(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarMovimiento_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvClienteCuentaCorriente.RowCount > 0)
                {
                    if (dgvClienteCuentaCorriente.SelectedRows.Count == 1)
                    {
                        string id = dgvClienteCuentaCorriente.SelectedRows[0].Cells["Id"].Value.ToString(); //es es el id_cliente_cuenta_corriente
                        string id_factura = dgvClienteCuentaCorriente.SelectedRows[0].Cells["Id_factura"].Value.ToString();

                        if (id_factura == "") //si no tiene id_factura , la puedo eliminar ya que no fue cargada en mi sistema
                        {
                            if (id != "")
                            {
                                int id_cliente_cuenta_corriente = Convert.ToInt32(id);
                                if (Logica_Cliente_Cuenta_Corriente.eliminar_movimiento_CCC(id_cliente_cuenta_corriente) == false)
                                {
                                    throw new Exception("Error al eliminar el movimiento de la cuenta corriente");
                                }

                            }
                            DataGridViewRow row = dgvClienteCuentaCorriente.SelectedRows[0];
                            dgvClienteCuentaCorriente.Rows.Remove(row);


                            dgvClienteCuentaCorriente.Sort(this.dgvClienteCuentaCorriente.Columns["Fecha"], ListSortDirection.Ascending);

                            //refresh dgvClienteCuentaCorriente
                            DataTable dt = buscar_movimientos_CCC();
                            cargar_dgv(dt);
                        }
                        else
                        {
                            throw new Exception("No se puede eliminar una factura ya generada en el sistema");
                        }
                    }
                    else
                    {
                        throw new Exception("Debe seleccionar una fila");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvClienteCuentaCorriente_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                string id = dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                string tipo_factura = dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Tipo_Factura"].Value.ToString();
                string fecha = dgvClienteCuentaCorriente.Rows[e.RowIndex].Cells["Fecha"].Value.ToString();

                if (id != "" && dgvClienteCuentaCorriente.Columns[e.ColumnIndex].Name == "Id")
                {
                    e.Cancel = true;
                }
                else if (id != "" && dgvClienteCuentaCorriente.Columns[e.ColumnIndex].Name == "Id_factura")
                {
                    e.Cancel = true;
                }
                else if (id != "" && dgvClienteCuentaCorriente.Columns[e.ColumnIndex].Name == "Fecha")
                {
                    e.Cancel = true;
                }
                else if (id != "" && dgvClienteCuentaCorriente.Columns[e.ColumnIndex].Name == "Tipo_Factura")
                {
                    e.Cancel = true;
                }
                else if (id != "" && dgvClienteCuentaCorriente.Columns[e.ColumnIndex].Name == "Nro_Factura")
                {
                    e.Cancel = true;
                }
                else if (id != "" && dgvClienteCuentaCorriente.Columns[e.ColumnIndex].Name == "Imp_Factura" && tipo_factura == ttipo_factura_constantes.s_valor_remito) //NO VOY A PODER MODIFICAR "Imp_Factura" SI EL TIPO DE FACTURA ES "REMITO" , PARA LOS DEMAS SI
                {
                    e.Cancel = true;
                }

                if (tipo_factura == "" && dgvClienteCuentaCorriente.Columns[e.ColumnIndex].Name == "Nro_Factura" && fecha != "")
                {
                    e.Cancel = true;
                    throw new Exception("Debe seleccionar un tipo de factura antes de cargar el número de la misma");
                }

                //SI ESTOY AGREANDO UN MOVIMIENTO , NO VOY A PODER EDITAR EL NRO FACTURA , PORQUE ESE VALOR LO TRAIGO DE LA BD
                if (id == "" && dgvClienteCuentaCorriente.Columns[e.ColumnIndex].Name == "Nro_Factura")
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }






        private void rdDeuda_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable dt = buscar_movimientos_CCC();
                cargar_dgv(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rdTodos_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = buscar_movimientos_CCC();
                cargar_dgv(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void cargar_dgv(DataTable dt)
        {
            try
            {

                dgvClienteCuentaCorriente.DataSource = null;

                //CONVIERTO LA COLUMNA [Tipo_Factura] EN DataGridViewComboBoxColumn EN BASE AL dgvClienteCuentaCorriente
                DataGridViewComboBoxColumn colTipoFactura = new DataGridViewComboBoxColumn();
                colTipoFactura.HeaderText = "Tipo_Factura";
                colTipoFactura.Name = "Tipo_Factura";
                colTipoFactura.DataPropertyName = "Tipo_Factura";
                colTipoFactura.DataSource = tiposFactura_Todos;
                dgvClienteCuentaCorriente.Columns.Add(colTipoFactura);

                //CALCULO S_Acum EN BASE AL DATATABLE
                decimal d_saldo = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Saldo"].ToString() != "")
                    {
                        d_saldo += Convert.ToDecimal(row["Saldo"].ToString());
                        row["S_Acum"] = d_saldo;
                    }

                }

                //SETEO dgvClienteCuentaCorriente
                dgvClienteCuentaCorriente.DataSource = dt;

                //SET DataGridViewComboBoxCell EN BASE AL dgvClienteCuentaCorriente
                for (int i = 0; i < dgvClienteCuentaCorriente.RowCount; i++)
                {
                    DataGridViewComboBoxCell colTipoFactura_cell = new DataGridViewComboBoxCell(); //GENERO UN DataGridViewComboBoxCell
                    if (dgvClienteCuentaCorriente.Rows[i].Cells["Tipo_Factura"].Value.ToString() == "") //SI LA COLUMNA [Tipo_Factura] DEL ROW QUE ESTOY , ESTA VACIA , ES UN MOVIMIENTO NUEVO
                    {
                        colTipoFactura_cell.DataSource = tiposFactura_de_un_Nuevo_Movimiento; //POR ENDE CARGO SOLAMENTE LOS TIPOS DE FACTURA DE MOVIMIENTOS NUEVOS
                    }
                    else //SINO ES UN MOVIMIENTO EXISTENTE
                    {
                        colTipoFactura_cell.DataSource = tiposFactura_Todos; //Y CARGO TODOS LOS TIPOS FACTURA

                    }

                    //SETEO colTipoFactura_cell EN COLUMNA [Tipo_Factura] DEL ROW QUE ESTOY
                    dgvClienteCuentaCorriente.Rows[i].Cells["Tipo_Factura"] = colTipoFactura_cell;

                }



                //DESPUES DE CARGAR EL dgvClienteCuentaCorriente
                dgvClienteCuentaCorriente.Columns["Id"].Visible = false;
                dgvClienteCuentaCorriente.Columns["Id_factura"].Visible = false;
                dgvClienteCuentaCorriente.Columns["Cliente_Nombre_Fantasia"].Visible = false;

                dgvClienteCuentaCorriente.Columns["Saldo"].ReadOnly = true;
                dgvClienteCuentaCorriente.Columns["S_Acum"].ReadOnly = true;
                dgvClienteCuentaCorriente.Columns["Observacion_Factura"].ReadOnly = true;
                dgvClienteCuentaCorriente.Columns["Condicion_Factura"].ReadOnly = true;

                dgvClienteCuentaCorriente.Columns["Fecha"].Width = 100;
                dgvClienteCuentaCorriente.Columns["Tipo_Factura"].Width = 250;
                dgvClienteCuentaCorriente.Columns["Pago_1"].Width = 100;
                dgvClienteCuentaCorriente.Columns["Pago_2"].Width = 100;
                dgvClienteCuentaCorriente.Columns["Pago_3"].Width = 100;
                dgvClienteCuentaCorriente.Columns["Pago_4"].Width = 100;

                ordenar_dataGridView(dgvClienteCuentaCorriente);

                calcular_summaries(dgvClienteCuentaCorriente);

                //Hago que no se pueda ordenar ninguna columna
                foreach (DataGridViewColumn column in dgvClienteCuentaCorriente.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }



        //funcion que indica que "," y "." es lo mismo
        private void dgvClienteCuentaCorriente_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            try
            {
                int colIndex = dgvClienteCuentaCorriente.CurrentCell.ColumnIndex;
                if (dgvClienteCuentaCorriente.Columns[colIndex].Name == "Imp_Factura" || dgvClienteCuentaCorriente.Columns[colIndex].Name == "Pago_1" || dgvClienteCuentaCorriente.Columns[colIndex].Name == "Pago_2" || dgvClienteCuentaCorriente.Columns[colIndex].Name == "Pago_3" || dgvClienteCuentaCorriente.Columns[colIndex].Name == "Pago_4")
                {
                    DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;
                    tb.KeyPress += new KeyPressEventHandler(dgvClienteCuentaCorriente_KeyPress);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }





        //--------------------------------------------------------------- FUNCIONES NUEVAS --------------------------------------------------------------------------------------

        private void dgvClienteCuentaCorriente_KeyPress(object sender, KeyPressEventArgs e) //funcion que indica que "," y "." es lo mismo
        {
            try
            {

                int column = dgvClienteCuentaCorriente.CurrentCellAddress.X;
                int row = dgvClienteCuentaCorriente.CurrentCellAddress.Y;

                if (dgvClienteCuentaCorriente.Columns[column].Name == "Imp_Factura" || dgvClienteCuentaCorriente.Columns[column].Name == "Pago_1" || dgvClienteCuentaCorriente.Columns[column].Name == "Pago_2" || dgvClienteCuentaCorriente.Columns[column].Name == "Pago_3" || dgvClienteCuentaCorriente.Columns[column].Name == "Pago_4")
                {
                    if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar == ',' || e.KeyChar == '.')
                    {
                        if (e.KeyChar == '.') //si llego a escribir un punto , lo reemplazo por coma
                        {
                            e.KeyChar = ',';
                        }
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool snFilaVacia(int index)
        {
            try
            {

                for (int col = 0; col < dgvClienteCuentaCorriente.Rows[index].Cells.Count; col++)
                {
                    var valor = dgvClienteCuentaCorriente.Rows[index].Cells[col].Value.ToString();
                    if (valor != "")
                    {
                        return false;
                    }

                }


                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ordenar_dataGridView(DataGridView dgvClienteCuentaCorriente)
        {
            try
            {
                dgvClienteCuentaCorriente.Columns["Id"].DisplayIndex = 0;
                dgvClienteCuentaCorriente.Columns["Id_factura"].DisplayIndex = 1;

                dgvClienteCuentaCorriente.Columns["Fecha"].DisplayIndex = 2;


                dgvClienteCuentaCorriente.Columns["Tipo_Factura"].DisplayIndex = 3;

                dgvClienteCuentaCorriente.Columns["Nro_Factura"].DisplayIndex = 4;


                dgvClienteCuentaCorriente.Columns["Imp_Factura"].DisplayIndex = 5;
                dgvClienteCuentaCorriente.Columns["Pago_1"].DisplayIndex = 6;

                dgvClienteCuentaCorriente.Columns["Fecha_Pago_1"].DisplayIndex = 7;
                ((DataGridViewTextBoxColumn)dgvClienteCuentaCorriente.Columns["Fecha_Pago_1"]).MaxInputLength = 10;

                dgvClienteCuentaCorriente.Columns["Pago_2"].DisplayIndex = 8;

                dgvClienteCuentaCorriente.Columns["Fecha_Pago_2"].DisplayIndex = 9;
                ((DataGridViewTextBoxColumn)dgvClienteCuentaCorriente.Columns["Fecha_Pago_2"]).MaxInputLength = 10;

                dgvClienteCuentaCorriente.Columns["Pago_3"].DisplayIndex = 10;

                dgvClienteCuentaCorriente.Columns["Fecha_Pago_3"].DisplayIndex = 11;
                ((DataGridViewTextBoxColumn)dgvClienteCuentaCorriente.Columns["Fecha_Pago_3"]).MaxInputLength = 10;

                dgvClienteCuentaCorriente.Columns["Pago_4"].DisplayIndex = 12;

                dgvClienteCuentaCorriente.Columns["Fecha_Pago_4"].DisplayIndex = 13;
                ((DataGridViewTextBoxColumn)dgvClienteCuentaCorriente.Columns["Fecha_Pago_4"]).MaxInputLength = 10;

                dgvClienteCuentaCorriente.Columns["Saldo"].DisplayIndex = 14;
                dgvClienteCuentaCorriente.Columns["S_Acum"].DisplayIndex = 15;
                dgvClienteCuentaCorriente.Columns["Condicion_Factura"].DisplayIndex = 16;
                dgvClienteCuentaCorriente.Columns["Observacion_Factura"].DisplayIndex = 17;
                dgvClienteCuentaCorriente.Columns["Observacion_Movimiento_Cta_Cte"].DisplayIndex = 18;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        private void calcular_summaries(DataGridView dgvClienteCuentaCorriente)
        {
            try
            {
                DataTable dt = (DataTable)dgvClienteCuentaCorriente.DataSource;
                decimal _lblImpFactura = 0;
                decimal _lblPago1 = 0;
                decimal _lblPago2 = 0;
                decimal _lblPago3 = 0;
                decimal _lblPago4 = 0;
                decimal _lblSaldo = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Imp_Factura"].ToString() != "")
                    {
                        _lblImpFactura = _lblImpFactura + Convert.ToDecimal(row["Imp_Factura"].ToString());
                    }

                    if (row["Pago_1"].ToString() != "")
                    {
                        _lblPago1 = _lblPago1 + Convert.ToDecimal(row["Pago_1"].ToString());
                    }

                    if (row["Pago_2"].ToString() != "")
                    {
                        _lblPago2 = _lblPago2 + Convert.ToDecimal(row["Pago_2"].ToString());
                    }

                    if (row["Pago_3"].ToString() != "")
                    {
                        _lblPago3 = _lblPago3 + Convert.ToDecimal(row["Pago_3"].ToString());
                    }

                    if (row["Pago_4"].ToString() != "")
                    {
                        _lblPago4 = _lblPago4 + Convert.ToDecimal(row["Pago_4"].ToString());
                    }

                    if (row["Saldo"].ToString() != "")
                    {
                        _lblSaldo = _lblSaldo + Convert.ToDecimal(row["Saldo"].ToString());
                    }

                }


                lblImpFactura.Text = _lblImpFactura.ToString("C");
                lblPago1.Text = _lblPago1.ToString("C");
                lblPago2.Text = _lblPago2.ToString("C");
                lblPago3.Text = _lblPago3.ToString("C");
                lblPago4.Text = _lblPago4.ToString("C");
                lblSaldo.Text = _lblSaldo.ToString("C");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable buscar_movimientos_CCC()
        {
            try
            {

                int tipo;
                if (rdDeuda.Checked == true)
                {
                    tipo = 1;
                }
                else
                {
                    tipo = 2;
                }
                DataTable dt = Logica_Cliente_Cuenta_Corriente.buscar_movimientos_CCC(cliente.id_cliente, tipo).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}
