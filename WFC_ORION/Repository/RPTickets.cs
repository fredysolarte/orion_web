using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WFC_ORION.DAL;
using WFC_ORION.Models;

namespace WFC_ORION.Repository
{
    public class RPTickets
    {
        public List<consulta_ticket> GetDatosTickets(string inConecction,string usua_usuario)
        {
            DBAccess Obj = new DBAccess();
            Obj.ConnectionString = inConecction;

            return _GetDatosTickets(Obj, usua_usuario);
        }
        private static List<consulta_ticket> _GetDatosTickets(DBAccess ObjDB, string usua_usuario)
        {
            List<consulta_ticket> lst = new List<consulta_ticket>();
            try
            {
                using (SqlDataReader reader = Tickets.GetTickets(ObjDB, usua_usuario))
                {
                    while (reader.Read())
                    {
                        lst.Add(GetDatosTickets(reader,lst.Count + 1));
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
                lst = null;
            }
        }
        private static admi_tusuario GetDatosUsuarioItem(SqlDataReader Reader)
        {
            admi_tusuario item = new admi_tusuario();
            try
            {
                item.usua_usuario = Convert.ToString(Reader["usua_usuario"]);
                item.usua_clave = Convert.ToString(Reader["usua_clave"]);
                item.usua_nombres = Convert.ToString(Reader["usua_nombres"]);
                item.usua_estado = Convert.ToInt32(Reader["usua_estado"]);
                item.usua_secuencia = Convert.ToInt32(Reader["usua_secuencia"]);
                item.usua_administrador = Convert.ToInt32(Reader["usua_administrador"]);

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
        private static consulta_ticket GetDatosTickets(SqlDataReader Reader,int id)
        {
            consulta_ticket item = new consulta_ticket();
            admi_tusuario _itm = new admi_tusuario();
            Appointments _itma = new Appointments();
            TicketHD _itmh = new TicketHD();
            try
            {
                item.id = id;

                _itm.usua_usuario = Convert.ToString(Reader["usua_usuario"]);
                _itm.usua_clave = Convert.ToString(Reader["usua_clave"]);
                _itm.usua_nombres = Convert.ToString(Reader["usua_nombres"]);
                _itm.usua_estado = Convert.ToInt32(Reader["usua_estado"]);
                _itm.usua_secuencia = Convert.ToInt32(Reader["usua_secuencia"]);
                _itm.usua_administrador = Convert.ToInt32(Reader["usua_administrador"]);

                item.admi_Tusuario.Add(_itm);


                _itma.id = 1;
                _itma.descripcion = "";
                _itma.inicio = Reader.IsDBNull(Reader.GetOrdinal("inicio")) ? null : (DateTime?)Convert.ToDateTime(Reader["inicio"]); 
                _itma.final = Reader.IsDBNull(Reader.GetOrdinal("final")) ? null : (DateTime?)Convert.ToDateTime(Reader["final"]);  
                _itma.RoomID = 0;
                _itma.usuario = "";
                _itma.RecurrenceRule = "";
                _itma.RecurrenceParentID = (int?)null;
                _itma.tipo = 0;
                _itma.usuario_responsable = Convert.ToString(Reader["TK_RESPONSABLE"]);
                _itma.PH_CODIGO = Convert.ToInt32(0);
                _itma.TK_NUMERO = Convert.ToInt32(Reader["TK_NUMERO"]);
                _itma.TRCODTER = 0;
                _itma.SERVICIO = "";

                item.appointments.Add(_itma);


                _itmh.TK_NUMERO = Convert.ToInt32(Reader["TK_NUMERO"]);
                _itmh.TK_RESPONSABLE = Convert.ToString(Reader["TK_RESPONSABLE"]);
                _itmh.TK_PROPIETARIO = Convert.ToString(Reader["TK_PROPIETARIO"]);
                _itmh.TK_PRIORIDAD = Convert.ToString(Reader["TK_PRIORIDAD"]);
                _itmh.TK_ASUNTO = Convert.ToString(Reader["TK_ASUNTO"]);
                _itmh.TK_OBSERVACIONES = Convert.ToString(Reader["TK_OBSERVACIONES"]);
                _itmh.TK_ESTADO = Convert.ToString(Reader["TK_ESTADO"]);
                _itmh.TK_FECING = Convert.ToDateTime(Reader["TK_FECING"]);
                _itmh.TK_FECVEN = Reader.IsDBNull(Reader.GetOrdinal("TK_FECVEN")) ? null : (DateTime?)Convert.ToDateTime(Reader["TK_FECVEN"]);
                _itmh.TK_FECFIN = Reader.IsDBNull(Reader.GetOrdinal("TK_FECFIN")) ? null : (DateTime?)Convert.ToDateTime(Reader["TK_FECFIN"]);
                _itmh.PH_CODIGO = Reader.IsDBNull(Reader.GetOrdinal("PH_CODIGO")) ? null : (int?)Convert.ToInt32(Reader["PH_CODIGO"]);
                _itmh.TK_TIPO = Convert.ToString(Reader["TK_TIPO"]);
                _itmh.AT_CODIGO = Reader.IsDBNull(Reader.GetOrdinal("AT_CODIGO")) ? null : (int?)Convert.ToInt32(Reader["AT_CODIGO"]);

                item.tbTickethd.Add(_itmh);

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                item = null;
                _itm = null;
                _itma = null;
                _itmh = null;
            }
        }
    }
    public class consulta_ticket
    {
        public int id { get; set; }
        public List<admi_tusuario> admi_Tusuario { get; set; } = new List<admi_tusuario>();
        public List<TicketHD> tbTickethd { get; set; } = new List<TicketHD>();        
        public List<Appointments> appointments { get; set; }  = new List<Appointments>();
        
    }
}
