using Modulo_Administracion.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Modulo_Administracion.Logica
{
    static class Logica_Geografia
    {
        public static DataSet buscar_calles_por_txtDesc(List<string> Calle)
        {
            try
            {

                Modulo_AdministracionContext db = new Modulo_AdministracionContext();
                string calle_busqueda = Calle[0].ToString();
                var calles = (from tc in db.tcalle
                              where tc.txt_desc.Contains(calle_busqueda)
                              select new
                              {
                                  tc.cod_calle,
                                  tc.txt_desc
                              }).ToList();


                DataTable dt = new DataTable();
                dt.Columns.Add("Codigo_calle");
                dt.Columns.Add("Calle");

                foreach (var calle in calles)
                {
                    DataRow row = dt.NewRow();
                    row["Codigo_calle"] = calle.cod_calle;
                    row["Calle"] = calle.txt_desc;
                    dt.Rows.Add(row);
                }

                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                return ds;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataSet buscar_municipio_por_codPais_codProvincia_y_txtDesc(List<string> Municipio)
        {
            Modulo_AdministracionContext db = new Modulo_AdministracionContext();

            int cod_pais = Convert.ToInt32(Municipio[0].ToString());
            int cod_provincia = Convert.ToInt32(Municipio[1].ToString());
            string municipio_busqueda = Municipio[2].ToString();


            var municipios = (from tm in db.tmunicipio
                              where tm.cod_pais == cod_pais &&
                                    tm.cod_provincia == cod_provincia &&
                                    tm.txt_desc.Contains(municipio_busqueda)
                              select new
                              {
                                  tm.cod_municipio,
                                  tm.txt_desc,
                                  tm.cod_divipola
                              }).Distinct().ToList();


            DataTable dt = new DataTable();
            dt.Columns.Add("cod_municipio");
            dt.Columns.Add("Municipio");
            dt.Columns.Add("Codigo_Postal");

            foreach (var municipio in municipios)
            {
                DataRow row = dt.NewRow();
                row["cod_municipio"] = municipio.cod_municipio;
                row["Municipio"] = municipio.txt_desc;
                row["Codigo_Postal"] = municipio.cod_divipola;
                dt.Rows.Add(row);
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds;
        }

        public static DataSet buscar_provincia_por_codPais_y_txtDesc(List<string> Provincia)
        {
            Modulo_AdministracionContext db = new Modulo_AdministracionContext();

            int cod_pais = Convert.ToInt32(Provincia[0].ToString());
            string provincia_busqueda = Provincia[1].ToString();


            var provincias = (from tp in db.tprovincia
                              where tp.cod_pais == cod_pais &&
                                    tp.txt_desc.Contains(provincia_busqueda)
                              select new
                              {
                                  tp.cod_provincia,
                                  tp.txt_desc
                              }).ToList();


            DataTable dt = new DataTable();
            dt.Columns.Add("cod_provincia");
            dt.Columns.Add("Provincia");

            foreach (var provincia in provincias)
            {
                DataRow row = dt.NewRow();
                row["cod_provincia"] = provincia.cod_provincia;
                row["Provincia"] = provincia.txt_desc;
                dt.Rows.Add(row);
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds;
        }

        public static DataSet buscar_pais_por_txtDesc(List<string> Pais)
        {
            Modulo_AdministracionContext db = new Modulo_AdministracionContext();


            string pais_busqueda = Pais[0].ToString();


            var paises = (from tp in db.tpais
                          where tp.txt_desc.Contains(pais_busqueda)
                          select new
                          {
                              tp.cod_pais,
                              tp.txt_desc
                          }).ToList();


            DataTable dt = new DataTable();
            dt.Columns.Add("Codigo_Pais");
            dt.Columns.Add("Pais");

            foreach (var pais in paises)
            {
                DataRow row = dt.NewRow();
                row["Codigo_Pais"] = pais.cod_pais;
                row["Pais"] = pais.txt_desc;
                dt.Rows.Add(row);
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds;
        }
    }
}
