using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WFC_ORION.DAL;
using WFC_ORION.Models;

namespace WFC_ORION.BLL
{
    public class articuloBL
    {
        public List<articulo> getArticulos(string inConnection, string inFiltro)
        {
            DBAccess Obj = new DBAccess();
            List<articulo> lst = new List<articulo>();
            try
            {
                Obj.ConnectionString = inConnection;
                using (SqlDataReader reader = articuloBD.getArticulos(Obj, inFiltro))
                {
                    while (reader.Read())
                    {
                        lst.Add(getArticulo(reader));
                    }
                }
                return lst;
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
        public List<articuloimagenes> getArticulosImagenes(string inConnection, string inFiltro)
        {
            DBAccess Obj = new DBAccess();
            List<articuloimagenes> lst = new List<articuloimagenes>();
            
            int i = 0;
            try
            {
                Obj.ConnectionString = inConnection;                
                foreach (DataRow ra in articuloBD.getArticulosTable(Obj, inFiltro).Rows)
                {
                    articuloimagenes item = new articuloimagenes();
                    i++;
                    item.id = i;
                    item.inarticulo.Add(getArticulo(ra));
                    foreach (DataRow rw in articuloBD.getImagenArticuloTable(Obj, Convert.ToString(ra["ARCODEMP"]), Convert.ToString(ra["ARTIPPRO"]), Convert.ToString(ra["ARCLAVE1"])).Rows)
                        item.inimagen.Add(imagenesBL.getImagenesItem(rw));
                    lst.Add(item);
                    item = null;
                }                 
                return lst;
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
        public static articulo getArticulo(SqlDataReader Reader) {
            articulo item = new articulo();
            try {
                item.arcodemp = Convert.ToString(Reader["ARCODEMP"]);
                item.artippro = Convert.ToString(Reader["ARTIPPRO"]);
                item.arclave1 = Convert.ToString(Reader["ARCLAVE1"]);
                item.arclave2 = Convert.ToString(Reader["ARCLAVE2"]);
                item.arclave3 = Convert.ToString(Reader["ARCLAVE3"]);
                item.arclave4 = Convert.ToString(Reader["ARCLAVE4"]);
                item.arnombre = Convert.ToString(Reader["ARNOMBRE"]);
                item.arundinv = Convert.ToString(Reader["ARUNDINV"]);
                item.arumalt1 = Convert.ToString(Reader["ARUMALT1"]);
                item.arumalt2 = Convert.ToString(Reader["ARUMALT2"]);
                item.arfca1in = Reader.IsDBNull(Reader.GetOrdinal("ARFCA1IN")) ? null : (double?)Convert.ToDouble(Reader["ARFCA1IN"]); 
                item.arfca2in = Reader.IsDBNull(Reader.GetOrdinal("ARFCA2IN")) ? null : (double?)Convert.ToDouble(Reader["ARFCA2IN"]);
                item.arcdaltr = Convert.ToString(Reader["ARCDALTR"]);
                item.armoneda = Convert.ToString(Reader["ARMONEDA"]);
                item.arcostoa = Reader.IsDBNull(Reader.GetOrdinal("ARCOSTOA")) ? null : (double?)Convert.ToDouble(Reader["ARCOSTOA"]);
                item.arcstmpr = Reader.IsDBNull(Reader.GetOrdinal("ARCSTMPR")) ? null : (double?)Convert.ToDouble(Reader["ARCSTMPR"]);
                item.arcstmob = Reader.IsDBNull(Reader.GetOrdinal("ARCSTMOB")) ? null : (double?)Convert.ToDouble(Reader["ARCSTMOB"]);
                item.arcstcif = Reader.IsDBNull(Reader.GetOrdinal("ARCSTCIF")) ? null : (double?)Convert.ToDouble(Reader["ARCSTCIF"]);
                item.arcostob = Reader.IsDBNull(Reader.GetOrdinal("ARCOSTOB")) ? null : (double?)Convert.ToDouble(Reader["ARCOSTOB"]);
                item.arprecio = Reader.IsDBNull(Reader.GetOrdinal("ARPRECIO")) ? null : (double?)Convert.ToDouble(Reader["ARPRECIO"]);
                item.arcdimpf = Convert.ToString(Reader["ARCDIMPF"]);
                item.arorigen = Convert.ToString(Reader["ARORIGEN"]);
                item.arposara = Convert.ToString(Reader["ARPOSARA"]);
                item.arpesoun = Convert.ToString(Reader["ARPESOUN"]);
                item.arpesoum = Convert.ToString(Reader["ARPESOUM"]);
                item.arcdcla1 = Convert.ToString(Reader["ARCDCLA1"]);
                item.arcdcla2 = Convert.ToString(Reader["ARCDCLA2"]);
                item.arcdcla3 = Convert.ToString(Reader["ARCDCLA3"]);
                item.arcdcla4 = Convert.ToString(Reader["ARCDCLA4"]);
                item.arcdcla6 = Convert.ToString(Reader["ARCDCLA6"]);
                item.ardttec1 = Convert.ToString(Reader["ARDTTEC1"]);
                item.ardttec2 = Convert.ToString(Reader["ARDTTEC2"]);
                item.ardttec3 = Convert.ToString(Reader["ARDTTEC3"]);
                item.ardttec4 = Convert.ToString(Reader["ARDTTEC4"]);
                item.ardttec5 = Convert.ToString(Reader["ARDTTEC5"]);
                item.ardttec6 = Convert.ToString(Reader["ARDTTEC6"]);
                item.arcodpro = Convert.ToString(Reader["ARCODPRO"]);
                item.arfeccom = Convert.ToString(Reader["ARFECCOM"]);
                item.arprecom = Convert.ToString(Reader["ARPRECOM"]);
                item.armoncom = Convert.ToString(Reader["ARMONCOM"]);
                item.arprocom = Convert.ToString(Reader["ARPROCOM"]);
                item.arprogdt = Convert.ToString(Reader["ARPROGDT"]);
                item.arestado = Convert.ToString(Reader["ARESTADO"]);
                item.arcausae = Convert.ToString(Reader["ARCAUSAE"]);
                item.arnmuser = Convert.ToString(Reader["ARNMUSER"]);
                item.arfecing = Convert.ToString(Reader["ARFECING"]);
                item.arfecmod = Convert.ToString(Reader["ARFECMOD"]);
                item.arfcina1 = Convert.ToString(Reader["ARFCINA1"]);
                item.arcompos = Convert.ToString(Reader["ARCOMPOS"]);
                item.arconven = Convert.ToString(Reader["ARCONVEN"]);
                item.armercon = Convert.ToString(Reader["ARMERCON"]);
                item.arcodcom = Convert.ToString(Reader["ARCODCOM"]);
                item.arcdcla5 = Convert.ToString(Reader["ARCDCLA5"]);
                item.arcaopds = Convert.ToString(Reader["ARCAOPDS"]);
                item.arundods = Convert.ToString(Reader["ARUNDODS"]);
                item.artiptar = Convert.ToString(Reader["ARTIPTAR"]);
                item.arano = Convert.ToString(Reader["ARANO"]);
                item.arcoleccion = Convert.ToString(Reader["ARCOLECCION"]);
                item.arprioridad = Convert.ToString(Reader["ARPRIORIDAD"]);
                item.tr_procedencia = Convert.ToString(Reader["TR_PROCEDENCIA"]);
                item.tr_uen = Convert.ToString(Reader["TR_UEN"]);
                item.tr_tp = Convert.ToString(Reader["TR_TP"]);
                item.tr_sct = Convert.ToString(Reader["TR_SCT"]);
                item.tr_fondo = Convert.ToString(Reader["TR_FONDO"]);
                item.tr_tejido = Convert.ToString(Reader["TR_TEJIDO"]);
                item.arcdcla7 = Convert.ToString(Reader["ARCDCLA7"]);
                item.ardttec7 = Convert.ToString(Reader["ARDTTEC7"]);
                item.arcdcla8 = Convert.ToString(Reader["ARCDCLA8"]);
                item.ardttec8 = Convert.ToString(Reader["ARDTTEC8"]);
                item.arcdcla9 = Convert.ToString(Reader["ARCDCLA9"]);
                item.ardttec9 = Convert.ToString(Reader["ARDTTEC9"]);
                item.arcdcla10 = Convert.ToString(Reader["ARCDCLA10"]);
                item.ardttec10 = Convert.ToString(Reader["ARDTTEC10"]);
                item.asnombre1 = Convert.ToString(Reader["ASNOMBRE1"]);
                item.asnombre2 = Convert.ToString(Reader["ASNOMBRE2"]);
                item.asnombre3 = Convert.ToString(Reader["ASNOMBRE3"]);

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                item = null;
            }
        }

        public static articulo getArticulo(DataRow rw)
        {
            articulo item = new articulo();
            try
            {
                item.arcodemp = Convert.ToString(rw["ARCODEMP"]);
                item.artippro = Convert.ToString(rw["ARTIPPRO"]);
                item.arclave1 = Convert.ToString(rw["ARCLAVE1"]);
                item.arclave2 = Convert.ToString(rw["ARCLAVE2"]);
                item.arclave3 = Convert.ToString(rw["ARCLAVE3"]);
                item.arclave4 = Convert.ToString(rw["ARCLAVE4"]);
                item.arnombre = Convert.ToString(rw["ARNOMBRE"]);
                item.arundinv = Convert.ToString(rw["ARUNDINV"]);
                item.arumalt1 = Convert.ToString(rw["ARUMALT1"]);
                item.arumalt2 = Convert.ToString(rw["ARUMALT2"]);
                item.arfca1in = Convert.ToDouble(rw["ARFCA1IN"]);
                item.arfca2in = Convert.ToDouble(rw["ARFCA2IN"]);
                item.arcdaltr = Convert.ToString(rw["ARCDALTR"]);
                item.armoneda = Convert.ToString(rw["ARMONEDA"]);
                item.arcostoa = Convert.ToDouble(rw["ARCOSTOA"]);
                item.arcstmpr = Convert.ToDouble(rw["ARCSTMPR"]);
                item.arcstmob = Convert.ToDouble(rw["ARCSTMOB"]);
                item.arcstcif = Convert.ToDouble(rw["ARCSTCIF"]);
                item.arcostob = Convert.ToDouble(rw["ARCOSTOB"]);
                item.arprecio = Convert.ToDouble(rw["ARPRECIO"]);
                item.arcdimpf = Convert.ToString(rw["ARCDIMPF"]);
                item.arorigen = Convert.ToString(rw["ARORIGEN"]);
                item.arposara = Convert.ToString(rw["ARPOSARA"]);
                item.arpesoun = Convert.ToString(rw["ARPESOUN"]);
                item.arpesoum = Convert.ToString(rw["ARPESOUM"]);
                item.arcdcla1 = Convert.ToString(rw["ARCDCLA1"]);
                item.arcdcla2 = Convert.ToString(rw["ARCDCLA2"]);
                item.arcdcla3 = Convert.ToString(rw["ARCDCLA3"]);
                item.arcdcla4 = Convert.ToString(rw["ARCDCLA4"]);
                item.arcdcla6 = Convert.ToString(rw["ARCDCLA6"]);
                item.ardttec1 = Convert.ToString(rw["ARDTTEC1"]);
                item.ardttec2 = Convert.ToString(rw["ARDTTEC2"]);
                item.ardttec3 = Convert.ToString(rw["ARDTTEC3"]);
                item.ardttec4 = Convert.ToString(rw["ARDTTEC4"]);
                item.ardttec5 = Convert.ToString(rw["ARDTTEC5"]);
                item.ardttec6 = Convert.ToString(rw["ARDTTEC6"]);
                item.arcodpro = Convert.ToString(rw["ARCODPRO"]);
                item.arfeccom = Convert.ToString(rw["ARFECCOM"]);
                item.arprecom = Convert.ToString(rw["ARPRECOM"]);
                item.armoncom = Convert.ToString(rw["ARMONCOM"]);
                item.arprocom = Convert.ToString(rw["ARPROCOM"]);
                item.arprogdt = Convert.ToString(rw["ARPROGDT"]);
                item.arestado = Convert.ToString(rw["ARESTADO"]);
                item.arcausae = Convert.ToString(rw["ARCAUSAE"]);
                item.arnmuser = Convert.ToString(rw["ARNMUSER"]);
                item.arfecing = Convert.ToString(rw["ARFECING"]);
                item.arfecmod = Convert.ToString(rw["ARFECMOD"]);
                item.arfcina1 = Convert.ToString(rw["ARFCINA1"]);
                item.arcompos = Convert.ToString(rw["ARCOMPOS"]);
                item.arconven = Convert.ToString(rw["ARCONVEN"]);
                item.armercon = Convert.ToString(rw["ARMERCON"]);
                item.arcodcom = Convert.ToString(rw["ARCODCOM"]);
                item.arcdcla5 = Convert.ToString(rw["ARCDCLA5"]);
                item.arcaopds = Convert.ToString(rw["ARCAOPDS"]);
                item.arundods = Convert.ToString(rw["ARUNDODS"]);
                item.artiptar = Convert.ToString(rw["ARTIPTAR"]);
                item.arano = Convert.ToString(rw["ARANO"]);
                item.arcoleccion = Convert.ToString(rw["ARCOLECCION"]);
                item.arprioridad = Convert.ToString(rw["ARPRIORIDAD"]);
                item.tr_procedencia = Convert.ToString(rw["TR_PROCEDENCIA"]);
                item.tr_uen = Convert.ToString(rw["TR_UEN"]);
                item.tr_tp = Convert.ToString(rw["TR_TP"]);
                item.tr_sct = Convert.ToString(rw["TR_SCT"]);
                item.tr_fondo = Convert.ToString(rw["TR_FONDO"]);
                item.tr_tejido = Convert.ToString(rw["TR_TEJIDO"]);
                item.arcdcla7 = Convert.ToString(rw["ARCDCLA7"]);
                item.ardttec7 = Convert.ToString(rw["ARDTTEC7"]);
                item.arcdcla8 = Convert.ToString(rw["ARCDCLA8"]);
                item.ardttec8 = Convert.ToString(rw["ARDTTEC8"]);
                item.arcdcla9 = Convert.ToString(rw["ARCDCLA9"]);
                item.ardttec9 = Convert.ToString(rw["ARDTTEC9"]);
                item.arcdcla10 = Convert.ToString(rw["ARCDCLA10"]);
                item.ardttec10 = Convert.ToString(rw["ARDTTEC10"]);
                item.asnombre1 = Convert.ToString(rw["ASNOMBRE1"]);
                item.asnombre2 = Convert.ToString(rw["ASNOMBRE2"]);
                item.asnombre3 = Convert.ToString(rw["ASNOMBRE3"]);

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                item = null;
            }
        }
        public byte[] getImagenArticulo(string inConnection, string artippro, string arclave1)
        {
            DBAccess Obj = new DBAccess();

            Obj.ConnectionString = inConnection;
            byte[] archivo = null;

            using (SqlDataReader reader = articuloBD.getImagenArticulo(Obj, "001",artippro,arclave1))                
                while(reader.Read())
                    archivo = ((byte[])reader["IM_IMAGEN"]);

            return archivo;
            
        }
    }

    public class articuloimagenes {
        public int id { get; set; }
        public List<articulo> inarticulo { get; set; } = new List<articulo>();
        public List<imagenes> inimagen { get; set; } = new List<imagenes>();
    }
}
