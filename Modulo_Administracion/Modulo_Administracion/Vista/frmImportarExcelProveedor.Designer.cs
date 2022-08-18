namespace Modulo_Administracion
{
    partial class frmImportarExcelProveedor
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnExcel = new System.Windows.Forms.Button();
            this.cbProveedor = new System.Windows.Forms.ComboBox();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.lblMarca = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtArchivoExcel = new System.Windows.Forms.TextBox();
            this.panelBody = new System.Windows.Forms.Panel();
            this.lblInfoBody = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage0 = new System.Windows.Forms.TabPage();
            this.panelSeteoPMF = new System.Windows.Forms.Panel();
            this.lblCaptionExcludeRows_2 = new System.Windows.Forms.Label();
            this.btnEliminarFilas = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.btnCancelarPMF = new System.Windows.Forms.Button();
            this.cbFamilia = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbMarca = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvNovedades = new System.Windows.Forms.DataGridView();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.btnImportar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnCancelarSeleccion = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelHeader.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage0.SuspendLayout();
            this.panelSeteoPMF.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNovedades)).BeginInit();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panelHeader.Controls.Add(this.btnExcel);
            this.panelHeader.Controls.Add(this.cbProveedor);
            this.panelHeader.Controls.Add(this.lblProveedor);
            this.panelHeader.Controls.Add(this.lblMarca);
            this.panelHeader.Controls.Add(this.btnBuscar);
            this.panelHeader.Controls.Add(this.txtArchivoExcel);
            this.panelHeader.Location = new System.Drawing.Point(16, 12);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(917, 118);
            this.panelHeader.TabIndex = 37;
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnExcel.BackColor = System.Drawing.Color.Firebrick;
            this.btnExcel.FlatAppearance.BorderColor = System.Drawing.Color.IndianRed;
            this.btnExcel.FlatAppearance.CheckedBackColor = System.Drawing.Color.IndianRed;
            this.btnExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.IndianRed;
            this.btnExcel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Location = new System.Drawing.Point(591, 17);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(4);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(267, 38);
            this.btnExcel.TabIndex = 42;
            this.btnExcel.Text = "Abrir template excel para cargar datos";
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // cbProveedor
            // 
            this.cbProveedor.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProveedor.FormattingEnabled = true;
            this.cbProveedor.Location = new System.Drawing.Point(119, 25);
            this.cbProveedor.Name = "cbProveedor";
            this.cbProveedor.Size = new System.Drawing.Size(405, 24);
            this.cbProveedor.TabIndex = 40;
            this.cbProveedor.SelectionChangeCommitted += new System.EventHandler(this.cbProveedor_SelectionChangeCommitted);
            // 
            // lblProveedor
            // 
            this.lblProveedor.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblProveedor.AutoSize = true;
            this.lblProveedor.Location = new System.Drawing.Point(10, 25);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(74, 17);
            this.lblProveedor.TabIndex = 41;
            this.lblProveedor.Text = "Proveedor";
            // 
            // lblMarca
            // 
            this.lblMarca.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblMarca.AutoSize = true;
            this.lblMarca.Location = new System.Drawing.Point(13, 80);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(55, 17);
            this.lblMarca.TabIndex = 39;
            this.lblMarca.Text = "Archivo";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnBuscar.BackColor = System.Drawing.Color.Firebrick;
            this.btnBuscar.FlatAppearance.BorderColor = System.Drawing.Color.IndianRed;
            this.btnBuscar.FlatAppearance.CheckedBackColor = System.Drawing.Color.IndianRed;
            this.btnBuscar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.IndianRed;
            this.btnBuscar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(826, 69);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(80, 38);
            this.btnBuscar.TabIndex = 38;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtArchivoExcel
            // 
            this.txtArchivoExcel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtArchivoExcel.Location = new System.Drawing.Point(121, 77);
            this.txtArchivoExcel.Name = "txtArchivoExcel";
            this.txtArchivoExcel.ReadOnly = true;
            this.txtArchivoExcel.Size = new System.Drawing.Size(675, 22);
            this.txtArchivoExcel.TabIndex = 37;
            // 
            // panelBody
            // 
            this.panelBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBody.Controls.Add(this.lblInfoBody);
            this.panelBody.Controls.Add(this.tabControl1);
            this.panelBody.Location = new System.Drawing.Point(16, 136);
            this.panelBody.Name = "panelBody";
            this.panelBody.Size = new System.Drawing.Size(917, 468);
            this.panelBody.TabIndex = 38;
            // 
            // lblInfoBody
            // 
            this.lblInfoBody.AutoSize = true;
            this.lblInfoBody.BackColor = System.Drawing.Color.White;
            this.lblInfoBody.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoBody.ForeColor = System.Drawing.Color.Red;
            this.lblInfoBody.Location = new System.Drawing.Point(13, 12);
            this.lblInfoBody.Name = "lblInfoBody";
            this.lblInfoBody.Size = new System.Drawing.Size(77, 17);
            this.lblInfoBody.TabIndex = 3;
            this.lblInfoBody.Text = "lblInfoBody";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage0);
            this.tabControl1.Location = new System.Drawing.Point(13, 43);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(893, 410);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage0
            // 
            this.tabPage0.Controls.Add(this.panelSeteoPMF);
            this.tabPage0.Controls.Add(this.dgvNovedades);
            this.tabPage0.Location = new System.Drawing.Point(4, 25);
            this.tabPage0.Name = "tabPage0";
            this.tabPage0.Size = new System.Drawing.Size(885, 381);
            this.tabPage0.TabIndex = 1;
            this.tabPage0.Text = "Novedades";
            this.tabPage0.UseVisualStyleBackColor = true;
            // 
            // panelSeteoPMF
            // 
            this.panelSeteoPMF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSeteoPMF.Controls.Add(this.lblCaptionExcludeRows_2);
            this.panelSeteoPMF.Controls.Add(this.btnEliminarFilas);
            this.panelSeteoPMF.Controls.Add(this.label4);
            this.panelSeteoPMF.Controls.Add(this.btnAplicar);
            this.panelSeteoPMF.Controls.Add(this.btnCancelarPMF);
            this.panelSeteoPMF.Controls.Add(this.cbFamilia);
            this.panelSeteoPMF.Controls.Add(this.label1);
            this.panelSeteoPMF.Controls.Add(this.cbMarca);
            this.panelSeteoPMF.Controls.Add(this.label3);
            this.panelSeteoPMF.Location = new System.Drawing.Point(6, 174);
            this.panelSeteoPMF.Name = "panelSeteoPMF";
            this.panelSeteoPMF.Size = new System.Drawing.Size(873, 204);
            this.panelSeteoPMF.TabIndex = 44;
            // 
            // lblCaptionExcludeRows_2
            // 
            this.lblCaptionExcludeRows_2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCaptionExcludeRows_2.AutoSize = true;
            this.lblCaptionExcludeRows_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaptionExcludeRows_2.Location = new System.Drawing.Point(17, 150);
            this.lblCaptionExcludeRows_2.Name = "lblCaptionExcludeRows_2";
            this.lblCaptionExcludeRows_2.Size = new System.Drawing.Size(373, 17);
            this.lblCaptionExcludeRows_2.TabIndex = 53;
            this.lblCaptionExcludeRows_2.Text = "Seleccione fila/s para excluirla/s de la importación";
            // 
            // btnEliminarFilas
            // 
            this.btnEliminarFilas.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEliminarFilas.BackColor = System.Drawing.Color.Firebrick;
            this.btnEliminarFilas.FlatAppearance.BorderColor = System.Drawing.Color.IndianRed;
            this.btnEliminarFilas.FlatAppearance.CheckedBackColor = System.Drawing.Color.IndianRed;
            this.btnEliminarFilas.FlatAppearance.MouseDownBackColor = System.Drawing.Color.IndianRed;
            this.btnEliminarFilas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btnEliminarFilas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarFilas.ForeColor = System.Drawing.Color.White;
            this.btnEliminarFilas.Location = new System.Drawing.Point(559, 137);
            this.btnEliminarFilas.Margin = new System.Windows.Forms.Padding(4);
            this.btnEliminarFilas.Name = "btnEliminarFilas";
            this.btnEliminarFilas.Size = new System.Drawing.Size(138, 42);
            this.btnEliminarFilas.TabIndex = 52;
            this.btnEliminarFilas.Text = "Eliminar Fila/s";
            this.btnEliminarFilas.UseVisualStyleBackColor = false;
            this.btnEliminarFilas.Click += new System.EventHandler(this.btnEliminarFilas_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(622, 17);
            this.label4.TabIndex = 51;
            this.label4.Text = "Selecciona filas para asignarle proveedor - marca - familia ,  luego presionar ap" +
    "licar";
            // 
            // btnAplicar
            // 
            this.btnAplicar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAplicar.BackColor = System.Drawing.Color.Firebrick;
            this.btnAplicar.FlatAppearance.BorderColor = System.Drawing.Color.IndianRed;
            this.btnAplicar.FlatAppearance.CheckedBackColor = System.Drawing.Color.IndianRed;
            this.btnAplicar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.IndianRed;
            this.btnAplicar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btnAplicar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicar.ForeColor = System.Drawing.Color.White;
            this.btnAplicar.Location = new System.Drawing.Point(559, 56);
            this.btnAplicar.Margin = new System.Windows.Forms.Padding(4);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(138, 42);
            this.btnAplicar.TabIndex = 50;
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.UseVisualStyleBackColor = false;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // btnCancelarPMF
            // 
            this.btnCancelarPMF.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancelarPMF.BackColor = System.Drawing.Color.Firebrick;
            this.btnCancelarPMF.FlatAppearance.BorderColor = System.Drawing.Color.IndianRed;
            this.btnCancelarPMF.FlatAppearance.CheckedBackColor = System.Drawing.Color.IndianRed;
            this.btnCancelarPMF.FlatAppearance.MouseDownBackColor = System.Drawing.Color.IndianRed;
            this.btnCancelarPMF.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btnCancelarPMF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarPMF.ForeColor = System.Drawing.Color.White;
            this.btnCancelarPMF.Location = new System.Drawing.Point(705, 56);
            this.btnCancelarPMF.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelarPMF.Name = "btnCancelarPMF";
            this.btnCancelarPMF.Size = new System.Drawing.Size(138, 42);
            this.btnCancelarPMF.TabIndex = 49;
            this.btnCancelarPMF.Text = "Cancelar";
            this.btnCancelarPMF.UseVisualStyleBackColor = false;
            this.btnCancelarPMF.Click += new System.EventHandler(this.btnCancelarPMF_Click);
            // 
            // cbFamilia
            // 
            this.cbFamilia.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbFamilia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFamilia.FormattingEnabled = true;
            this.cbFamilia.Location = new System.Drawing.Point(135, 81);
            this.cbFamilia.Name = "cbFamilia";
            this.cbFamilia.Size = new System.Drawing.Size(405, 24);
            this.cbFamilia.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 17);
            this.label1.TabIndex = 48;
            this.label1.Text = "Familia";
            // 
            // cbMarca
            // 
            this.cbMarca.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbMarca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMarca.FormattingEnabled = true;
            this.cbMarca.Location = new System.Drawing.Point(135, 51);
            this.cbMarca.Name = "cbMarca";
            this.cbMarca.Size = new System.Drawing.Size(405, 24);
            this.cbMarca.TabIndex = 44;
            this.cbMarca.SelectionChangeCommitted += new System.EventHandler(this.cbMarca_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 17);
            this.label3.TabIndex = 45;
            this.label3.Text = "Marca";
            // 
            // dgvNovedades
            // 
            this.dgvNovedades.AllowUserToAddRows = false;
            this.dgvNovedades.AllowUserToDeleteRows = false;
            this.dgvNovedades.AllowUserToOrderColumns = true;
            this.dgvNovedades.AllowUserToResizeRows = false;
            this.dgvNovedades.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNovedades.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvNovedades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNovedades.Location = new System.Drawing.Point(6, 6);
            this.dgvNovedades.Name = "dgvNovedades";
            this.dgvNovedades.ReadOnly = true;
            this.dgvNovedades.RowHeadersWidth = 51;
            this.dgvNovedades.RowTemplate.Height = 24;
            this.dgvNovedades.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNovedades.Size = new System.Drawing.Size(873, 162);
            this.dgvNovedades.TabIndex = 18;
            // 
            // panelFooter
            // 
            this.panelFooter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFooter.Controls.Add(this.btnImportar);
            this.panelFooter.Controls.Add(this.btnSalir);
            this.panelFooter.Controls.Add(this.btnCancelarSeleccion);
            this.panelFooter.Location = new System.Drawing.Point(16, 708);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(917, 66);
            this.panelFooter.TabIndex = 39;
            // 
            // btnImportar
            // 
            this.btnImportar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnImportar.BackColor = System.Drawing.Color.Firebrick;
            this.btnImportar.FlatAppearance.BorderColor = System.Drawing.Color.IndianRed;
            this.btnImportar.FlatAppearance.CheckedBackColor = System.Drawing.Color.IndianRed;
            this.btnImportar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.IndianRed;
            this.btnImportar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btnImportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportar.ForeColor = System.Drawing.Color.White;
            this.btnImportar.Location = new System.Drawing.Point(570, 14);
            this.btnImportar.Margin = new System.Windows.Forms.Padding(4);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(107, 42);
            this.btnImportar.TabIndex = 33;
            this.btnImportar.Text = "Importar";
            this.btnImportar.UseVisualStyleBackColor = false;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSalir.BackColor = System.Drawing.Color.Firebrick;
            this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.IndianRed;
            this.btnSalir.FlatAppearance.CheckedBackColor = System.Drawing.Color.IndianRed;
            this.btnSalir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.IndianRed;
            this.btnSalir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(799, 15);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(107, 42);
            this.btnSalir.TabIndex = 32;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnCancelarSeleccion
            // 
            this.btnCancelarSeleccion.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancelarSeleccion.BackColor = System.Drawing.Color.Firebrick;
            this.btnCancelarSeleccion.FlatAppearance.BorderColor = System.Drawing.Color.IndianRed;
            this.btnCancelarSeleccion.FlatAppearance.CheckedBackColor = System.Drawing.Color.IndianRed;
            this.btnCancelarSeleccion.FlatAppearance.MouseDownBackColor = System.Drawing.Color.IndianRed;
            this.btnCancelarSeleccion.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btnCancelarSeleccion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarSeleccion.ForeColor = System.Drawing.Color.White;
            this.btnCancelarSeleccion.Location = new System.Drawing.Point(685, 15);
            this.btnCancelarSeleccion.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelarSeleccion.Name = "btnCancelarSeleccion";
            this.btnCancelarSeleccion.Size = new System.Drawing.Size(107, 42);
            this.btnCancelarSeleccion.TabIndex = 31;
            this.btnCancelarSeleccion.Text = "Cancelar";
            this.btnCancelarSeleccion.UseVisualStyleBackColor = false;
            this.btnCancelarSeleccion.Click += new System.EventHandler(this.btnCancelarSeleccion_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(16, 610);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(917, 92);
            this.panel1.TabIndex = 40;
            // 
            // frmImportarExcelProveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(942, 795);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelBody);
            this.Controls.Add(this.panelHeader);
            this.Name = "frmImportarExcelProveedor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Importar Excel";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmImportarExcelProveedor_FormClosing);
            this.Load += new System.EventHandler(this.frmImportarExcel_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage0.ResumeLayout(false);
            this.panelSeteoPMF.ResumeLayout(false);
            this.panelSeteoPMF.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNovedades)).EndInit();
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelBody;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage0;
        private System.Windows.Forms.Panel panelSeteoPMF;
        private System.Windows.Forms.Button btnEliminarFilas;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.Button btnCancelarPMF;
        private System.Windows.Forms.ComboBox cbFamilia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbMarca;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvNovedades;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.ComboBox cbProveedor;
        private System.Windows.Forms.Label lblProveedor;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtArchivoExcel;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnCancelarSeleccion;
        private System.Windows.Forms.Label lblInfoBody;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCaptionExcludeRows_2;
    }
}