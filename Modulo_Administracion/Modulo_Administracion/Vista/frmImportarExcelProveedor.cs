using Modulo_Administracion.Clases;
using Modulo_Administracion.Logica;
using Modulo_Administracion.Vista;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Modulo_Administracion
{
    public partial class frmImportarExcelProveedor : Form
    {

        //LABEL DE CADA TABINDEX
        string s_tabPageNovedades = "*Articulos nuevos en el sistema";
        int i_tabPageNovedades = 0;


        //NOMBRE DE COLUMNAS QUE VIENEN DE LA BASE DE DATOS
        string columnName_codigoArticuloProveedor_dgvNovedades = "codigo_articulo";
        string columnName_descripcionArticuloProveedor_dgvNovedades = "descripcion_articulo";
        string columnName_precioListaProveedor_dgvNovedades = "precio_lista";
        string columnName_idTablaFamilia_dgvNovedades = "id_tabla_familia";

        string lblCaptionExcludeRows = "Seleccione fila/s para excluirla/s de la importación";

        public new Form ParentForm { get; set; }


        public frmImportarExcelProveedor()
        {
            try
            {
                InitializeComponent();
                Logica_Funciones_Generales.cargar_comboBox("proveedor", cbProveedor, "razon_social", "sn_activo = -1", "razon_social", "id_proveedor");
                cbProveedor.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelarSeleccion_Click(object sender, EventArgs e)
        {
            try
            {
                limpio_form();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void limpio_form()
        {
            try
            {
                tabControl1.Enabled = false;

                txtArchivoExcel.Text = "";
                txtArchivoExcel.Enabled = false;

                cbProveedor.Enabled = true;
                cbProveedor.SelectedItem = null;


                dgvNovedades.DataSource = null;
                dgvNovedades.Columns.Clear();



                panelSeteoPMF.Enabled = false;

                btnImportar.Enabled = false;
                btnBuscar.Enabled = false;

                cbMarca.DataSource = null;
                cbFamilia.DataSource = null;




                btnExcel.Enabled = true;
                btnBuscar.Enabled = true;

                lblCaptionExcludeRows_2.Text = lblCaptionExcludeRows;
                lblInfoBody.Text = s_tabPageNovedades;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            frmEspere form = new frmEspere();
            try
            {
                if (cbProveedor.SelectedItem == null)
                {
                    cbProveedor.Focus();
                    throw new Exception("Debe seleccionar un proveedor");
                }
                //creamos un objeto OpenDialog que es un cuadro de dialogo para buscar archivos
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Archivos de Excel (*.xls;*.xlsx;*.xlsm)|*.xls;*.xlsx;*.xlsm"; //le indicamos el tipo de filtro en este caso que busque solo los archivos excel

                dialog.Title = "Seleccione el archivo de Excel";//le damos un titulo a la ventana

                dialog.FileName = string.Empty;//inicializamos con vacio el nombre del archivo

                //si al seleccionar el archivo damos Ok
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    form.Show();
                    txtArchivoExcel.Enabled = false;
                    cbProveedor.Enabled = false;
                    dgvNovedades.Enabled = true;

                    //el nombre del archivo sera asignado al textbox
                    txtArchivoExcel.Text = dialog.FileName;
                    Llenar_DataGridView(txtArchivoExcel.Text); //se manda a llamar al metodo


                    tabControl1.Enabled = true;



                    form.Hide();
                    Cursor.Current = Cursors.Default;


                }

            }
            catch (Exception ex)
            {
                form.Hide();
                txtArchivoExcel.Text = "";
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void Llenar_DataGridView(string archivo)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {


                try
                {

                    DataTable dt_excel_proveedor = new DataTable();
                    int id_proveedor = Convert.ToInt32(cbProveedor.SelectedValue);

                    using (Stream inputStream = File.OpenRead(archivo))
                    {
                        using (ExcelEngine excelEngine = new ExcelEngine())
                        {
                            IApplication application = excelEngine.Excel;
                            IWorkbook workbook = application.Workbooks.Open(inputStream);
                            IWorksheet worksheet = workbook.Worksheets[0]; //hoja 0

                            dt_excel_proveedor = worksheet.ExportDataTable(worksheet.UsedRange, ExcelExportDataTableOptions.ColumnNames);
                        }
                    }



                    List<DataRow> deletedRows = new List<DataRow>();
                    List<DataRow> addRows = new List<DataRow>();



                    //elimino las columnas del data_table_datos_correctos con nro de columna mayor a 3
                    int desiredSize = 3;
                    while (dt_excel_proveedor.Columns.Count > desiredSize)
                    {
                        dt_excel_proveedor.Columns.RemoveAt(desiredSize);
                    }


                    foreach (DataRow row in dt_excel_proveedor.Rows)
                    {
                        //elimino los caracteres de mas del principio del string
                        string descripcion_articulo = row["descripcion_articulo"].ToString().TrimStart();
                        string codigo_articulo = row["codigo_articulo"].ToString().TrimStart();

                        //elimino los caracteres de mas del final del string
                        descripcion_articulo = descripcion_articulo.TrimEnd();
                        codigo_articulo = codigo_articulo.TrimEnd();

                        //reemplazo ' por -
                        descripcion_articulo = descripcion_articulo.Replace("'", "-");

                        codigo_articulo = codigo_articulo.Replace("'", "-");

                        //elimino los espacios en blanco de mas por uno solo espacio en blanco
                        row["descripcion_articulo"] = Regex.Replace(descripcion_articulo, @"\s+", " ");
                        row["codigo_articulo"] = Regex.Replace(codigo_articulo, @"\s+", " ");
                    }

                    //agrego la columna de id_proveedor
                    dt_excel_proveedor.Columns.Add("id_proveedor", typeof(System.Int32));
                    foreach (DataRow row in dt_excel_proveedor.Rows)
                    {
                        row["id_proveedor"] = id_proveedor;
                    }

                    DataSet ds = Logica_Articulo.buscar_articulos_en_relacion_a_tmpListaPreciosProveedor(dt_excel_proveedor, id_proveedor);

                    //cargo los dataGridView

                    //0
                    dgvNovedades.DataSource = ds.Tables[0];
                    if (dgvNovedades.Rows.Count > 0)
                    {
                        panelSeteoPMF.Enabled = true;
                        cbMarca.Enabled = true;
                        cargarCombos(2);
                    }


                    //deshabilito esto
                    btnExcel.Enabled = false;
                    btnBuscar.Enabled = false;

                    btnImportar.Enabled = true;


                }
                catch (Exception ex)
                {
                    //en caso de haber una excepcion que nos mande un mensaje de error
                    throw new Exception(ex.Message);

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }


        }



        private void cargarCombos(int tipo)
        {
            try
            {
                if (tipo == 2) //limpio marca y familia y cargo las marcas
                {

                    cbMarca.DataSource = null;
                    cbFamilia.DataSource = null;
                    Logica_Funciones_Generales.cargar_comboBox("marca", cbMarca, "txt_desc_marca", "id_proveedor = " + cbProveedor.SelectedValue + "and sn_activo = -1", "txt_desc_marca", "id_tabla_marca");

                    cbMarca.SelectedItem = null;
                    //cbMarca.SelectedIndexChanged += new EventHandler(cbMarca_SelectedIndexChanged);

                }
                if (tipo == 3) // limpio familia y cargo familia 
                {
                    cbFamilia.DataSource = null;
                    Logica_Funciones_Generales.cargar_comboBox("familia", cbFamilia, "txt_desc_familia", "id_tabla_marca = " + cbMarca.SelectedValue + "and sn_activo = -1", "txt_desc_familia", "id_tabla_familia");
                    cbFamilia.SelectedItem = null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }







        private void frmImportarExcel_Load(object sender, EventArgs e)
        {
            try
            {
                limpio_form();
            }
            catch (Exception ex)
            {
                //en caso de haber una excepcion que nos mande un mensaje de error
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private string Validacion(DataGridView dataGridView)
        {
            foreach (DataGridViewRow rw in dataGridView.Rows)
            {
                for (int i = 0; i < rw.Cells.Count; i++)
                {
                    //VALIDO DE QUE NO HAYA CELDAS VACIAS
                    if (rw.Cells[i].Value == null || rw.Cells[i].Value == DBNull.Value || String.IsNullOrWhiteSpace(rw.Cells[i].Value.ToString()))
                    {

                        return "No puede haber celdas vacias en ARTICULOS NUEVOS";
                    }

                    //VALIDO DE QUE ESTAN COMPLETOS TODOS LOS ID_TABLA_FAMILIA
                    if (Convert.ToInt32(rw.Cells[columnName_idTablaFamilia_dgvNovedades].Value.ToString()) == 0)
                    {

                        return "No puede haber celdas en donde el valor de id_tabla_familia sea 0 en ARTICULOS NUEVOS";
                    }
                }
            }
            return "";
        }


        private void btnImportar_Click(object sender, EventArgs e)
        {
            frmEspere form = new frmEspere();
            articulo articulo = null;
            List<articulo> lista_articulos = null;
            Modulo_AdministracionContext db = new Modulo_AdministracionContext();
            Cursor.Current = Cursors.WaitCursor;
            form.Show();
            using (DbContextTransaction dbContextTransaction = db.Database.BeginTransaction())
            {

                try
                {
                    //SOLAMENTE VOY A IMPORTAR A LA BASE dgvNovedades
                    if (
                            dgvNovedades.Rows.Count == 0
                       )
                    {
                        throw new Exception("No hay registros para importar");
                    }


                    if (dgvNovedades.Rows.Count != 0)
                    {
                        string mensaje_validacion = Validacion(dgvNovedades);
                        if (mensaje_validacion != "")
                        {
                            tabControl1.SelectedIndex = i_tabPageNovedades;
                            dgvNovedades.Focus();
                            throw new Exception(mensaje_validacion);
                        }

                        lista_articulos = new List<articulo>() { };
                        foreach (DataGridViewRow row in dgvNovedades.Rows)
                        {
                            articulo = new articulo();
                            articulo.codigo_articulo = row.Cells[columnName_codigoArticuloProveedor_dgvNovedades].Value.ToString();
                            articulo.descripcion_articulo = row.Cells[columnName_descripcionArticuloProveedor_dgvNovedades].Value.ToString();
                            articulo.precio_lista = Convert.ToDecimal(row.Cells[columnName_precioListaProveedor_dgvNovedades].Value);
                            articulo.id_tabla_familia = Convert.ToInt32(row.Cells[columnName_idTablaFamilia_dgvNovedades].Value);
                            lista_articulos.Add(articulo);
                        }


                        if (Logica_Articulo.alta_articulos_inexistentes_en_relacion_a_ListaProveedor(lista_articulos, db) == false)
                        {
                            throw new Exception("Error al actualizar los ARTICULOS NUEVOS");
                        }

                        lista_articulos = null;

                    }

                    MessageBox.Show("Importación de articulos correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dbContextTransaction.Commit();
                    limpio_form();
                    form.Hide();
                    Cursor.Current = Cursors.Default;


                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    form.Hide();
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    db = null;
                    form.Hide();
                    Cursor.Current = Cursors.Default;
                }
            }




        }



        private void btnCancelarPMF_Click(object sender, EventArgs e)
        {
            try
            {



                cbMarca.SelectedItem = null;
                cbFamilia.SelectedItem = null;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Default;
            try
            {

                if (dgvNovedades.SelectedRows.Count > 0)
                {

                    if (cbProveedor.SelectedItem == null)
                    {
                        cbProveedor.Focus();
                        throw new Exception("Debe seleccionar un proveedor");
                    }

                    if (cbMarca.SelectedItem == null)
                    {
                        cbMarca.Focus();
                        throw new Exception("Debe seleccionar una marca");
                    }

                    if (cbFamilia.SelectedItem == null)
                    {
                        cbFamilia.Focus();
                        throw new Exception("Debe seleccionar una familia");
                    }

                    foreach (DataGridViewRow row in dgvNovedades.SelectedRows)
                    {
                        row.Cells["id_tabla_familia"].Value = Convert.ToInt32(cbFamilia.SelectedValue);
                    }

                    MessageBox.Show("Acción realizada correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    throw new Exception("Debe seleccionar al menos una fila");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.WaitCursor;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {

                string nameFile = "templateImportarListaProveedor.xlsx";
                DialogResult dialogResult = MessageBox.Show("Se abrira un Excel que aparecera en escritorio con el nombre " + nameFile +
                                                            "\n\nEn caso de que el archivo exista , se sobreescribira " +
                                                            "\n\n¿Desea continuar?", "Atención", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                    string origen = Path.Combine(Application.StartupPath, @"Documento\" + nameFile);
                    string destino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + nameFile;

                    if (File.Exists(destino))
                    {
                        File.Delete(destino);
                    }

                    System.IO.File.Copy(origen, destino, true);

                    Process.Start(destino);

                    btnBuscar.Enabled = true;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbProveedor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {

                btnBuscar.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void btnEliminarFilas_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Default;
            try
            {

                if (dgvNovedades.SelectedRows.Count > 0)
                {

                    foreach (DataGridViewRow row in dgvNovedades.SelectedRows)
                    {

                        dgvNovedades.Rows.RemoveAt(row.Index);
                    }

                    if (dgvNovedades.SelectedRows.Count == 1)
                    {
                        MessageBox.Show("Fila eliminada", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (dgvNovedades.SelectedRows.Count > 1)
                    {
                        MessageBox.Show("Filas eliminadas", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    throw new Exception("Debe seleccionar al menos una fila");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.WaitCursor;
            }
        }




        private void frmImportarExcelProveedor_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.ParentForm.Show();
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbMarca_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {

                if (cbMarca.SelectedItem != null && cbProveedor.SelectedItem != null) //si tengo algo elegido en el combo de proveedor y marca , cargo el combo de familia segun lo que dice proveedor y marca
                {
                    cbFamilia.Enabled = true;
                    cargarCombos(3);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

