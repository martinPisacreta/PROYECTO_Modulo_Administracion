using Modulo_Administracion.Clases;

using System;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Modulo_Administracion.Logica
{
    static class Logica_Cliente
    {



        public static bool alta_cliente(cliente cliente)
        {
            Modulo_AdministracionContext db = new Modulo_AdministracionContext();
            bool bandera = false;
            using (DbContextTransaction dbContextTransaction = db.Database.BeginTransaction())
            {

                try
                {
                    cliente cliente_a_insertar = new cliente();

                    int cantidad = db.cliente.Count();
                    if (cantidad == 0)
                    {
                        cliente_a_insertar.id_cliente = 1;
                    }
                    else
                    {
                        cliente_a_insertar.id_cliente = db.cliente.Max(c => c.id_cliente) + 1;
                    }
                    cliente_a_insertar.razon_social = cliente.razon_social;
                    cliente_a_insertar.nombre_fantasia = cliente.nombre_fantasia;
                    cliente_a_insertar.id_condicion_ante_iva = cliente.id_condicion_ante_iva;
                    cliente_a_insertar.id_condicion_pago = cliente.id_condicion_pago;
                    cliente_a_insertar.sn_activo = cliente.sn_activo;
                    cliente_a_insertar.fec_ult_modif = cliente.fec_ult_modif;
                    cliente_a_insertar.accion = cliente.accion;
                    cliente_a_insertar.id_condicion_factura = cliente.id_condicion_factura;
                    cliente_a_insertar.id_tipo_cliente = cliente.id_tipo_cliente;
                    cliente_a_insertar.id_vendedor = cliente.id_vendedor > 0 ? cliente.id_vendedor : null;
                    db.cliente.Add(cliente_a_insertar);
                    db.SaveChanges();


                    //doy de alta el/los datos del cliente
                    if (cliente.cliente_datos != null)
                    {
                        if (cliente.cliente_datos.Count > 0)
                        {
                            foreach (cliente_datos cliente_dato in cliente.cliente_datos)
                            {
                                if (Logica_Cliente_Datos.alta_clienteDatos(cliente_dato, cliente_a_insertar.id_cliente, db) == false)
                                {
                                    throw new Exception("Error al dar de alta los datos del cliente");
                                }
                            }
                        }
                    }

                    //doy de alta la/las direcciones
                    if (cliente.cliente_dir != null)
                    {
                        if (cliente.cliente_dir.Count > 0)
                        {
                            foreach (cliente_dir cliente_dir in cliente.cliente_dir)
                            {
                                if (Logica_Cliente_Direccion.alta_clienteDireccion(cliente_dir, cliente_a_insertar.id_cliente, db) == false)
                                {
                                    throw new Exception("Error al dar de alta las direcciones del cliente");
                                }
                            }
                        }
                    }


                    bandera = true;
                    dbContextTransaction.Commit();
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

        public static bool modificar_cliente(cliente cliente)
        {
            Modulo_AdministracionContext db = new Modulo_AdministracionContext();
            bool bandera = false;
            using (DbContextTransaction dbContextTransaction = db.Database.BeginTransaction())
            {

                try
                {

                    cliente cliente_db = db.cliente.FirstOrDefault(f => f.id_cliente == cliente.id_cliente);
                    cliente_db.id_cliente = cliente.id_cliente;
                    cliente_db.razon_social = cliente.razon_social;
                    cliente_db.nombre_fantasia = cliente.nombre_fantasia;
                    cliente_db.id_condicion_ante_iva = cliente.id_condicion_ante_iva;
                    cliente_db.id_condicion_pago = cliente.id_condicion_pago;
                    cliente_db.sn_activo = cliente.sn_activo;
                    cliente_db.accion = "MODIFICACION";
                    cliente_db.fec_ult_modif = DateTime.Now;
                    cliente_db.id_condicion_factura = cliente.id_condicion_factura;
                    cliente_db.id_tipo_cliente = cliente.id_tipo_cliente;
                    cliente_db.id_vendedor = cliente.id_vendedor > 0 ? cliente.id_vendedor : null;
                    db.SaveChanges();


                    //modifico el/los datos del cliente
                    if (cliente.cliente_datos != null)
                    {
                        if (cliente.cliente_datos.Count > 0)
                        {
                            foreach (cliente_datos cliente_dato in cliente.cliente_datos)
                            {
                                if (Logica_Cliente_Datos.modificar_clienteDatos(cliente_dato, db) == false)
                                {
                                    throw new Exception("Error al modificar los datos del cliente");
                                }
                            }
                        }
                    }

                    //modifico la/las direcciones
                    if (cliente.cliente_dir != null)
                    {
                        if (cliente.cliente_dir.Count > 0)
                        {
                            foreach (cliente_dir cliente_dir in cliente.cliente_dir)
                            {
                                if (Logica_Cliente_Direccion.modificar_clienteDireccion(cliente_dir, db) == false)
                                {
                                    throw new Exception("Error al modificar las direcciones del cliente");
                                }
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

        public static bool dar_de_baja_cliente(cliente cliente)
        {
            Modulo_AdministracionContext db = new Modulo_AdministracionContext();
            bool bandera = false;
            using (DbContextTransaction dbContextTransaction = db.Database.BeginTransaction())
            {

                try
                {


                    cliente cliente_db = db.cliente.FirstOrDefault(f => f.id_cliente == cliente.id_cliente);
                    cliente_db.id_cliente = cliente.id_cliente;
                    cliente_db.razon_social = cliente.razon_social;
                    cliente_db.nombre_fantasia = cliente.nombre_fantasia;
                    cliente_db.id_condicion_ante_iva = cliente.id_condicion_ante_iva;
                    cliente_db.id_condicion_pago = cliente.id_condicion_pago;
                    cliente_db.sn_activo = 0;
                    cliente_db.accion = "ELIMINACION";
                    cliente_db.fec_ult_modif = DateTime.Now;
                    db.SaveChanges();


                    //elimino el/los datos del cliente
                    if (cliente.cliente_datos != null)
                    {
                        if (cliente.cliente_datos.Count > 0)
                        {

                            if (Logica_Cliente_Datos.dar_de_baja_clienteDatos(cliente.id_cliente, db) == false)
                            {
                                throw new Exception("Error al dar de baja dato/s del cliente");
                            }
                        }
                    }

                    //elimino la/las direcciones
                    if (cliente.cliente_dir != null)
                    {
                        if (cliente.cliente_dir.Count > 0)
                        {

                            if (Logica_Cliente_Direccion.dar_de_baja_clienteDireccion(cliente.id_cliente, db) == false)
                            {
                                throw new Exception("Error al dar de baja direccion/es del cliente");
                            }
                        }
                    }
                    //no habilite la opcion de eliminar facturas 


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

        public static object buscar_clientes_activos()
        {

            Modulo_AdministracionContext db = new Modulo_AdministracionContext();
            try
            {


                var clientes = (from p in db.cliente
                                where p.sn_activo == -1
                                select new
                                {
                                    p.id_cliente,
                                    p.nombre_fantasia,
                                    p.vendedor.nombre
                                }).ToList();


                return clientes;
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


        public static cliente buscar_clientes_activos_con_nombreFantasia_repetido(string nombre_fantasia, int id_cliente)
        {

            Modulo_AdministracionContext db = new Modulo_AdministracionContext();
            try
            {
                //si esto devuelve algo , quiere decir que aparte del id_cliente que se envio por parametro , existe otro con ese mismo nombre_fantasia 
                cliente cliente = db.cliente.FirstOrDefault(p => p.nombre_fantasia == nombre_fantasia &&
                                                                 p.sn_activo == -1 &&
                                                                 p.id_cliente != id_cliente);

                return cliente;
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

        public static cliente buscar_clientes_activos_con_razonSocial_repetido(string razon_social, int id_cliente)
        {

            Modulo_AdministracionContext db = new Modulo_AdministracionContext();
            try
            {
                //si esto devuelve algo , quiere decir que aparte del id_cliente que se envio por parametro , existe otro con ese mismo razon_social 
                cliente cliente = db.cliente.FirstOrDefault(p => p.razon_social == razon_social &&
                                                                 p.sn_activo == -1
                                                                 && p.id_cliente != id_cliente);

                return cliente;
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


        public static cliente buscar_cliente(int id_cliente, Modulo_AdministracionContext db)
        {


            try
            {
                cliente cliente = null;
                if (db == null)
                {
                    Modulo_AdministracionContext _db = new Modulo_AdministracionContext();
                    cliente = _db.cliente.FirstOrDefault(p => p.id_cliente == id_cliente);
                }
                else
                {
                    cliente = db.cliente.FirstOrDefault(p => p.id_cliente == id_cliente);
                }


                return cliente;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public static object buscar_clientes(string nombreFantasia_o_nombreVendedor, int valor_busqueda)
        {

            Modulo_AdministracionContext db = new Modulo_AdministracionContext();
            try
            {

                //si valor_busqueda es 1 , el filtro es por cliente
                //si valor_busqueda es 1 , el filtro es por vendedor
                var clientes = (from p in db.cliente
                                where
                                      p.sn_activo == -1
                                      &&
                                      (
                                          (p.nombre_fantasia.Contains(nombreFantasia_o_nombreVendedor) && valor_busqueda == 1)
                                          ||
                                          (p.vendedor.nombre.Contains(nombreFantasia_o_nombreVendedor) && valor_busqueda == 2)
                                      )
                                select new
                                {
                                    p.id_cliente,
                                    p.nombre_fantasia,
                                    p.vendedor.nombre
                                }).ToList();


                return clientes;
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

        public static DataSet buscar_clientes_activos(string nombre_fantasia)
        {
            Modulo_AdministracionContext db = new Modulo_AdministracionContext();

            var clientes = (from c in db.cliente
                            where c.nombre_fantasia.Contains(nombre_fantasia) &&
                                  c.sn_activo == -1
                            select new
                            {
                                c.id_cliente,
                                c.nombre_fantasia,
                                c.id_condicion_factura
                            }).ToList();


            DataTable dt = new DataTable();
            dt.Columns.Add("Codigo Cliente");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Id Condicion Factura");

            foreach (var cliente in clientes)
            {
                DataRow row = dt.NewRow();
                row["Codigo Cliente"] = cliente.id_cliente;
                row["Nombre"] = cliente.nombre_fantasia;
                row["Id Condicion Factura"] = cliente.id_condicion_factura;
                dt.Rows.Add(row);
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds;
        }

        public static cliente buscar_cliente(string razon_social)
        {

            Modulo_AdministracionContext db = new Modulo_AdministracionContext();
            try
            {

                cliente cliente = db.cliente.FirstOrDefault(p => p.razon_social == razon_social);

                return cliente;
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

        public static object buscar_clientes_activos(int id_vendedor)
        {

            Modulo_AdministracionContext db = new Modulo_AdministracionContext();
            try
            {

                var clientes = (from p in db.cliente
                                where p.id_vendedor == id_vendedor && p.sn_activo == -1
                                select new
                                {
                                    p.id_cliente,
                                    p.nombre_fantasia,
                                    p.vendedor.nombre
                                }).ToList();

                return clientes;
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
