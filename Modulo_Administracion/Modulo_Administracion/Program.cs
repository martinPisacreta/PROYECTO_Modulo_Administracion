using Modulo_Administracion.Vista;
using System;
using System.Configuration;
using System.Net;
using System.Windows.Forms;

namespace Modulo_Administracion
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        public const int Inicio = 0;
        public const int Alta = 1;
        public const int Modif = 2;
        public const int nro_copias = 2;
        public const string password = "123456";

        public static string ruta_guardar_factura_pdf;
        public static string ruta_guardar_reporte_cliente_cuenta_corriente;
        [STAThread]



        static void Main()
        {


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SetConnectionStringSettings();
            Application.Run(new frmMain());
        }

        static void SetConnectionStringSettings()
        {

            //DESKTOP - I7NMOJE
            //MAXI - PC\SQLEXPRESS->MAXI - PC

            string namePc = Dns.GetHostName();

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings.Remove("Modulo_AdministracionContext");
            config.ConnectionStrings.ConnectionStrings.Remove("Modulo_Administracion.Properties.Settings.reporteFacturaConnectionString");
            config.ConnectionStrings.ConnectionStrings.Remove("LocalSqlServer");
            if (namePc.Contains("I7NMOJE"))
            {
                config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("Modulo_AdministracionContext", @"data source=DESKTOP-I7NMOJE\SQLEXPRESS;initial catalog=CarritoCompras;user id=sa;password=123456;MultipleActiveResultSets=True;App=EntityFramework", "System.Data.SqlClient"));
                config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("Modulo_Administracion.Properties.Settings.reporteFacturaConnectionString", @"Data Source=DESKTOP-I7NMOJE\SQLEXPRESS;Initial Catalog=CarritoCompras;Integrated Security=True", "System.Data.SqlClient"));


                ruta_guardar_factura_pdf = @"C:\Users\Martin\Desktop\";
                ruta_guardar_reporte_cliente_cuenta_corriente = @"C:\Users\Martin\Desktop\REPORTE CLIENTE CUENTA CORRIENTE\";
            }
            else
            {
                config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("Modulo_AdministracionContext", @"data source=MAXI-PC\SQLEXPRESS;initial catalog=CarritoCompras;user id=sa;password=123456;MultipleActiveResultSets=True;App=EntityFramework", "System.Data.SqlClient"));
                config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("Modulo_Administracion.Properties.Settings.reporteFacturaConnectionString", @"Data Source=MAXI-PC\SQLEXPRESS;Initial Catalog=CarritoCompras;Integrated Security=True", "System.Data.SqlClient"));


                ruta_guardar_factura_pdf = @"\\MAXI-PC\Users\Windows 10\Desktop\Compartida\FACTURAS DEL DIA\";
                ruta_guardar_reporte_cliente_cuenta_corriente = @"\\MAXI-PC\Users\Windows 10\Desktop\Compartida\REPORTE CLIENTE CUENTA CORRIENTE\";

               
            }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");
        }
    }
}
