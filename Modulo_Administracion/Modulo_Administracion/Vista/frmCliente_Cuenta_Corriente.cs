using DevExpress.Export;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
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



        public frmCliente_Cuenta_Corriente(cliente _cliente)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                InitializeComponent();
                cliente = _cliente;

                this.Text = cliente.nombre_fantasia;

                iniciar();

                cargar_gridControl();



                gridView1.Columns["Id"].Visible = false;
                gridView1.Columns["Id_factura"].Visible = false;
                gridView1.Columns["Cliente_Nombre_Fantasia"].Visible = false;

                gridView1.Columns["Fecha"].Width = 100;
                gridView1.Columns["Tipo_Factura"].Width = 250;
                gridView1.Columns["Pago_1"].Width = 100;
                gridView1.Columns["Pago_2"].Width = 100;
                gridView1.Columns["Pago_3"].Width = 100;
                gridView1.Columns["Pago_4"].Width = 100;

                gridView1.Columns["Saldo"].OptionsColumn.ReadOnly = true;
                gridView1.Columns["S_Acum"].OptionsColumn.ReadOnly = true;
                gridView1.Columns["Observacion_Factura"].OptionsColumn.ReadOnly = true;
                gridView1.Columns["Condicion_Factura"].OptionsColumn.ReadOnly = true;

                gridView1.Columns["Imp_Factura"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Imp_Factura", "{0}");
                gridView1.Columns["Pago_1"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Pago_1", "{0}");
                gridView1.Columns["Pago_2"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Pago_2", "{0}");
                gridView1.Columns["Pago_3"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Pago_3", "{0}");
                gridView1.Columns["Pago_4"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Pago_4", "{0}");
                gridView1.Columns["Saldo"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Saldo", "{0}");

                gridControl1.ForceInitialize();
                RepositoryItemComboBox _riEditor = new RepositoryItemComboBox();
                _riEditor.Items.AddRange(Logica_Tipo_Factura.loadComboBox_cbTipoFactura_relacionado_a_CCC(false)); //MANDO FALSE PORQUE NO ESTOY LLAMANDO DESDE "AGREGAR MOVIMIENTO"
                gridControl1.RepositoryItems.Add(_riEditor);
                gridView1.Columns[3].ColumnEdit = _riEditor;

                gridView1.BestFitColumns();

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

                gridControl1.Focus();
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


                //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + "CUENTA CORRIENTE " + cliente.nombre_fantasia + ".pdf";



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

                if (Program.snUsoDevExpress == true)
                {
                    reporte_cliente_cuenta_corriente report = new reporte_cliente_cuenta_corriente();
                    report.Parameters["id_cliente"].Value = Convert.ToInt32(cliente.id_cliente);
                    report.Parameters["id_cliente"].Visible = false;

                    report.Parameters["tipo"].Value = tipo;
                    report.Parameters["tipo"].Visible = false;

                    frm_Espere.Hide();
                    Cursor.Current = Cursors.Default;

                    report.ShowPreview();
                }
                else
                {
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + "CUENTA CORRIENTE " + cliente.nombre_fantasia + ".pdf";
                    DataTable dt = new DataTable();
                    foreach (GridColumn column in gridView1.Columns)
                    {
                        dt.Columns.Add(column.FieldName, column.ColumnType);
                    }
                    for (int i = 0; i < gridView1.DataRowCount; i++)
                    {
                        DataRow row = dt.NewRow();
                        foreach (GridColumn column in gridView1.Columns)
                        {
                            row[column.FieldName] = gridView1.GetRowCellValue(i, column);
                        }
                        dt.Rows.Add(row);
                    }
                    Logica_Funciones_Generales.generar_reporteClienteCuentaCorriente(dt, path);

                    frm_Espere.Hide();
                    Cursor.Current = Cursors.Default;

                    MessageBox.Show("PDF generado en " + path, "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
              
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

        public void Options_CustomizeDocumentColumn(CustomizeDocumentColumnEventArgs e)
        {

            if (e.ColumnFieldName == "Fecha_Pago_1")
            {
                e.DocumentColumn.WidthInPixels = 0;
            }
            if (e.ColumnFieldName == "Fecha_Pago_2")
            {
                e.DocumentColumn.WidthInPixels = 0;
            }
            if (e.ColumnFieldName == "Fecha_Pago_3")
            {
                e.DocumentColumn.WidthInPixels = 0;
            }
            if (e.ColumnFieldName == "Fecha_Pago_4")
            {
                e.DocumentColumn.WidthInPixels = 0;
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
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                GridView view = sender as GridView;
                if (view == null) return;
                string saldo = "0,0000";
                decimal imp_factura = 0.0000M;
                decimal pago1 = 0.0000M;
                decimal pago2 = 0.0000M;
                decimal pago3 = 0.0000M;
                decimal pago4 = 0.0000M;
                Int64 nro_factura;
                int cod_tipo_factura = ttipo_factura_constantes.i_valor_vacio;

                if (e.Column.FieldName == "Tipo_Factura") //si la columna de la que estoy saliendo es Tipo_Factura
                {
                    if (view.GetRowCellValue(e.RowHandle, view.Columns["Tipo_Factura"]).ToString() != "") //si hay algo escrito en Tipo_Factura
                    {
                        cod_tipo_factura = convert_tipoFactura_de_STRING_a_INT(view.GetRowCellValue(e.RowHandle, "Tipo_Factura").ToString());

                        Int64 ultimo_nro_factura_vieja = Logica_Factura.ult_nro_factura_no_usado(cod_tipo_factura, false); //el FALSE indica que voy a buscar los datos a la tabla cliente_cuenta_corriente
                        view.SetRowCellValue(e.RowHandle, view.Columns["Nro_Factura"], ultimo_nro_factura_vieja);
                    }
                }
                else if (e.Column.FieldName == "Nro_Factura") //si la columna de la que estoy saliendo es Nro_Factura
                {
                    if (view.GetRowCellValue(e.RowHandle, view.Columns["Nro_Factura"]).ToString() != "") //si hay algo escrito en Nro_Factura
                    {
                        nro_factura = Convert.ToInt64(view.GetRowCellValue(e.RowHandle, view.Columns["Nro_Factura"]).ToString()); //convierto Nro_Factura en INT64
                        if (view.GetRowCellValue(e.RowHandle, "Tipo_Factura").ToString() != "") //si hay algo escrito en Tipo_Factura , cargo cod_tipo_factura
                        {
                            cod_tipo_factura = convert_tipoFactura_de_STRING_a_INT(view.GetRowCellValue(e.RowHandle, "Tipo_Factura").ToString());
                        }
                        if (cod_tipo_factura == ttipo_factura_constantes.i_valor_vacio) //si cod_tipo_factura es 0 , hubo un error
                        {
                            throw new Exception("Error en el tipo de factura");
                        }
                    }
                }
                else if (e.Column.FieldName == "Imp_Factura" || e.Column.FieldName == "Pago_1" || e.Column.FieldName == "Pago_2" || e.Column.FieldName == "Pago_3" || e.Column.FieldName == "Pago_4")
                {


                    if (view.GetRowCellValue(e.RowHandle, view.Columns["Imp_Factura"]).ToString() != "")
                    {
                        imp_factura = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, view.Columns["Imp_Factura"]).ToString());
                    }

                    if (view.GetRowCellValue(e.RowHandle, view.Columns["Pago_1"]).ToString() != "")
                    {
                        pago1 = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, view.Columns["Pago_1"]).ToString());
                    }

                    if (view.GetRowCellValue(e.RowHandle, view.Columns["Pago_2"]).ToString() != "")
                    {
                        pago2 = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, view.Columns["Pago_2"]).ToString());
                    }

                    if (view.GetRowCellValue(e.RowHandle, view.Columns["Pago_3"]).ToString() != "")
                    {
                        pago3 = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, view.Columns["Pago_3"]).ToString());
                    }

                    if (view.GetRowCellValue(e.RowHandle, view.Columns["Pago_4"]).ToString() != "")
                    {
                        pago4 = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, view.Columns["Pago_4"]).ToString());
                    }

                    saldo = (imp_factura - pago1 - pago2 - pago3 - pago4).ToString("N2");

                    view.SetRowCellValue(e.RowHandle, view.Columns["Saldo"], saldo);
                }
                if (e.Column.FieldName == "Fecha_Pago_1")
                {
                    DateTime fecha;
                    if (view.GetRowCellValue(e.RowHandle, view.Columns["Fecha_Pago_1"]).ToString() != "")
                    {
                        fecha = Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, view.Columns["Fecha_Pago_1"]).ToString());
                        if (fecha.Date > DateTime.Now.Date)
                        {
                            view.SetRowCellValue(e.RowHandle, view.Columns["Fecha_Pago_1"], null);
                            throw new Exception("La fecha del pago 1 no puede ser mayor a hoy");
                        }
                    }
                }
                if (e.Column.FieldName == "Fecha_Pago_2")
                {
                    DateTime fecha;
                    if (view.GetRowCellValue(e.RowHandle, view.Columns["Fecha_Pago_2"]).ToString() != "")
                    {
                        fecha = Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, view.Columns["Fecha_Pago_2"]).ToString());
                        if (fecha.Date > DateTime.Now.Date)
                        {
                            view.SetRowCellValue(e.RowHandle, view.Columns["Fecha_Pago_2"], null);
                            throw new Exception("La fecha del pago 2 no puede ser mayor a hoy");
                        }
                    }
                }
                if (e.Column.FieldName == "Fecha_Pago_3")
                {
                    DateTime fecha;
                    if (view.GetRowCellValue(e.RowHandle, view.Columns["Fecha_Pago_3"]).ToString() != "")
                    {
                        fecha = Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, view.Columns["Fecha_Pago_3"]).ToString());
                        if (fecha.Date > DateTime.Now.Date)
                        {
                            view.SetRowCellValue(e.RowHandle, view.Columns["Fecha_Pago_3"], null);
                            throw new Exception("La fecha del pago 3 no puede ser mayor a hoy");
                        }
                    }
                }
                if (e.Column.FieldName == "Fecha_Pago_4")
                {
                    DateTime fecha;
                    if (view.GetRowCellValue(e.RowHandle, view.Columns["Fecha_Pago_4"]).ToString() != "")
                    {
                        fecha = Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, view.Columns["Fecha_Pago_4"]).ToString());
                        if (fecha.Date > DateTime.Now.Date)
                        {
                            view.SetRowCellValue(e.RowHandle, view.Columns["Fecha_Pago_4"], null);
                            throw new Exception("La fecha del pago 4 no puede ser mayor a hoy");
                        }
                    }
                }
                if (e.Column.FieldName == "Observacion_Movimiento_Cta_Cte")
                {
                    string observacion = "";
                    observacion = view.GetRowCellValue(e.RowHandle, view.Columns["Observacion_Movimiento_Cta_Cte"]).ToString();
                    if (observacion.Length > 50)
                    {

                        view.SetRowCellValue(e.RowHandle, view.Columns["Observacion_Movimiento_Cta_Cte"], "");
                        throw new Exception("La observacion del movimiento de cta cte tiene mas de 50 caracteres");
                    }
                }

                gridView1.BestFitColumns();

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

        private void Valido(GridView gridView)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {

                //validacion
                for (int i = 0; i < gridView.DataRowCount; i++)
                {


                    if (gridView.GetRowCellValue(i, "Id_factura").ToString() == "") //si es una factura de las viejas
                    {
                        if (gridView.GetRowCellValue(i, "Fecha").ToString() == "") //y la fecha esta vacia...
                        {
                            gridView.FocusedRowHandle = i;
                            gridView.FocusedColumn = gridView.Columns["Fecha"];
                            gridView.ShowEditor();
                            throw new Exception("Debe cargar la fecha de la factura");
                        }

                        if (gridView.GetRowCellValue(i, "Tipo_Factura").ToString() == "") //y el tipo de factura esta vacio...
                        {
                            gridView.FocusedRowHandle = i;
                            gridView.FocusedColumn = gridView.Columns["Tipo_Factura"];
                            gridView.ShowEditor();
                            throw new Exception("Debe cargar el tipo de factura");
                        }

                        if (gridView.GetRowCellValue(i, "Nro_Factura").ToString() == "") //y el nro de factura esta vacio...
                        {
                            gridView.FocusedRowHandle = i;
                            gridView.FocusedColumn = gridView.Columns["Nro_Factura"];
                            gridView.ShowEditor();
                            throw new Exception("Debe cargar el nro de factura");
                        }


                    }

                    if (gridView.GetRowCellValue(i, "Pago_1").ToString() != "" && gridView.GetRowCellValue(i, "Fecha_Pago_1").ToString() == "")
                    {
                        gridView.FocusedRowHandle = i;
                        gridView.FocusedColumn = gridView.Columns["Fecha_Pago_1"];
                        gridView.ShowEditor();
                        throw new Exception("Debe cargar la fecha del pago 1");
                    }

                    if (gridView.GetRowCellValue(i, "Pago_2").ToString() != "" && gridView.GetRowCellValue(i, "Fecha_Pago_2").ToString() == "")
                    {
                        gridView.FocusedRowHandle = i;
                        gridView.FocusedColumn = gridView.Columns["Fecha_Pago_2"];
                        gridView.ShowEditor();
                        throw new Exception("Debe cargar la fecha del pago 2");
                    }

                    if (gridView.GetRowCellValue(i, "Pago_3").ToString() != "" && gridView.GetRowCellValue(i, "Fecha_Pago_3").ToString() == "")
                    {

                        gridView.FocusedRowHandle = i;
                        gridView.FocusedColumn = gridView.Columns["Fecha_Pago_3"];
                        gridView.ShowEditor();
                        throw new Exception("Debe cargar la fecha del pago 3");
                    }

                    if (gridView.GetRowCellValue(i, "Pago_4").ToString() != "" && gridView.GetRowCellValue(i, "Fecha_Pago_4").ToString() == "")
                    {
                        gridView.FocusedRowHandle = i;
                        gridView.FocusedColumn = gridView.Columns["Fecha_Pago_4"];
                        gridView.ShowEditor();
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


                Valido(gridView1);


                //grabacion
                for (int i = 0; i < gridView1.DataRowCount; i++)
                {

                    cliente_cuenta_corriente = new cliente_cuenta_corriente();

                    if (gridView1.GetRowCellValue(i, "Id_factura").ToString() == "") //si es una factura de las viejas
                    {

                        if (gridView1.GetRowCellValue(i, "Id").ToString() != "")
                        {
                            cliente_cuenta_corriente.id_cliente_cuenta_corriente = Convert.ToInt32(gridView1.GetRowCellValue(i, "Id").ToString());
                        }

                        cliente_cuenta_corriente.id_cliente = cliente.id_cliente;

                        cliente_cuenta_corriente.id_factura = null;
                        if (gridView1.GetRowCellValue(i, "Id_factura").ToString() != "")
                        {
                            cliente_cuenta_corriente.id_factura = Convert.ToInt32(gridView1.GetRowCellValue(i, "Id_factura").ToString());
                        }

                        cliente_cuenta_corriente.fecha_factura_vieja = null;
                        if (gridView1.GetRowCellValue(i, "Fecha").ToString() != "")
                        {
                            cliente_cuenta_corriente.fecha_factura_vieja = Convert.ToDateTime(gridView1.GetRowCellValue(i, "Fecha").ToString());
                        }


                        cliente_cuenta_corriente.nro_factura_vieja = null;
                        if (gridView1.GetRowCellValue(i, "Nro_Factura").ToString() != "")
                        {
                            cliente_cuenta_corriente.nro_factura_vieja = Convert.ToInt64(gridView1.GetRowCellValue(i, "Nro_Factura").ToString());
                        }

                        cliente_cuenta_corriente.cod_tipo_factura_vieja = null;
                        if (gridView1.GetRowCellValue(i, "Tipo_Factura").ToString() != "")
                        {
                            cliente_cuenta_corriente.cod_tipo_factura_vieja = convert_tipoFactura_de_STRING_a_INT(gridView1.GetRowCellValue(i, "Tipo_Factura").ToString());
                        }

                    }
                    else
                    {
                        cliente_cuenta_corriente.id_cliente_cuenta_corriente = Convert.ToInt32(gridView1.GetRowCellValue(i, "Id").ToString());
                        cliente_cuenta_corriente.id_cliente = cliente.id_cliente;
                        cliente_cuenta_corriente.id_factura = Convert.ToInt32(gridView1.GetRowCellValue(i, "Id_factura").ToString());

                        cliente_cuenta_corriente.fecha_factura_vieja = null;
                        cliente_cuenta_corriente.nro_factura_vieja = null;
                        cliente_cuenta_corriente.cod_tipo_factura_vieja = null;
                    }

                    cliente_cuenta_corriente.imp_factura = null;
                    if (gridView1.GetRowCellValue(i, "Imp_Factura").ToString() != "")
                    {
                        cliente_cuenta_corriente.imp_factura = Convert.ToDecimal(gridView1.GetRowCellValue(i, "Imp_Factura").ToString());
                    }

                    cliente_cuenta_corriente.pago_1 = null;
                    if (gridView1.GetRowCellValue(i, "Pago_1").ToString() != "")
                    {
                        cliente_cuenta_corriente.pago_1 = Convert.ToDecimal(gridView1.GetRowCellValue(i, "Pago_1").ToString());
                    }

                    cliente_cuenta_corriente.pago_1_fecha = null;
                    if (gridView1.GetRowCellValue(i, "Fecha_Pago_1").ToString() != "")
                    {
                        cliente_cuenta_corriente.pago_1_fecha = Convert.ToDateTime(gridView1.GetRowCellValue(i, "Fecha_Pago_1").ToString());
                    }


                    cliente_cuenta_corriente.pago_2 = null;
                    if (gridView1.GetRowCellValue(i, "Pago_2").ToString() != "")
                    {
                        cliente_cuenta_corriente.pago_2 = Convert.ToDecimal(gridView1.GetRowCellValue(i, "Pago_2").ToString());
                    }

                    cliente_cuenta_corriente.pago_2_fecha = null;
                    if (gridView1.GetRowCellValue(i, "Fecha_Pago_2").ToString() != "")
                    {
                        cliente_cuenta_corriente.pago_2_fecha = Convert.ToDateTime(gridView1.GetRowCellValue(i, "Fecha_Pago_2").ToString());
                    }

                    cliente_cuenta_corriente.pago_3 = null;
                    if (gridView1.GetRowCellValue(i, "Pago_3").ToString() != "")
                    {
                        cliente_cuenta_corriente.pago_3 = Convert.ToDecimal(gridView1.GetRowCellValue(i, "Pago_3").ToString());
                    }

                    cliente_cuenta_corriente.pago_3_fecha = null;
                    if (gridView1.GetRowCellValue(i, "Fecha_Pago_3").ToString() != "")
                    {
                        cliente_cuenta_corriente.pago_3_fecha = Convert.ToDateTime(gridView1.GetRowCellValue(i, "Fecha_Pago_3").ToString());
                    }


                    cliente_cuenta_corriente.pago_4 = null;
                    if (gridView1.GetRowCellValue(i, "Pago_4").ToString() != "")
                    {
                        cliente_cuenta_corriente.pago_4 = Convert.ToDecimal(gridView1.GetRowCellValue(i, "Pago_4").ToString());
                    }

                    cliente_cuenta_corriente.pago_4_fecha = null;
                    if (gridView1.GetRowCellValue(i, "Fecha_Pago_4").ToString() != "")
                    {
                        cliente_cuenta_corriente.pago_4_fecha = Convert.ToDateTime(gridView1.GetRowCellValue(i, "Fecha_Pago_4").ToString());
                    }

                    cliente_cuenta_corriente.observacion = gridView1.GetRowCellValue(i, "Observacion_Movimiento_Cta_Cte").ToString();

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


                    cargar_gridControl();

                    gridView1.Columns["Fecha"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
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
                gridView1.AddNewRow();

                RepositoryItemComboBox _riEditor = new RepositoryItemComboBox();
                _riEditor.Items.AddRange(Logica_Tipo_Factura.loadComboBox_cbTipoFactura_relacionado_a_CCC(true)); //MANDO TRUE PORQUE  ESTOY LLAMANDO DESDE "AGREGAR MOVIMIENTO"
                gridControl1.RepositoryItems.Add(_riEditor);
                gridView1.Columns[3].ColumnEdit = _riEditor;


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
                if (gridView1.RowCount > 0)
                {
                    string id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id").ToString(); //es es el id_cliente_cuenta_corriente
                    string id_factura = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id_factura").ToString();

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
                        gridView1.DeleteRow(gridView1.FocusedRowHandle);
                        gridView1.Columns["Fecha"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                        //gridControl1.DataSource = null;
                        //gridControl1.DataSource = Logica_Cliente_Cuenta_Corriente.buscar_movimientos_cuenta_corriente_por_id_cliente(cliente.id_cliente).Tables[0]; //cargo en gridControl1
                    }
                    else
                    {
                        throw new Exception("No se puede eliminar una factura ya generada en el sistema");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                string id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id").ToString();
                string tipo_factura = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Tipo_Factura").ToString();
                string fecha = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Fecha").ToString();

                if (id != "" && gridView1.FocusedColumn.FieldName == "Id")
                {
                    e.Cancel = true;
                }
                else if (id != "" && gridView1.FocusedColumn.FieldName == "Id_factura")
                {
                    e.Cancel = true;
                }
                else if (id != "" && gridView1.FocusedColumn.FieldName == "Fecha")
                {
                    e.Cancel = true;
                }
                else if (id != "" && gridView1.FocusedColumn.FieldName == "Tipo_Factura")
                {
                    e.Cancel = true;
                }
                else if (id != "" && gridView1.FocusedColumn.FieldName == "Nro_Factura")
                {
                    e.Cancel = true;
                }
                else if (id != "" && gridView1.FocusedColumn.FieldName == "Imp_Factura" && tipo_factura == ttipo_factura_constantes.s_valor_remito) //NO VOY A PODER MODIFICAR "Imp_Factura" SI EL TIPO DE FACTURA ES "REMITO" , PARA LOS DEMAS SI
                {
                    e.Cancel = true;
                }

                if (tipo_factura == "" && gridView1.FocusedColumn.FieldName == "Nro_Factura" && fecha != "")
                {
                    e.Cancel = true;
                    throw new Exception("Debe seleccionar un tipo de factura antes de cargar el número de la misma");
                }

                //SI ESTOY AGREANDO UN MOVIMIENTO , NO VOY A PODER EDITAR EL NRO FACTURA , PORQUE ESE VALOR LO TRAIGO DE LA BD
                if (id == "" && gridView1.FocusedColumn.FieldName == "Nro_Factura")
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmCliente_Cuenta_Corriente_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rdDeuda_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                cargar_gridControl();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rdTodos_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                cargar_gridControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cargar_gridControl()
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

                gridControl1.DataSource = null;
                gridControl1.DataSource = Logica_Cliente_Cuenta_Corriente.buscar_movimientos_CCC(cliente.id_cliente, tipo).Tables[0]; //cargo en gridControl1

                decimal d_saldo = 0;
                for (int i = 0; i < gridView1.DataRowCount; i++)
                {
                    d_saldo += Convert.ToDecimal(gridView1.GetRowCellValue(i, "Saldo"));
                    gridView1.SetRowCellValue(i, "S_Acum", d_saldo);
                   
                }


            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        private void gridControl1_EditorKeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                GridControl grid = sender as GridControl;
                gridView1_KeyPress(grid.FocusedView, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //funcion que indica que "," y "." es lo mismo
        private void gridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (view.FocusedColumn.FieldName == "Imp_Factura" || view.FocusedColumn.FieldName == "Pago_1" || view.FocusedColumn.FieldName == "Pago_2" || view.FocusedColumn.FieldName == "Pago_3" || view.FocusedColumn.FieldName == "Pago_4")
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

    }
}
