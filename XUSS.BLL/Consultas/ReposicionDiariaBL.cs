using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using XUSS.DAL.Consultas;
using DataAccess;
using System.Data;

namespace XUSS.BLL.Consultas
{
    [DataObject(true)]
    public class ReposicionDiariaBL
    {
        public DataTable GetReposicioDiaria(string conecction, string inCodemp, string filter, DateTime infechaIni, DateTime infechaFin)
        {
            ReposicionDiariaBD ObjDB = new ReposicionDiariaBD();
            SessionManager oSessionManager = new SessionManager(null);
            int a = 0, b = 0, c = 0, d = 0, e = 0, f = 0, g = 0,h = 0,i = 0;
            int aa = 0, bb = 0, cc = 0, dd = 0, ee = 0, ff = 0, gg = 0, hh = 0, ii = 0;

            DataTable dt = new DataTable();
            DataColumn caa = new DataColumn();
            caa.ColumnName = "AA";
            caa.DataType = typeof(Int32);
            caa.AllowDBNull = true;
            DataColumn cbb = new DataColumn();
            cbb.ColumnName = "BB";
            cbb.DataType = typeof(Int32);
            cbb.AllowDBNull = true;
            DataColumn ccc = new DataColumn();
            ccc.ColumnName = "CC";
            ccc.DataType = typeof(Int32);
            ccc.AllowDBNull = true;
            DataColumn cdd = new DataColumn();
            cdd.ColumnName = "DD";
            cdd.DataType = typeof(Int32);
            cdd.AllowDBNull = true;
            DataColumn cee = new DataColumn();
            cee.ColumnName = "EE";
            cee.DataType = typeof(Int32);
            cee.AllowDBNull = true;
            DataColumn cff = new DataColumn();
            cff.ColumnName = "FF";
            cff.DataType = typeof(Int32);
            cff.AllowDBNull = true;
            DataColumn cgg = new DataColumn();
            cgg.ColumnName = "GG";
            cgg.DataType = typeof(Int32);
            cgg.AllowDBNull = true;
            DataColumn chh = new DataColumn();
            chh.ColumnName = "HH";
            chh.DataType = typeof(Int32);
            chh.AllowDBNull = true;
            DataColumn cii = new DataColumn();
            cii.ColumnName = "II";
            cii.DataType = typeof(Int32);
            cii.AllowDBNull = true;

            dt.Columns.Add("BDBODEGA",typeof(string));
            dt.Columns.Add("BDNOMBRE", typeof(string));
            dt.Columns.Add("DTTIPPRO", typeof(string));
            dt.Columns.Add("TANOMBRE", typeof(string));
            dt.Columns.Add("DTCLAVE1", typeof(string));
            dt.Columns.Add("DTCLAVE2", typeof(string));
            dt.Columns.Add("DTCLAVE3", typeof(string));
            dt.Columns.Add("ASNOMBRE", typeof(string));
            dt.Columns.Add("A", typeof(Int32));
            //dt.Columns.Add("AA", typeof(Int32));            
            dt.Columns.Add(caa);
            dt.Columns.Add("B", typeof(Int32));
            //dt.Columns.Add("BB", typeof(Int32));
            dt.Columns.Add(cbb);
            dt.Columns.Add("C", typeof(Int32));            
            //dt.Columns.Add("CC", typeof(Int32));
            dt.Columns.Add(ccc);
            dt.Columns.Add("D", typeof(Int32));
            //dt.Columns.Add("DD", typeof(Int32));
            dt.Columns.Add(cdd);
            dt.Columns.Add("E", typeof(Int32));
            //dt.Columns.Add("EE", typeof(Int32));
            dt.Columns.Add(cee);
            dt.Columns.Add("F", typeof(Int32));
            //dt.Columns.Add("FF", typeof(Int32));
            dt.Columns.Add(cff);
            dt.Columns.Add("G", typeof(Int32));
            //dt.Columns.Add("GG", typeof(Int32));
            dt.Columns.Add(cgg);
            dt.Columns.Add("H", typeof(Int32));
            //dt.Columns.Add("HH", typeof(Int32));
            dt.Columns.Add(chh);
            dt.Columns.Add("I", typeof(Int32));
            //dt.Columns.Add("II", typeof(Int32));
            dt.Columns.Add(cii);
            dt.Columns.Add("X", typeof(Int32));



            try
            {
                //carga Datos
                using (IDataReader reader = ObjDB.GetLista(oSessionManager, filter, infechaIni, infechaFin))
                {
                    while (reader.Read())
                    {
                        //if (dt.Select("BDBODEGA='" + Convert.ToString(reader["BDBODEGA"]) + "' ") ) //AND DTCLAVE1 ='" + Convert.ToString(reader["DTCLAVE1"]) + "' AND DTCLAVE3 ='" + Convert.ToString(reader["DTCLAVE3"])+"'"                        
                            //DataRow[] myresult = dt.Select("BDBODEGA='" + Convert.ToString(reader["BDBODEGA"]) + "' ");
                            //if (myresult.Count() == 0)
                            //{
                                DataRow item = dt.NewRow();
                                item["BDBODEGA"] = Convert.ToString(reader["BDBODEGA"]);
                                item["BDNOMBRE"] = Convert.ToString(reader["BDNOMBRE"]);
                                item["DTTIPPRO"] = Convert.ToString(reader["DTTIPPRO"]);
                                item["TANOMBRE"] = Convert.ToString(reader["TANOMBRE"]);
                                item["DTCLAVE1"] = Convert.ToString(reader["DTCLAVE1"]);
                                //item["DTCLAVE2"] = Convert.ToString(reader["DTCLAVE2"]);
                                item["DTCLAVE3"] = Convert.ToString(reader["DTCLAVE3"]);
                                item["ASNOMBRE"] = Convert.ToString(reader["ASNOMBRE"]);
                                item["A"] = Convert.ToInt32(reader["A"]);
                                item["B"] = Convert.ToInt32(reader["B"]);
                                item["C"] = Convert.ToInt32(reader["C"]);
                                item["D"] = Convert.ToInt32(reader["D"]);
                                item["E"] = Convert.ToInt32(reader["E"]);
                                item["F"] = Convert.ToInt32(reader["F"]);
                                item["G"] = Convert.ToInt32(reader["G"]);
                                item["H"] = Convert.ToInt32(reader["H"]);
                                item["I"] = Convert.ToInt32(reader["I"]);
                                item["X"] = Convert.ToInt32(reader["X"]);
                                dt.Rows.Add(item);
                                item = null;
                            //}                        
                    }
                }
                //Recorre y aplica Distribucion
                using (IDataReader reader = ObjDB.GetListaRecorrido(oSessionManager, filter, infechaIni, infechaFin))
                {
                    while (reader.Read())
                    {
                        DataRow[] myresult = dt.Select("DTCLAVE1 ='" + Convert.ToString(reader["DTCLAVE1"]) + "' AND DTCLAVE3 ='" + Convert.ToString(reader["DTCLAVE3"]) + "' AND DTTIPPRO ='" + Convert.ToString(reader["DTTIPPRO"])+"'");
                        if (myresult.Count() > 0)
                        {
                            a = ObjDB.GetValorBodega(oSessionManager, inCodemp, Convert.ToString(reader["DTTIPPRO"]), Convert.ToString(reader["DTCLAVE1"]), "NOT IN ('3XL','18','XXXL','XXS','35','4','XS','36','6','S','37','8','M','38','10','L','39','12','XL','40','14','2XL','16','XXL','3XL','18','XXXL')", Convert.ToString(reader["DTCLAVE3"]));
                            b = ObjDB.GetValorBodega(oSessionManager, inCodemp, Convert.ToString(reader["DTTIPPRO"]), Convert.ToString(reader["DTCLAVE1"]), "IN ('XXS','35','4')", Convert.ToString(reader["DTCLAVE3"]));
                            c = ObjDB.GetValorBodega(oSessionManager, inCodemp, Convert.ToString(reader["DTTIPPRO"]), Convert.ToString(reader["DTCLAVE1"]), "IN ('XS','36','6')", Convert.ToString(reader["DTCLAVE3"]));
                            d = ObjDB.GetValorBodega(oSessionManager, inCodemp, Convert.ToString(reader["DTTIPPRO"]), Convert.ToString(reader["DTCLAVE1"]), "IN ('S','37','8')", Convert.ToString(reader["DTCLAVE3"]));
                            e = ObjDB.GetValorBodega(oSessionManager, inCodemp, Convert.ToString(reader["DTTIPPRO"]), Convert.ToString(reader["DTCLAVE1"]), "IN ('M','38','10')", Convert.ToString(reader["DTCLAVE3"]));
                            f = ObjDB.GetValorBodega(oSessionManager, inCodemp, Convert.ToString(reader["DTTIPPRO"]), Convert.ToString(reader["DTCLAVE1"]), "IN ('L','39','12') ", Convert.ToString(reader["DTCLAVE3"]));
                            g = ObjDB.GetValorBodega(oSessionManager, inCodemp, Convert.ToString(reader["DTTIPPRO"]), Convert.ToString(reader["DTCLAVE1"]), "IN ('XL','40','14')", Convert.ToString(reader["DTCLAVE3"]));
                            h = ObjDB.GetValorBodega(oSessionManager, inCodemp, Convert.ToString(reader["DTTIPPRO"]), Convert.ToString(reader["DTCLAVE1"]), "IN ('2XL','16','XXL')", Convert.ToString(reader["DTCLAVE3"]));
                            i = ObjDB.GetValorBodega(oSessionManager, inCodemp, Convert.ToString(reader["DTTIPPRO"]), Convert.ToString(reader["DTCLAVE1"]), "IN ('3XL','18','XXXL')", Convert.ToString(reader["DTCLAVE3"]));

                            foreach (DataRow row in myresult)
                            {
                                row["aa"] = 0;
                                row["bb"] = 0;
                                row["cc"] = 0;
                                row["dd"] = 0;
                                row["ee"] = 0;
                                row["ff"] = 0;
                                row["gg"] = 0;
                                row["hh"] = 0;
                                row["ii"] = 0;

                                if (Convert.ToInt32(row["a"]) > 0)                                 
                                {                                    
                                    aa = a;
                                    a = a - Convert.ToInt32(row["a"]);
                                    if (a >= 0) row["aa"] = row["a"];
                                    else row["aa"] = aa;
                                    
                                }

                                
                                if (Convert.ToInt32(row["b"]) > 0)
                                {
                                    bb = b;
                                    b = b - Convert.ToInt32(row["b"]);
                                    if (b >= 0) row["bb"] = row["b"];
                                    else row["bb"] = bb;

                                }

                                
                                if (Convert.ToInt32(row["c"]) > 0)
                                {
                                    cc = c;
                                    c = c - Convert.ToInt32(row["c"]);
                                    if (c >= 0) row["cc"] = row["c"];
                                    else row["cc"] = cc;

                                }

                                
                                if (Convert.ToInt32(row["d"]) > 0)
                                {
                                    dd = d;
                                    d = d - Convert.ToInt32(row["d"]);
                                    if (d >= 0) row["dd"] = row["d"];
                                    else row["dd"] = cc;

                                }

                                
                                if (Convert.ToInt32(row["e"]) > 0)
                                {
                                    ee = e;
                                    e = e - Convert.ToInt32(row["e"]);
                                    if (e >= 0) row["ee"] = row["e"];
                                    else row["ee"] = ee;

                                }

                                
                                if (Convert.ToInt32(row["f"]) > 0)
                                {
                                    ff = f;
                                    f = f - Convert.ToInt32(row["f"]);
                                    if (f >= 0) row["ff"] = row["f"];
                                    else row["ff"] = ff;

                                }
                                
                                if (Convert.ToInt32(row["g"]) > 0)
                                {
                                    gg = g;
                                    g = g - Convert.ToInt32(row["g"]);
                                    if (g >= 0) row["gg"] = row["g"];
                                    else row["gg"] = gg;

                                }

                                
                                if (Convert.ToInt32(row["h"]) > 0)
                                {
                                    hh = h;
                                    h = h - Convert.ToInt32(row["h"]);
                                    if (h >= 0) row["hh"] = row["h"];
                                    else row["hh"] = hh;

                                }

                                if (Convert.ToInt32(row["i"]) > 0)
                                {
                                    ii = i;
                                    i = i - Convert.ToInt32(row["i"]);
                                    if (i >= 0) row["ii"] = row["i"];
                                    else row["ii"] = ii;

                                }
                                


                            }
                        }
                    }
                }

                dt.AcceptChanges();
                foreach (DataRow row in dt.Rows)
                {
                    if (row.IsNull("AA")) row["AA"] = 0;
                    if (row.IsNull("BB")) row["BB"] = 0;
                    if (row.IsNull("CC")) row["CC"] = 0;
                    if (row.IsNull("DD")) row["DD"] = 0;
                    if (row.IsNull("EE")) row["EE"] = 0;
                    if (row.IsNull("FF")) row["FF"] = 0;
                    if (row.IsNull("GG")) row["GG"] = 0;
                    if (row.IsNull("HH")) row["HH"] = 0;
                    if (row.IsNull("II")) row["II"] = 0;
                    


                    if ((Convert.ToInt32(row["AA"]) <= 0) && (Convert.ToInt32(row["BB"]) <= 0) && (Convert.ToInt32(row["CC"]) <= 0) && (Convert.ToInt32(row["DD"]) <= 0) 
                        && (Convert.ToInt32(row["EE"]) <= 0) && (Convert.ToInt32(row["FF"]) <= 0) && (Convert.ToInt32(row["GG"]) <= 0) && (Convert.ToInt32(row["HH"]) <= 0) 
                        && (Convert.ToInt32(row["II"]) <= 0))
                        row.Delete();
                    else
                    {
                        if (Convert.ToInt32(row["AA"]) <= 0) row["AA"] = DBNull.Value;
                        if (Convert.ToInt32(row["BB"]) <= 0) row["BB"] = DBNull.Value;
                        if (Convert.ToInt32(row["CC"]) <= 0) row["CC"] = DBNull.Value;
                        if (Convert.ToInt32(row["DD"]) <= 0) row["DD"] = DBNull.Value;
                        if (Convert.ToInt32(row["EE"]) <= 0) row["EE"] = DBNull.Value;
                        if (Convert.ToInt32(row["FF"]) <= 0) row["FF"] = DBNull.Value;
                        if (Convert.ToInt32(row["GG"]) <= 0) row["GG"] = DBNull.Value;
                        if (Convert.ToInt32(row["HH"]) <= 0) row["HH"] = DBNull.Value;
                        if (Convert.ToInt32(row["II"]) <= 0) row["II"] = DBNull.Value;
                    }
                    
                }
                dt.AcceptChanges();
    
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                caa = null;
                cbb = null;
                ccc = null;
                cdd = null;
                cee = null;
                cff = null;
                cgg = null;
                chh = null;
                cii = null;

                ObjDB = null;
                oSessionManager = null;
            }
        }
    }
}
