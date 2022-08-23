using Modulo_Administracion.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Modulo_Administracion.Logica
{
    static class Logica_Cliente_Datos
    {

        public static bool alta_clienteDatos(cliente_datos dato, int id_cliente, Modulo_AdministracionContext db)
        {

            bool bandera = false;
            try
            {

                cliente_datos cliente_datos_a_insertar = new cliente_datos();
                cliente_datos_a_insertar.id_cliente = id_cliente;
                cliente_datos_a_insertar.cod_tipo_dato = dato.cod_tipo_dato;
                cliente_datos_a_insertar.txt_dato_cliente = dato.txt_dato_cliente;
                cliente_datos_a_insertar.sn_activo = -1;
                cliente_datos_a_insertar.fec_ult_modif = DateTime.Now;
                cliente_datos_a_insertar.accion = "ALTA";
                db.cliente_datos.Add(cliente_datos_a_insertar);
                db.SaveChanges();

                bandera = true;

                return bandera;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static bool modificar_clienteDatos(cliente_datos dato, Modulo_AdministracionContext db)
        {


            try
            {
                if (dato.sn_activo == 0)
                {
                    if (eliminar_clienteDatos(dato, db) == false)
                    {
                        throw new Exception("Error al eliminar un dato del cliente");
                    }
                }
                else
                {
                    cliente_datos cliente_datos_db = db.cliente_datos.FirstOrDefault(c => c.id_cliente == dato.id_cliente && c.cod_tipo_dato == dato.cod_tipo_dato);
                    if (cliente_datos_db == null)
                    {
                        if (alta_clienteDatos(dato, dato.id_cliente, db) == false)
                        {
                            throw new Exception("Error al dar de alta un dato del cliente");
                        }
                    }
                    else
                    {
                        cliente_datos_db.id_cliente = dato.id_cliente;
                        cliente_datos_db.cod_tipo_dato = dato.cod_tipo_dato;
                        cliente_datos_db.txt_dato_cliente = dato.txt_dato_cliente;
                        cliente_datos_db.fec_ult_modif = DateTime.Now;
                        cliente_datos_db.sn_activo = dato.sn_activo;
                        cliente_datos_db.accion = "MODIFICACION";

                        db.SaveChanges();
                    }

                }



                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static bool eliminar_clienteDatos(cliente_datos dato, Modulo_AdministracionContext db)
        {


            try
            {

                cliente_datos cliente_datos_db = db.cliente_datos.FirstOrDefault(c => c.id_cliente == dato.id_cliente && c.cod_tipo_dato == dato.cod_tipo_dato);
                db.cliente_datos.Remove(cliente_datos_db);
                db.SaveChanges();




                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static bool dar_de_baja_clienteDatos(int id_cliente, Modulo_AdministracionContext db)
        {
            bool bandera = false;
            try
            {

                List<cliente_datos> lista_cliente_datos = (from pt in db.cliente_datos
                                                           where pt.id_cliente == id_cliente
                                                           select pt).ToList();
                foreach (cliente_datos pt in lista_cliente_datos)
                {

                    pt.sn_activo = 0;
                    pt.accion = "ELIMINACION";
                    pt.fec_ult_modif = DateTime.Now;



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
    }
}
