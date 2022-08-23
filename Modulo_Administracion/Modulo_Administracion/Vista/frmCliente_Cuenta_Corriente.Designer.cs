namespace Modulo_Administracion.Vista
{
    partial class frmCliente_Cuenta_Corriente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
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
            this.panelPrincipal = new System.Windows.Forms.Panel();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.lblSaldo = new System.Windows.Forms.Label();
            this.lblSaldoLegend = new System.Windows.Forms.Label();
            this.lblPago4 = new System.Windows.Forms.Label();
            this.lblPago4Legend = new System.Windows.Forms.Label();
            this.lblPago3 = new System.Windows.Forms.Label();
            this.lblPago3Legend = new System.Windows.Forms.Label();
            this.lblPago2 = new System.Windows.Forms.Label();
            this.lblPago2Legend = new System.Windows.Forms.Label();
            this.lblPago1 = new System.Windows.Forms.Label();
            this.lblPago1Legend = new System.Windows.Forms.Label();
            this.lblImpFactura = new System.Windows.Forms.Label();
            this.lblImpFacturaLegend = new System.Windows.Forms.Label();
            this.dgvClienteCuentaCorriente = new System.Windows.Forms.DataGridView();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.panelVer = new System.Windows.Forms.Panel();
            this.rdTodos = new System.Windows.Forms.RadioButton();
            this.rdDeuda = new System.Windows.Forms.RadioButton();
            this.btnEliminarFila = new System.Windows.Forms.Button();
            this.btnAgregarMovimiento = new System.Windows.Forms.Button();
            this.btnGenerarReporte = new System.Windows.Forms.Button();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.panelPrincipal.SuspendLayout();
            this.panelSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClienteCuentaCorriente)).BeginInit();
            this.panelFooter.SuspendLayout();
            this.panelVer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelPrincipal
            // 
            this.panelPrincipal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPrincipal.Controls.Add(this.panelSummary);
            this.panelPrincipal.Controls.Add(this.dgvClienteCuentaCorriente);
            this.panelPrincipal.Controls.Add(this.panelFooter);
            this.panelPrincipal.Location = new System.Drawing.Point(12, 3);
            this.panelPrincipal.Name = "panelPrincipal";
            this.panelPrincipal.Size = new System.Drawing.Size(1091, 553);
            this.panelPrincipal.TabIndex = 83;
            // 
            // panelSummary
            // 
            this.panelSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSummary.Controls.Add(this.lblSaldo);
            this.panelSummary.Controls.Add(this.lblSaldoLegend);
            this.panelSummary.Controls.Add(this.lblPago4);
            this.panelSummary.Controls.Add(this.lblPago4Legend);
            this.panelSummary.Controls.Add(this.lblPago3);
            this.panelSummary.Controls.Add(this.lblPago3Legend);
            this.panelSummary.Controls.Add(this.lblPago2);
            this.panelSummary.Controls.Add(this.lblPago2Legend);
            this.panelSummary.Controls.Add(this.lblPago1);
            this.panelSummary.Controls.Add(this.lblPago1Legend);
            this.panelSummary.Controls.Add(this.lblImpFactura);
            this.panelSummary.Controls.Add(this.lblImpFacturaLegend);
            this.panelSummary.Location = new System.Drawing.Point(13, 378);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Size = new System.Drawing.Size(1065, 83);
            this.panelSummary.TabIndex = 104;
            // 
            // lblSaldo
            // 
            this.lblSaldo.AutoSize = true;
            this.lblSaldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaldo.ForeColor = System.Drawing.Color.Red;
            this.lblSaldo.Location = new System.Drawing.Point(938, 44);
            this.lblSaldo.Name = "lblSaldo";
            this.lblSaldo.Size = new System.Drawing.Size(66, 17);
            this.lblSaldo.TabIndex = 113;
            this.lblSaldo.Text = "lblSaldo";
            // 
            // lblSaldoLegend
            // 
            this.lblSaldoLegend.AutoSize = true;
            this.lblSaldoLegend.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaldoLegend.Location = new System.Drawing.Point(938, 17);
            this.lblSaldoLegend.Name = "lblSaldoLegend";
            this.lblSaldoLegend.Size = new System.Drawing.Size(49, 17);
            this.lblSaldoLegend.TabIndex = 112;
            this.lblSaldoLegend.Text = "Saldo";
            // 
            // lblPago4
            // 
            this.lblPago4.AutoSize = true;
            this.lblPago4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPago4.ForeColor = System.Drawing.Color.Red;
            this.lblPago4.Location = new System.Drawing.Point(792, 44);
            this.lblPago4.Name = "lblPago4";
            this.lblPago4.Size = new System.Drawing.Size(71, 17);
            this.lblPago4.TabIndex = 111;
            this.lblPago4.Text = "lblPago4";
            // 
            // lblPago4Legend
            // 
            this.lblPago4Legend.AutoSize = true;
            this.lblPago4Legend.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPago4Legend.Location = new System.Drawing.Point(792, 17);
            this.lblPago4Legend.Name = "lblPago4Legend";
            this.lblPago4Legend.Size = new System.Drawing.Size(59, 17);
            this.lblPago4Legend.TabIndex = 110;
            this.lblPago4Legend.Text = "Pago 4";
            // 
            // lblPago3
            // 
            this.lblPago3.AutoSize = true;
            this.lblPago3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPago3.ForeColor = System.Drawing.Color.Red;
            this.lblPago3.Location = new System.Drawing.Point(646, 44);
            this.lblPago3.Name = "lblPago3";
            this.lblPago3.Size = new System.Drawing.Size(71, 17);
            this.lblPago3.TabIndex = 109;
            this.lblPago3.Text = "lblPago3";
            // 
            // lblPago3Legend
            // 
            this.lblPago3Legend.AutoSize = true;
            this.lblPago3Legend.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPago3Legend.Location = new System.Drawing.Point(646, 17);
            this.lblPago3Legend.Name = "lblPago3Legend";
            this.lblPago3Legend.Size = new System.Drawing.Size(59, 17);
            this.lblPago3Legend.TabIndex = 108;
            this.lblPago3Legend.Text = "Pago 3";
            // 
            // lblPago2
            // 
            this.lblPago2.AutoSize = true;
            this.lblPago2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPago2.ForeColor = System.Drawing.Color.Red;
            this.lblPago2.Location = new System.Drawing.Point(472, 44);
            this.lblPago2.Name = "lblPago2";
            this.lblPago2.Size = new System.Drawing.Size(71, 17);
            this.lblPago2.TabIndex = 107;
            this.lblPago2.Text = "lblPago2";
            // 
            // lblPago2Legend
            // 
            this.lblPago2Legend.AutoSize = true;
            this.lblPago2Legend.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPago2Legend.Location = new System.Drawing.Point(472, 17);
            this.lblPago2Legend.Name = "lblPago2Legend";
            this.lblPago2Legend.Size = new System.Drawing.Size(59, 17);
            this.lblPago2Legend.TabIndex = 106;
            this.lblPago2Legend.Text = "Pago 2";
            // 
            // lblPago1
            // 
            this.lblPago1.AutoSize = true;
            this.lblPago1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPago1.ForeColor = System.Drawing.Color.Red;
            this.lblPago1.Location = new System.Drawing.Point(259, 44);
            this.lblPago1.Name = "lblPago1";
            this.lblPago1.Size = new System.Drawing.Size(71, 17);
            this.lblPago1.TabIndex = 105;
            this.lblPago1.Text = "lblPago1";
            // 
            // lblPago1Legend
            // 
            this.lblPago1Legend.AutoSize = true;
            this.lblPago1Legend.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPago1Legend.Location = new System.Drawing.Point(259, 17);
            this.lblPago1Legend.Name = "lblPago1Legend";
            this.lblPago1Legend.Size = new System.Drawing.Size(59, 17);
            this.lblPago1Legend.TabIndex = 104;
            this.lblPago1Legend.Text = "Pago 1";
            // 
            // lblImpFactura
            // 
            this.lblImpFactura.AutoSize = true;
            this.lblImpFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImpFactura.ForeColor = System.Drawing.Color.Red;
            this.lblImpFactura.Location = new System.Drawing.Point(15, 44);
            this.lblImpFactura.Name = "lblImpFactura";
            this.lblImpFactura.Size = new System.Drawing.Size(105, 17);
            this.lblImpFactura.TabIndex = 103;
            this.lblImpFactura.Text = "lblImpFactura";
            // 
            // lblImpFacturaLegend
            // 
            this.lblImpFacturaLegend.AutoSize = true;
            this.lblImpFacturaLegend.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImpFacturaLegend.Location = new System.Drawing.Point(15, 17);
            this.lblImpFacturaLegend.Name = "lblImpFacturaLegend";
            this.lblImpFacturaLegend.Size = new System.Drawing.Size(98, 17);
            this.lblImpFacturaLegend.TabIndex = 102;
            this.lblImpFacturaLegend.Text = "Imp. Factura";
            // 
            // dgvClienteCuentaCorriente
            // 
            this.dgvClienteCuentaCorriente.AllowUserToAddRows = false;
            this.dgvClienteCuentaCorriente.AllowUserToResizeRows = false;
            this.dgvClienteCuentaCorriente.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvClienteCuentaCorriente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClienteCuentaCorriente.Location = new System.Drawing.Point(13, 9);
            this.dgvClienteCuentaCorriente.MultiSelect = false;
            this.dgvClienteCuentaCorriente.Name = "dgvClienteCuentaCorriente";
            this.dgvClienteCuentaCorriente.RowHeadersWidth = 51;
            this.dgvClienteCuentaCorriente.RowTemplate.Height = 24;
            this.dgvClienteCuentaCorriente.Size = new System.Drawing.Size(1065, 363);
            this.dgvClienteCuentaCorriente.TabIndex = 89;
            this.dgvClienteCuentaCorriente.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvClienteCuentaCorriente_CellBeginEdit);
            this.dgvClienteCuentaCorriente.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClienteCuentaCorriente_CellEndEdit);
            this.dgvClienteCuentaCorriente.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvClienteCuentaCorriente_EditingControlShowing);
            // 
            // panelFooter
            // 
            this.panelFooter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFooter.Controls.Add(this.panelVer);
            this.panelFooter.Controls.Add(this.btnEliminarFila);
            this.panelFooter.Controls.Add(this.btnAgregarMovimiento);
            this.panelFooter.Controls.Add(this.btnGenerarReporte);
            this.panelFooter.Controls.Add(this.btnGrabar);
            this.panelFooter.Controls.Add(this.btnSalir);
            this.panelFooter.Location = new System.Drawing.Point(13, 467);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1065, 83);
            this.panelFooter.TabIndex = 88;
            // 
            // panelVer
            // 
            this.panelVer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelVer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelVer.Controls.Add(this.rdTodos);
            this.panelVer.Controls.Add(this.rdDeuda);
            this.panelVer.Location = new System.Drawing.Point(405, 9);
            this.panelVer.Name = "panelVer";
            this.panelVer.Size = new System.Drawing.Size(227, 55);
            this.panelVer.TabIndex = 128;
            // 
            // rdTodos
            // 
            this.rdTodos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rdTodos.AutoSize = true;
            this.rdTodos.Location = new System.Drawing.Point(135, 20);
            this.rdTodos.Name = "rdTodos";
            this.rdTodos.Size = new System.Drawing.Size(77, 21);
            this.rdTodos.TabIndex = 129;
            this.rdTodos.TabStop = true;
            this.rdTodos.Text = "TODAS";
            this.rdTodos.UseVisualStyleBackColor = true;
            this.rdTodos.Click += new System.EventHandler(this.rdTodos_Click);
            // 
            // rdDeuda
            // 
            this.rdDeuda.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rdDeuda.AutoSize = true;
            this.rdDeuda.Location = new System.Drawing.Point(15, 20);
            this.rdDeuda.Name = "rdDeuda";
            this.rdDeuda.Size = new System.Drawing.Size(114, 21);
            this.rdDeuda.TabIndex = 128;
            this.rdDeuda.TabStop = true;
            this.rdDeuda.Text = "ADEUDADAS";
            this.rdDeuda.UseVisualStyleBackColor = true;
            this.rdDeuda.Click += new System.EventHandler(this.rdDeuda_Click);
            // 
            // btnEliminarFila
            // 
            this.btnEliminarFila.BackColor = System.Drawing.Color.OrangeRed;
            this.btnEliminarFila.FlatAppearance.BorderColor = System.Drawing.Color.Orange;
            this.btnEliminarFila.FlatAppearance.CheckedBackColor = System.Drawing.Color.Orange;
            this.btnEliminarFila.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Orange;
            this.btnEliminarFila.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.btnEliminarFila.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarFila.ForeColor = System.Drawing.Color.White;
            this.btnEliminarFila.Location = new System.Drawing.Point(164, 9);
            this.btnEliminarFila.Margin = new System.Windows.Forms.Padding(4);
            this.btnEliminarFila.Name = "btnEliminarFila";
            this.btnEliminarFila.Size = new System.Drawing.Size(154, 42);
            this.btnEliminarFila.TabIndex = 125;
            this.btnEliminarFila.Text = "Eliminar Movimiento";
            this.btnEliminarFila.UseVisualStyleBackColor = false;
            this.btnEliminarFila.Click += new System.EventHandler(this.btnEliminarMovimiento_Click);
            // 
            // btnAgregarMovimiento
            // 
            this.btnAgregarMovimiento.BackColor = System.Drawing.Color.OrangeRed;
            this.btnAgregarMovimiento.FlatAppearance.BorderColor = System.Drawing.Color.Orange;
            this.btnAgregarMovimiento.FlatAppearance.CheckedBackColor = System.Drawing.Color.Orange;
            this.btnAgregarMovimiento.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Orange;
            this.btnAgregarMovimiento.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.btnAgregarMovimiento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarMovimiento.ForeColor = System.Drawing.Color.White;
            this.btnAgregarMovimiento.Location = new System.Drawing.Point(4, 9);
            this.btnAgregarMovimiento.Margin = new System.Windows.Forms.Padding(4);
            this.btnAgregarMovimiento.Name = "btnAgregarMovimiento";
            this.btnAgregarMovimiento.Size = new System.Drawing.Size(152, 42);
            this.btnAgregarMovimiento.TabIndex = 124;
            this.btnAgregarMovimiento.Text = "Agregar Movimiento";
            this.btnAgregarMovimiento.UseVisualStyleBackColor = false;
            this.btnAgregarMovimiento.Click += new System.EventHandler(this.btnAgregarMovimiento_Click);
            // 
            // btnGenerarReporte
            // 
            this.btnGenerarReporte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerarReporte.BackColor = System.Drawing.Color.Firebrick;
            this.btnGenerarReporte.FlatAppearance.BorderColor = System.Drawing.Color.IndianRed;
            this.btnGenerarReporte.FlatAppearance.CheckedBackColor = System.Drawing.Color.IndianRed;
            this.btnGenerarReporte.FlatAppearance.MouseDownBackColor = System.Drawing.Color.IndianRed;
            this.btnGenerarReporte.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btnGenerarReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarReporte.ForeColor = System.Drawing.Color.White;
            this.btnGenerarReporte.Location = new System.Drawing.Point(649, 9);
            this.btnGenerarReporte.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerarReporte.Name = "btnGenerarReporte";
            this.btnGenerarReporte.Size = new System.Drawing.Size(138, 42);
            this.btnGenerarReporte.TabIndex = 122;
            this.btnGenerarReporte.Text = "Generar Reporte";
            this.btnGenerarReporte.UseVisualStyleBackColor = false;
            this.btnGenerarReporte.Click += new System.EventHandler(this.btnGenerarReporte_Click);
            // 
            // btnGrabar
            // 
            this.btnGrabar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrabar.BackColor = System.Drawing.Color.Firebrick;
            this.btnGrabar.FlatAppearance.BorderColor = System.Drawing.Color.IndianRed;
            this.btnGrabar.FlatAppearance.CheckedBackColor = System.Drawing.Color.IndianRed;
            this.btnGrabar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.IndianRed;
            this.btnGrabar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btnGrabar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrabar.ForeColor = System.Drawing.Color.White;
            this.btnGrabar.Location = new System.Drawing.Point(795, 9);
            this.btnGrabar.Margin = new System.Windows.Forms.Padding(4);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(138, 42);
            this.btnGrabar.TabIndex = 85;
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.UseVisualStyleBackColor = false;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.BackColor = System.Drawing.Color.Firebrick;
            this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.IndianRed;
            this.btnSalir.FlatAppearance.CheckedBackColor = System.Drawing.Color.IndianRed;
            this.btnSalir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.IndianRed;
            this.btnSalir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(941, 9);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(4);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(111, 42);
            this.btnSalir.TabIndex = 84;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // frmCliente_Cuenta_Corriente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 568);
            this.Controls.Add(this.panelPrincipal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmCliente_Cuenta_Corriente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCliente_Cuenta_Corriente";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelPrincipal.ResumeLayout(false);
            this.panelSummary.ResumeLayout(false);
            this.panelSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClienteCuentaCorriente)).EndInit();
            this.panelFooter.ResumeLayout(false);
            this.panelVer.ResumeLayout(false);
            this.panelVer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelPrincipal;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.Button btnGenerarReporte;
        private System.Windows.Forms.Button btnAgregarMovimiento;
        private System.Windows.Forms.Button btnEliminarFila;
        private System.Windows.Forms.Panel panelVer;
        private System.Windows.Forms.RadioButton rdTodos;
        private System.Windows.Forms.RadioButton rdDeuda;
        private System.Windows.Forms.Panel panelSummary;
        private System.Windows.Forms.Label lblSaldo;
        private System.Windows.Forms.Label lblSaldoLegend;
        private System.Windows.Forms.Label lblPago4;
        private System.Windows.Forms.Label lblPago4Legend;
        private System.Windows.Forms.Label lblPago3;
        private System.Windows.Forms.Label lblPago3Legend;
        private System.Windows.Forms.Label lblPago2;
        private System.Windows.Forms.Label lblPago2Legend;
        private System.Windows.Forms.Label lblPago1;
        private System.Windows.Forms.Label lblPago1Legend;
        private System.Windows.Forms.Label lblImpFactura;
        private System.Windows.Forms.Label lblImpFacturaLegend;
        private System.Windows.Forms.DataGridView dgvClienteCuentaCorriente;
    }
}