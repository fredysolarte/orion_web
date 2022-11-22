using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using XUSS.DAL.Descuentos;
using System.Data;
using XUSS.DAL.Parametros;

namespace XUSS.BLL
{
    public class DescuentosBL
    {        
        private int pCodDcto;
        private int pTipDcto;
        private double pVlrDcto;
        private double pVlrDctoP;

        public int CodDcto
        {
            get { return pCodDcto; }
            set { pCodDcto = value; }
        }
        public double VlrDcto
        {
            get { return pVlrDcto; }
            set { pVlrDcto = value; }
        }
        public int TipDcto
        {
            get { return pTipDcto; }
            set { pTipDcto = value; }
        }
        public double VlrDctoP
        {
            get { return pVlrDctoP; }
            set { pVlrDctoP = value; }
        }
        
        public void GetVlrDctoArt(string inTP, string inC1, string inC2, string inC3, string inC4, string inBD, double inPrecio)
        {
            int ln_TdesTer = 0, ln_TdesArt = 0, ln_TdesTp = 0; //tipos de dcto (porcentaje ó valor)
            int ln_coddcto = 0, ln_codter = 0, ln_codart = 0, ln_codtp = 0;//codigos dcto
            double ln_dctoter = 0, ln_dctoart = 0, ln_dctopag = 0; // Dcto por Tipo
            double ln_vlrdctoapli = 0;

            SessionManager oSessionManager = new SessionManager(null);
            DescuentosBD obj = new DescuentosBD();

            try
            {
                obj.GetDctoArticulo(oSessionManager, inTP, inC1, inC2, inC3, inC4, inBD);
                ln_TdesTer = obj.TipDcto;
                ln_codart = obj.CodDcto;
                ln_dctoter = obj.VlrDcto;

                if (ln_TdesTer == 1)
                    ln_dctoter = (inPrecio * ln_dctoter) / 100;
                if (ln_TdesArt == 1)
                    ln_dctoart = (inPrecio * ln_dctoart) / 100;
                if (ln_TdesTp == 1)
                    ln_dctopag = (inPrecio * ln_dctopag) / 100;

                if ((ln_dctoter > ln_dctoart) && (ln_dctoter > ln_dctopag))
                {
                    ln_vlrdctoapli = ln_dctoter;
                    ln_coddcto = ln_codter;
                }
                else

                    if ((ln_dctoart > ln_dctoter) && (ln_dctoart > ln_dctopag))
                    {
                        ln_vlrdctoapli = ln_dctoart;
                        ln_coddcto = ln_codart;
                    }
                    else
                    {
                        if ((ln_dctopag > ln_dctoter) && (ln_dctopag > ln_dctoart))
                        {
                            ln_vlrdctoapli = ln_dctopag;
                            ln_coddcto = ln_codtp;
                        }
                    }

                CodDcto = ln_coddcto;
                VlrDcto = ln_vlrdctoapli;
                if (inPrecio == 0)
                    VlrDctoP = 0;
                else
                    VlrDctoP = (ln_vlrdctoapli * 100) / inPrecio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
                obj = null;
            }
        }
        public double GetPrecio(string inCodEmp, string inTP, string inC1, string inC2, string inC3, string inC4,string Lista)
        {
            double retorno = 0;
            DescuentosBD obj = new DescuentosBD();
            SessionManager oSessionManager = new SessionManager(null);
            try {
                using (IDataReader reader = obj.GetPrecio(oSessionManager, inTP, inC1, inC2, inC3, inC4, inCodEmp, Lista))
                {
                    while (reader.Read())
                    {
                        if (Convert.ToDouble(reader["PRECIO4"]) != 0)
                            retorno = Convert.ToDouble(reader["PRECIO4"]);
                        else
                            if (Convert.ToDouble(reader["PRECIO3"]) != 0)
                                retorno = Convert.ToDouble(reader["PRECIO3"]);
                            else
                                if (Convert.ToDouble(reader["PRECIO2"]) != 0)
                                    retorno = Convert.ToDouble(reader["PRECIO2"]);
                                else
                                    if (Convert.ToDouble(reader["PRECIO1"]) != 0)
                                        retorno = Convert.ToDouble(reader["PRECIO1"]);
                                    
                    }
                }
                
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
                obj = null;
            }
        }
        public void GetVlrDcto(string inTP, string inC1, string inC2, string inC3, string inC4, string inBD, double inValor, string inCedula)
        {            
            SessionManager oSessionManager = new SessionManager(null);
            DescuentosBD obj = new DescuentosBD();
            double ln_valora = 0;
            double ln_valorcc = 0;

            try
            {
                obj.GetDctoArticulo(oSessionManager, inTP, inC1, inC2, inC3, inC4, inBD);
                obj.GetDctoCedula(oSessionManager, inTP, inC1, inC2, inC3, inC4, inBD, inCedula, inValor);

                //1 Porcentaje 2 Valor
                ln_valora = VlrDcto;
                if (obj.TipDcto == 2)
                    ln_valora = (VlrDcto * 100) / inValor;

                ln_valorcc = obj.VlrDctoCC;
                if (obj.TipDctoCC == 2)
                    ln_valorcc = (obj.VlrDctoCC * 100) / inValor;

                //Cedula
                CodDcto = obj.CodDctoCC;                
                VlrDcto = obj.VlrDctoCC;

                if (ln_valorcc < ln_valora)
                {
                    //Articulo
                    CodDcto = obj.CodDcto;                    
                    VlrDcto = obj.VlrDcto;
                }
   
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
                obj = null;
            }
        }
        public void GetVlrDctoTF(string inTP, string inC1, string inC2, string inC3, string inC4, string inTF, double inValor, string inCedula,int inLista)
        {
            SessionManager oSessionManager = new SessionManager(null);
            DescuentosBD obj = new DescuentosBD();
            TipoFacturaBD ObjT = new TipoFacturaBD();
            string inBD = "";
            double ln_valora = 0;
            double ln_valorcc = 0;
            double ln_valorlt = 0;

            try
            {
                if (inValor != 0)
                {

                    foreach (DataRow rw in ObjT.GetTiposFactura(oSessionManager, " TFTIPFAC='" + inTF + "'", 0, 0).Rows)
                    {
                        inBD = Convert.ToString(rw["BDBODEGA"]);
                    }
                    obj.GetDctoArticulo(oSessionManager, inTP, inC1, inC2, inC3, inC4, inBD);
                    obj.GetDctoCedula(oSessionManager, inTP, inC1, inC2, inC3, inC4, inBD, inCedula, inValor);
                    obj.GetDctoLista(oSessionManager, inTP, inC1, inC2, inC3, inC4, inBD, inLista);
                    //1 Porcentaje 2 Valor
                    ln_valora = obj.VlrDcto;
                    if (obj.TipDcto == 2)
                        ln_valora = (obj.VlrDcto * 100) / inValor;

                    ln_valorcc = obj.VlrDctoCC;
                    if (obj.TipDctoCC == 2)
                        ln_valorcc = (obj.VlrDctoCC * 100) / inValor;

                    ln_valorlt = obj.VlrDctoLT;
                    if (obj.TipDctoLT == 2)
                        ln_valorlt = (obj.VlrDctoLT * 100) / inValor;

                    //Cedula
                    CodDcto = obj.CodDctoCC;
                    VlrDcto = obj.VlrDctoCC;
                    TipDcto = obj.TipDctoCC;
                    if (ln_valorcc < ln_valora)
                    {
                        //Articulo
                        CodDcto = obj.CodDcto;
                        VlrDcto = obj.VlrDcto;
                        TipDcto = obj.TipDcto;
                        if (ln_valora < ln_valorlt)
                        {
                            //Lista
                            CodDcto = obj.CodDctoLT;
                            VlrDcto = obj.VlrDctoLT;
                            TipDcto = obj.TipDctoLT;
                        }
                    }
                    else
                    {
                        //Articulo
                        CodDcto = obj.CodDctoCC;
                        VlrDcto = obj.VlrDctoCC;
                        TipDcto = obj.TipDctoCC;

                        if (ln_valorcc < ln_valorlt)
                        {
                            //Lista
                            CodDcto = obj.CodDctoLT;
                            VlrDcto = obj.VlrDctoLT;
                            TipDcto = obj.TipDctoLT;
                        }
                    }
                }
                else {
                    CodDcto = 0;
                    VlrDcto = 0;
                    TipDcto = 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
                obj = null;
            }
        }
    }
}
