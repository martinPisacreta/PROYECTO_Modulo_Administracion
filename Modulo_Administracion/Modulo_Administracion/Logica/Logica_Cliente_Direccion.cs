using Modulo_Administracion.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Modulo_Administracion.Logica
{
    static class Logica_Cliente_Direccion
    {


        public static bool alta_clienteDireccion(cliente_dir direccion, int id_cliente, Modulo_AdministracionContext db)
        {

            bool bandera = false;
            try
            {



                cliente_dir cliente_dir_a_insertar = new cliente_dir();
                cliente_dir_a_insertar.id_cliente = id_cliente;
                cliente_dir_a_insertar.cod_tipo_dir = direccion.ttipo_dir.cod_tipo_dir;
                cliente_dir_a_insertar.cod_clase_dir = direccion.cod_clase_dir;
                cliente_dir_a_insertar.cod_tipo_calle = direccion.ttipo_calle.cod_tipo_calle;
                cliente_dir_a_insertar.cod_calle = direccion.cod_calle;
                cliente_dir_a_insertar.txt_numero = direccion.txt_numero;
                cliente_dir_a_insertar.txt_apto = direccion.txt_apto;
                cliente_dir_a_insertar.txt_piso = direccion.txt_piso;
                cliente_dir_a_insertar.txt_direccion = direccion.txt_direccion;
                cliente_dir_a_insertar.txt_cod_postal = direccion.txt_cod_postal;
                cliente_dir_a_insertar.cod_pais = direccion.cod_pais;
                cliente_dir_a_insertar.cod_provincia = direccion.cod_provincia;
                cliente_dir_a_insertar.cod_municipio = direccion.tmunicipio.cod_municipio;
                cliente_dir_a_insertar.sn_activo = direccion.sn_activo;
                cliente_dir_a_insertar.fec_ult_modif = DateTime.Now;
                cliente_dir_a_insertar.accion = direccion.accion;
                db.cliente_dir.Add(cliente_dir_a_insertar);
                db.SaveChanges();

                bandera = true;

                return bandera;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static bool modificar_clienteDireccion(cliente_dir direccion, Modulo_AdministracionContext db)
        {
          
            try
            {

                if (direccion.sn_activo == 0)
                {
                    if (eliminar_clienteDireccion(direccion, db) == false)
                    {
                        throw new Exception("Error al eliminar una direccion del cliente");
                    }
                }
                else
                {
                    cliente_dir direccion_db = db.cliente_dir.FirstOrDefault(p => p.id_cliente == direccion.id_cliente && p.cod_tipo_dir == direccion.cod_tipo_dir);
                    if (direccion_db == null)
                    {
                        if (alta_clienteDireccion(direccion, direccion.id_cliente, db) == false)
                        {
                            throw new Exception("Error al dar de alta una direccion del cliente");
                        }
                    }
                    else
                    {

                        direccion_db.id_cliente = direccion.id_cliente;
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
                        direccion_db.cod_municipio = direccion.tmunicipio.cod_municipio;
                        direccion_db.fec_ult_modif = DateTime.Now;
                        direccion_db.sn_activo = direccion.sn_activo;
                        direccion_db.accion = "MODIFICACION";
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

        public static bool eliminar_clienteDireccion(cliente_dir direccion, Modulo_AdministracionContext db)
        {


            try
            {

                cliente_dir direccion_db = db.cliente_dir.FirstOrDefault(p => p.id_cliente == direccion.id_cliente && p.cod_tipo_dir == direccion.cod_tipo_dir);
                db.cliente_dir.Remove(direccion_db);
                db.SaveChanges();



                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static bool dar_de_baja_clienteDireccion(int id_cliente, Modulo_AdministracionContext db)
        {
            bool bandera = false;
            try
            {

                List<cliente_dir> lista_cliente_dir = (from pd in db.cliente_dir
                                                       where pd.id_cliente == id_cliente
                                                       select pd).ToList();
                foreach (cliente_dir pd in lista_cliente_dir)
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
