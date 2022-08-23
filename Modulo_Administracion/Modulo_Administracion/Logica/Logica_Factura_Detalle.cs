using Modulo_Administracion.Clases;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Modulo_Administracion.Logica
{
    static class Logica_Factura_Detalle
    {

        //pongo en factura_detalle NULL en el id_articulo enviado por parametro -> ya que se da de baja ese articulo , pero debe seguir figurando en el detalle de factura
        public static bool modificar_facturaDetalle(long id_articulo, Modulo_AdministracionContext db)
        {

            bool bandera = false;
            try
            {

                List<factura_detalle> lista_factura_detalle = (from fd in db.factura_detalle
                                                               where fd.id_articulo == id_articulo
                                                               select fd).ToList();

                if (lista_factura_detalle != null)
                {
                    foreach (factura_detalle fd in lista_factura_detalle)
                    {
                        fd.id_articulo = null;
                    }
                }
                db.SaveChanges();
                bandera = true;
                return bandera;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static List<factura_detalle> buscar_facturaDetalle(int id_factura, Modulo_AdministracionContext db)
        {

            try
            {

                List<factura_detalle> factura_detalle = (from fd in db.factura_detalle
                                                         where fd.id_factura == id_factura && fd.fec_baja == null
                                                         select fd).ToList();


                return factura_detalle;

            }
            catch (Exception ex)
            {
                throw ex;
            }



        }




        public static bool dar_de_baja_facturaDetalle(int id_factura_detalle)
        {

            Modulo_AdministracionContext db = new Modulo_AdministracionContext();


            using (DbContextTransaction sqlTransaction = db.Database.BeginTransaction())
            {
                try
                {

                    factura_detalle factura_detalle_db = db.factura_detalle.FirstOrDefault(c => c.id_factura_detalle == id_factura_detalle);
                    factura_detalle_db.fec_baja = DateTime.Now;
                    db.SaveChanges();

                    sqlTransaction.Commit();


                    return true;
                }
                catch (Exception ex)
                {
                    sqlTransaction.Rollback();
                    throw ex;
                }
                finally
                {
                    db = null;
                }
            }
        }




        public static bool alta_facturaDetalle(factura_detalle item_factura, factura factura_db, Modulo_AdministracionContext db)
        {

            bool bandera = false;
            factura_detalle factura_detalle_a_insertar;
            try
            {
                factura_detalle_a_insertar = new factura_detalle();
                factura_detalle_a_insertar.id_factura = factura_db.id_factura;
                factura_detalle_a_insertar.cantidad = item_factura.cantidad;
                factura_detalle_a_insertar.codigo_articulo_marca = item_factura.codigo_articulo_marca;
                factura_detalle_a_insertar.codigo_articulo = item_factura.codigo_articulo;
                factura_detalle_a_insertar.descripcion_articulo = item_factura.descripcion_articulo;
                factura_detalle_a_insertar.precio_lista_x_coeficiente = item_factura.precio_lista_x_coeficiente;
                factura_detalle_a_insertar.iva = item_factura.iva;

                if (factura_db.sn_emitida == -1)
                {
                    factura_detalle_a_insertar.id_articulo = null;
                }
                else
                {
                    factura_detalle_a_insertar.id_articulo = item_factura.id_articulo;
                }


                factura_detalle_a_insertar.fec_baja = null;
                db.factura_detalle.Add(factura_detalle_a_insertar);
                db.SaveChanges();

                bandera = true;
                return bandera;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool modificar_facturaDetalle(factura_detalle factura_detalle_db, factura_detalle item_factura_a_modificar, factura factura_db, Modulo_AdministracionContext db)
        {

            bool bandera = false;
            try
            {
                factura_detalle_db.id_factura = factura_db.id_factura;
                factura_detalle_db.cantidad = item_factura_a_modificar.cantidad;
                factura_detalle_db.codigo_articulo_marca = item_factura_a_modificar.codigo_articulo_marca;
                factura_detalle_db.codigo_articulo = item_factura_a_modificar.codigo_articulo;
                factura_detalle_db.descripcion_articulo = item_factura_a_modificar.descripcion_articulo;
                factura_detalle_db.precio_lista_x_coeficiente = item_factura_a_modificar.precio_lista_x_coeficiente;
                factura_detalle_db.iva = item_factura_a_modificar.iva;

                if (factura_db.sn_emitida == -1)
                {
                    factura_detalle_db.id_articulo = null;
                }
                else
                {
                    factura_detalle_db.id_articulo = item_factura_a_modificar.id_articulo;
                }


                //factura_detalle_db.fec_baja = null; no lo modifico esto
                db.SaveChanges();

                bandera = true;
                return bandera;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
