using Modulo_Administracion.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Modulo_Administracion.Logica
{
    static class Logica_Proveedor_Datos
    {

        public static bool alta_proveedorDatos(proveedor_datos dato, int id_proveedor, Modulo_AdministracionContext db)
        {

            bool bandera = false;
            try
            {

                proveedor_datos proveedor_datos_a_insertar = new proveedor_datos();
                proveedor_datos_a_insertar.id_proveedor = id_proveedor;
                proveedor_datos_a_insertar.cod_tipo_dato = dato.cod_tipo_dato;
                proveedor_datos_a_insertar.txt_dato_proveedor = dato.txt_dato_proveedor;
                proveedor_datos_a_insertar.sn_activo = -1;
                proveedor_datos_a_insertar.fec_ult_modif = DateTime.Now;
                proveedor_datos_a_insertar.accion = "ALTA";
                db.proveedor_datos.Add(proveedor_datos_a_insertar);
                db.SaveChanges();

                bandera = true;

                return bandera;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static bool modificar_proveedorDatos(proveedor_datos dato, Modulo_AdministracionContext db)
        {


            try
            {
                if (dato.sn_activo == 0)
                {
                    if (eliminar_proveedorDatos(dato, db) == false)
                    {
                        throw new Exception("Error al eliminar un dato del proveedor");
                    }
                }
                else
                {
                    proveedor_datos proveedor_datos_db = db.proveedor_datos.FirstOrDefault(c => c.id_proveedor == dato.id_proveedor && c.cod_tipo_dato == dato.cod_tipo_dato);
                    if (proveedor_datos_db == null)
                    {
                        if (alta_proveedorDatos(dato, dato.id_proveedor, db) == false)
                        {
                            throw new Exception("Error al dar de alta un dato del proveedor");
                        }
                    }
                    else
                    {
                        proveedor_datos_db.id_proveedor = dato.id_proveedor;
                        proveedor_datos_db.cod_tipo_dato = dato.cod_tipo_dato;
                        proveedor_datos_db.txt_dato_proveedor = dato.txt_dato_proveedor;
                        proveedor_datos_db.fec_ult_modif = DateTime.Now;
                        proveedor_datos_db.sn_activo = dato.sn_activo;
                        proveedor_datos_db.accion = "MODIFICACION";

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

        public static bool eliminar_proveedorDatos(proveedor_datos dato, Modulo_AdministracionContext db)
        {

          
            try
            {

                proveedor_datos proveedor_datos_db = db.proveedor_datos.FirstOrDefault(c => c.id_proveedor == dato.id_proveedor && c.cod_tipo_dato == dato.cod_tipo_dato);
                db.proveedor_datos.Remove(proveedor_datos_db);
                db.SaveChanges();


               

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static bool dar_de_baja_proveedorDatos(int id_proveedor, Modulo_AdministracionContext db)
        {
            bool bandera = false;
            try
            {

                List<proveedor_datos> lista_proveedor_datos = (from pt in db.proveedor_datos
                                                               where pt.id_proveedor == id_proveedor
                                                               select pt).ToList();
                foreach (proveedor_datos pt in lista_proveedor_datos)
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
