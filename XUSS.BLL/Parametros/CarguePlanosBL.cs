using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XUSS.DAL.Parametros;

namespace XUSS.BLL.Parametros
{
    public class CarguePlanosBL
    {
        public DataTable GetTablas(string connection)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return CarguePlanosBD.GetTablas(oSessionManager);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }

        }
        public DataTable GetCampos(string connection, int object_id)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return CarguePlanosBD.GetCampos(oSessionManager,object_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }

        }

        public int GenerarCargue(string connection, string tabla, object inCampos, object inDatos)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            object[] lst_datos = new object[(inCampos as DataTable).Rows.Count];
             int ln_contador = 0;
            string lc_insert = "INSERT INTO " + tabla + "( ";
            try
            {
                oSessionManager.BeginTransaction();
                //CONSTRUIR INSERT
                foreach (DataRow rwc in (inCampos as DataTable).Rows)
                {
                    lc_insert += Convert.ToString(rwc["nom_campo"]) + ",";
                }
                ln_contador = 0;
                lc_insert = lc_insert.Substring(0, lc_insert.Length - 1)+") VALUES (";
                foreach (DataRow rwc in (inCampos as DataTable).Rows)
                {
                    lc_insert += "@p" + Convert.ToString(ln_contador)+",";
                    ln_contador++;
                }
                lc_insert = lc_insert.Substring(0, lc_insert.Length - 1) + ")";
                // CONSTRUCCION INSERT
                int i = 0;
                for (i = 0; i < (inDatos as DataTable).Rows.Count; i++)
                {
                    int ln_i = 0;
                    foreach (DataRow rwc in (inCampos as DataTable).Rows)
                    {
                        string lc_columna = Convert.ToString(rwc["campo"]);
                        string lc_tipo = Convert.ToString(rwc["nom_tipo"]);
                        foreach (DataRow rwd in (inDatos as DataTable).Rows)
                        {
                            if (!string.IsNullOrWhiteSpace(lc_columna))
                            {
                                switch (lc_tipo)
                                {
                                    case "text": lst_datos[ln_i] = Convert.ToString(rwd[lc_columna]); break;
                                    case "char": lst_datos[ln_i] = Convert.ToChar(rwd[lc_columna]); break;
                                    case "nchar": lst_datos[ln_i] = Convert.ToString(rwd[lc_columna]); break;
                                    case "varchar": lst_datos[ln_i] = Convert.ToString(rwd[lc_columna]); break;
                                    case "date": lst_datos[ln_i] = Convert.ToDateTime(rwd[lc_columna]); break;
                                    case "time": lst_datos[ln_i] = Convert.ToDateTime(rwd[lc_columna]); break;
                                    case "datetime2": lst_datos[ln_i] = Convert.ToDateTime(rwd[lc_columna]); break;
                                    case "datetimeoffset": lst_datos[ln_i] = Convert.ToDateTime(rwd[lc_columna]); break;
                                    case "smalldatetime": lst_datos[ln_i] = Convert.ToDateTime(rwd[lc_columna]); break;
                                    case "datetime": lst_datos[ln_i] = Convert.ToDateTime(rwd[lc_columna]); break;
                                    case "tinyint": lst_datos[ln_i] = Convert.ToInt32(rwd[lc_columna]); break;
                                    case "smallint": lst_datos[ln_i] = Convert.ToInt32(rwd[lc_columna]); break;
                                    case "int": lst_datos[ln_i] = Convert.ToInt32(rwd[lc_columna]); break;
                                    case "real": lst_datos[ln_i] = Convert.ToDouble(rwd[lc_columna]); break;
                                    case "money": lst_datos[ln_i] = Convert.ToDouble(rwd[lc_columna]); break;
                                    case "float": lst_datos[ln_i] = Convert.ToDouble(rwd[lc_columna]); break;
                                    case "bit": lst_datos[ln_i] = Convert.ToByte(rwd[lc_columna]); break;
                                    case "decimal": lst_datos[ln_i] = Convert.ToDouble(rwd[lc_columna]); break;
                                    case "numeric": lst_datos[ln_i] = Convert.ToDouble(rwd[lc_columna]); break;
                                    case "smallmoney": lst_datos[ln_i] = Convert.ToDouble(rwd[lc_columna]); break;
                                    case "bigint": lst_datos[ln_i] = Convert.ToInt32(rwd[lc_columna]); break;
                                    case "timestamp": lst_datos[ln_i] = Convert.ToDateTime(rwd[lc_columna]); break;
                                }
                            }
                            else
                            {
                                lst_datos[ln_i] = null;
                            }
                            break;
                        }
                        ln_i++;
                        //
                    }
                }

                CarguePlanosBD.EjecucionInsert(oSessionManager, lc_insert, lst_datos);

                oSessionManager.CommitTranstaction();
                return 0;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
    }
}
