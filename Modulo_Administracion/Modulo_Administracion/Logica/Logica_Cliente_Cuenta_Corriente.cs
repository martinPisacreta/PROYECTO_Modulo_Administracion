using Modulo_Administracion.Clases;
using Modulo_Administracion.Clases.Custom;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace Modulo_Administracion.Logica
{
    static class Logica_Cliente_Cuenta_Corriente
    {



        public static bool alta_movimiento_CCC(factura factura, Modulo_AdministracionContext db) // esta funcion se relaciona 100% al dar de alta una factura en el sistema
        {

            bool bandera = false;
            cliente_cuenta_corriente cliente_cuenta_corriente_a_insertar;
            try
            {
                if (factura.cod_tipo_factura == ttipo_factura_constantes.i_valor_remito) //a pedido de maxi , solamente el remito se da de alta en cuenta corriente al generar una factura
                {

                    cliente_cuenta_corriente_a_insertar = new cliente_cuenta_corriente();
                    cliente_cuenta_corriente_a_insertar.id_cliente = factura.id_cliente;
                    cliente_cuenta_corriente_a_insertar.id_factura = factura.id_factura;




                    //el imp_factura es segun la condicion de factura de la factura
                    if (factura.id_condicion_factura == 1)
                    {
                        cliente_cuenta_corriente_a_insertar.imp_factura = factura.precio_final_con_pago_mayor_a_30_dias;
                    }
                    else if (factura.id_condicion_factura == 2)
                    {
                        cliente_cuenta_corriente_a_insertar.imp_factura = factura.precio_final_con_pago_menor_a_30_dias;
                    }
                    else if (factura.id_condicion_factura == 3)
                    {
                        cliente_cuenta_corriente_a_insertar.imp_factura = factura.precio_final_con_pago_menor_a_7_dias;
                    }

                    db.cliente_cuenta_corriente.Add(cliente_cuenta_corriente_a_insertar);
                    db.SaveChanges();
                }

                bandera = true;
                return bandera;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public static bool eliminar_movimiento_CCC(int id_cliente_cuenta_corriente)
        {
            bool bandera = false;
            Modulo_AdministracionContext db = new Modulo_AdministracionContext();
            using (DbContextTransaction dbContextTransaction = db.Database.BeginTransaction())
            {

                try
                {
                    cliente_cuenta_corriente cliente_cuenta_corriente_db = db.cliente_cuenta_corriente.FirstOrDefault(ccc => ccc.id_cliente_cuenta_corriente == id_cliente_cuenta_corriente);

                    db.cliente_cuenta_corriente.Remove(cliente_cuenta_corriente_db);
                    db.SaveChanges();



                    dbContextTransaction.Commit();

                    bandera = true;

                    return bandera;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }
                finally
                {
                    db = null;
                }
            }
        }


        public static bool modificar_movimientos_CCC(List<cliente_cuenta_corriente> lista_cliente_cuenta_corriente)
        {
            bool bandera = false;
            Modulo_AdministracionContext db = new Modulo_AdministracionContext();
            using (DbContextTransaction dbContextTransaction = db.Database.BeginTransaction())
            {

                try
                {
                    foreach (cliente_cuenta_corriente ccc in lista_cliente_cuenta_corriente)
                    {
                        if (modificar_movimiento_CCC(ccc, db) == false)
                        {
                            throw new Exception("Error al actualizar cuenta corriente");
                        }
                    }



                    dbContextTransaction.Commit();

                    bandera = true;

                    return bandera;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }
                finally
                {
                    db = null;
                }
            }
        }




        public static bool modificar_movimiento_CCC(cliente_cuenta_corriente cliente_cuenta_corriente, Modulo_AdministracionContext db)
        {

            try
            {
                bool bandera = false;
                cliente_cuenta_corriente cliente_cuenta_corriente_a_insertar = null;
                cliente_cuenta_corriente cliente_cuenta_corriente_db = db.cliente_cuenta_corriente.FirstOrDefault(ccc => ccc.id_cliente_cuenta_corriente == cliente_cuenta_corriente.id_cliente_cuenta_corriente);

                if (cliente_cuenta_corriente_db == null) //si no encuentra el cliente_cuenta_corriente significa que estoy cargado en la grilla de cuenta corriente una factura vieja que no va a existir en mi sistema
                {

                    cliente_cuenta_corriente_a_insertar = new cliente_cuenta_corriente();
                    //cliente_cuenta_corriente_a_insertar.id_cliente_cuenta_corriente = cliente_cuenta_corriente.id_cliente_cuenta_corriente;
                    cliente_cuenta_corriente_a_insertar.id_cliente = cliente_cuenta_corriente.id_cliente;
                    cliente_cuenta_corriente_a_insertar.id_factura = cliente_cuenta_corriente.id_factura;
                    cliente_cuenta_corriente_a_insertar.fecha_factura_vieja = cliente_cuenta_corriente.fecha_factura_vieja;
                    cliente_cuenta_corriente_a_insertar.nro_factura_vieja = cliente_cuenta_corriente.nro_factura_vieja;
                    cliente_cuenta_corriente_a_insertar.cod_tipo_factura_vieja = cliente_cuenta_corriente.cod_tipo_factura_vieja;
                    cliente_cuenta_corriente_a_insertar.imp_factura = cliente_cuenta_corriente.imp_factura;
                    cliente_cuenta_corriente_a_insertar.pago_1 = cliente_cuenta_corriente.pago_1;
                    cliente_cuenta_corriente_a_insertar.pago_2 = cliente_cuenta_corriente.pago_2;
                    cliente_cuenta_corriente_a_insertar.pago_3 = cliente_cuenta_corriente.pago_3;
                    cliente_cuenta_corriente_a_insertar.pago_4 = cliente_cuenta_corriente.pago_4;
                    cliente_cuenta_corriente_a_insertar.pago_1_fecha = cliente_cuenta_corriente.pago_1_fecha;
                    cliente_cuenta_corriente_a_insertar.pago_2_fecha = cliente_cuenta_corriente.pago_2_fecha;
                    cliente_cuenta_corriente_a_insertar.pago_3_fecha = cliente_cuenta_corriente.pago_3_fecha;
                    cliente_cuenta_corriente_a_insertar.pago_4_fecha = cliente_cuenta_corriente.pago_4_fecha;
                    cliente_cuenta_corriente_a_insertar.observacion = cliente_cuenta_corriente.observacion;
                    db.cliente_cuenta_corriente.Add(cliente_cuenta_corriente_a_insertar);
                    db.SaveChanges();

                }
                else //si existe , es una modificacion
                {

                    //cliente_cuenta_corriente_db.id_cliente_cuenta_corriente = cliente_cuenta_corriente.id_cliente_cuenta_corriente;
                    //cliente_cuenta_corriente_db.id_cliente = cliente_cuenta_corriente. id_cliente;
                    //cliente_cuenta_corriente_db.id_factura = cliente_cuenta_corriente. id_factura;               
                    cliente_cuenta_corriente_db.imp_factura = cliente_cuenta_corriente.imp_factura;
                    cliente_cuenta_corriente_db.pago_1 = cliente_cuenta_corriente.pago_1;
                    cliente_cuenta_corriente_db.pago_2 = cliente_cuenta_corriente.pago_2;
                    cliente_cuenta_corriente_db.pago_3 = cliente_cuenta_corriente.pago_3;
                    cliente_cuenta_corriente_db.pago_4 = cliente_cuenta_corriente.pago_4;
                    cliente_cuenta_corriente_db.pago_1_fecha = cliente_cuenta_corriente.pago_1_fecha;
                    cliente_cuenta_corriente_db.pago_2_fecha = cliente_cuenta_corriente.pago_2_fecha;
                    cliente_cuenta_corriente_db.pago_3_fecha = cliente_cuenta_corriente.pago_3_fecha;
                    cliente_cuenta_corriente_db.pago_4_fecha = cliente_cuenta_corriente.pago_4_fecha;
                    cliente_cuenta_corriente_db.observacion = cliente_cuenta_corriente.observacion;

                    db.SaveChanges();
                }

                bandera = true;

                return bandera;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public static DataSet buscar_movimientos_CCC(int id_cliente, int tipo)
        {

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Modulo_AdministracionContext"].ConnectionString))
            {
                connection.Open();
                using (SqlTransaction sqlTransaction = connection.BeginTransaction())
                {

                    try
                    {

                        DataSet ds = new DataSet();

                        //store
                        SqlCommand command = new SqlCommand("cliente_cuenta_corriente_buscar_por_idCliente", connection, sqlTransaction);

                        //parametros
                        command.Parameters.AddWithValue("@id_cliente", id_cliente);
                        command.Parameters.AddWithValue("@tipo", tipo);

                        //tiempo y tipo
                        command.CommandTimeout = 0;
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command;
                        adapter.Fill(ds);
                        sqlTransaction.Commit();
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

    }
}
