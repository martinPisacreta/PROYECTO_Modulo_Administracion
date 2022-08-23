using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Modulo_Administracion.Reporte.RDLC.reporteFactura
{
    public partial class frmReporteFactura : Form
    {
        private DataTable dt;
        private string path_factura;
        public frmReporteFactura(DataTable _dt, string _path_factura)
        {
            try
            {
                InitializeComponent();
                dt = _dt;
                path_factura = _path_factura;

                ReportDataSource datasource = new ReportDataSource("reporteFacturaDS", dt);
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
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void frmReporteFactura_Load(object sender, EventArgs e)
        {



        }
    }
}
