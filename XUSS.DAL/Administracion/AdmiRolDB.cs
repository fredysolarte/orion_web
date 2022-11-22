using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BE.Administracion;
using DataAccess;

namespace DAL.Administracion
{
	public class AdmiRolDB : GenericBaseDB<AdmiRol>
	{

        public AdmiRolDB()
            : base("admi_pGeneraSecuencia")
        {
        }

        public void UpdateConfiguration(SessionManager oSessionManager, List<AdmiArbolOpcion> olEntity, int idRol, int userId)
        {
            StringBuilder sSql;
            try
            {
                sSql = new StringBuilder();
                sSql.AppendLine("DELETE admi_tmoduloxrol WHERE rolm_rolm = " + idRol + ";");
                sSql.AppendLine("DELETE admi_topcionxrol WHERE rolm_rolm = " + idRol + ";");
                sSql.AppendLine("DELETE admi_tcontrolxrol WHERE rolm_rolm = " + idRol);
                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString().Trim(), CommandType.Text, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                sSql = null;
            }

            if (olEntity.Count > 0)
            {
                foreach (AdmiArbolOpcion oEntity in olEntity)
                {
                    switch (oEntity.AropEntidad)
                    {
                        case "M":
                            AdmiModuloxrol oModuloxrol = new AdmiModuloxrol();
                            AdmiModuloxrolDB oModuloxrolDB = new AdmiModuloxrolDB();

                            oModuloxrol.ModuModulo = Convert.ToInt32(oEntity.AropIdOriginal);
                            oModuloxrol.RolmRolm = idRol;
                            oModuloxrol.SistSistema = Convert.ToInt32(oEntity.SistSistema);
                            oModuloxrol.LogsUsuario = userId;
                            oModuloxrol.LogsFecha = DateTime.Now;
                            oModuloxrolDB.Add(oSessionManager, oModuloxrol);

                            break;
                        case "O":
                            AdmiOpcionxrol oOpcionxrol = new AdmiOpcionxrol();
                            AdmiOpcionxrolDB oOpcionxrolDB = new AdmiOpcionxrolDB();

                            oOpcionxrol.RolmRolm = idRol;
                            oOpcionxrol.OpciOpcion = Convert.ToInt32(oEntity.AropIdOriginal);
                            oOpcionxrol.LogsUsuario = userId;
                            oOpcionxrol.LogsFecha = DateTime.Now;
                            oOpcionxrolDB.Add(oSessionManager, oOpcionxrol);
                            break;
                        case "C":
                            AdmiControlxrol oControlxrol = new AdmiControlxrol();
                            AdmiControlxrolDB oControlxrolDB = new AdmiControlxrolDB();

                            oControlxrol.RolmRolm = idRol;
                            oControlxrol.CtrlControl = Convert.ToInt32(oEntity.AropIdOriginal);
                            oControlxrol.LogsUsuario = userId;
                            oControlxrol.LogsFecha = DateTime.Now;
                            oControlxrolDB.Add(oSessionManager, oControlxrol);
                            break;
                        default:
                            break;
                    }
                }
            }
                
        }

        public DataTable GetDataTableByUserId(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows, string userId)
        {
            StringBuilder sSql = new StringBuilder();
            object[] parameterValues = new object[1];
            parameterValues[0] = userId;
            try
            {
                sSql.AppendLine("SELECT admi_trol.rolm_rolm");
                sSql.AppendLine("      ,admi_trol.rolm_nombre");
                sSql.AppendLine("      ,admi_trol.rolm_descripcion");
                sSql.AppendLine("      ,admi_trol.rolm_estado");
                sSql.AppendLine("      ,admi_trol.logs_usuario");
                sSql.AppendLine("      ,admi_trol.logs_fecha, 'false' AS IsChecked FROM admi_trol");
                sSql.AppendLine("WHERE admi_trol.rolm_rolm NOT IN");
                sSql.AppendLine("(");
                sSql.AppendLine("	SELECT admi_trol.rolm_rolm FROM admi_trol");
                sSql.AppendLine("	INNER JOIN admi_trolxusuario ON admi_trolxusuario.rolm_rolm = admi_trol.rolm_rolm");
                sSql.AppendLine("	INNER JOIN admi_tusuario ON admi_trolxusuario.usua_usuario = admi_tusuario.usua_usuario");
                sSql.AppendLine("	WHERE admi_tusuario.usua_usuario = @p0 AND admi_trol.rolm_estado = 1");
                sSql.AppendLine(")");
                sSql.AppendLine("AND admi_trol.rolm_estado = 1");
                sSql.AppendLine("UNION");
                sSql.AppendLine("SELECT admi_trol.rolm_rolm");
                sSql.AppendLine("      ,admi_trol.rolm_nombre");
                sSql.AppendLine("      ,admi_trol.rolm_descripcion");
                sSql.AppendLine("      ,admi_trol.rolm_estado");
                sSql.AppendLine("      ,admi_trol.logs_usuario");
                sSql.AppendLine("      ,admi_trol.logs_fecha, 'true' AS IsChecked FROM admi_trol");
                sSql.AppendLine("	INNER JOIN admi_trolxusuario ON admi_trolxusuario.rolm_rolm = admi_trol.rolm_rolm");
                sSql.AppendLine("	INNER JOIN admi_tusuario ON admi_trolxusuario.usua_usuario = admi_tusuario.usua_usuario");
                sSql.AppendLine("	WHERE admi_tusuario.usua_usuario = @p0 AND admi_trol.rolm_estado = 1");
                sSql.AppendLine("ORDER BY admi_trol.rolm_nombre ");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString().Trim(), CommandType.Text, parameterValues[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sSql = null;
            }
        }

     
	}
}
