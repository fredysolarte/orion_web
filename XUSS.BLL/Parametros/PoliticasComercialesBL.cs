using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XUSS.DAL.Parametros;

namespace XUSS.BLL.Parametros
{
    public class PoliticasComercialesBL
    {
        public DataTable GetPoliticaHD(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return PoliticasComercialesBD.GetPoliticaHD(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetPoliticaDT(string connection, string filter, int ID_DESCUENTO, string CODEMP)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return PoliticasComercialesBD.GetPoliticaDT(oSessionManager, filter, ID_DESCUENTO, CODEMP);
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
        public int InsertPoliticaDT(string connection, int ID_DESCUENTO, string BODEGA, string TP, string CLAVE1, string CLAVE2, string CLAVE3, string CLAVE4, double? VALOR, string CONDICION_1,
                                           string CONDICION_2, DateTime? FECHAINI, DateTime? FECHAFIN, string USUARIO, string ESTADO, double CONDICION_3, double CONDICION_4, int CONDICION_5)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try {
                return PoliticasComercialesBD.InsertPoliticaDT(oSessionManager, ID_DESCUENTO, BODEGA, TP, CLAVE1, CLAVE2, CLAVE3, CLAVE4, VALOR, CONDICION_1, CONDICION_2, FECHAINI, FECHAFIN, USUARIO, ESTADO, CONDICION_3, CONDICION_4, CONDICION_5);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                oSessionManager = null;
            }
        }
        public int InsertPoliticaDT(string connection, int ID_DESCUENTO, object tbBodega, object tbCategoria, object tbArticulos, DateTime? FECHAINI, DateTime? FECHAFIN,double? VALOR, string USUARIO)
        {
            Boolean lb_indbodega = true,lb_indarticulo = true,lb_indcategoria= true;
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                //Valida Todos los almacenes
                foreach (DataRow rw in (tbBodega as DataTable).Rows)
                {
                    if (Convert.ToString(rw["CHK"]) == "N")
                        lb_indbodega = false;
                }
                //Valida Todas las Lineas y Articulos
                foreach (DataRow rw in (tbCategoria as DataTable).Rows)
                {
                    if (Convert.ToString(rw["CHK"]) == "S")
                    {
                        foreach (DataRow rx in (tbArticulos as DataTable).Rows)
                        {
                            if (Convert.ToString(rw["COD"]) == Convert.ToString(rx["TP"]))
                            {
                                if (Convert.ToString(rx["CHK"]) == "N")
                                    lb_indarticulo = false;                                
                            }
                        }
                    }
                }

                if (lb_indbodega && lb_indcategoria && lb_indarticulo)
                {
                    PoliticasComercialesBD.InsertPoliticaDT(oSessionManager, ID_DESCUENTO, ".", ".", ".", ".", ".", ".", VALOR, null, null, FECHAINI, FECHAFIN, USUARIO, "AC", 0, 0, 0);
                }
                else
                {
                    if (lb_indbodega)
                    {
                        foreach (DataRow rw in (tbCategoria as DataTable).Rows)
                        {
                            if (Convert.ToString(rw["CHK"]) == "S")
                            {
                                foreach (DataRow rx in (tbArticulos as DataTable).Rows)
                                {
                                    if (Convert.ToString(rw["COD"]) == Convert.ToString(rx["COD"]))
                                    {
                                        if (Convert.ToString(rx["CHK"]) == "N")
                                            lb_indarticulo = false;
                                    }
                                }
                            }
                            if (Convert.ToString(rw["CHK"]) == "S")
                            {
                                //Inserta Por Categoria
                                if (lb_indarticulo)
                                {
                                    PoliticasComercialesBD.InsertPoliticaDT(oSessionManager, ID_DESCUENTO, ".", Convert.ToString(rw["COD"]), ".", ".", ".", ".", VALOR, null, null, FECHAINI, FECHAFIN, USUARIO, "AC", 0, 0, 0);
                                }
                                else
                                {
                                    foreach (DataRow rx in (tbArticulos as DataTable).Rows)
                                    {
                                        if (Convert.ToString(rw["COD"]) == Convert.ToString(rx["TP"]))
                                        {
                                            if (Convert.ToString(rx["CHK"]) == "S")
                                            {
                                                PoliticasComercialesBD.InsertPoliticaDT(oSessionManager, ID_DESCUENTO, ".", Convert.ToString(rw["COD"]), Convert.ToString(rx["COD"]), ".", ".", ".", VALOR, null, null, FECHAINI, FECHAFIN, USUARIO, "AC", 0, 0, 0);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //Valida Todos los almacenes
                        foreach (DataRow ry in (tbBodega as DataTable).Rows)
                        {
                            if (Convert.ToString(ry["CHK"]) == "S")
                            {
                                foreach (DataRow rw in (tbCategoria as DataTable).Rows)
                                {
                                    if (Convert.ToString(rw["CHK"]) == "S")
                                    {
                                        foreach (DataRow rx in (tbArticulos as DataTable).Rows)
                                        {
                                            if (Convert.ToString(rw["COD"]) == Convert.ToString(rx["TP"]))
                                            {
                                                if (Convert.ToString(rx["CHK"]) == "N")
                                                    lb_indarticulo = false;
                                            }
                                        }
                                    }
                                    if (Convert.ToString(rw["CHK"]) == "S")
                                    {
                                        //Inserta Por Categoria
                                        if (lb_indarticulo)
                                        {
                                            PoliticasComercialesBD.InsertPoliticaDT(oSessionManager, ID_DESCUENTO, Convert.ToString(ry["COD"]), Convert.ToString(rw["COD"]), ".", ".", ".", ".", VALOR, null, null, FECHAINI, FECHAFIN, USUARIO, "AC", 0, 0, 0);
                                        }
                                        else
                                        {
                                            foreach (DataRow rx in (tbArticulos as DataTable).Rows)
                                            {
                                                if (Convert.ToString(rw["COD"]) == Convert.ToString(rx["TP"]))
                                                {
                                                    if (Convert.ToString(rx["CHK"]) == "S")
                                                    {
                                                        PoliticasComercialesBD.InsertPoliticaDT(oSessionManager, ID_DESCUENTO, Convert.ToString(ry["COD"]), Convert.ToString(rw["COD"]), Convert.ToString(rx["COD"]), ".", ".", ".", VALOR, null, null, FECHAINI, FECHAFIN, USUARIO, "AC", 0, 0, 0);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                

                return 0;
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
        public int AnularPoliticaDT(string connection, int ID, string USUARIO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return PoliticasComercialesBD.AnularPolitaDT(oSessionManager, ID,USUARIO);
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
    }
}
