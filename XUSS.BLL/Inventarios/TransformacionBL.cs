using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XUSS.DAL.Comun;
using XUSS.DAL.Inventarios;

namespace XUSS.BLL.Inventarios
{
    public class TransformacionBL
    {
        public DataTable GetTransformacion(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            TransformacionBD Obj = new TransformacionBD();
            try
            {
                return Obj.GetTransformacion(oSessionManager, filter, startRowIndex, maximumRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
                Obj = null;
            }
        }

        public int InsertTransformacion(string connection, string TR_CODEMP, int TR_NROTRA, DateTime TR_FECTRA, string TR_BODEGA, string TR_CDTRAN, string TR_OTTRAN,
                                  int TR_MOVENT, int TR_MOVSAL, string TR_COMENT, string TR_ESTADO, string TR_CAUSAE, string TR_NMUSER, string P_CLISPRE,  object tbitems, object tbTester)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            TransformacionBD Obj = new TransformacionBD();
            MovimientosBL ObjM = new MovimientosBL();


            int MIOTMOVI = 0, MIIDMOVI = 0;

            try
            {
                oSessionManager.BeginTransaction();
                //Genera Nro Traslado
                TR_NROTRA = ComunBD.GeneraConsecutivo(oSessionManager, "CODTRAN", TR_CODEMP);
                TR_FECTRA = System.DateTime.Today;

                //Genera Movimiento Salida
                MIIDMOVI = ObjM.InsertMovimiento(oSessionManager, TR_CODEMP, MIOTMOVI, TR_BODEGA, null, "18", 0, 0, 0, null, TR_FECTRA, null, "CE", ".", TR_NMUSER, 0, 0, TR_NROTRA, null, 0, null, null, null);
                foreach (DataRow rw in (tbitems as DataTable).Rows)
                {
                    ObjM.InsertMovimiento(oSessionManager, TR_CODEMP, TR_BODEGA, null, TR_FECTRA, "18", Convert.ToString(rw["TP"]), Convert.ToString(rw["C1"]), Convert.ToString(rw["C2"]), Convert.ToString(rw["C3"]),
                                          Convert.ToString(rw["C4"]), ".", Convert.ToDouble(rw["MBCANTID"]), Convert.ToDouble(rw["MBCANTID"]), "UN", MIIDMOVI, MIOTMOVI, Convert.ToInt32(rw["IT"]), Convert.ToString(rw["MLCDLOTE"]), null, null, null,
                                          null, null, 0, 0, "CE", "CE", ".", TR_NMUSER, 0, 0);
                }

                //Movimiento de Entrada
                MIOTMOVI = ObjM.InsertMovimiento(oSessionManager, TR_CODEMP, MIOTMOVI, TR_BODEGA, null, "19", 0, 0, 0, null, TR_FECTRA, null, "AC", ".", TR_NMUSER, 0, 0, TR_NROTRA, null, 0, null, null, null);
                foreach (DataRow rw in (tbTester as DataTable).Rows)
                {
                    ObjM.InsertMovimiento(oSessionManager, TR_CODEMP, TR_BODEGA,null , System.DateTime.Today, "19", Convert.ToString(rw["TP"]), Convert.ToString(rw["C1"]), Convert.ToString(rw["C2"]), Convert.ToString(rw["C3"]),
                                          Convert.ToString(rw["C4"]), ".", Convert.ToDouble(rw["MBCANTID"]), Convert.ToDouble(rw["MBCANTID"]), "UN", MIOTMOVI, MIIDMOVI, Convert.ToInt32(rw["IT"]), Convert.ToString(rw["MLCDLOTE"]), null, null, null,
                                          null, null, 0, 0, "CE", "CE", ".", TR_NMUSER, 0, 0);
                }

                //Inserta Traslado
                Obj.InsertTransformacion(oSessionManager, TR_CODEMP, TR_NROTRA, TR_FECTRA, TR_BODEGA, TR_CDTRAN, TR_OTTRAN, MIOTMOVI, MIIDMOVI, TR_COMENT, P_CLISPRE, TR_ESTADO, TR_CAUSAE, TR_NMUSER);                

                //Inserta Soportes
                //foreach (DataRow rw in (tbSoportes as DataTable).Rows)
                //{
                //    if (SoportesBD.ExisteImagen(oSessionManager, Convert.ToInt32(rw["SP_CONSECUTIVO"]), TSNROTRA) == 0)
                //    {
                //        Stream ioArchivo = File.OpenRead(Convert.ToString(rw["RUTA"]));
                //        byte[] result;
                //        using (MemoryStream ms = new MemoryStream())
                //        {
                //            ioArchivo.CopyTo(ms);
                //            result = ms.ToArray();
                //        }

                //        SoportesBD.InsertSoporte(oSessionManager, Convert.ToString(rw["SP_TIPO"]), TSNROTRA, Convert.ToString(rw["SP_DESCRIPCION"]), Convert.ToString(rw["SP_EXTENCION"]), result, TSNMUSER, null, null);

                //    }
                //}
                oSessionManager.CommitTranstaction();
                return TR_NROTRA;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager = null;
                ObjM = null;
                Obj = null;
            }
        }

    }
}
