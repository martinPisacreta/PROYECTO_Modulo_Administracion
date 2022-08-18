using Modulo_Administracion.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Modulo_Administracion.Logica
{
    static class Logica_Proveedor
    {


        public static bool alta_proveedor(proveedor proveedor)
        {
            Modulo_AdministracionContext db = new Modulo_AdministracionContext();
            bool bandera = false;
            using (DbContextTransaction dbContextTransaction = db.Database.BeginTransaction())
            {

                try
                {
                    proveedor proveedor_a_insertar = new proveedor();

                    int cantidad = db.proveedor.Count();
                    if (cantidad == 0)
                    {
                        proveedor_a_insertar.id_proveedor = 1;
                    }
                    else
                    {
                        proveedor_a_insertar.id_proveedor = db.proveedor.Max(p => p.id_proveedor) + 1;
                    }
                    proveedor_a_insertar.razon_social = proveedor.razon_social;
                    proveedor_a_insertar.id_condicion_ante_iva = proveedor.id_condicion_ante_iva;
                    proveedor_a_insertar.id_condicion_pago = proveedor.id_condicion_pago;
                    proveedor_a_insertar.sn_activo = proveedor.sn_activo;
                    proveedor_a_insertar.fec_ult_modif = proveedor.fec_ult_modif;
                    proveedor_a_insertar.accion = proveedor.accion;
                    proveedor_a_insertar.path_img = proveedor.path_img;
                    db.proveedor.Add(proveedor_a_insertar);
                    db.SaveChanges();


                    //doy de alta el/los datos del proveedor
                    if (proveedor.proveedor_datos != null)
                    {
                        foreach (proveedor_datos proveedor_dato in proveedor.proveedor_datos)
                        {
                            if (Logica_Proveedor_Datos.alta_proveedorDatos(proveedor_dato, proveedor_a_insertar.id_proveedor, db) == false)
                            {
                                throw new Exception("Error al dar de alta los datos del proveedor");
                            }
                        }
                    }

                    //doy de alta la/las direcciones
                    if (proveedor.proveedor_dir != null)
                    {
                        foreach (proveedor_dir proveedor_dir in proveedor.proveedor_dir)
                        {
                            if (Logica_Proveedor_Direccion.alta_proveedorDireccion(proveedor_dir, proveedor_a_insertar.id_proveedor, db) == false)
                            {
                                throw new Exception("Error al dar de alta las direcciones del proveedor");
                            }
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

        public static bool modificar_proveedor(proveedor proveedor)
        {
            Modulo_AdministracionContext db = new Modulo_AdministracionContext();
            bool bandera = false;
            using (DbContextTransaction dbContextTransaction = db.Database.BeginTransaction())
            {

                try
                {


                    proveedor proveedor_db = db.proveedor.FirstOrDefault(f => f.id_proveedor == proveedor.id_proveedor);
                    proveedor_db.id_proveedor = proveedor.id_proveedor;
                    proveedor_db.razon_social = proveedor.razon_social;
                    proveedor_db.id_condicion_ante_iva = proveedor.id_condicion_ante_iva;
                    proveedor_db.id_condicion_pago = proveedor.id_condicion_pago;
                    proveedor_db.sn_activo = proveedor.sn_activo;
                    proveedor_db.accion = "MODIFICACION";
                    proveedor_db.fec_ult_modif = DateTime.Now;
                    proveedor_db.path_img = proveedor.path_img;
                    db.SaveChanges();

                    //modifico el/los datos del proveedor
                    if (proveedor.proveedor_datos != null)
                    {
                        foreach (proveedor_datos proveedor_dato in proveedor.proveedor_datos)
                        {
                            if (Logica_Proveedor_Datos.modificar_proveedorDatos(proveedor_dato, db) == false)
                            {
                                throw new Exception("Error al modificar los datos del proveedor");
                            }
                        }
                    }

                    //modifico la/las direcciones
                    if (proveedor.proveedor_dir != null)
                    {
                        foreach (proveedor_dir proveedor_dir in proveedor.proveedor_dir)
                        {
                            if (Logica_Proveedor_Direccion.modificar_proveedorDireccion(proveedor_dir, db) == false)
                            {
                                throw new Exception("Error al modificar las direcciones del proveedor");
                            }
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

        public static bool dar_de_baja_proveedor(proveedor proveedor)
        {
            Modulo_AdministracionContext db = new Modulo_AdministracionContext();
            bool bandera = false;
            using (DbContextTransaction dbContextTransaction = db.Database.BeginTransaction())
            {

                try
                {


                    proveedor proveedor_db = db.proveedor.FirstOrDefault(f => f.id_proveedor == proveedor.id_proveedor);
                    proveedor_db.id_proveedor = proveedor.id_proveedor;
                    proveedor_db.razon_social = proveedor.razon_social;
                    proveedor_db.id_condicion_ante_iva = proveedor.id_condicion_ante_iva;
                    proveedor_db.id_condicion_pago = proveedor.id_condicion_pago;
                    proveedor_db.sn_activo = 0;
                    proveedor_db.accion = "ELIMINACION";
                    proveedor_db.fec_ult_modif = DateTime.Now;
                    proveedor_db.path_img = proveedor.path_img;
                    db.SaveChanges();

                    if (proveedor.proveedor_dir != null)
                    {
                        if (proveedor.proveedor_dir.Count > 0)
                        {

                            if (Logica_Proveedor_Direccion.dar_de_baja_proveedorDireccion(proveedor.id_proveedor, db) == false)
                            {
                                throw new Exception("Error al dar de baja direccion/es del proveedor");
                            }
                        }
                    }

                    if (proveedor.proveedor_datos != null)
                    {
                        if (proveedor.proveedor_datos.Count > 0)
                        {

                            if (Logica_Proveedor_Datos.dar_de_baja_proveedorDatos(proveedor.id_proveedor, db) == false)
                            {
                                throw new Exception("Error al dar de baja dato/s del proveedor");
                            }
                        }
                    }


                    if (proveedor.marca != null)
                    {
                        if (proveedor.marca.Count > 0)
                        {

                            if (Logica_Marca.dar_de_baja_marcas(proveedor.id_proveedor, db) == false)
                            {
                                throw new Exception("Error al dar de baja marcas del proveedor");
                            }
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

        public static List<proveedor> buscar_proveedores()
        {

            Modulo_AdministracionContext db = new Modulo_AdministracionContext();
            try
            {


                List<proveedor> proveedores = (from p in db.proveedor
                                               where p.sn_activo == -1
                                               select p).ToList();


                return proveedores;
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

        public static proveedor buscar_proveedor(int id_proveedor)
        {

            Modulo_AdministracionContext db = new Modulo_AdministracionContext();
            try
            {

                proveedor proveedor = db.proveedor.FirstOrDefault(p => p.id_proveedor == id_proveedor);

                return proveedor;
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



        public static DataTable buscar_proveedores_activos(string txtBusqueda)
        {
            Modulo_AdministracionContext db = new Modulo_AdministracionContext();

            var proveedores = (from p in db.proveedor
                               where p.razon_social.Contains(txtBusqueda) &&
                                     p.sn_activo == -1
                               select new
                               {
                                   id_proveedor = p.id_proveedor,
                                   razon_social = p.razon_social,
                                   sn_activo = p.sn_activo,
                                   fec_ult_modif = p.fec_ult_modif,
                                   accion = p.accion,
                                   path_img = p.path_img,
                                   id_condicion_ante_iva = p.id_condicion_ante_iva,
                                   id_condicion_pago = p.id_condicion_pago
                               }).ToList();


            DataTable dt = new DataTable();
            dt.Columns.Add("id_proveedor");
            dt.Columns.Add("razon_social");
            dt.Columns.Add("sn_activo");
            dt.Columns.Add("fec_ult_modif");
            dt.Columns.Add("accion");
            dt.Columns.Add("path_img");
            dt.Columns.Add("id_condicion_ante_iva");
            dt.Columns.Add("id_condicion_pago");

            foreach (var proveedor in proveedores)
            {
                DataRow row = dt.NewRow();
                row["id_proveedor"] = proveedor.id_proveedor;
                row["razon_social"] = proveedor.razon_social;
                row["sn_activo"] = proveedor.sn_activo;
                row["fec_ult_modif"] = proveedor.fec_ult_modif;
                row["accion"] = proveedor.accion;
                row["path_img"] = proveedor.path_img;
                row["id_condicion_ante_iva"] = proveedor.id_condicion_ante_iva;
                row["id_condicion_pago"] = proveedor.id_condicion_pago;
                dt.Rows.Add(row);
            }
            return dt;
        }

        public static proveedor buscar_proveedores_activos_con_razonSocial_repetido(string razon_social, int id_proveedor)
        {

            Modulo_AdministracionContext db = new Modulo_AdministracionContext();
            try
            {

                proveedor proveedor = db.proveedor.FirstOrDefault(p => p.razon_social.Contains(razon_social) && p.sn_activo == -1 && p.id_proveedor != id_proveedor);

                return proveedor;
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
    }
}
