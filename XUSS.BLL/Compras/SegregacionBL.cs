using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XUSS.BLL.Comun;
using XUSS.DAL.Compras;
using XUSS.DAL.Comun;
using XUSS.DAL.Parametros;

namespace XUSS.BLL.Compras
{
    public class SegregacionBL
    {
        public DataTable GetSegregacionHD(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return SegregacionBD.GetSegregacionHD(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetSegregacionDT(string connection, string FD_CODEMP, int SG_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return SegregacionBD.GetSegregacionDT(oSessionManager, SG_CODIGO);
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
        public DataTable GetSegregacionProforma(string connection, string FD_CODEMP, int SGH_CODIGO,string SGH_TIPO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                //if (SGH_TIPO == "PR")
                    return SegregacionBD.GetSegregacionProforma(oSessionManager, SGH_CODIGO);
                //else
                //    return SegregacionBD.GetSegregacionFactura(oSessionManager, SGH_CODIGO);
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
        public DataTable GetSegregacionFactura(string connection, string FD_CODEMP, int SGH_CODIGO, string SGH_TIPO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {                
                    return SegregacionBD.GetSegregacionFactura(oSessionManager, SGH_CODIGO);
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
        public DataTable GetSegregacionFactura(string connection, int SGH_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return SegregacionBD.GetSegregacionFacturaTraslados(oSessionManager, SGH_CODIGO);
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
        public DataTable GetSegregacionProformasBodegasDT(string connection, string FD_CODEMP, int SGH_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return SegregacionBD.GetSegregacionProformasBodegasDT(oSessionManager, FD_CODEMP, SGH_CODIGO);
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
        public DataTable GetSegregacionBodegasDT(string connection, string FD_CODEMP, int SG_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return SegregacionBD.GetSegregacionBodegasDT(oSessionManager, FD_CODEMP, SG_CODIGO);
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
        public DataTable GetProformasProveedor(string connection, int CH_PROVEEDOR, string inFilter)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return SegregacionBD.GetProformasProveedor(oSessionManager, CH_PROVEEDOR, inFilter);
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
        public DataTable GetFacturasProveedor(string connection, int CH_PROVEEDOR, string inFilter)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return SegregacionBD.GetFacturasProveedor(oSessionManager, CH_PROVEEDOR, inFilter);
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
        public DataTable GetDetalleSegregacionFacturaDT(string connection, int SGH_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return SegregacionBD.GetDetalleSegregacionFacturaDT(oSessionManager, SGH_CODIGO);
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
        public int InsertSegregacion(string connection, string HDCODEMP, string HDTIPFAC, int HDNROFAC, string SGH_NROFAC, DateTime? SG_FECPROFORMA,string SG_NROPROFORMA,int? SG_VENDEDORPRO,int? SG_COMPRADORPRO, string SGH_TIPO, int SGH_PROVEEDOR, string SGH_OBSERVACIONES, string SGH_BODCAN, string SGH_BODDIF, string SGH_PARCIAL, string SGH_ESTADO, string SGH_USUARIO, object inDt, object tbBodega)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                oSessionManager.BeginTransaction();
                int ln_consecutivo = ComunBL.GeneraConsecutivo(connection, "SEGRE");
                SegregacionBD.InsertSegregacionHD(oSessionManager, ln_consecutivo,HDCODEMP, HDTIPFAC, HDNROFAC, SGH_NROFAC, SG_FECPROFORMA, SG_NROPROFORMA, SG_VENDEDORPRO, SG_COMPRADORPRO, SGH_TIPO, SGH_PROVEEDOR, SGH_OBSERVACIONES, SGH_BODCAN, SGH_BODDIF, SGH_PARCIAL, "AC", SGH_USUARIO);
                if (SGH_TIPO == "FA")
                {
                    foreach (DataRow rw in ((DataTable)inDt).Rows)
                        SegregacionBD.InsertSegregacionFactura(oSessionManager, ln_consecutivo, HDCODEMP, Convert.ToInt32(rw["PR_NROCMP"]), Convert.ToInt32(rw["PR_NROITEM"]), Convert.ToInt32(rw["SGD_CANTIDAD"]), Convert.ToInt32(rw["PR_CANTIDAD"]), Convert.ToInt32(rw["PR_CANTIDAD"]), Convert.ToDouble(rw["SGD_PRECIO"]), ".", "gp_detalle", SGH_USUARIO, SGH_ESTADO);
                }
                else
                {
                    foreach (DataRow rw in ((DataTable)inDt).Rows)
                        SegregacionBD.InsertSegregacionProforma(oSessionManager, ln_consecutivo, HDCODEMP, Convert.ToInt32(rw["PR_NROCMP"]), Convert.ToInt32(rw["PR_NROITEM"]), Convert.ToInt32(rw["SGD_CANTIDAD"]), Convert.ToInt32(rw["PR_CANTIDAD"]), Convert.ToInt32(rw["PR_CANTIDAD"]), Convert.ToDouble(rw["SGD_PRECIO"]), ".", "gp_detalle", SGH_USUARIO, SGH_ESTADO);
                }

                //Disponible               
                //    foreach (DataRow rw in ((DataTable)inDt).Rows)
                //        SegregacionBD.InsertSegregacionProforma(oSessionManager, ln_consecutivo, HDCODEMP, Convert.ToInt32(rw["PR_NROCMP"]), Convert.ToInt32(rw["PR_NROITEM"]), Convert.ToInt32(rw["SGD_CANTIDAD"]), Convert.ToInt32(rw["SGD_PRECIO"]), SGH_BODCAN, "0", SGH_USUARIO, SGH_ESTADO);

                //Diferencia                
                //        foreach (DataRow rw in ((DataTable)inDt).Rows)
                //            SegregacionBD.InsertSegregacionProforma(oSessionManager, ln_consecutivo, HDCODEMP, Convert.ToInt32(rw["PR_NROCMP"]), Convert.ToInt32(rw["PR_NROITEM"]), Convert.ToInt32(rw["PR_CANTIDAD"])- Convert.ToInt32(rw["SGD_CANTIDAD"]), Convert.ToInt32(rw["SGD_PRECIO"]), SGH_BODDIF, "gp_detalle", SGH_USUARIO, SGH_ESTADO);                    

                /*
                if (SGH_TIPO == "FA")
                {
                    foreach (DataRow ry in ((DataTable)tbBodega).Rows)
                    {
                        foreach (DataColumn cl in ((DataTable)inDt).Columns)
                        {
                            if (Convert.ToString(ry["BDBODEGA"]) == Convert.ToString(cl.ColumnName))
                            {
                                foreach (DataRow rw in ((DataTable)inDt).Rows)
                                    SegregacionBD.InsertSegregacionFactura(oSessionManager, ln_consecutivo, HDCODEMP, Convert.ToInt32(rw["PR_NROCMP"]), Convert.ToInt32(rw["PR_NROITEM"]), Convert.ToInt32(rw[cl.ColumnName]), 0, 0, Convert.ToInt32(rw["SGD_PRECIO"]), cl.ColumnName, Convert.ToString(ry["Tipo"]), SGH_USUARIO, SGH_ESTADO);
                            }
                        }
                    }
                }
                else
                {
                    foreach (DataRow ry in ((DataTable)tbBodega).Rows)
                    {
                        foreach (DataColumn cl in ((DataTable)inDt).Columns)
                        {
                            if (Convert.ToString(ry["BDBODEGA"]) == Convert.ToString(cl.ColumnName))
                            {
                                foreach (DataRow rw in ((DataTable)inDt).Rows)
                                    SegregacionBD.InsertSegregacionProforma(oSessionManager, ln_consecutivo, HDCODEMP, Convert.ToInt32(rw["PR_NROCMP"]), Convert.ToInt32(rw["PR_NROITEM"]), Convert.ToInt32(rw[cl.ColumnName]), 0, 0, Convert.ToInt32(rw["SGD_PRECIO"]), cl.ColumnName, Convert.ToString(ry["Tipo"]), SGH_USUARIO, SGH_ESTADO);
                            }
                        }
                    }
                }
                */

                oSessionManager.CommitTranstaction();
                return ln_consecutivo;
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
        public int UpdateSegregacion(string connection,string HDCODEMP, string HDTIPFAC, int HDNROFAC, string SGH_NROFAC, DateTime? SG_FECPROFORMA, string SG_NROPROFORMA, int? SG_VENDEDORPRO, int? SG_COMPRADORPRO, 
                                     string SGH_ESTADO, string SGH_OBSERVACIONES, string SGH_PARCIAL, string SGH_USUARIO, string SGH_BODCAN,string SGH_BODDIF, object inDt, object inDtf, object tbBodega, int original_SGH_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                oSessionManager.BeginTransaction();
                
                SegregacionBD.UpdateSegregacionHD(oSessionManager, original_SGH_CODIGO, HDCODEMP, HDTIPFAC, HDNROFAC, SGH_NROFAC, SG_FECPROFORMA, SG_NROPROFORMA, SG_VENDEDORPRO, SG_COMPRADORPRO, "AC", SGH_OBSERVACIONES, SGH_PARCIAL, SGH_USUARIO, SGH_BODCAN, SGH_BODDIF);
                //Proforma
                foreach (DataRow rw in ((DataTable)inDt).Rows)
                    SegregacionBD.UpdateSegregacionProforma(oSessionManager, Convert.ToDouble(rw["SGD_CANTIDAD"]), Convert.ToDouble(rw["PR_CANTIDAD"]) - Convert.ToDouble(rw["SGD_CANTIDAD"]), Convert.ToInt32(rw["SGD_ID"]));
                //SegregacionBD.UpdateSegregacionProforma(oSessionManager, Convert.ToDouble(rw["SGD_CANTIDAD"]), Convert.ToDouble(rw["PR_CANTIDAD"]) - Convert.ToDouble(rw["SGD_CANTIDAD"]), Convert.ToInt32(rw["SGD_ID"]));

                //Factura
                //foreach (DataRow ry in ((DataTable)tbBodega).Rows)
                //{
                //    foreach (DataColumn cl in ((DataTable)inDtf).Columns)
                //    {
                //        if (Convert.ToString(ry["BDBODEGA"]) == Convert.ToString(cl.ColumnName))
                //        {
                //            foreach (DataRow rw in ((DataTable)inDtf).Rows)
                //                if (Convert.ToString(ry["Tipo"])== "gp_detalle")
                //                    SegregacionBD.InsertSegregacionFactura(oSessionManager, original_SGH_CODIGO, HDCODEMP, Convert.ToInt32(rw["PR_NROCMP"]), Convert.ToInt32(rw["PR_NROITEM"]), Convert.ToDouble(rw["SGD_CANTIDAD"]), Convert.ToDouble(rw["PR_CANTIDAD"]) - Convert.ToDouble(rw["SGD_CANTIDAD"]), Convert.ToInt32(rw["SGD_PRECIO"]), cl.ColumnName, "gp_detalle", SGH_USUARIO, SGH_ESTADO);
                //                else
                //                    SegregacionBD.InsertSegregacionFactura(oSessionManager, original_SGH_CODIGO, HDCODEMP, Convert.ToInt32(rw["PR_NROCMP"]), Convert.ToInt32(rw["PR_NROITEM"]), Convert.ToInt32(rw[cl.ColumnName]), 0, Convert.ToInt32(rw["SGD_PRECIO"]), cl.ColumnName, Convert.ToString(ry["Tipo"]), SGH_USUARIO, SGH_ESTADO);
                //        }
                //    }
                //}


                SegregacionBD.DeleteSegregacionFactura(oSessionManager, original_SGH_CODIGO);
                foreach (DataRow rw in ((DataTable)inDtf).Rows)
                   SegregacionBD.InsertSegregacionFactura(oSessionManager, original_SGH_CODIGO, HDCODEMP, Convert.ToInt32(rw["PR_NROCMP"]), Convert.ToInt32(rw["PR_NROITEM"]), Convert.ToDouble(rw["SGD_CANTIDAD"]), Convert.ToDouble(rw["SGD_CANTIDADAPRO"]), Convert.ToDouble(rw["SGD_CANTIDADCMP"]), Convert.ToInt32(rw["SGD_PRECIO"]), "JR", "gp_detalle", SGH_USUARIO, SGH_ESTADO);                 


                oSessionManager.CommitTranstaction();
                return original_SGH_CODIGO;
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
        //public int InsertSegregacion(string connection, string FD_CODEMP, string SG_USUARIO, object inDt)
        //{
        //    SessionManager oSessionManager = new SessionManager(connection);
        //    int ln_codigo = 0;
        //    try
        //    {
        //        oSessionManager.BeginTransaction();
        //        ln_codigo = ComunBD.GeneraConsecutivo(oSessionManager, "CONSEGR", FD_CODEMP);
        //        foreach (DataColumn cl in (inDt as DataTable).Columns)
        //        {
        //            if (cl.ColumnName.Substring(0, 4) == "BOD_")
        //            {
        //                foreach (DataRow rw in (inDt as DataTable).Rows)
        //                {
        //                    SegregacionBD.InsertSegregacion(oSessionManager, ln_codigo, FD_CODEMP, Convert.ToInt32(rw["FD_NROCMP"]), Convert.ToInt32(rw["FD_NROITEM"]), Convert.ToString(rw["FD_NROFACTURA"]),
        //                        Convert.ToString(rw["ARTIPPRO"]), Convert.ToString(rw["ARCLAVE1"]), Convert.ToString(rw["ARCLAVE2"]), Convert.ToString(rw["ARCLAVE3"]), Convert.ToString(rw["ARCLAVE4"]), cl.ColumnName.Substring(4, cl.ColumnName.Length-4), Convert.ToDouble(rw[cl.ColumnName]), "AC", SG_USUARIO);
        //                }
        //            }
        //        }
        //        oSessionManager.CommitTranstaction();
        //        return ln_codigo;
        //    }
        //    catch (Exception ex)
        //    {
        //        oSessionManager.RollBackTransaction();
        //        throw ex;
        //    }
        //    finally
        //    {
        //        oSessionManager = null;
        //    }
        //}
        public DataTable GetSegregacionxWR(string connection, int WOH_CONSECUTIVO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return SegregacionBD.GetSegregacionxWR(oSessionManager, WOH_CONSECUTIVO);
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
        public DataTable GetSegregacionxTraslado(string connection, int TSNROTRA,string TSCODEMP)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return SegregacionBD.GetSegregacionxTraslado(oSessionManager, TSNROTRA, TSCODEMP);
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
