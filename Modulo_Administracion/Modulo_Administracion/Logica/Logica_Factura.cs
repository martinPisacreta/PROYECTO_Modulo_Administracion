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
    static class Logica_Factura
    {



        public static factura alta_factura(factura factura)
        {

          
          
            Int64 nro_factura;

            Modulo_AdministracionContext db = new Modulo_AdministracionContext();
            using (DbContextTransaction dbContextTransaction = db.Database.BeginTransaction())
            {
                ttipo_factura ttipo_factura_bd = db.ttipo_factura.FirstOrDefault(f => f.cod_tipo_factura == factura.cod_tipo_factura);
                nro_factura = ult_nro_factura_no_usado(factura.cod_tipo_factura, true); //voy a buscar el ult_nro_factura_no_usado de la tabla factura

                try
                {
                    factura factura_a_insertar = new factura();
                    //id_factura -- es identity
                    factura_a_insertar.id_cliente = factura.id_cliente;
                    factura_a_insertar.cod_tipo_factura = factura.cod_tipo_factura;
                    factura_a_insertar.ttipo_factura = ttipo_factura_bd;

                    factura_a_insertar.nro_factura = Convert.ToInt64(nro_factura.ToString());


                    factura_a_insertar.precio_final = factura.precio_final;
                    factura_a_insertar.sn_modifica_precio_final = factura.sn_modifica_precio_final;
                    factura_a_insertar.precio_final_con_pago_mayor_a_30_dias = factura.precio_final_con_pago_mayor_a_30_dias;
                    factura_a_insertar.precio_final_con_pago_menor_a_30_dias = factura.precio_final_con_pago_menor_a_30_dias;
                    factura_a_insertar.precio_final_con_pago_menor_a_7_dias = factura.precio_final_con_pago_menor_a_7_dias;

                    factura_a_insertar.sn_emitida = factura.sn_emitida;
                    factura_a_insertar.observacion = factura.observacion;
                    factura_a_insertar.sn_mostrar_pago_mayor_30_dias = factura.sn_mostrar_pago_mayor_30_dias;
                    factura_a_insertar.sn_mostrar_pago_menor_30_dias = factura.sn_mostrar_pago_menor_30_dias;
                    factura_a_insertar.sn_mostrar_pago_menor_7_dias = factura.sn_mostrar_pago_menor_7_dias;
                    factura_a_insertar.id_condicion_factura = Logica_Cliente.buscar_cliente(factura.id_cliente, db).id_condicion_factura;

                    if (factura_a_insertar.sn_emitida == -1)
                    {
                        factura_a_insertar.fecha = Convert.ToDateTime(factura.fecha.ToString("yyyy-MM-dd"));
                        factura_a_insertar.fecha_sn_emitida = factura.fecha_sn_emitida;
                        factura_a_insertar.path_factura = Logica_Funciones_Generales.crear_path_a_partir_de_factura(factura_a_insertar);
                    }
                    else
                    {
                        factura_a_insertar.fecha = Convert.ToDateTime(factura.fecha.ToString("yyyy-MM-dd"));
                        factura_a_insertar.fecha_sn_emitida = factura.fecha_sn_emitida;
                        factura_a_insertar.path_factura = factura.path_factura;
                    }

                    db.factura.Add(factura_a_insertar);
                    db.SaveChanges();


                    foreach (factura_detalle item_factura in factura.factura_detalle)
                    {
                        if (Logica_Factura_Detalle.alta_facturaDetalle(item_factura, factura_a_insertar, db) == false)
                        {
                            throw new Exception("Error al dar de alta el item a la factura");
                        }
                    }

                    if (factura.sn_emitida == -1)
                    {
                        if (Logica_Cliente_Cuenta_Corriente.alta_movimiento_CCC(factura_a_insertar, db) == false)
                        {
                            throw new Exception("Error al dar de alta movimiento en cuenta corriente");
                        }
                    }

                    db.SaveChanges();


                    dbContextTransaction.Commit();
                    return factura_a_insertar;
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

        //EN ESTE METODO VOY A BUSCAR A LA TABLA FACTURA O CLIENTE_CUENTA_CORRIENTE
        public static Int32 ult_nro_factura_no_usado(decimal cod_tipo_factura, bool sn_busco_en_tabla_FACTURA)
        {

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Modulo_AdministracionContext"].ConnectionString))
            {
                connection.Open();
                using (SqlTransaction sqlTransaction = connection.BeginTransaction())
                {

                    try
                    {
                        int _ult_nro_factura_no_usado_en_tipo_factura = 0;
                        DataSet ds = new DataSet();

                        //store
                        SqlCommand command = new SqlCommand("buscar_ult_nro_no_usado_por_codTipoFactura", connection, sqlTransaction);

                        //parametros
                        command.Parameters.AddWithValue("@cod_tipo_factura", cod_tipo_factura);
                        command.Parameters.AddWithValue("@sn_busco_en_tabla_FACTURA", sn_busco_en_tabla_FACTURA);

                        //tiempo y tipo
                        command.CommandTimeout = 0;
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command;
                        adapter.Fill(ds);

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            _ult_nro_factura_no_usado_en_tipo_factura = Convert.ToInt32(dr[0].ToString());
                        }

                        sqlTransaction.Commit();
                        return _ult_nro_factura_no_usado_en_tipo_factura;
                    }
                    catch (Exception ex)
                    {
                        sqlTransaction.Rollback();
                        throw ex;
                    }

                }
            }
        }

        public static DataSet buscar_factura_print(int id_factura)
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
                        SqlCommand command = new SqlCommand("factura_imprimir", connection, sqlTransaction);

                        //parametros
                        command.Parameters.AddWithValue("@id_factura", id_factura);

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

        public static factura modificar_factura(factura factura)
        {
           
            Modulo_AdministracionContext db = new Modulo_AdministracionContext();
            using (DbContextTransaction dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {

                    factura factura_db = db.factura.FirstOrDefault(c => c.id_factura == factura.id_factura);
                    factura_db.id_cliente = factura.id_cliente;
                    factura_db.cod_tipo_factura = factura.cod_tipo_factura;
                    //factura_db.fecha = factura.fecha;
                    factura_db.nro_factura = factura.nro_factura;

                    factura_db.precio_final = factura.precio_final;
                    factura_db.sn_modifica_precio_final = factura.sn_modifica_precio_final;
                    factura_db.precio_final_con_pago_mayor_a_30_dias = factura.precio_final_con_pago_mayor_a_30_dias;
                    factura_db.precio_final_con_pago_menor_a_30_dias = factura.precio_final_con_pago_menor_a_30_dias;

                    factura_db.precio_final_con_pago_menor_a_7_dias = factura.precio_final_con_pago_menor_a_7_dias;

                    factura_db.sn_emitida = factura.sn_emitida;
                    factura_db.observacion = factura.observacion;


                    factura_db.sn_mostrar_pago_mayor_30_dias = factura.sn_mostrar_pago_mayor_30_dias;
                    factura_db.sn_mostrar_pago_menor_30_dias = factura.sn_mostrar_pago_menor_30_dias;
                    factura_db.sn_mostrar_pago_menor_7_dias = factura.sn_mostrar_pago_menor_7_dias;
                    factura_db.id_condicion_factura = Logica_Cliente.buscar_cliente(factura.id_cliente, db).id_condicion_factura;



                    //SI CAMBIO ACA , TAMBIEN CAMBIAR LINEA DONDE DICE "PEPE EL PISTOLERO"
                    if (factura_db.sn_emitida == -1)
                    {
                        factura_db.fecha = Convert.ToDateTime(factura.fecha.ToString("yyyy-MM-dd"));
                        factura_db.fecha_sn_emitida = factura.fecha_sn_emitida;
                        factura_db.path_factura = Logica_Funciones_Generales.crear_path_a_partir_de_factura(factura_db);
                    }
                    else
                    {
                        factura_db.fecha_sn_emitida = null;
                        factura_db.path_factura = null;
                    }
                    //HASTA ACA




                    db.SaveChanges();

                    foreach (factura_detalle item_factura in factura.factura_detalle)
                    {
                        factura_detalle factura_detalle_db = db.factura_detalle.FirstOrDefault(c => c.id_factura_detalle == item_factura.id_factura_detalle);
                        if (factura_detalle_db != null) //es modificacion
                        {
                            if (Logica_Factura_Detalle.modificar_facturaDetalle(factura_detalle_db, item_factura, factura_db, db) == false)
                            {
                                throw new Exception("Error al modificar el item a la factura");
                            }

                        }
                        else
                        {
                            if (Logica_Factura_Detalle.alta_facturaDetalle(item_factura, factura_db, db) == false)
                            {
                                throw new Exception("Error al dar de alta el item a la factura");
                            }
                        }


                    }

                    if (factura.sn_emitida == -1 && factura.cod_tipo_factura == ttipo_factura_constantes.i_valor_remito) //solamente si es remito guardo el movimiento en la cuenta corriente
                    {
                        if (Logica_Cliente_Cuenta_Corriente.alta_movimiento_CCC(factura, db) == false)
                        {
                            throw new Exception("Error al dar de alta movimiento en cuenta corriente");
                        }
                    }

                    dbContextTransaction.Commit();
                    return factura_db;
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






        public static factura buscar_factura(int id_factura)
        {
            Modulo_AdministracionContext db = new Modulo_AdministracionContext();
            try
            {

                factura factura = db.factura.FirstOrDefault(t => t.id_factura == id_factura);
                factura.factura_detalle.Clear();
                factura.factura_detalle = Logica_Factura_Detalle.buscar_facturaDetalle(factura.id_factura, db);

                return factura;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
            }
        }


        public static List<factura> buscar_facturas(DateTime fecha)
        {
            Modulo_AdministracionContext db = new Modulo_AdministracionContext();
            try
            {

                List<factura> facturas = (from t in db.factura
                                          where t.fecha == fecha
                                          select t).ToList();


                return facturas;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
            }


        }


        //voy a buscar a las tablas : FACTURA - CLIENTE_CUENTA_CORRIENTE
        public static DataSet buscar_facturas(Int64 nro_factura, int cod_tipo_factura)
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
                        SqlCommand command = new SqlCommand("factura_buscar_por_nroFactura_codTipoFactura", connection, sqlTransaction);

                        //parametros
                        command.Parameters.AddWithValue("@nro_factura", nro_factura);
                        command.Parameters.AddWithValue("@cod_tipo_factura", cod_tipo_factura);

                        //tiempo y tipo
                        command.CommandTimeout = 0;
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command;
                        adapter.Fill(ds);

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

        public static List<factura> buscar_facturas(int cod_tipo_factura, Int64 nro_factura, int id_cliente, string codigo_articulo, string codigo_articulo_marca, DateTime? fecha_desde, DateTime? fecha_hasta)
        {
            Modulo_AdministracionContext db = new Modulo_AdministracionContext();
            try
            {

                List<factura> facturas = ((from f in db.factura
                                           join fd in db.factura_detalle on f.id_factura equals fd.id_factura
                                           where
                                                 (f.cod_tipo_factura == cod_tipo_factura || cod_tipo_factura == ttipo_factura_constantes.i_valor_todos) &&
                                                 (f.nro_factura == nro_factura || nro_factura == 0) &&
                                                 (f.id_cliente == id_cliente || id_cliente == -999) &&
                                                 (fd.codigo_articulo == codigo_articulo || codigo_articulo == "") &&
                                                 (fd.codigo_articulo_marca == codigo_articulo_marca || codigo_articulo_marca == "") &&
                                                 ((f.fecha >= fecha_desde && f.fecha <= fecha_hasta) || (fecha_desde == null && fecha_hasta == null))
                                           select f).Distinct()).ToList();


                return facturas;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
            }


        }


        public static bool eliminar_factura(factura factura)
        {


            bool bandera = false;
            Modulo_AdministracionContext db = new Modulo_AdministracionContext();
            using (DbContextTransaction dbContextTransaction = db.Database.BeginTransaction())
            {

                try
                {

                    var eliminar_factura_detalle = from fd in db.factura_detalle
                                                   where fd.id_factura == factura.id_factura
                                                   select fd;

                    var eliminar_cliente_cuenta_corriente = from ccc in db.cliente_cuenta_corriente
                                                            where ccc.id_factura == factura.id_factura
                                                            select ccc;

                    var eliminar_factura = from f in db.factura
                                           where f.id_factura == factura.id_factura
                                           select f;



                    db.factura_detalle.RemoveRange(eliminar_factura_detalle);
                    db.cliente_cuenta_corriente.RemoveRange(eliminar_cliente_cuenta_corriente);
                    db.factura.RemoveRange(eliminar_factura);


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
    }
}
