﻿using Microsoft.Reporting.WinForms;
using Modulo_Administracion.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modulo_Administracion.Vista
{
    public partial class frmReporteClienteCuentaCorriente : Form
    {
        private DataTable dt;
        private string path_factura;
        public frmReporteClienteCuentaCorriente(DataTable _dt,string _path_factura)
        {
            try
            {
                InitializeComponent();
                dt = _dt;
                path_factura = _path_factura;

                ReportDataSource datasource = new ReportDataSource("reporteClienteCuentaCorrienteDS", dt);
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(datasource);

                this.reportViewer1.RefreshReport();


                //EXPORT TO PDF
                var deviceInfo = @"<DeviceInfo>
                            <EmbedFonts>None</EmbedFonts>
                            </DeviceInfo>";

                byte[] Bytes = this.reportViewer1.LocalReport.Render(format: "PDF", deviceInfo: deviceInfo);
                using (FileStream stream = new FileStream(path_factura, FileMode.Create))
                {
                    stream.Write(Bytes, 0, Bytes.Length);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
          
        }

        private void frmReporteFactura_Load(object sender, EventArgs e)
        {

           

        }
    }
}
