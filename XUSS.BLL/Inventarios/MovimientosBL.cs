using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using XUSS.DAL.Inventarios;
using XUSS.DAL.Comun;
using System.Data;
using XUSS.DAL.Parametros;

namespace XUSS.BLL.Inventarios
{
    public class MovimientosBL
    {
        public DataTable GetMovimInv(string connection, string filter)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            MovimientosBD Obj = new MovimientosBD();
            try
            {
                return Obj.GetMovimInv(oSessionManager, filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
            }
        }
        public DataTable CargarMovimiento(string connection, string MBCODEMP, int MBIDMOVI)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            MovimientosBD Obj = new MovimientosBD();
            try {
                return Obj.CargarMovimiento(oSessionManager, MBCODEMP, MBIDMOVI);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
            }
        }       
        public int InsertMovimiento(SessionManager oSessionManager, string MICODEMP, int MIOTMOVI, string MIBODEGA, string MIOTBODE, string MICDTRAN,
                                    int? MIPEDIDO, int? MICOMPRA, int? MICODTER, string MICDDOCU, DateTime? MIFECDOC, string MICOMENT, string MIESTADO, string MICAUSAE, string MINMUSER,
                                    int? MIORDPRO, int? MILINPRO, int? MINROTRA, string MICODMAQ, int? MIRECIBO, int? MISUCURSAL, string MIUSERSOL, string MIUSERAPR)
        {
            int MIIDMOVI = 0;
            MovimientosBD Obj = new MovimientosBD();
            try {
                MIIDMOVI = ComunBD.GeneraConsecutivo(oSessionManager, "MOVINV", MICODEMP);
                Obj.InsertMovimiento(oSessionManager,MICODEMP,MIIDMOVI,MIOTMOVI,MIBODEGA,MIOTBODE, MICDTRAN,
                                    MIPEDIDO, MICOMPRA, MICODTER, MICDDOCU, MIFECDOC, MICOMENT, MIESTADO, MICAUSAE, MINMUSER,
                                    MIORDPRO, MILINPRO, MINROTRA, MICODMAQ, MIRECIBO, MISUCURSAL, MIUSERSOL, MIUSERAPR);
                return MIIDMOVI;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                Obj = null;
            }        
        }        
        public int InsertMovimiento(SessionManager oSessionManager, string CODEMP, string BODEGA, string OTBODEGA,DateTime FECMOV, string CDTRAN, string TP, string C1, string C2, string C3, string C4, string QL, double CANTID, double CANMOV,
                                    string UNMOV, int IDMOVI, int IDOMOVI,int ITEM, string LOTE,string ELEM, string LTEC1,string LTEC2, string LTEC3,string LTEC4,double LTEC5,double LTEC6,
                                    string ESTANT, string ESTADO, string CAUSAE, string USUARIO,int IDLOTE, int IDELEM)
        {
            Boolean lb_lote = false, lb_elem = false;
            string lc_tipmov="",lc_clapro="";
            double ln_cantrn = 0;
            MovimientosBD Obj = new MovimientosBD();

            try
            {                
                //Valida Transsacion
                using (IDataReader reader = TipoMovimientoBD.GetTipoMovimientoR(oSessionManager, CODEMP, CDTRAN ))
                {
                    while (reader.Read())
                    {
                        lc_tipmov =  Convert.ToString(reader["TMENTSAL"]);
                    }
                }

                //Valida Tipo de Manejo por Tipo de Producto
                using (IDataReader reader = TipoProductosBD.GetTipoProductoxBodegaR(oSessionManager, CODEMP, BODEGA, TP))
                {
                    while (reader.Read())
                    {
                        if (Convert.ToString(reader["ABMNLOTE"]) == "S")
                            lb_lote = true;
                        if (Convert.ToString(reader["ABMNELEM"]) == "S")
                            lb_elem = true;
                        lc_clapro = Convert.ToString(reader["TACLAPRO"]);
                    }
                }
                //Valid tipo Articulo(servicio-Articulo)
                if (lc_clapro == "A")
                {
                    if (lb_lote)
                        if (string.IsNullOrEmpty(LOTE))
                            throw new System.ArgumentException("No se Ha Especificado Numero de Lote");

                    if (lb_elem)
                        if (string.IsNullOrEmpty(ELEM))
                            throw new System.ArgumentException("No se Ha Especificado Numero de Elemento");
                }


                if (ESTADO == "CE" && ESTANT == "AC")
                {                    
                    Obj.UpdateMovimBod(oSessionManager, CODEMP, IDMOVI, ITEM, ESTADO, ".",USUARIO);

                    if (lb_lote)                    
                        Obj.UpdateMovimLot(oSessionManager, CODEMP, IDMOVI, ITEM, IDLOTE, ESTADO, USUARIO);
                                            
                    if (lb_elem)
                        Obj.UpdateMovimEle(oSessionManager, CODEMP, IDMOVI, ITEM, IDLOTE, IDELEM, ESTADO, USUARIO);                                            
                }
                if (Obj.ExisteMovimiento(oSessionManager, CODEMP, IDMOVI, ITEM) == 0)
                    Obj.InsertMovimBod(oSessionManager, CODEMP, IDMOVI, ITEM, FECMOV, BODEGA, TP, C1, C2, C3, C4, QL, CDTRAN, CANMOV, UNMOV, CANTID, 0, 0, 0, 0, IDOMOVI, OTBODEGA, ESTADO, CAUSAE, USUARIO, 0, 0, 0, 0, 0, null);                

                if (!lb_lote) LOTE = ".";

                if (lb_lote)
                {
                    if (Obj.ExisteMovimientoLote(oSessionManager,CODEMP,IDMOVI,ITEM,IDLOTE)==0)
                        Obj.InsertMovimLot(oSessionManager, CODEMP, IDMOVI, ITEM, IDLOTE, FECMOV, BODEGA, TP, C1, C2, C3, C4, QL, LOTE, CDTRAN, CANTID, 0, 0, null, LTEC1, LTEC2, LTEC3, LTEC4, LTEC5, LTEC6, ESTADO, CAUSAE, USUARIO, null);         
                    else
                    {
                        double ln_canmovtot = Obj.GetTotalMovimientoLote(oSessionManager, CODEMP, IDMOVI, IDLOTE);
                        Obj.UpdateMovimBod(oSessionManager, CODEMP, IDMOVI, ITEM, ln_canmovtot);
                    }
                }
                if (lb_elem)
                {
                    if (Obj.ExisteMovimientoEle(oSessionManager, CODEMP, IDMOVI, ITEM, IDLOTE,IDELEM) == 0)
                    {
                        Obj.InsertMovimEle(oSessionManager, CODEMP, IDMOVI, ITEM, IDLOTE, IDELEM, FECMOV, BODEGA, TP, C1, C2, C3, C4, QL, LOTE, ELEM, CDTRAN, CANTID, 0, 0, "UN", 0, null, 0, 0, 0, 0, LTEC1, LTEC2, LTEC3, LTEC4, LTEC5, LTEC6, ESTADO, CAUSAE, USUARIO, null);
                        
                        double ln_canmovlot = Obj.GetTotalMovimientoEle(oSessionManager, CODEMP, IDMOVI, ITEM, IDLOTE);
                        Obj.UpdateMovimLot(oSessionManager, CODEMP, IDMOVI, ITEM, IDLOTE, ln_canmovlot);
                        double ln_canmovtot = Obj.GetTotalMovimientoLote(oSessionManager, CODEMP, IDMOVI, ITEM);
                        Obj.UpdateMovimBod(oSessionManager, CODEMP, IDMOVI, ITEM, ln_canmovtot);
                    }
                }

                //Tipo de Movimiento de Entrada
                switch (lc_tipmov)
                {
                    case "E":                        
                        if (ESTADO == "AC")
                            ln_cantrn = CANTID;
                        if (ESTADO == "CE" && ESTANT=="AC")
                            ln_cantrn = CANTID*-1;
                        break;
                    case "S":
                        CANTID *= -1;
                        if (ESTADO == "AC")
                            ln_cantrn = CANTID;
                        if (ESTADO == "CE" && ESTANT == "AC")
                            ln_cantrn = CANTID; 
                        break;

                }
                
                //Valid tipo Articulo(servicio-Articulo)
                if (lc_clapro == "A")
                {
                    //Validar Existencias
                    switch (ESTADO)
                    {
                        case "CE":
                            //if (lc_tipmov == "S")
                            //    if (Obj.GetInvDisponible(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4) < Math.Abs(CANTID))
                            //        throw new System.ArgumentException("La Cantidad Supera Disponible "+ BODEGA + "-" + TP + "-" + C1 + "-" + C2 + "-" + C3 +"-"+C4);
                            break;
                        case "AC":
                            if (lc_tipmov == "S")
                            {                                
                                if (Obj.GetInvDisponible(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4) < Math.Abs(CANTID))
                                    throw new System.ArgumentException("La Cantidad Supera Disponible " + BODEGA + "-" + TP + "-" + C1 + "-" + C2 + "-" + C3 + "-" + C4);
                                //Validacion x Elemento
                                if (lb_lote)
                                {
                                    if (Obj.GetInvDisponibleElem(oSessionManager, CODEMP, BODEGA, ELEM) < Math.Abs(CANTID))
                                        throw new System.ArgumentException("La Cantidad Supera Disponible " + BODEGA + "-" + TP + "-" + C1 + "-" + C2 + "-" + C3 + "-" + C4 + "-" + ELEM);
                                }
                            }


                            break;
                    }

                    //Bodegas
                    if (Obj.ExisteArticuloBodega(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4, QL) == 0)
                    {
                        //if (CANTID > 0)
                        if (ESTADO == "AC" && lc_tipmov == "E")
                            Obj.InsertBalanBod(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4, QL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, null, null, null, USUARIO, Math.Abs(CANTID), 0);
                        if (ESTADO == "AC" && lc_tipmov == "S")
                            Obj.InsertBalanBod(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4, QL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, Math.Abs(CANTID), null, null, null, USUARIO, 0, 0);
                        if (ESTADO == "CE")
                            Obj.InsertBalanBod(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4, QL, CANTID, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, null, null, null, USUARIO, 0, 0);
                    }
                    //else
                    //    throw new System.ArgumentException("No se puede Mover Mercancia en Negativo"+TP+" "+C1+" "+C2+" "+C3+" "+C4);
                    else
                    {
                        switch (ESTADO)
                        {
                            case "CE":
                                Obj.UpdateBalanBodCantid(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4, QL, CANTID, USUARIO);
                                if (ESTANT == "AC" && lc_tipmov == "S")
                                    Obj.UpdateBalanBodCanTrn(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4, QL, ln_cantrn, USUARIO);
                                if (ESTANT == "AC" && lc_tipmov == "E")
                                    Obj.UpdateBalanBodCanCtl(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4, QL, ln_cantrn, USUARIO);
                                break;
                            case "AN":
                                if (ESTANT == "CE")
                                    Obj.UpdateBalanBodCantid(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4, QL, CANTID, USUARIO);
                                if (ESTANT == "AC" && lc_tipmov == "E")
                                    Obj.UpdateBalanBodCanCtl(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4, QL, -CANTID, USUARIO);
                                if (ESTANT == "AC" && lc_tipmov == "S")
                                    Obj.UpdateBalanBodCanTrn(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4, QL, -CANTID, USUARIO);
                                //Falta Mercancia Libre
                                break;
                            case "AC":
                                if (lc_tipmov == "E")
                                    Obj.UpdateBalanBodCanCtl(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4, QL, CANTID, USUARIO);
                                if (lc_tipmov == "S")
                                    Obj.UpdateBalanBodCanTrn(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4, QL, CANTID * -1, USUARIO);
                                break;
                        }

                    }
                    //Lotes
                    if (lb_lote)
                    {
                        if (Obj.ExisteLote(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4, QL, LOTE) == 0)
                        {
                            //if (CANTID > 0)
                            if (ESTADO == "AC" && lc_tipmov == "E")
                                Obj.InsertBalanLot(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4, QL, LOTE, 0, 0, 0, 0, null, LTEC1, LTEC2, LTEC3, LTEC4, LTEC5, LTEC6, null, null, USUARIO, null, Math.Abs(CANTID));
                            if (ESTADO == "AC" && lc_tipmov == "S")
                                Obj.InsertBalanLot(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4, QL, LOTE, 0, Math.Abs(CANTID), 0, 0, null, LTEC1, LTEC2, LTEC3, LTEC4, LTEC5, LTEC6, null, null, USUARIO, null, 0);
                            if (ESTADO == "CE")
                                Obj.InsertBalanLot(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4, QL, LOTE, CANTID, 0, 0, 0, null, LTEC1, LTEC2, LTEC3, LTEC4, LTEC5, LTEC6, null, null, USUARIO, null, 0);
                            //else
                            //    throw new System.ArgumentException("No se puede Mover Mercancia en Negativo" + TP + " " + C1 + " " + C2 + " " + C3 + " " + C4 + " " + LOTE);
                        }
                        else
                        {
                            switch (ESTADO)
                            {
                                case "CE":
                                    Obj.UpdateBalanLotCantid(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4, QL, LOTE, CANTID, USUARIO);
                                    if (ESTANT == "AC" && lc_tipmov == "S")
                                        Obj.UpdateBalanLotCanBod(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4, QL, LOTE, ln_cantrn, USUARIO);                                    
                                    if (ESTANT == "AC" && lc_tipmov == "E")
                                        Obj.UpdateBalanLotCanCtl(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4, QL, LOTE, ln_cantrn, USUARIO);
                                    break;                                
                                case "AN":
                                    if (ESTANT == "CE")
                                        Obj.UpdateBalanLotCantid(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4, QL, LOTE, CANTID, USUARIO);
                                    if (ESTANT == "AC" && lc_tipmov == "E")
                                        Obj.UpdateBalanLotCanCtl(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4, QL, LOTE, -CANTID, USUARIO);
                                    if (ESTANT == "AC" && lc_tipmov == "S")
                                        Obj.UpdateBalanLotCanBod(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4, QL, LOTE, -CANTID, USUARIO);
                                    //Falta Mercancia Libre
                                    break;

                                case "AC":                                    
                                    if (lc_tipmov == "E")
                                        Obj.UpdateBalanLotCanCtl(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4, QL, LOTE, CANTID, USUARIO);                                    
                                    if (lc_tipmov == "S")
                                        Obj.UpdateBalanLotCanBod(oSessionManager, CODEMP, BODEGA, TP, C1, C2, C3, C4, QL, LOTE, CANTID * -1, USUARIO);                                    
                                    break;
                                //Falta Mercancia Libre
                            }
                        }
                    }
                    //Elementos
                    if (lb_elem)
                    {
                        if (Obj.ExisteEle(oSessionManager, CODEMP, BODEGA, ELEM) == 0)
                        {                            
                            if (ESTADO == "AC" && lc_tipmov == "E")
                                Obj.InsertBalanEle(oSessionManager, CODEMP, ELEM, BODEGA, TP, C1, C2, C3, C4, QL, LOTE, 0, 0, 0, "UN", 0, 0, 0, 0, null, 0, 0, 0, 0, LTEC1, LTEC2, LTEC3, LTEC4, LTEC5, LTEC6, null, null, USUARIO, null, Math.Abs(CANTID));
                            if (ESTADO == "AC" && lc_tipmov == "S")
                                Obj.InsertBalanEle(oSessionManager, CODEMP, ELEM, BODEGA, TP, C1, C2, C3, C4, QL, LOTE, 0, 0, 0, "UN", 0, Math.Abs(CANTID), 0, 0, null, 0, 0, 0, 0, LTEC1, LTEC2, LTEC3, LTEC4, LTEC5, LTEC6, null, null, USUARIO, null, 0);
                            if (ESTADO == "CE")
                                Obj.InsertBalanEle(oSessionManager, CODEMP, ELEM, BODEGA, TP, C1, C2, C3, C4, QL, LOTE, 0, CANTID, 0, "UN", 0, 0, 0, 0, null, 0, 0, 0, 0, LTEC1, LTEC2, LTEC3, LTEC4, LTEC5, LTEC6, null, null, USUARIO, null, 0);                            
                        }
                        else
                        {
                            switch (ESTADO)
                            {
                                case "CE":
                                    Obj.UpdateBalanEleCantid(oSessionManager, CODEMP, BODEGA, ELEM, USUARIO, CANTID);
                                    if (ESTANT == "AC" && lc_tipmov == "S")
                                        Obj.UpdateBalanEleCanBod(oSessionManager, CODEMP, BODEGA, ELEM, USUARIO, ln_cantrn);
                                    if (ESTANT == "AC" && lc_tipmov == "E")
                                        Obj.UpdateBalanEleCanCtl(oSessionManager, CODEMP, BODEGA, ELEM, USUARIO, ln_cantrn);
                                    break;
                                case "AN":
                                    if (ESTANT == "CE")
                                        Obj.UpdateBalanEleCantid(oSessionManager, CODEMP, BODEGA, ELEM, USUARIO, CANTID);
                                    if (ESTANT == "AC" && lc_tipmov == "E")
                                        Obj.UpdateBalanEleCanCtl(oSessionManager, CODEMP, BODEGA, ELEM, USUARIO, -CANTID);                                    
                                    if (ESTANT == "AC" && lc_tipmov == "S")
                                        Obj.UpdateBalanEleCanBod(oSessionManager, CODEMP, BODEGA, ELEM, USUARIO, -CANTID);                                    
                                    //Falta Mercancia Libre
                                    break;
                                case "AC":
                                    if (lc_tipmov == "E")
                                        Obj.UpdateBalanEleCanCtl(oSessionManager, CODEMP, BODEGA, ELEM, USUARIO, CANTID);
                                    if (lc_tipmov == "S")
                                        Obj.UpdateBalanEleCanBod(oSessionManager, CODEMP, BODEGA, ELEM, USUARIO, CANTID * -1);                                        
                                    break;
                            }
                        }
                    }
                }    
                //Falta Elemento                    
                //        break;
                //    case "S":

                //        break;
                //}

                return 0;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            finally
            {
                Obj = null;
            }
        }

        public int AnularMovimientoM(string connection, string MBCODEMP, int MBIDMOVI, int Item, string inUsuario)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try {
                return this.AnularMovimiento(oSessionManager, MBCODEMP, MBIDMOVI, Item, inUsuario);
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
        public int AnularMovimiento(SessionManager oSessionManager, string CODEMP, int IDMOVI, int ITEM, string USUARIO)
        {
            Boolean lb_lote = false, lb_elem = false;
            string lc_tipmov = "", lc_clapro = "";
            MovimientosBD Obj = new MovimientosBD();
            try {
                //oSessionManager.BeginTransaction();
                Obj.UpdateMovimiento(oSessionManager, CODEMP, IDMOVI, "AN", USUARIO);

                    foreach(DataRow r_mbod in Obj.CargarMovimiento(oSessionManager, CODEMP, IDMOVI, ITEM).Rows)
                    {
                        Obj.UpdateMovimBod(oSessionManager, Convert.ToString(r_mbod["MBCODEMP"]), Convert.ToInt32(r_mbod["MBIDMOVI"]), Convert.ToInt32(r_mbod["MBIDITEM"]), Convert.ToDateTime(r_mbod["MBFECMOV"]),
                            Convert.ToString(r_mbod["MBBODEGA"]), Convert.ToString(r_mbod["MBTIPPRO"]), Convert.ToString(r_mbod["MBCLAVE1"]), Convert.ToString(r_mbod["MBCLAVE2"]), Convert.ToString(r_mbod["MBCLAVE3"]),
                            Convert.ToString(r_mbod["MBCLAVE4"]), Convert.ToString(r_mbod["MBCODCAL"]), Convert.ToString(r_mbod["MBCDTRAN"]), 0, Convert.ToString(r_mbod["MBUNDMOV"]),
                            0, Convert.ToDouble(r_mbod["MBCANORI"]), Convert.ToDouble(r_mbod["MBSALDOI"]), Convert.ToDouble(r_mbod["MBCOSTOA"]), Convert.ToDouble(r_mbod["MBCOSTOB"]),
                            Convert.ToInt32(r_mbod["MBOTMOVI"]), Convert.ToString(r_mbod["MBOTBODE"]), "AN", ".", USUARIO, 0, 0, 0, 0, 0, null);
                        
                        //Valida Transsacion
                        using (IDataReader reader = TipoMovimientoBD.GetTipoMovimientoR(oSessionManager, CODEMP, Convert.ToString(r_mbod["MBCDTRAN"])))
                        {
                            while (reader.Read())
                            {
                                lc_tipmov = Convert.ToString(reader["TMENTSAL"]);
                            }
                        }

                        //Valida Tipo de Manejo por Tipo de Producto
                        using (IDataReader reader = TipoProductosBD.GetTipoProductoxBodegaR(oSessionManager, CODEMP, Convert.ToString(r_mbod["MBBODEGA"]), Convert.ToString(r_mbod["MBTIPPRO"])))
                        {
                            while (reader.Read())
                            {
                                if (Convert.ToString(reader["ABMNLOTE"]) == "S")
                                    lb_lote = true;
                                if (Convert.ToString(reader["ABMNELEM"]) == "S")
                                    lb_elem = true;
                                lc_clapro = Convert.ToString(reader["TACLAPRO"]);
                            }
                        }

                        switch (lc_tipmov)
                        { 
                            case "E":
                                if (Convert.ToString(r_mbod["MBESTADO"]) == "AC")
                                    Obj.UpdateBalanBodCanCtl(oSessionManager, CODEMP, Convert.ToString(r_mbod["MBBODEGA"]), Convert.ToString(r_mbod["MBTIPPRO"]), Convert.ToString(r_mbod["MBCLAVE1"]), Convert.ToString(r_mbod["MBCLAVE2"]), Convert.ToString(r_mbod["MBCLAVE3"]),
                                                         Convert.ToString(r_mbod["MBCLAVE4"]), Convert.ToString(r_mbod["MBCODCAL"]), -Convert.ToDouble(r_mbod["MBCANTID"]), USUARIO);
                                else
                                    if (Convert.ToString(r_mbod["MBESTADO"]) == "CE")
                                        Obj.UpdateBalanBodCantid(oSessionManager, CODEMP, Convert.ToString(r_mbod["MBBODEGA"]), Convert.ToString(r_mbod["MBTIPPRO"]), Convert.ToString(r_mbod["MBCLAVE1"]), Convert.ToString(r_mbod["MBCLAVE2"]), Convert.ToString(r_mbod["MBCLAVE3"]),
                                                             Convert.ToString(r_mbod["MBCLAVE4"]), Convert.ToString(r_mbod["MBCODCAL"]), -Convert.ToDouble(r_mbod["MBCANTID"]), USUARIO);
                                break;
                            case "S":
                                if (Convert.ToString(r_mbod["MBESTADO"]) == "AC")
                                    Obj.UpdateBalanBodCanTrn(oSessionManager, CODEMP, Convert.ToString(r_mbod["MBBODEGA"]), Convert.ToString(r_mbod["MBTIPPRO"]), Convert.ToString(r_mbod["MBCLAVE1"]), Convert.ToString(r_mbod["MBCLAVE2"]), Convert.ToString(r_mbod["MBCLAVE3"]),
                                                         Convert.ToString(r_mbod["MBCLAVE4"]), Convert.ToString(r_mbod["MBCODCAL"]), -Convert.ToDouble(r_mbod["MBCANTID"]), USUARIO);
                                else
                                    if (Convert.ToString(r_mbod["MBESTADO"]) == "CE")
                                        Obj.UpdateBalanBodCantid(oSessionManager, CODEMP, Convert.ToString(r_mbod["MBBODEGA"]), Convert.ToString(r_mbod["MBTIPPRO"]), Convert.ToString(r_mbod["MBCLAVE1"]), Convert.ToString(r_mbod["MBCLAVE2"]), Convert.ToString(r_mbod["MBCLAVE3"]),
                                                         Convert.ToString(r_mbod["MBCLAVE4"]), Convert.ToString(r_mbod["MBCODCAL"]), Convert.ToDouble(r_mbod["MBCANTID"]), USUARIO);
                                break;
                        }

                        if (lb_lote)
                        { 
                            //using(IDataReader r_mlot = Obj.CargarMovimientoLot(oSessionManager,Convert.ToString(r_mbod["MBCODEMP"]), Convert.ToInt32(r_mbod["MBIDMOVI"]), Convert.ToInt32(r_mbod["MBIDITEM"])))
                            //{
                            foreach(DataRow r_mlot in Obj.CargarMovimientoLot(oSessionManager,Convert.ToString(r_mbod["MBCODEMP"]), Convert.ToInt32(r_mbod["MBIDMOVI"]), Convert.ToInt32(r_mbod["MBIDITEM"])).Rows)    
                            {
                                    Obj.UpdateMovimLot(oSessionManager,Convert.ToString(r_mlot["MLCODEMP"]),Convert.ToInt32(r_mlot["MLIDMOVI"]),Convert.ToInt32(r_mlot["MLIDITEM"]),Convert.ToInt32(r_mlot["MLIDLOTE"]),
                                                        Convert.ToDateTime(r_mlot["MLFECMOV"]),Convert.ToString(r_mlot["MLBODEGA"]),Convert.ToString(r_mlot["MLTIPPRO"]),Convert.ToString(r_mlot["MLCLAVE1"]),
                                                        Convert.ToString(r_mlot["MLCLAVE2"]),Convert.ToString(r_mlot["MLCLAVE3"]),Convert.ToString(r_mlot["MLCLAVE4"]),Convert.ToString(r_mlot["MLCODCAL"]),
                                                        Convert.ToString(r_mlot["MLCDLOTE"]),Convert.ToString(r_mlot["MLCDTRAN"]),0,Convert.ToDouble(r_mlot["MLCANORI"]),
                                                        Convert.ToDouble(r_mlot["MLSALDOI"]),Convert.ToString(r_mlot["MLLOCALI"]),Convert.ToString(r_mlot["MLDTTEC1"]),Convert.ToString(r_mlot["MLDTTEC2"]),
                                                        Convert.ToString(r_mlot["MLDTTEC3"]),Convert.ToString(r_mlot["MLDTTEC4"]),Convert.ToDouble(r_mlot["MLDTTEC5"]),Convert.ToDouble(r_mlot["MLDTTEC6"]),
                                                        "AN",".",USUARIO,null);
                                    switch (lc_tipmov)
                                    {
                                        case "E":
                                            if (Convert.ToString(r_mlot["MLESTADO"]) == "AC")
                                                Obj.UpdateBalanLotCanCtl(oSessionManager, CODEMP, Convert.ToString(r_mlot["MLBODEGA"]), Convert.ToString(r_mlot["MLTIPPRO"]), Convert.ToString(r_mlot["MLCLAVE1"]),
                                                            Convert.ToString(r_mlot["MLCLAVE2"]), Convert.ToString(r_mlot["MLCLAVE3"]), Convert.ToString(r_mlot["MLCLAVE4"]), Convert.ToString(r_mlot["MLCODCAL"]), Convert.ToString(r_mlot["MLCDLOTE"]), -Convert.ToDouble(r_mlot["MLCANTID"]), USUARIO);
                                            else
                                                if (Convert.ToString(r_mlot["MLESTADO"]) == "CE")
                                                Obj.UpdateBalanLotCantid(oSessionManager, CODEMP, Convert.ToString(r_mlot["MLBODEGA"]), Convert.ToString(r_mlot["MLTIPPRO"]), Convert.ToString(r_mlot["MLCLAVE1"]),
                                                        Convert.ToString(r_mlot["MLCLAVE2"]), Convert.ToString(r_mlot["MLCLAVE3"]), Convert.ToString(r_mlot["MLCLAVE4"]), Convert.ToString(r_mlot["MLCODCAL"]), Convert.ToString(r_mlot["MLCDLOTE"]), -Convert.ToDouble(r_mlot["MLCANTID"]), USUARIO);
                                            break;
                                        case "S":
                                            if (Convert.ToString(r_mlot["MLESTADO"]) == "CE")
                                                Obj.UpdateBalanLotCantid(oSessionManager, CODEMP, Convert.ToString(r_mlot["MLBODEGA"]), Convert.ToString(r_mlot["MLTIPPRO"]), Convert.ToString(r_mlot["MLCLAVE1"]),
                                                        Convert.ToString(r_mlot["MLCLAVE2"]), Convert.ToString(r_mlot["MLCLAVE3"]), Convert.ToString(r_mlot["MLCLAVE4"]), Convert.ToString(r_mlot["MLCODCAL"]), Convert.ToString(r_mlot["MLCDLOTE"]), Convert.ToDouble(r_mlot["MLCANTID"]), USUARIO);
                                            break;
                                    }
                            }
                            
                        }
                            //Obj.UpdateMovimLot(
                    }
                
                //oSessionManager.CommitTranstaction();
                return 0;
            }
            catch (Exception ex)
            {
                //oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally { 
                Obj = null;
            }
        }
        public DataTable GetLotes(string connection, string BLCODEMP, string BLBODEGA, string BLTIPPRO, string BLCLAVE1, string BLCLAVE2, string BLCLAVE3, string BLCLAVE4, string BLCODCAL)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            MovimientosBD Obj = new MovimientosBD();
            try
            {
                return Obj.GetLotes(oSessionManager, BLCODEMP, BLBODEGA, BLTIPPRO, BLCLAVE1, BLCLAVE2, BLCLAVE3, BLCLAVE4, BLCODCAL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
            }
        }
        public DataTable GetLotesTF(string connection, string BLCODEMP, string TFTIPFAC, string BLTIPPRO, string BLCLAVE1, string BLCLAVE2, string BLCLAVE3, string BLCLAVE4, string BLCODCAL)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            MovimientosBD Obj = new MovimientosBD();
            try
            {
                return Obj.GetLotesTF(oSessionManager, BLCODEMP, TFTIPFAC, BLTIPPRO, BLCLAVE1, BLCLAVE2, BLCLAVE3, BLCLAVE4, BLCODCAL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
            }
        }
        public DataTable CargarMovimientoLot(string connection, string MLCODEMP, int MLIDMOVI, int MLIDITEM)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            MovimientosBD Obj = new MovimientosBD();
            try {
                return Obj.CargarMovimientoLot(oSessionManager, MLCODEMP, MLIDMOVI, MLIDITEM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
            }
        }
        public DataTable CargarMovimientoEle(string connection, string MECODEMP, int MEIDMOVI,int MEIDITEM, int MEIDLOTE)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            MovimientosBD Obj = new MovimientosBD();
            try
            {
                return Obj.CargarMovimientoEle(oSessionManager, MECODEMP, MEIDMOVI, MEIDITEM, MEIDLOTE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
            }
        }
        public DataTable CargarMovimientoEle(string connection, string inLlave)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            MovimientosBD Obj = new MovimientosBD();
            try
            {
                return Obj.CargarMovimientoEle(oSessionManager, inLlave);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
            }
        }
    }
}
