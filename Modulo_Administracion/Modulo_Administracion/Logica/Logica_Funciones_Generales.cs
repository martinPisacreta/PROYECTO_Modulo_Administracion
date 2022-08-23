using Microsoft.VisualBasic;
using Modulo_Administracion.Clases;
using Modulo_Administracion.Reporte.RDLC.reporteFactura;
using Modulo_Administracion.Vista;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Modulo_Administracion.Logica
{
    static class Logica_Funciones_Generales
    {
        public static DataSet cargar_comboBox(string sTablas, ComboBox cControl, string sNomCamp, string sWhere, string sOrderBy, string sValueMember)
        {

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Modulo_AdministracionContext"].ConnectionString))
            {
                connection.Open();
                using (SqlTransaction sqlTransaction = connection.BeginTransaction())
                {

                    try
                    {
                        ComboBox rb = cControl;
                        DataSet ds = new DataSet();

                        //store
                        SqlCommand command = new SqlCommand("cargar_combo_box", connection, sqlTransaction);

                        //parametros
                        command.Parameters.AddWithValue("@Tabla", sTablas);
                        command.Parameters.AddWithValue("@WHERE", sWhere);
                        command.Parameters.AddWithValue("@ORDERBY", sOrderBy);

                        //tiempo y tipo
                        command.CommandTimeout = 0;
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command;
                        adapter.Fill(ds);

                        sqlTransaction.Commit();
                        rb.DataSource = ds.Tables[0];
                        rb.DisplayMember = sNomCamp;
                        rb.ValueMember = sValueMember;

                        return ds;

                    }
                    catch (Exception ex)
                    {
                        sqlTransaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public static bool validar_cuit(string cuit)
        {

            try
            {


                if (string.IsNullOrEmpty(cuit)) throw new ArgumentNullException(nameof(cuit));
                if (cuit.Length != 13) throw new ArgumentException(nameof(cuit));
                bool rv = false;
                int verificador;
                int resultado = 0;
                string cuit_nro = cuit.Replace("-", string.Empty);
                string codes = "6789456789";
                long cuit_long = 0;
                if (long.TryParse(cuit_nro, out cuit_long))
                {
                    verificador = int.Parse(cuit_nro[cuit_nro.Length - 1].ToString());
                    int x = 0;
                    while (x < 10)
                    {

                        int digitoValidador = int.Parse(codes.Substring((x), 1));
                        int digito = int.Parse(cuit_nro.Substring((x), 1));
                        int digitoValidacion = digitoValidador * digito;
                        resultado += digitoValidacion;
                        x++;
                    }
                    resultado = resultado % 11;
                    rv = (resultado == verificador);
                }
                return rv;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static string crear_path_a_partir_de_factura(factura factura)
        {
            string FilePath = "";
            cliente cliente = null;

            try
            {
                string anio = factura.fecha.ToString("yyyy", CultureInfo.CreateSpecificCulture("es"));
                if (anio == "2020")
                {
                    FilePath = Program.ruta_guardar_factura_pdf + "AÑO 2020";
                }
                else
                {
                    FilePath = Program.ruta_guardar_factura_pdf + anio;
                    if (!Directory.Exists(FilePath))
                    {
                        Directory.CreateDirectory(FilePath);
                    }
                }

                string mes_en_letras = factura.fecha.ToString("MMMM", CultureInfo.CreateSpecificCulture("es"));
                string mes_en_nro = factura.fecha.ToString("MM", CultureInfo.CreateSpecificCulture("es"));
                FilePath = FilePath + @"\" + mes_en_letras;
                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                }

                string dia = factura.fecha.ToString("dd", CultureInfo.CreateSpecificCulture("es"));
                FilePath = FilePath + @"\" + dia + "-" + mes_en_nro + "-" + anio;
                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                }

                cliente = Logica_Cliente.buscar_cliente(factura.id_cliente, null);
                FilePath = FilePath + "\\" + cliente.nombre_fantasia + " - " + factura.ttipo_factura.letra + factura.nro_factura.ToString() + ".pdf";

                return FilePath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static string generar_reporteFactura(factura factura)
        {

            try
            {
                DataSet ds = null;
             

                //mato los procesos que sean igual a FOXITREADER
                var resultado = from item in System.Diagnostics.Process.GetProcesses()
                                where item.ProcessName.ToUpper() == "FOXITREADER"
                                select item;

                foreach (var item in resultado)
                {
                    item.Kill();
                }


             
                //genero 
                ds = Logica_Factura.buscar_factura_print(factura.id_factura);

                //llamo al formulario pero no es necesario que se abra porque solamente quiero generar el PDF
                frmReporteFactura form = new frmReporteFactura(ds.Tables[0], factura.path_factura);
                return factura.path_factura;
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void generar_reporteClienteCuentaCorriente(DataTable dt, string path)
        {

            try
            {
                //si ya existe no hago nada , sino lo agrego
                

                //mato los procesos que sean igual a FOXITREADER
                var resultado = from item in System.Diagnostics.Process.GetProcesses()
                                where item.ProcessName.ToUpper() == "FOXITREADER"
                                select item;

                foreach (var item in resultado)
                {
                    item.Kill();
                }

                //genero 
                //llamo al formulario pero no es necesario que se abra porque solamente quiero generar el PDF
                frmReporteClienteCuentaCorriente form = new frmReporteClienteCuentaCorriente(dt, path);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static bool mandar_a_imprimir(string ruta, string namePrinter, short nro_copias)
        {
            bool bandera = false;
            try
            {


                for (int i = 1; i <= nro_copias; i++)
                {

                    System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(ruta);
                    info.Arguments = "\"" + namePrinter + "\"";
                    info.CreateNoWindow = true;
                    info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    info.UseShellExecute = true;
                    info.Verb = "PrintTo";
                    System.Diagnostics.Process.Start(info);
                }





                bandera = true;
                return bandera;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string validar_fecha(string dtfecha)
        {
            // Tiene que venir la fecha con por lo menos el día y algún separador
            // (blancos, puntos, etc.) Si recibe una fecha con el mes mayor que 12,
            // asume que es el mes y lo da vuelta con el día
            // Devuelve la fecha con barras o ""
            string ValidarFecha = "";
            try
            {

                int iLongFecha;
                int iContador;
                string sFecha;
                string sAnio;
                int position;
                sFecha = "";
                iLongFecha = Strings.Len(dtfecha);
                if (iLongFecha == 0)
                {
                    ValidarFecha = "";
                    return ValidarFecha;
                }
                iContador = 1;
                while (iContador <= iLongFecha)
                {
                    if (Information.IsNumeric(Strings.Mid(dtfecha, iContador, 1)))
                        sFecha = sFecha + Strings.Mid(dtfecha, iContador, 1);
                    else
                        sFecha = sFecha + "/";
                    iContador = iContador + 1;
                }
                position = Strings.InStr(1, sFecha, "/");
                if (position == 0)
                    sFecha = sFecha + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
                else
                {
                    position = Strings.InStr(position + 1, sFecha, "/");
                    if (position == 0)
                    {
                        sFecha = sFecha + "/" + DateTime.Now.ToString("yyyy");
                        position = Strings.Len(sFecha);
                    }
                    else
                    {
                        if (position + 1 > Strings.Len(sFecha))
                            sAnio = "";
                        else
                            sAnio = Strings.Mid(sFecha, position + 1, Strings.Len(sFecha) - position + 1);
                        if (sAnio == "")
                            sFecha = Strings.Left(sFecha, position) + DateTime.Now.ToString("yyyy");
                    }
                }
                if (!Information.IsNumeric(sFecha))
                    sFecha = Strings.Format(Convert.ToDateTime(sFecha), "dd/MM/yyyy"); // Format(sFecha, "c")
                if (!(Information.IsDate(sFecha)))
                    ValidarFecha = "";
                else
                    ValidarFecha = sFecha;

                return ValidarFecha;
            }
            catch
            {
                ValidarFecha = "";
                return ValidarFecha;
            }
        }

        public static bool email_bien_escrito(string email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static decimal monto_mas_menos_un_porcentaje(int porcentaje, decimal monto, int operador)
        {
            decimal aumento_descuento;
            decimal monto_final = 0.0000M;
            try
            {
                if (operador == 1) //suma
                {
                    aumento_descuento = (porcentaje * monto) / 100;
                    monto_final = (monto + aumento_descuento);
                }
                if (operador == 2) //resta
                {
                    aumento_descuento = (porcentaje * monto) / 100;
                    monto_final = (monto - aumento_descuento);
                }
                return monto_final;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}