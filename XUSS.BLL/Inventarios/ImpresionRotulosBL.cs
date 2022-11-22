using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XUSS.DAL.Articulos;
using XUSS.DAL.Parametros;

namespace XUSS.BLL.Inventarios
{
    public class ImpresionRotulosBL
    {
        public int GenerarCodigoBarras(string connection,string CODEMP,string TP, string C1,string C2,string C3,string C4,string inUsuario)
        {
            int ln_codalt = 0;
            string lc_codalt = "",lc_codbarras="",lc_codean=""; 
            SessionManager oSessionManager = new SessionManager(connection);
            try {
                if (ArticulosBD.TieneBarras(oSessionManager, CODEMP, TP, C1, C2, C3, C4) == 0)
                {
                    oSessionManager.BeginTransaction();
                    foreach (DataRow rw in TipoProductosBD.GetTipoProducto(oSessionManager, " TATIPPRO='" + TP + "'", 0, 0).Rows)
                    {
                        if (Convert.ToString(rw["TAINDALT"]) == "S")
                        {
                            CompaniaBD.UpdateCompania(oSessionManager, CODEMP);
                            foreach (DataRow rx in CompaniaBD.GetCompania(oSessionManager, " CNCODEMP='" + CODEMP + "'", 0, 0).Rows)
                            {
                                ln_codalt = Convert.ToInt32(rx["CNCDALTR"]);
                                lc_codean = Convert.ToString(rx["CNCODEAN"]);
                            }
                        }
                        else
                        {
                            throw new System.ArgumentException("No Se Permite Generar Cod Barras al TP (TAINDALT)");
                        }
                    }

                    lc_codalt = Convert.ToString(ln_codalt);
                    lc_codalt = this.strZero(ln_codalt, 4);
                    if (ln_codalt > 9999)
                        throw new System.ArgumentException("El Codigo EAN13 a Llegado a su limite Asignar Consecutivo de Compañia Nuevamente Reiniciar Numero de Rotulacion");
                    lc_codbarras = lc_codean + lc_codalt + this.CalcularDigitoCK(lc_codean, Convert.ToString(ln_codalt));
                    //Inserta Codigo de Barras
                    ArticulosBD.InserTbBarras(oSessionManager, CODEMP, lc_codbarras, TP, C1, C2, C3, C4, ".", lc_codean.Substring(1, 3), "001", inUsuario);
                    oSessionManager.CommitTranstaction();
                }
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
        public string CalcularDigitoCK(string inCodEAN, string inCodigo)
        {
            int ln_suma = 0;
            try {
                inCodigo = inCodEAN + this.strZero(Convert.ToInt32(inCodigo),5);
                for (int i = 1; i < 12; i++)
                {
                    if (i % 2 != 0)
                        ln_suma += Convert.ToInt32(inCodigo[i]);
                    else
                        ln_suma += (Convert.ToInt32(inCodigo[i])*3);
                }

                if ((10-(ln_suma%10))==10)
                    return "0";
                else
                    return Convert.ToString((10-(ln_suma%10)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
            
            }
        }
        public string strZero(int inNumero, int inTamano)
        {
            string lc_cadena = Convert.ToString(inNumero);
            while (lc_cadena.Length < inTamano)
                lc_cadena = "0" + lc_cadena;
            return lc_cadena;
        }
        public int InsertCodigoTMP(string connection, object tbDetalle,string inUsuario)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                ArticulosBD.DeleteTMPImpresion(oSessionManager, inUsuario);
                foreach (DataRow rw in (tbDetalle as DataTable).Rows)
                {
                    ArticulosBD.InsertTMPImpresion(oSessionManager, Convert.ToString(rw["TP"]), Convert.ToString(rw["C1"]), Convert.ToString(rw["C2"]), Convert.ToString(rw["C3"]), ".", inUsuario,Convert.ToInt32(rw["CAN"]));
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
    }
}
