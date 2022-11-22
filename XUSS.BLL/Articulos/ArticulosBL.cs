using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using XUSS.DAL.Articulos;
using System.ComponentModel;
using XUSS.DAL.ListaPrecios;
using System.IO;
using XUSS.DAL.Parametros;

namespace XUSS.BLL.Articulos
{
    [DataObject(true)]
    public class ArticulosBL
    {
        public DataTable GetArticulos(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ArticulosBD.GetArticulos(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetArticulosD(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ArticulosBD.GetArticulosD(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetArticulosDINV(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ArticulosBD.GetArticulosDINV(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetTbBarras(string connection, string BCODEMP, string BTIPPRO, string BCLAVE1)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ArticulosBD.GetTbBarras(oSessionManager, BCODEMP, BTIPPRO, BCLAVE1);
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
        public DataTable GetTbBarras(string connection, string BCODIGO,string LT)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ArticulosBD.GetTbBarras(oSessionManager, BCODIGO, LT);
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
        public DataTable GetTbBarrasNoInv(string connection, string BCODIGO, string LT)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ArticulosBD.GetTbBarrasNoInv(oSessionManager, BCODIGO, LT);
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
        public DataTable GetTbBarrasInv(string connection, string BCODIGO, string LT, string BODEGA)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {                
                return ArticulosBD.GetTbBarrasInv(oSessionManager, BCODIGO, LT, BODEGA);
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
        public DataTable GetTbBarrasInv(string connection, string BCODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ArticulosBD.GetTbBarrasInv(oSessionManager,BCODIGO);
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
        public DataTable GetArticuloInv(string connection, string ARCLAVE1, string ARCLAVE2, string ARCLAVE3, string ARCLAVE4, string LT, string BODEGA)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ArticulosBD.GetArticuloInv(oSessionManager, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4, LT, BODEGA);
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
        public DataTable GetArticuloInv(string connection, string ARTIPPRO,string ARCLAVE1, string ARCLAVE2, string ARCLAVE3, string ARCLAVE4, string LT, string BODEGA)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ArticulosBD.GetArticuloInv(oSessionManager, ARTIPPRO,ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4, LT, BODEGA);
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
        public DataTable GetClave2(string connection, string ARCODEMP, string ARTIPPRO, string ARCLAVE1)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ArticulosBD.GetClave2(oSessionManager, ARCODEMP, ARTIPPRO, ARCLAVE1);
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
        public DataTable GetClave3(string connection, string ARCODEMP, string ARTIPPRO, string ARCLAVE1)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ArticulosBD.GetClave3(oSessionManager, ARCODEMP, ARTIPPRO, ARCLAVE1);
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
        public DataTable GetImagenes(string connection, string ARCODEMP, string ARTIPPRO, string ARCLAVE1)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ArticulosBD.GetImagenes(oSessionManager, ARCODEMP, ARTIPPRO, ARCLAVE1);
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
        public int InsertArticulo(string connection, string ARCODEMP, string ARTIPPRO, string ARCLAVE1, string ARCLAVE2, string ARCLAVE3, string ARCLAVE4, string ARNOMBRE, string ARUNDINV, string ARUMALT1, string ARUMALT2, double? ARFCA1IN, double? ARFCA2IN, string ARCDALTR,
                            string ARMONEDA, double? ARCOSTOA, double? ARCSTMPR, double? ARCSTMOB, double? ARCSTCIF, double? ARCOSTOB, double ARPRECIO, string ARCDIMPF, string ARORIGEN, string ARPOSARA, double? ARPESOUN, string ARPESOUM, string ARCDCLA1, string ARCDCLA2, string ARCDCLA3, string ARCDCLA4,
                            string ARCDCLA6, string ARCDCLA7, string ARCDCLA8, string ARCDCLA9, string ARCDCLA10, string ARDTTEC1, string ARDTTEC2, string ARDTTEC3, string ARDTTEC4, string ARDTTEC5, double? ARDTTEC6, string ARDTTEC7, string ARDTTEC8, string ARDTTEC9, string ARDTTEC10, int? ARCODPRO, DateTime? ARFECCOM, double? ARPRECOM, string ARMONCOM, int ARPROCOM, int ARPROGDT,
                            double? ARFCINA1, string ARCOMPOS, string ARCONVEN, string ARMERCON, string ARCODCOM, string ARCDCLA5, int? ARCAOPDS, string ARUNDODS, string ARTIPTAR, string ARANO, string ARCOLECCION, string ARPRIORIDAD, string TR_PROCEDENCIA,
                            string TR_UEN, string TR_TP, string TR_SCT, string TR_FONDO, string TR_TEJIDO, string ARESTADO, string ARCAUSAE, string ARNMUSER, object tbclave2, object tbclave3, object tbPrecios, object tbBarras)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try {
                oSessionManager.BeginTransaction();
                if ((tbclave2 as DataTable).Rows.Count > 0)
                {
                    foreach (DataRow rw in (tbclave2 as DataTable).Rows)
                    {
                        if ((tbclave3 as DataTable).Rows.Count > 0)
                        {
                            foreach (DataRow rx in (tbclave3 as DataTable).Rows)
                            {
                                ArticulosBD.InsertArticulo(oSessionManager, ARCODEMP, ARTIPPRO, ARCLAVE1, Convert.ToString(rw["ARCLAVE2"]), Convert.ToString(rx["ARCLAVE3"]), ARCLAVE4, ARNOMBRE, ARUNDINV, ARUMALT1, ARUMALT2, ARFCA1IN, ARFCA2IN, ARCDALTR,
                                           ARMONEDA, ARCOSTOA, ARCSTMPR, ARCSTMOB, ARCSTCIF, ARCOSTOB, ARPRECIO, ARCDIMPF, ARORIGEN, ARPOSARA, ARPESOUN, ARPESOUM, ARCDCLA1, ARCDCLA2, ARCDCLA3, ARCDCLA4,
                                           ARCDCLA6, ARCDCLA7, ARCDCLA8, ARCDCLA9, ARCDCLA10, ARDTTEC1, ARDTTEC2, ARDTTEC3, ARDTTEC4, ARDTTEC5, ARDTTEC6, ARDTTEC7, ARDTTEC8, ARDTTEC9, ARDTTEC10, ARCODPRO, ARFECCOM, ARPRECOM, ARMONCOM, ARPROCOM, ARPROGDT,
                                           ARFCINA1, ARCOMPOS, ARCONVEN, ARMERCON, ARCODCOM, ARCDCLA5, ARCAOPDS, ARUNDODS, ARTIPTAR, ARANO, ARCOLECCION, ARPRIORIDAD, TR_PROCEDENCIA,
                                           TR_UEN, TR_TP, TR_SCT, TR_FONDO, TR_TEJIDO, ARESTADO, ARCAUSAE, ARNMUSER);
                            }
                        }
                        else
                        {
                            ArticulosBD.InsertArticulo(oSessionManager, ARCODEMP, ARTIPPRO, ARCLAVE1, Convert.ToString(rw["ARCLAVE2"]), ".", ARCLAVE4, ARNOMBRE, ARUNDINV, ARUMALT1, ARUMALT2, ARFCA1IN, ARFCA2IN, ARCDALTR,
                                           ARMONEDA, ARCOSTOA, ARCSTMPR, ARCSTMOB, ARCSTCIF, ARCOSTOB, ARPRECIO, ARCDIMPF, ARORIGEN, ARPOSARA, ARPESOUN, ARPESOUM, ARCDCLA1, ARCDCLA2, ARCDCLA3, ARCDCLA4,
                                           ARCDCLA6, ARCDCLA7, ARCDCLA8, ARCDCLA9, ARCDCLA10, ARDTTEC1, ARDTTEC2, ARDTTEC3, ARDTTEC4, ARDTTEC5, ARDTTEC6, ARDTTEC7, ARDTTEC8, ARDTTEC9, ARDTTEC10, ARCODPRO, ARFECCOM, ARPRECOM, ARMONCOM, ARPROCOM, ARPROGDT,
                                           ARFCINA1, ARCOMPOS, ARCONVEN, ARMERCON, ARCODCOM, ARCDCLA5, ARCAOPDS, ARUNDODS, ARTIPTAR, ARANO, ARCOLECCION, ARPRIORIDAD, TR_PROCEDENCIA,
                                           TR_UEN, TR_TP, TR_SCT, TR_FONDO, TR_TEJIDO, ARESTADO, ARCAUSAE, ARNMUSER);
                        }
                    }
                }
                else
                {
                    ArticulosBD.InsertArticulo(oSessionManager, ARCODEMP, ARTIPPRO, ARCLAVE1, ".", ".", ARCLAVE4, ARNOMBRE, ARUNDINV, ARUMALT1, ARUMALT2, ARFCA1IN, ARFCA2IN, ARCDALTR,
                                           ARMONEDA, ARCOSTOA, ARCSTMPR, ARCSTMOB, ARCSTCIF, ARCOSTOB, ARPRECIO, ARCDIMPF, ARORIGEN, ARPOSARA, ARPESOUN, ARPESOUM, ARCDCLA1, ARCDCLA2, ARCDCLA3, ARCDCLA4,
                                           ARCDCLA6, ARCDCLA7, ARCDCLA8, ARCDCLA9, ARCDCLA10, ARDTTEC1, ARDTTEC2, ARDTTEC3, ARDTTEC4, ARDTTEC5, ARDTTEC6, ARDTTEC7, ARDTTEC8, ARDTTEC9, ARDTTEC10, ARCODPRO, ARFECCOM, ARPRECOM, ARMONCOM, ARPROCOM, ARPROGDT,
                                           ARFCINA1, ARCOMPOS, ARCONVEN, ARMERCON, ARCODCOM, ARCDCLA5, ARCAOPDS, ARUNDODS, ARTIPTAR, ARANO, ARCOLECCION, ARPRIORIDAD, TR_PROCEDENCIA,
                                           TR_UEN, TR_TP, TR_SCT, TR_FONDO, TR_TEJIDO, ARESTADO, ARCAUSAE, ARNMUSER);
                }

                
                //Lista de Precios
                foreach (DataRow rw in (tbPrecios as DataTable).Rows)
                {
                    if (ListaPreciosBD.ExisteListaPrecioDT(oSessionManager, ARCODEMP, Convert.ToString(rw["P_RLISPRE"]), Convert.ToString(rw["P_RTIPPRO"]), Convert.ToString(rw["P_RCLAVE1"]), Convert.ToString(rw["P_RCLAVE2"]), Convert.ToString(rw["P_RCLAVE3"]), Convert.ToString(rw["P_RCLAVE4"])) > 0)
                    {
                        ListaPreciosBD.UpdateListaPrecioDT(oSessionManager, ARCODEMP, Convert.ToString(rw["P_RLISPRE"]), null, Convert.ToString(rw["P_RTIPPRO"]), Convert.ToString(rw["P_RCLAVE1"]), Convert.ToString(rw["P_RCLAVE2"]),
                                                           Convert.ToString(rw["P_RCLAVE3"]), Convert.ToString(rw["P_RCLAVE4"]), ".", ".", "UN", Convert.ToDouble(rw["P_RPRECIO"]), 1, "AC", ".", ARNMUSER);
                    }
                    else
                    {
                        ListaPreciosBD.InsertListaPrecioDT(oSessionManager, ARCODEMP, Convert.ToString(rw["P_RLISPRE"]), null, Convert.ToString(rw["P_RTIPPRO"]), Convert.ToString(rw["P_RCLAVE1"]), Convert.ToString(rw["P_RCLAVE2"]),
                                                           Convert.ToString(rw["P_RCLAVE3"]), Convert.ToString(rw["P_RCLAVE4"]), ".", ".", "UN", Convert.ToDouble(rw["P_RPRECIO"]), 1, "AC", ".", ARNMUSER);
                    }
                }
                
                //Barras
                foreach (DataRow rw in (tbBarras as DataTable).Rows)
                {
                    if (ArticulosBD.ExisteBarras(oSessionManager, ARCODEMP, Convert.ToString(rw["BCODIGO"])) == 0)
                    {
                        ArticulosBD.InserTbBarras(oSessionManager, ARCODEMP, Convert.ToString(rw["BCODIGO"]), ARTIPPRO, ARCLAVE1, Convert.ToString(rw["BCLAVE2"]), Convert.ToString(rw["BCLAVE3"]), ".", ".", "770", "001", ARNMUSER);
                    }
                }
                
                 

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
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public int UpdateArticulo(string connection, string ARCODEMP, string ARTIPPRO, string ARCLAVE1, string ARCLAVE2, string ARCLAVE3, string ARCLAVE4, string ARNOMBRE, string ARUNDINV, string ARUMALT1, string ARUMALT2, double? ARFCA1IN, double? ARFCA2IN, string ARCDALTR,
                            string ARMONEDA, double? ARCOSTOA, double? ARCSTMPR, double? ARCSTMOB, double? ARCSTCIF, double? ARCOSTOB, double ARPRECIO, string ARCDIMPF, string ARORIGEN, string ARPOSARA, double? ARPESOUN, string ARPESOUM, string ARCDCLA1, string ARCDCLA2, string ARCDCLA3, string ARCDCLA4,
                            string ARCDCLA6, string ARCDCLA7, string ARCDCLA8, string ARCDCLA9, string ARCDCLA10, string ARDTTEC1, string ARDTTEC2, string ARDTTEC3, string ARDTTEC4, string ARDTTEC5, double? ARDTTEC6, string ARDTTEC7, string ARDTTEC8, string ARDTTEC9, string ARDTTEC10, int? ARCODPRO, DateTime? ARFECCOM, double? ARPRECOM, string ARMONCOM, int ARPROCOM, int ARPROGDT,
                            double? ARFCINA1, string ARCOMPOS, string ARCONVEN, string ARMERCON, string ARCODCOM, string ARCDCLA5, int? ARCAOPDS, string ARUNDODS, string ARTIPTAR, string ARANO, string ARCOLECCION, string ARPRIORIDAD, string TR_PROCEDENCIA,
                            string TR_UEN, string TR_TP, string TR_SCT, string TR_FONDO, string TR_TEJIDO, string ARESTADO, string ARCAUSAE, string ARNMUSER, object tbclave2, object tbclave3, object tbPrecios,object tbSoportes, object tbOrigen, object tbTester,object tbBarras,object tbRSanitario)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                oSessionManager.BeginTransaction();
                if ((tbclave2 as DataTable).Rows.Count > 1)
                {
                    foreach (DataRow rw in (tbclave2 as DataTable).Rows)
                    {
                        if ((tbclave3 as DataTable).Rows.Count > 0)
                        {
                            foreach (DataRow rx in (tbclave3 as DataTable).Rows)
                            {
                                if (ArticulosBD.ExisteArticulo(oSessionManager, ARCODEMP, ARTIPPRO, ARCLAVE1, Convert.ToString(rw["ARCLAVE2"]), Convert.ToString(rx["ARCLAVE3"]), ARCLAVE4) > 0)
                                {
                                    ArticulosBD.UpdateArticulo(oSessionManager, ARCODEMP, ARTIPPRO, ARCLAVE1, Convert.ToString(rw["ARCLAVE2"]), Convert.ToString(rx["ARCLAVE3"]), ARCLAVE4, ARNOMBRE, ARUNDINV, ARUMALT1, ARUMALT2, ARFCA1IN, ARFCA2IN, ARCDALTR,
                                               ARMONEDA, ARCOSTOA, ARCSTMPR, ARCSTMOB, ARCSTCIF, ARCOSTOB, ARPRECIO, ARCDIMPF, ARORIGEN, ARPOSARA, ARPESOUN, ARPESOUM, ARCDCLA1, ARCDCLA2, ARCDCLA3, ARCDCLA4,
                                               ARCDCLA6, ARCDCLA7, ARCDCLA8, ARCDCLA9, ARCDCLA10, ARDTTEC1, ARDTTEC2, ARDTTEC3, ARDTTEC4, ARDTTEC5, ARDTTEC6, ARDTTEC7, ARDTTEC8, ARDTTEC9, ARDTTEC10, ARCODPRO, ARFECCOM, ARPRECOM, ARMONCOM, ARPROCOM, ARPROGDT,
                                               ARFCINA1, ARCOMPOS, ARCONVEN, ARMERCON, ARCODCOM, ARCDCLA5, ARCAOPDS, ARUNDODS, ARTIPTAR, ARANO, ARCOLECCION, ARPRIORIDAD, TR_PROCEDENCIA,
                                               TR_UEN, TR_TP, TR_SCT, TR_FONDO, TR_TEJIDO, ARESTADO, ARCAUSAE, ARNMUSER);
                                }
                                else
                                {
                                    ArticulosBD.InsertArticulo(oSessionManager, ARCODEMP, ARTIPPRO, ARCLAVE1, Convert.ToString(rw["ARCLAVE2"]), Convert.ToString(rx["ARCLAVE3"]), ARCLAVE4, ARNOMBRE, ARUNDINV, ARUMALT1, ARUMALT2, ARFCA1IN, ARFCA2IN, ARCDALTR,
                                           ARMONEDA, ARCOSTOA, ARCSTMPR, ARCSTMOB, ARCSTCIF, ARCOSTOB, ARPRECIO, ARCDIMPF, ARORIGEN, ARPOSARA, ARPESOUN, ARPESOUM, ARCDCLA1, ARCDCLA2, ARCDCLA3, ARCDCLA4,
                                           ARCDCLA6, ARCDCLA7, ARCDCLA8, ARCDCLA9, ARCDCLA10, ARDTTEC1, ARDTTEC2, ARDTTEC3, ARDTTEC4, ARDTTEC5, ARDTTEC6, ARDTTEC7, ARDTTEC8, ARDTTEC9, ARDTTEC10, ARCODPRO, ARFECCOM, ARPRECOM, ARMONCOM, ARPROCOM, ARPROGDT,
                                           ARFCINA1, ARCOMPOS, ARCONVEN, ARMERCON, ARCODCOM, ARCDCLA5, ARCAOPDS, ARUNDODS, ARTIPTAR, ARANO, ARCOLECCION, ARPRIORIDAD, TR_PROCEDENCIA,
                                           TR_UEN, TR_TP, TR_SCT, TR_FONDO, TR_TEJIDO, ARESTADO, ARCAUSAE, ARNMUSER);
                                }
                            }
                        }
                        else
                        {
                            ArticulosBD.UpdateArticulo(oSessionManager, ARCODEMP, ARTIPPRO, ARCLAVE1, Convert.ToString(rw["ARCLAVE2"]), ARCLAVE3, ARCLAVE4, ARNOMBRE, ARUNDINV, ARUMALT1, ARUMALT2, ARFCA1IN, ARFCA2IN, ARCDALTR,
                                           ARMONEDA, ARCOSTOA, ARCSTMPR, ARCSTMOB, ARCSTCIF, ARCOSTOB, ARPRECIO, ARCDIMPF, ARORIGEN, ARPOSARA, ARPESOUN, ARPESOUM, ARCDCLA1, ARCDCLA2, ARCDCLA3, ARCDCLA4,
                                           ARCDCLA6, ARCDCLA7, ARCDCLA8, ARCDCLA9, ARCDCLA10, ARDTTEC1, ARDTTEC2, ARDTTEC3, ARDTTEC4, ARDTTEC5, ARDTTEC6, ARDTTEC7, ARDTTEC8, ARDTTEC9, ARDTTEC10, ARCODPRO, ARFECCOM, ARPRECOM, ARMONCOM, ARPROCOM, ARPROGDT,
                                           ARFCINA1, ARCOMPOS, ARCONVEN, ARMERCON, ARCODCOM, ARCDCLA5, ARCAOPDS, ARUNDODS, ARTIPTAR, ARANO, ARCOLECCION, ARPRIORIDAD, TR_PROCEDENCIA,
                                           TR_UEN, TR_TP, TR_SCT, TR_FONDO, TR_TEJIDO, ARESTADO, ARCAUSAE, ARNMUSER);
                        }
                    }
                }
                else
                {
                    if (ArticulosBD.ExisteArticulo(oSessionManager, ARCODEMP, ARTIPPRO, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4) > 0)
                    {
                        ArticulosBD.UpdateArticulo(oSessionManager, ARCODEMP, ARTIPPRO, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4, ARNOMBRE, ARUNDINV, ARUMALT1, ARUMALT2, ARFCA1IN, ARFCA2IN, ARCDALTR,
                                   ARMONEDA, ARCOSTOA, ARCSTMPR, ARCSTMOB, ARCSTCIF, ARCOSTOB, ARPRECIO, ARCDIMPF, ARORIGEN, ARPOSARA, ARPESOUN, ARPESOUM, ARCDCLA1, ARCDCLA2, ARCDCLA3, ARCDCLA4,
                                   ARCDCLA6, ARCDCLA7, ARCDCLA8, ARCDCLA9, ARCDCLA10, ARDTTEC1, ARDTTEC2, ARDTTEC3, ARDTTEC4, ARDTTEC5, ARDTTEC6, ARDTTEC7, ARDTTEC8, ARDTTEC9, ARDTTEC10, ARCODPRO, ARFECCOM, ARPRECOM, ARMONCOM, ARPROCOM, ARPROGDT,
                                   ARFCINA1, ARCOMPOS, ARCONVEN, ARMERCON, ARCODCOM, ARCDCLA5, ARCAOPDS, ARUNDODS, ARTIPTAR, ARANO, ARCOLECCION, ARPRIORIDAD, TR_PROCEDENCIA,
                                   TR_UEN, TR_TP, TR_SCT, TR_FONDO, TR_TEJIDO, ARESTADO, ARCAUSAE, ARNMUSER);
                    }
                    else
                    {
                        ArticulosBD.InsertArticulo(oSessionManager, ARCODEMP, ARTIPPRO, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4, ARNOMBRE, ARUNDINV, ARUMALT1, ARUMALT2, ARFCA1IN, ARFCA2IN, ARCDALTR,
                               ARMONEDA, ARCOSTOA, ARCSTMPR, ARCSTMOB, ARCSTCIF, ARCOSTOB, ARPRECIO, ARCDIMPF, ARORIGEN, ARPOSARA, ARPESOUN, ARPESOUM, ARCDCLA1, ARCDCLA2, ARCDCLA3, ARCDCLA4,
                               ARCDCLA6, ARCDCLA7, ARCDCLA8, ARCDCLA9, ARCDCLA10, ARDTTEC1, ARDTTEC2, ARDTTEC3, ARDTTEC4, ARDTTEC5, ARDTTEC6, ARDTTEC7, ARDTTEC8, ARDTTEC9, ARDTTEC10, ARCODPRO, ARFECCOM, ARPRECOM, ARMONCOM, ARPROCOM, ARPROGDT,
                               ARFCINA1, ARCOMPOS, ARCONVEN, ARMERCON, ARCODCOM, ARCDCLA5, ARCAOPDS, ARUNDODS, ARTIPTAR, ARANO, ARCOLECCION, ARPRIORIDAD, TR_PROCEDENCIA,
                               TR_UEN, TR_TP, TR_SCT, TR_FONDO, TR_TEJIDO, ARESTADO, ARCAUSAE, ARNMUSER);
                    }
                }

                //Lista de Precios
                foreach (DataRow rw in (tbPrecios as DataTable).Rows)
                {
                    if (ListaPreciosBD.ExisteListaPrecioDT(oSessionManager, ARCODEMP, Convert.ToString(rw["P_RLISPRE"]), Convert.ToString(rw["P_RTIPPRO"]), Convert.ToString(rw["P_RCLAVE1"]), Convert.ToString(rw["P_RCLAVE2"]), Convert.ToString(rw["P_RCLAVE3"]), Convert.ToString(rw["P_RCLAVE4"])) > 0)
                    {
                        ListaPreciosBD.UpdateListaPrecioDT(oSessionManager, ARCODEMP, Convert.ToString(rw["P_RLISPRE"]), null, Convert.ToString(rw["P_RTIPPRO"]), Convert.ToString(rw["P_RCLAVE1"]), Convert.ToString(rw["P_RCLAVE2"]),
                                                           Convert.ToString(rw["P_RCLAVE3"]), Convert.ToString(rw["P_RCLAVE4"]), ".", ".", "UN", Convert.ToDouble(rw["P_RPRECIO"]), 1, "AC", ".", ARNMUSER);
                    }
                    else {
                        ListaPreciosBD.InsertListaPrecioDT(oSessionManager, ARCODEMP, Convert.ToString(rw["P_RLISPRE"]), null, Convert.ToString(rw["P_RTIPPRO"]), Convert.ToString(rw["P_RCLAVE1"]), Convert.ToString(rw["P_RCLAVE2"]),
                                                           Convert.ToString(rw["P_RCLAVE3"]), Convert.ToString(rw["P_RCLAVE4"]), ".", ".", "UN", Convert.ToDouble(rw["P_RPRECIO"]), 1, "AC", ".", ARNMUSER);
                    }
                }
                //Soportes
                foreach (DataRow rw in (tbSoportes as DataTable).Rows)
                {
                    if (SoportesBD.ExisteSoporteArticulo(oSessionManager, Convert.ToInt32(rw["SP_CONSECUTIVO"]), ARTIPPRO,ARCLAVE1) == 0)
                    {
                        Stream ioArchivo = File.OpenRead(Convert.ToString(rw["RUTA"]));
                        byte[] result;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            ioArchivo.CopyTo(ms);
                            result = ms.ToArray();
                        }

                        SoportesBD.InsertSoporte(oSessionManager, Convert.ToString(rw["SP_TIPO"]), 0, Convert.ToString(rw["SP_DESCRIPCION"]), Convert.ToString(rw["SP_EXTENCION"]), result, ARNMUSER, ARTIPPRO, ARCLAVE1);

                    }
                }
                //Origen
                foreach (DataRow rw in (tbOrigen as DataTable).Rows)
                {
                    if (ArticulosBD.ExisteOrigen(oSessionManager, ARCODEMP, ARTIPPRO, ARCLAVE1, ".", ".", ".", Convert.ToString(rw["OR_REFERENCIA"])) == 0)
                    {
                        ArticulosBD.InsertOrigen(oSessionManager, ARCODEMP, ARTIPPRO, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4, Convert.ToString(rw["OR_REFERENCIA"]), ARNMUSER, "AC");
                    }
                }
                //Barras
                foreach (DataRow rw in (tbBarras as DataTable).Rows)
                {
                    if (ArticulosBD.ExisteBarras(oSessionManager,ARCODEMP,Convert.ToString(rw["BCODIGO"]))==0)
                    {
                        ArticulosBD.InserTbBarras(oSessionManager, ARCODEMP, Convert.ToString(rw["BCODIGO"]), ARTIPPRO, ARCLAVE1, Convert.ToString(rw["BCLAVE2"]), Convert.ToString(rw["BCLAVE3"]), ".", ".", "770", "001", ARNMUSER);
                    }
                }
                //Tester
                foreach (DataRow rw in (tbTester as DataTable).Rows)
                {
                    if (ArticulosBD.ExisteTester(oSessionManager, ARCODEMP, ARTIPPRO, ARCLAVE1, ".", ".", ".", ARCODEMP, Convert.ToString(rw["TT_TIPPRO"]), Convert.ToString(rw["TT_CLAVE1"]), ".", ".", ".") == 0)
                    {
                        ArticulosBD.InsertTester(oSessionManager, ARCODEMP, ARTIPPRO, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4, ARCODEMP, Convert.ToString(rw["TT_TIPPRO"]),
                                                 Convert.ToString(rw["TT_CLAVE1"]), Convert.ToString(rw["TT_CLAVE2"]), Convert.ToString(rw["TT_CLAVE3"]), Convert.ToString(rw["TT_CLAVE4"]), ARNMUSER, "AC");
                    }
                }
                //Registro Sanitario
                foreach (DataRow rw in (tbRSanitario as DataTable).Rows)
                {
                    if (ArticulosBD.ExisteRegistroSanitario(oSessionManager, Convert.ToInt32(rw["RS_CODIGO"])) == 0)
                    {
                        ArticulosBD.InsertRegistroSanitario(oSessionManager, ARCODEMP, ARTIPPRO, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4, Convert.ToString(rw["RS_REGISTRO"]), Convert.ToDateTime(rw["RS_FEINICIO"]), Convert.ToDateTime(rw["RS_FECFINAL"]), ARNMUSER, "AC");
                    }
                    else
                    {
                        ArticulosBD.UpdateRegistroSanitario(oSessionManager, Convert.ToString(rw["RS_REGISTRO"]), Convert.ToDateTime(rw["RS_FEINICIO"]), Convert.ToDateTime(rw["RS_FECFINAL"]), ARNMUSER, Convert.ToString(rw["RS_ESTADO"]),Convert.ToInt32(rw["RS_CODIGO"]));
                    }
                }

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
        public IDataReader GetClavesAlternas(string connection, string ASCODEMP, string ASTIPPRO, int ASNIVELC)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ArticulosBD.GetClavesAlternas(oSessionManager, ASCODEMP, ASTIPPRO, ASNIVELC);
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
        public DataTable GetArticulos(string connection, string filter)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ArticulosBD.GetArticulos(oSessionManager, filter);
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
        public DataTable GetArticulosInv(string connection, string inFilter, string inBodega)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ArticulosBD.GetArticuloInv(oSessionManager, inFilter,inBodega);
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
        public int DeleteTbBarras(string connection, string BCODEMP, string BCODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try {
                return ArticulosBD.DeleteTbBarras(oSessionManager, BCODEMP, BCODIGO);
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
        //Imagenes/Fotografias/Dibujos
        #region
        public int InsertImagenArticulo(string connection, string IM_CODEMP, string IM_TIPPRO, string IM_CLAVE1, string IM_CLAVE2, string IM_CLAVE3, string IM_CLAVE4, int IM_TIPIMA, string inRuta, string IM_NMUSER)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            Stream ioArchivo = File.OpenRead(inRuta);
            byte[] result;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    ioArchivo.CopyTo(ms);
                    result = ms.ToArray();
                }
                ArticulosBD.InsertImagen(oSessionManager, IM_CODEMP, IM_TIPPRO, IM_CLAVE1, IM_CLAVE2, IM_CLAVE3, IM_CLAVE4, IM_TIPIMA, result, IM_NMUSER);
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
                ioArchivo = null;
                result = null;
            }
        }
        public int DeteleImagen(string connection, int IM_CONSECUTIVO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ArticulosBD.DeteleImagen(oSessionManager, IM_CONSECUTIVO);
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
        #endregion
        //Tester - Origen
        #region
        public DataTable GetTester(string connection, string ARCODEMP, string ARTIPPRO, string ARCLAVE1, string ARCLAVE2, string ARCLAVE3, string ARCLAVE4)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try {
                return ArticulosBD.GetTester(oSessionManager, ARCODEMP, ARTIPPRO, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4);
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
        public DataTable GetTesterInv(string connection, string ARCODEMP, string ARTIPPRO, string ARCLAVE1, string ARCLAVE2, string ARCLAVE3, string ARCLAVE4,string BBBODEGA)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ArticulosBD.GetTesterInv(oSessionManager, ARCODEMP, ARTIPPRO, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4, BBBODEGA);
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
        public DataTable GetOrigen(string connection, string ARCODEMP, string ARTIPPRO, string ARCLAVE1, string ARCLAVE2, string ARCLAVE3, string ARCLAVE4)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ArticulosBD.GetOrigen(oSessionManager, ARCODEMP, ARTIPPRO, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4);
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
        public string GetReffromOrigen(string connection, string ARCODEMP, string OR_REFERENCIA)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ArticulosBD.GetReffromOrigen(oSessionManager, ARCODEMP, OR_REFERENCIA);
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
        public int DeteleTester(string connection, int TT_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try {
                return ArticulosBD.DeleteTester(oSessionManager, TT_CODIGO);
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
        public int DeleteOrigen(string connection, int OR_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ArticulosBD.DeleteOrigen(oSessionManager, OR_CODIGO);
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
        #endregion
        //Cod Aranceles
        #region
        public DataTable GetAranceles(string connection, string filter)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ArticulosBD.GetAranceles(oSessionManager, filter);
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
        #endregion
        //Registros Sanitarios
        #region
        public DataTable GetRegistrosSanitarios(string connection, string ARCODEMP, string ARTIPPRO, string ARCLAVE1, string ARCLAVE2, string ARCLAVE3, string ARCLAVE4)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ArticulosBD.GetRegistrosSanitarios(oSessionManager, ARCODEMP, ARTIPPRO, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4);
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
        public int DeleteRegistroSanitario(string connection, int RS_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ArticulosBD.DeleteRegistroSanitario(oSessionManager, RS_CODIGO);
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
        #endregion        
    }
}
