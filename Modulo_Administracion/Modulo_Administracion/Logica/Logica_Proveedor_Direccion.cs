using Modulo_Administracion.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Modulo_Administracion.Logica
{
    static class Logica_Proveedor_Direccion
    {


        public static bool alta_proveedorDireccion(proveedor_dir direccion, int id_proveedor, Modulo_AdministracionContext db)
        {

            bool bandera = false;
            try
            {



                proveedor_dir proveedor_dir_a_insertar = new proveedor_dir();
                proveedor_dir_a_insertar.id_proveedor = id_proveedor;
                proveedor_dir_a_insertar.cod_tipo_dir = direccion.ttipo_dir.cod_tipo_dir;
                proveedor_dir_a_insertar.cod_clase_dir = direccion.cod_clase_dir;
                proveedor_dir_a_insertar.cod_tipo_calle = direccion.cod_tipo_calle;
                proveedor_dir_a_insertar.cod_calle = direccion.cod_calle;
                proveedor_dir_a_insertar.txt_numero = direccion.txt_numero;
                proveedor_dir_a_insertar.txt_apto = direccion.txt_apto;
                proveedor_dir_a_insertar.txt_piso = direccion.txt_piso;
                proveedor_dir_a_insertar.txt_direccion = direccion.txt_direccion;
                proveedor_dir_a_insertar.txt_cod_postal = direccion.txt_cod_postal;
                proveedor_dir_a_insertar.cod_pais = direccion.cod_pais;
                proveedor_dir_a_insertar.cod_provincia = direccion.cod_provincia;
                proveedor_dir_a_insertar.cod_municipio = direccion.tmunicipio.cod_municipio;
                proveedor_dir_a_insertar.sn_activo = direccion.sn_activo;
                proveedor_dir_a_insertar.fec_ult_modif = DateTime.Now;
                proveedor_dir_a_insertar.accion = direccion.accion;
                db.proveedor_dir.Add(proveedor_dir_a_insertar);
                db.SaveChanges();

                bandera = true;

                return bandera;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static bool modificar_proveedorDireccion(proveedor_dir direccion, Modulo_AdministracionContext db)
        {

            try
            {

                if (direccion.sn_activo == 0)
                {
                    if (eliminar_proveedorDireccion(direccion, db) == false)
                    {
                        throw new Exception("Error al eliminar una direccion del proveedor");
                    }
                }
                else
                {
                    proveedor_dir direccion_db = db.proveedor_dir.FirstOrDefault(p => p.id_proveedor == direccion.id_proveedor && p.cod_tipo_dir == direccion.cod_tipo_dir);
                    if (direccion_db == null)
                    {
                        if (alta_proveedorDireccion(direccion, direccion.id_proveedor, db) == false)
                        {
                            throw new Exception("Error al dar de alta una direccion del proveedor");
                        }
                    }
                    else
                    {

                        direccion_db.id_proveedor = direccion.id_proveedor;
                        direccion_db.cod_tipo_dir = direccion.cod_tipo_dir;
                        direccion_db.cod_clase_dir = direccion.cod_clase_dir;
                        direccion_db.cod_tipo_calle = direccion.cod_tipo_calle;
                        direccion_db.cod_calle = direccion.cod_calle;
                        direccion_db.txt_numero = direccion.txt_numero;
                        direccion_db.txt_apto = direccion.txt_apto;
                        direccion_db.txt_piso = direccion.txt_piso;
                        direccion_db.txt_direccion = direccion.txt_direccion;
                        direccion_db.txt_cod_postal = direccion.txt_cod_postal;
                        direccion_db.cod_pais = direccion.cod_pais;
                        direccion_db.cod_provincia = direccion.cod_provincia;
                        direccion_db.cod_municipio = direccion.cod_municipio;
                        direccion_db.fec_ult_modif = DateTime.Now;
                        direccion_db.sn_activo = direccion.sn_activo;
                        direccion_db.accion = "MODIFICACION";
                        db.SaveChanges();
                    }

                }





                db.SaveChanges();



                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static bool eliminar_proveedorDireccion(proveedor_dir direccion, Modulo_AdministracionContext db)
        {


            try
            {

                proveedor_dir direccion_db = db.proveedor_dir.FirstOrDefault(p => p.id_proveedor == direccion.id_proveedor && p.cod_tipo_dir == direccion.cod_tipo_dir);
                db.proveedor_dir.Remove(direccion_db);
                db.SaveChanges();



                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public static bool dar_de_baja_proveedorDireccion(int id_proveedor, Modulo_AdministracionContext db)
        {
            bool bandera = false;
            try
            {

                List<proveedor_dir> lista_proveedor_dir = (from pd in db.proveedor_dir
                                                           where pd.id_proveedor == id_proveedor
                                                           select pd).ToList();
                foreach (proveedor_dir pd in lista_proveedor_dir)
                {

                    pd.sn_activo = 0;
                    pd.accion = "ELIMINACION";
                    pd.fec_ult_modif = DateTime.Now;



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
