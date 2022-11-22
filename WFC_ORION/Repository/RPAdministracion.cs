using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WFC_ORION.DAL;
using WFC_ORION.Models;

namespace WFC_ORION.Repository
{
    public class RPAdministracion
    {
        public IEnumerable<admi_tusuario> GetDatosUsuario(string inConecction,string usua_usuario)
        {
            DBAccess Obj = new DBAccess();
            Obj.ConnectionString = inConecction;

            return _GetDatosUsuario(Obj, usua_usuario);
        }

        private static List<admi_tusuario> _GetDatosUsuario(DBAccess ObjDB, string usua_usuario)
        {
            List<admi_tusuario> lst = new List<admi_tusuario>();
            try
            {
                using (SqlDataReader reader = Administracion.GetDatosUsuario(ObjDB, usua_usuario))
                {
                    while (reader.Read())
                    {
                        lst.Add(GetDatosUsuarioItem(reader));
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
                item.vr_version = Convert.ToString(Reader["vr_version"]);

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
    }
}
