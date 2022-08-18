using Modulo_Administracion.Clases.Custom;
using System;
using System.Data;
using System.Windows.Forms;

namespace Modulo_Administracion.Logica
{
    public static class Logica_Tipo_Factura
    {

        public static void loadComboBox_cbTipoFactura_relacionado_a_FACTURA(ComboBox comboBox)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("cod_tipo_factura");
                dt.Columns.Add("txt_desc");


                DataRow row = dt.NewRow();
                row["cod_tipo_factura"] = ttipo_factura_constantes.i_valor_remito;
                row["txt_desc"] = ttipo_factura_constantes.s_valor_remito;
                dt.Rows.Add(row);

                row = dt.NewRow();
                row["cod_tipo_factura"] = ttipo_factura_constantes.i_valor_nota_credito;
                row["txt_desc"] = ttipo_factura_constantes.s_valor_nota_credito;
                dt.Rows.Add(row);

                row = dt.NewRow();
                row["cod_tipo_factura"] = ttipo_factura_constantes.i_valor_nota_debito;
                row["txt_desc"] = ttipo_factura_constantes.s_valor_nota_debito;
                dt.Rows.Add(row);

                comboBox.DataSource = dt;
                comboBox.DisplayMember = "txt_desc";
                comboBox.ValueMember = "cod_tipo_factura";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string[] loadComboBox_cbTipoFactura_relacionado_a_CCC(bool sn_agregar_movimiento)
        {
            try
            {
                if (sn_agregar_movimiento == false) //SI VENGO A BUSCAR LOS DATOS PERO NO DESDE EL "AGREGAR MOVIMIENTO" , CARGO TODO
                {
                    string[] dataComboBox = {
                                                ttipo_factura_constantes.s_valor_remito,
                                                ttipo_factura_constantes.s_valor_nota_credito,
                                                ttipo_factura_constantes.s_valor_nota_debito,
                                                ttipo_factura_constantes.s_valor_factura_a_sin_comprobante,
                                                ttipo_factura_constantes.s_valor_factura_b_sin_comprobante,
                                                ttipo_factura_constantes.s_valor_remito_sin_comprobante,
                                                ttipo_factura_constantes. s_valor_nota_credito_sin_comprobante,
                                                ttipo_factura_constantes.s_valor_nota_debito_sin_comprobante
                                            };
                    return dataComboBox;
                }
                else //VENGO A BUSCAR LOS DATOS DESDE "AGREGAR MOVIMIENTO" , ENTONCES SOLAMENTE VOY A PODER GENERAR LINEAS SIN COMPROBANTES FISICOS
                {
                    string[] dataComboBox = {
                                                ttipo_factura_constantes.s_valor_factura_a_sin_comprobante,
                                                ttipo_factura_constantes.s_valor_factura_b_sin_comprobante,
                                                ttipo_factura_constantes.s_valor_remito_sin_comprobante,
                                                ttipo_factura_constantes.s_valor_nota_credito_sin_comprobante,
                                                ttipo_factura_constantes.s_valor_nota_debito_sin_comprobante
                                            };
                    return dataComboBox;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
