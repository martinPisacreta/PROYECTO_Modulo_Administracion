using Modulo_Administracion.Clases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Modulo_Administracion.Logica
{
    static class Logica_Articulo
    {


        public static void modificar_articulos_por_metodo_actualizar_porcentaje(DataTable dt)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Modulo_AdministracionContext"].ConnectionString))
            {
                connection.Open();
                using (SqlTransaction sqlTransaction = connection.BeginTransaction())
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, sqlTransaction))
                    {
                        try
                        {


                            SqlCommand crear_tabla = new SqlCommand(
                                                                        "create table #tmp_articulos" +
                                                                        "(" +
                                                                            "id_articulo bigint, " +
                                                                            "precio_lista numeric(18, 4)" +
                                                                        ")", connection, sqlTransaction
                                                                    );

                            SqlCommand actualizar_precios = new SqlCommand(
                                                                            "update " +
                                                                                "art " +
                                                                            "set " +
                                                                                "art.precio_lista = tmp.precio_lista," +
                                                                                "art.fecha_ult_modif = GETDATE(),art.accion = 'MODIFICACION' " +
                                                                            "from	" +
                                                                                "articulo art inner join #tmp_articulos tmp on tmp.id_articulo = art.id_articulo", connection, sqlTransaction
                                                                            );

                            crear_tabla.ExecuteNonQuery();

                            bulkCopy.BulkCopyTimeout = 180;
                            bulkCopy.DestinationTableName = "#tmp_articulos";
                            bulkCopy.WriteToServer(dt);

                            actualizar_precios.ExecuteNonQuery();

                            sqlTransaction.Commit();
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

        //SI "busqueda" ES 1 -> BUSCO LOS ARTICULOS EXISTENTES EN LA BASE DE DATOS EN RELACION A LA TABLA #tmp_lista_precios_proveedor
        //SI "busqueda" ES 2 -> BUSCO LOS ARTICULOS INEEXISTENTES EN LA BASE DE DATOS EN RELACION A LA TABLA #tmp_lista_precios_proveedor
        public static DataSet buscar_articulos_en_relacion_a_tmpListaPreciosProveedor(DataTable dt, int id_proveedor)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Modulo_AdministracionContext"].ConnectionString))
            {
                connection.Open();
                using (SqlTransaction sqlTransaction = connection.BeginTransaction())
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, sqlTransaction))
                    {
                        try
                        {



                            SqlCommand crear_tabla_tmp_lista_precios_proveedor = new SqlCommand(
                                                                                 "create table #tmp_lista_precios_proveedor" +
                                                                                 "(" +

                                                                                    "codigo_articulo nvarchar(100) NULL," +
                                                                                    "descripcion_articulo nvarchar(400) NULL," +
                                                                                    "precio_lista nvarchar(22) NULL," +
                                                                                    "id_proveedor int NULL" +
                                                                                ")", connection, sqlTransaction
                                                                               );



                            crear_tabla_tmp_lista_precios_proveedor.ExecuteNonQuery();


                            bulkCopy.BulkCopyTimeout = 180;
                            bulkCopy.DestinationTableName = "#tmp_lista_precios_proveedor";
                            bulkCopy.WriteToServer(dt);


                            DataSet dataSet = new DataSet();

                            SqlCommand command = new SqlCommand("articulo_buscar_en_relacion_a_tmpListaPreciosProveedor", connection, sqlTransaction);
                            SqlParameter param = new SqlParameter();
                            param.ParameterName = "@id_proveedor";
                            param.Value = id_proveedor;
                            param.SqlDbType = SqlDbType.Int;
                            command.Parameters.Add(param);

                            command.CommandTimeout = 0;

                            command.CommandType = CommandType.StoredProcedure;

                            SqlDataAdapter adapter = new SqlDataAdapter();
                            adapter.SelectCommand = command;
                            adapter.Fill(dataSet);


                            sqlTransaction.Commit();
                            return dataSet;

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

        public static DataSet alta_articulos_por_metodo_subida_excelMaxi(DataTable dt)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Modulo_AdministracionContext"].ConnectionString))
            {
                connection.Open();
                using (SqlTransaction sqlTransaction = connection.BeginTransaction())
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, sqlTransaction))
                    {
                        try
                        {

                            bulkCopy.BulkCopyTimeout = 180;
                            bulkCopy.DestinationTableName = "articulo_tmp";
                            bulkCopy.WriteToServer(dt);


                            DataSet dataSet = new DataSet();

                            SqlCommand command = new SqlCommand("articulo_alta_por_metodo_subida_excelMaxi", connection, sqlTransaction);
                            command.CommandTimeout = 0;

                            command.CommandType = CommandType.StoredProcedure;

                            SqlDataAdapter adapter = new SqlDataAdapter();
                            adapter.SelectCommand = command;
                            adapter.Fill(dataSet);

                            sqlTransaction.Commit();

                            return dataSet;

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



        public static DataSet buscar_articulo_activo(string codigo_articulo_marca, string codigo_articulo)
        {

            Modulo_AdministracionContext db = new Modulo_AdministracionContext();

            var articulo = (from a in db.articulo
                            join f in db.familia on a.id_tabla_familia equals f.id_tabla_familia
                            where a.codigo_articulo_marca == codigo_articulo_marca &&
                                  a.codigo_articulo == codigo_articulo &&
                                  a.fec_baja == null //ARTICULOS ACTIVOS
                            select new
                            {

                                descripcion_articulo = a.descripcion_articulo,
                                precio_lista = a.precio_lista,
                                id_tabla_familia = a.id_tabla_familia,
                                id_articulo = a.id_articulo,

                                algoritmo_1 = f.algoritmo_1,
                                algoritmo_2 = f.algoritmo_2,
                                algoritmo_3 = f.algoritmo_3,
                                algoritmo_4 = f.algoritmo_4,
                                algoritmo_5 = f.algoritmo_5,
                                algoritmo_6 = f.algoritmo_6,
                                algoritmo_7 = f.algoritmo_7,
                                algoritmo_8 = f.algoritmo_8,
                                algoritmo_9 = f.algoritmo_9,
                            }).FirstOrDefault();



            DataTable dt = new DataTable();
            dt.Columns.Add("descripcion_articulo");
            dt.Columns.Add("precio_lista");
            dt.Columns.Add("id_tabla_familia");
            dt.Columns.Add("id_articulo");
            dt.Columns.Add("coeficiente");

            if (articulo != null)
            {
                decimal coeficiente = Logica_Familia.precio_coeficiente(2, articulo.algoritmo_1, articulo.algoritmo_2, articulo.algoritmo_3, articulo.algoritmo_4, articulo.algoritmo_5, articulo.algoritmo_6, articulo.algoritmo_7, articulo.algoritmo_8, articulo.algoritmo_9);

                DataRow row = dt.NewRow();
                row["descripcion_articulo"] = articulo.descripcion_articulo;
                row["precio_lista"] = articulo.precio_lista;
                row["id_tabla_familia"] = articulo.id_tabla_familia;
                row["id_articulo"] = articulo.id_articulo;
                row["coeficiente"] = coeficiente;
                dt.Rows.Add(row);
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds;


            //using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Modulo_AdministracionContext"].ConnectionString))
            //{
            //    connection.Open();
            //    using (SqlTransaction sqlTransaction = connection.BeginTransaction())
            //    {

            //        try
            //        {

            //            DataSet ds = new DataSet();

            //            //store
            //            SqlCommand command = new SqlCommand("articulo_buscar_por_codigoArticuloMarca_codigoArticulo", connection, sqlTransaction);

            //            //parametros
            //            command.Parameters.AddWithValue("@codigo_articulo_marca", codigo_articulo_marca);
            //            command.Parameters.AddWithValue("@codigo_articulo", codigo_articulo);

            //            //tiempo y tipo
            //            command.CommandTimeout = 0;
            //            command.CommandType = CommandType.StoredProcedure;

            //            SqlDataAdapter adapter = new SqlDataAdapter();
            //            adapter.SelectCommand = command;
            //            adapter.Fill(ds);
            //            sqlTransaction.Commit();
            //            return ds;

            //        }
            //        catch (Exception ex)
            //        {
            //            sqlTransaction.Rollback();
            //            throw ex;
            //        }
            //    }
            //}
        }

        public static bool dar_de_baja_articulos(int id_tabla_familia, Modulo_AdministracionContext db) //doy de baja los articulos de una familia
        {

            bool bandera = false;
            try
            {

                List<articulo> lista_articulos = (from p in db.articulo
                                                  where p.id_tabla_familia == id_tabla_familia
                                                  select p).ToList();

                if (lista_articulos != null)
                {
                    foreach (articulo p in lista_articulos)
                    {
                        //pongo en factura_detalle NULL en el id_articulo enviado por parametro -> ya que se da de baja ese articulo , pero debe seguir figurando en el detalle de factura
                        if (Logica_Factura_Detalle.modificar_facturaDetalle(p.id_articulo, db) == false)
                        {
                            throw new Exception("Error al modificar id articulo en factura detalle");
                        }

                        p.fec_baja = DateTime.Today;
                        p.fecha_ult_modif = DateTime.Now;
                        p.accion = "ELIMINACION";
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




        public static DataSet buscar_articulos_activos(int id_proveedor, int id_tabla_marca, int id_tabla_familia, string cod_articulo, string descripcion)
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
                        SqlCommand command = new SqlCommand("articulo_buscar_por_idProveedor_idTablaMarca_idTablaFamilia_codArticulo_descripcionArticulo", connection, sqlTransaction);

                        //parametros
                        command.Parameters.AddWithValue("@id_proveedor", id_proveedor);
                        command.Parameters.AddWithValue("@id_tabla_marca", id_tabla_marca);
                        command.Parameters.AddWithValue("@id_tabla_familia", id_tabla_familia);
                        command.Parameters.AddWithValue("@cod_articulo", cod_articulo);
                        command.Parameters.AddWithValue("@descripcion", descripcion);

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


        public static DataSet buscar_articulos_activos()
        {
            Modulo_AdministracionContext db = new Modulo_AdministracionContext();

            var articulos = (from a in db.articulo
                             join f in db.familia on a.id_tabla_familia equals f.id_tabla_familia
                             where a.fec_baja == null //ARTICULOS ACTIVOS
                             orderby a.id_orden ascending
                             select new
                             {
                                 codigo_articulo_marca = a.codigo_articulo_marca,
                                 codigo_articulo = a.codigo_articulo,
                                 descripcion_articulo = a.descripcion_articulo,
                                 precio_lista = a.precio_lista,
                                 id_tabla_familia = a.id_tabla_familia,
                                 sn_oferta = a.sn_oferta,
                                 path_img = a.path_img, //ESTE DATO SOLAMENTE EXISTE EN LA BD DE LA WEB
                                 id_articulo = a.id_articulo,
                                 id_orden = a.id_orden,
                                 algoritmo_1 = f.algoritmo_1,
                                 algoritmo_2 = f.algoritmo_2,
                                 algoritmo_3 = f.algoritmo_3,
                                 algoritmo_4 = f.algoritmo_4,
                                 algoritmo_5 = f.algoritmo_5,
                                 algoritmo_6 = f.algoritmo_6,
                                 algoritmo_7 = f.algoritmo_7,
                                 algoritmo_8 = f.algoritmo_8,
                                 algoritmo_9 = f.algoritmo_9,
                             }).ToList();


            DataTable dt = new DataTable();
            dt.Columns.Add("codigo_articulo_marca");
            dt.Columns.Add("codigo_articulo");
            dt.Columns.Add("descripcion_articulo");
            dt.Columns.Add("precio_lista");
            dt.Columns.Add("coeficiente");
            dt.Columns.Add("precio_final");
            dt.Columns.Add("id_tabla_familia");
            dt.Columns.Add("sn_oferta");
            dt.Columns.Add("path_img");
            dt.Columns.Add("id_articulo");
            dt.Columns.Add("id_orden");

            if (articulos != null)
            {
                foreach (var articulo in articulos)
                {

                    decimal coeficiente = Logica_Familia.precio_coeficiente(2, articulo.algoritmo_1, articulo.algoritmo_2, articulo.algoritmo_3, articulo.algoritmo_4, articulo.algoritmo_5, articulo.algoritmo_6, articulo.algoritmo_7, articulo.algoritmo_8, articulo.algoritmo_9);


                    DataRow row = dt.NewRow();
                    row["codigo_articulo_marca"] = articulo.codigo_articulo_marca;
                    row["codigo_articulo"] = articulo.codigo_articulo;
                    row["descripcion_articulo"] = articulo.descripcion_articulo;
                    row["precio_lista"] = articulo.precio_lista;
                    row["coeficiente"] = coeficiente;
                    row["precio_final"] = articulo.precio_lista * coeficiente;
                    row["id_tabla_familia"] = articulo.id_tabla_familia;
                    row["sn_oferta"] = articulo.sn_oferta;
                    row["path_img"] = articulo.path_img;
                    row["id_articulo"] = articulo.id_articulo;
                    row["id_orden"] = articulo.id_orden;
                    dt.Rows.Add(row);
                }
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds;
        }

        public static bool modificar_articulos_existentes_en_relacion_a_ListaProveedor(List<articulo> lista_articulo, Modulo_AdministracionContext db)
        {
            try
            {
                foreach (articulo a in lista_articulo)
                {
                    if (modificar_articulo(a, db) == false)
                    {
                        throw new Exception("Error al modificar los articulos");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public static bool modificar_articulo(articulo articulo, Modulo_AdministracionContext db)
        {
            try
            {


                articulo articulo_db = db.articulo.FirstOrDefault(f => f.id_articulo == articulo.id_articulo);

                if (articulo.descripcion_articulo != null)
                {
                    articulo_db.descripcion_articulo = articulo.descripcion_articulo;
                }
                if (articulo.precio_lista != null)
                {
                    articulo_db.precio_lista = articulo.precio_lista;
                }

                articulo_db.fecha_ult_modif = DateTime.Now;
                articulo_db.accion = "MODIFICACION";
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static bool alta_articulos_inexistentes_en_relacion_a_ListaProveedor(List<articulo> lista_articulo, Modulo_AdministracionContext db)
        {


         
            try
            {
                foreach (articulo articulo in lista_articulo)
                {
                    if (alta_articulo(articulo, db) == false)
                    {
                        throw new Exception("Error al dar de alta los articulos");
                    }
                }
                

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static bool alta_articulo(articulo articulo, Modulo_AdministracionContext db)
        {
            try
            {
                bool bandera = false;
                articulo articulo_a_insertar = new articulo();
                familia familia_articulo = Logica_Familia.buscar_familia(articulo.id_tabla_familia.Value);

                //id_articulo es identity
                articulo_a_insertar.codigo_articulo_marca = familia_articulo.marca.txt_desc_marca;
                articulo_a_insertar.codigo_articulo = articulo.codigo_articulo;
                articulo_a_insertar.descripcion_articulo = articulo.descripcion_articulo;
                articulo_a_insertar.precio_lista = articulo.precio_lista;
                articulo_a_insertar.id_tabla_familia = articulo.id_tabla_familia;
                articulo_a_insertar.sn_oferta = 0;
                articulo_a_insertar.path_img = "";
                articulo_a_insertar.fecha_ult_modif = DateTime.Now;
                articulo_a_insertar.fec_baja = null;
                articulo_a_insertar.accion = "ALTA";

                int cantidad = db.articulo.Count();
                if (cantidad == 0)
                {
                    articulo_a_insertar.id_orden = 1;
                }
                else
                {
                    articulo_a_insertar.id_orden = db.articulo.Max(a => a.id_orden) + 1;
                }
                db.articulo.Add(articulo_a_insertar);
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
