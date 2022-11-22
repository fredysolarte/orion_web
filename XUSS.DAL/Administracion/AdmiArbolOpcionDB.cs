using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using BE.Administracion;
using Mapping;
using XUSS.DAL.Genericas;

namespace DAL.Administracion
{
    public class AdmiArbolOpcionDB : GenericBaseDB<AdmiArbolOpcion>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AdmiArbolOpcionDB()
            : base("admi_pGeneraSecuencia")
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oSessionManager"></param>
        /// <param name="filter"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="classId"></param>
        /// <returns></returns>
        public List<AdmiArbolOpcion> GetListBySystemAndModule(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows, int systemId, int moduleId)
        {
            StringBuilder sSql = new StringBuilder();

            try
            {
                sSql.AppendLine("SELECT arop_arbolOpcion");
                sSql.AppendLine("      ,sist_sistema");
                sSql.AppendLine("      ,modu_modulo");
                sSql.AppendLine("      ,arop_idOriginal");
                sSql.AppendLine("      ,arop_idUnicoPadre");
                sSql.AppendLine("      ,arop_entidad");
                sSql.AppendLine("      ,arop_nombre");
                sSql.AppendLine("FROM  admi_tarbolopcion");
                sSql.AppendLine("WHERE sist_sistema = " + systemId + " AND modu_modulo = " + moduleId);

                if (filter != String.Empty && filter != null)
                {
                    sSql.AppendLine(" AND " + filter);
                }
                return MappingData<AdmiArbolOpcion>.MappingList(DBAccess.GetDataReader(oSessionManager, sSql.ToString().Trim(), CommandType.Text, null));
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
        public List<AdmiArbolOpcion> GetListCheckedByRoles(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows, string rolesId, string userId, int systemId)
        {
            StringBuilder sSql = new StringBuilder();
            object[] parameterValues = new object[2];
            parameterValues[0] = userId;
            parameterValues[1] = systemId;
            try
            {
                sSql.AppendLine("SELECT T.arop_arbolopcion ");
                sSql.AppendLine(" 	  ,T.sist_sistema ");
                sSql.AppendLine(" 	  ,T.modu_modulo ");
                sSql.AppendLine(" 	  ,T.arop_idoriginal ");
                sSql.AppendLine(" 	  ,T.arop_idunicopadre ");
                sSql.AppendLine(" 	  ,T.arop_entidad ");
                sSql.AppendLine(" 	  ,T.arop_nombre");
                sSql.AppendLine(" 	  ,T.checked ");
                sSql.AppendLine("FROM");
                sSql.AppendLine("( 	  ");
                sSql.AppendLine("	SELECT T1.arop_arbolopcion ");
                sSql.AppendLine(" 		  ,T1.sist_sistema ");
                sSql.AppendLine(" 		  ,T1.modu_modulo ");
                sSql.AppendLine(" 		  ,T1.arop_idoriginal ");
                sSql.AppendLine(" 		  ,T1.arop_idunicopadre ");
                sSql.AppendLine(" 		  ,T1.arop_entidad ");
                sSql.AppendLine(" 		  ,T1.arop_nombre, rm.modu_modulo AS idModulo, ro.opci_opcion AS idOpcion, rc.ctrl_control AS idControl, ");
                sSql.AppendLine(" 		  CASE");
                sSql.AppendLine(" 			WHEN T1.arop_entidad = 'M' AND rm.modu_modulo IS NOT NULL THEN 'true' ");
                sSql.AppendLine(" 			WHEN T1.arop_entidad = 'O' AND ro.opci_opcion IS NOT NULL THEN 'true' ");
                sSql.AppendLine(" 			WHEN T1.arop_entidad = 'C' AND rc.ctrl_control IS NOT NULL THEN 'true'");
                sSql.AppendLine(" 			ELSE 'false'");
                sSql.AppendLine(" 		  END as checked	");
                sSql.AppendLine("	FROM");
                sSql.AppendLine("	(");
                sSql.AppendLine("		SELECT  admi_tarbolopcion.arop_arbolopcion ");
                sSql.AppendLine(" 			  ,admi_tarbolopcion.sist_sistema ");
                sSql.AppendLine(" 			  ,admi_tarbolopcion.modu_modulo ");
                sSql.AppendLine(" 			  ,admi_tarbolopcion.arop_idoriginal ");
                sSql.AppendLine(" 			  ,admi_tarbolopcion.arop_idunicopadre ");
                sSql.AppendLine(" 			  ,admi_tarbolopcion.arop_entidad ");
                sSql.AppendLine(" 			  ,admi_tarbolopcion.arop_nombre FROM admi_tarbolopcion ");
                sSql.AppendLine("		WHERE admi_tarbolopcion.arop_arbolopcion IN ");
                sSql.AppendLine("		(");
                sSql.AppendLine("			SELECT admi_tarbolopcion.arop_arbolopcion");
                sSql.AppendLine("			FROM  admi_tarbolopcion");
                sSql.AppendLine("			INNER JOIN admi_tmoduloxrol");
                sSql.AppendLine("			ON admi_tarbolopcion.arop_idoriginal = admi_tmoduloxrol.modu_modulo AND arop_entidad = 'M'");
                sSql.AppendLine("			AND admi_tmoduloxrol.rolm_rolm IN ( " + rolesId + " ) ");
                sSql.AppendLine("			UNION");
                sSql.AppendLine("			SELECT admi_tarbolopcion.arop_arbolopcion");
                sSql.AppendLine("			FROM  admi_tarbolopcion");
                sSql.AppendLine("			INNER JOIN admi_topcionxrol");
                sSql.AppendLine("			ON admi_tarbolopcion.arop_idoriginal = admi_topcionxrol.opci_opcion AND arop_entidad = 'O'");
                sSql.AppendLine("			AND admi_topcionxrol.rolm_rolm IN ( " + rolesId + " )");
                sSql.AppendLine("			UNION");
                sSql.AppendLine("			SELECT admi_tarbolopcion.arop_arbolopcion");
                sSql.AppendLine("			FROM  admi_tarbolopcion");
                sSql.AppendLine("			INNER JOIN admi_tcontrolxrol	  ");
                sSql.AppendLine("			ON admi_tarbolopcion.arop_idoriginal = admi_tcontrolxrol.ctrl_control AND arop_entidad = 'C'");
                sSql.AppendLine("			AND admi_tcontrolxrol.rolm_rolm IN ( " + rolesId + " )");
                sSql.AppendLine("		)");
                sSql.AppendLine("		UNION");

                sSql.AppendLine("SELECT        admi_tarbolopcion.arop_arbolopcion, admi_tarbolopcion.sist_sistema, admi_tarbolopcion.modu_modulo, admi_tarbolopcion.arop_idoriginal, ");
                sSql.AppendLine("admi_tarbolopcion.arop_idunicopadre, admi_tarbolopcion.arop_entidad, admi_tarbolopcion.arop_nombre");
                sSql.AppendLine("FROM            admi_topcionxrol INNER JOIN");
                sSql.AppendLine("admi_tformulario ON admi_topcionxrol.opci_opcion = admi_tformulario.opci_opcion INNER JOIN");
                sSql.AppendLine("admi_tarbolopcion ON admi_tarbolopcion.arop_idoriginal = admi_tformulario.form_formulario AND admi_tarbolopcion.arop_entidad = 'F'");
                sSql.AppendLine("WHERE        (admi_topcionxrol.rolm_rolm IN (" + rolesId + "))");
                sSql.AppendLine("		) AS T1");
                sSql.AppendLine("		LEFT JOIN admi_trestringemodulo rm ON T1.arop_idoriginal = rm.modu_modulo AND T1.arop_entidad = 'M' AND rm.usua_usuario = @p0");
                sSql.AppendLine("		LEFT JOIN admi_trestringeopcion ro ON T1.arop_idoriginal = ro.opci_opcion AND T1.arop_entidad = 'O' AND ro.usua_usuario = @p0");
                sSql.AppendLine("		LEFT JOIN admi_trestringecontrol rc ON T1.arop_idoriginal = rc.ctrl_control AND T1.arop_entidad = 'C' AND rc.usua_usuario = @p0");
                sSql.AppendLine(") AS T ");
                sSql.AppendLine("WHERE T.sist_sistema = " + systemId);

                if (filter != String.Empty && filter != null)
                {
                    sSql.AppendLine(" AND " + filter);
                }
                return MappingData<AdmiArbolOpcion>.MappingList(DBAccess.GetDataReader(oSessionManager, sSql.ToString().Trim(), CommandType.Text, parameterValues));
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
        public List<AdmiArbolOpcion> GetListCheckedBySystemModuleAndRol(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows, int systemId, int moduleId, int rolId)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT  admi_tarbolopcion.arop_arbolopcion ");
                sSql.AppendLine(" 	  ,admi_tarbolopcion.sist_sistema ");
                sSql.AppendLine(" 	  ,admi_tarbolopcion.modu_modulo ");
                sSql.AppendLine(" 	  ,admi_tarbolopcion.arop_idoriginal ");
                sSql.AppendLine(" 	  ,admi_tarbolopcion.arop_idunicopadre ");
                sSql.AppendLine(" 	  ,admi_tarbolopcion.arop_entidad ");
                sSql.AppendLine(" 	  ,admi_tarbolopcion.arop_nombre, 'false' as checked  FROM admi_tarbolopcion ");
                sSql.AppendLine("WHERE admi_tarbolopcion.arop_arbolopcion not in ");
                sSql.AppendLine("(");
                sSql.AppendLine("	SELECT admi_tarbolopcion.arop_arbolopcion");
                sSql.AppendLine("	FROM  admi_tarbolopcion");
                sSql.AppendLine("	inner join admi_tmoduloxrol");
                sSql.AppendLine("	on admi_tarbolopcion.arop_idoriginal = admi_tmoduloxrol.modu_modulo AND arop_entidad = 'M'");
                sSql.AppendLine("	WHERE ");
                sSql.AppendLine("	admi_tmoduloxrol.rolm_rolm = " + rolId);
                sSql.AppendLine("	union");
                sSql.AppendLine("	SELECT admi_tarbolopcion.arop_arbolopcion");
                sSql.AppendLine("	FROM  admi_tarbolopcion");
                sSql.AppendLine("	inner join admi_topcionxrol");
                sSql.AppendLine("	on admi_tarbolopcion.arop_idoriginal = admi_topcionxrol.opci_opcion AND arop_entidad = 'O'");
                sSql.AppendLine("	WHERE ");
                sSql.AppendLine("	admi_topcionxrol.rolm_rolm = " + rolId);
                sSql.AppendLine("	union");
                sSql.AppendLine("	SELECT admi_tarbolopcion.arop_arbolopcion");
                sSql.AppendLine("	FROM  admi_tarbolopcion");
                sSql.AppendLine("	inner join admi_tcontrolxrol	  ");
                sSql.AppendLine("	on admi_tarbolopcion.arop_idoriginal = admi_tcontrolxrol.ctrl_control AND arop_entidad = 'C'");
                sSql.AppendLine("	WHERE  admi_tcontrolxrol.rolm_rolm = " + rolId);
                sSql.AppendLine(")");
                sSql.AppendLine("");
                sSql.AppendLine("union");
                sSql.AppendLine("SELECT admi_tarbolopcion.arop_arbolopcion");
                sSql.AppendLine("	  ,admi_tarbolopcion.sist_sistema");
                sSql.AppendLine("	  ,admi_tarbolopcion.modu_modulo");
                sSql.AppendLine("	  ,admi_tarbolopcion.arop_idoriginal");
                sSql.AppendLine("	  ,admi_tarbolopcion.arop_idunicopadre");
                sSql.AppendLine("	  ,admi_tarbolopcion.arop_entidad");
                sSql.AppendLine("	  ,admi_tarbolopcion.arop_nombre, 'true' as checked");
                sSql.AppendLine("FROM  admi_tarbolopcion");
                sSql.AppendLine("inner join admi_tmoduloxrol");
                sSql.AppendLine("on admi_tarbolopcion.arop_idoriginal = admi_tmoduloxrol.modu_modulo AND arop_entidad = 'M'");
                sSql.AppendLine("WHERE ");
                sSql.AppendLine("admi_tmoduloxrol.rolm_rolm = " + rolId);
                sSql.AppendLine("union");
                sSql.AppendLine("SELECT admi_tarbolopcion.arop_arbolopcion");
                sSql.AppendLine("	  ,admi_tarbolopcion.sist_sistema");
                sSql.AppendLine("	  ,admi_tarbolopcion.modu_modulo");
                sSql.AppendLine("	  ,admi_tarbolopcion.arop_idoriginal");
                sSql.AppendLine("	  ,admi_tarbolopcion.arop_idunicopadre");
                sSql.AppendLine("	  ,admi_tarbolopcion.arop_entidad");
                sSql.AppendLine("	  ,admi_tarbolopcion.arop_nombre, 'true' as checked");
                sSql.AppendLine("FROM  admi_tarbolopcion");
                sSql.AppendLine("inner join admi_topcionxrol");
                sSql.AppendLine("on admi_tarbolopcion.arop_idoriginal = admi_topcionxrol.opci_opcion AND arop_entidad = 'O'");
                sSql.AppendLine("WHERE ");
                sSql.AppendLine("admi_topcionxrol.rolm_rolm = " + rolId);
                sSql.AppendLine("union");
                sSql.AppendLine("SELECT admi_tarbolopcion.arop_arbolopcion");
                sSql.AppendLine("	  ,admi_tarbolopcion.sist_sistema");
                sSql.AppendLine("	  ,admi_tarbolopcion.modu_modulo");
                sSql.AppendLine("	  ,admi_tarbolopcion.arop_idoriginal");
                sSql.AppendLine("	  ,admi_tarbolopcion.arop_idunicopadre");
                sSql.AppendLine("	  ,admi_tarbolopcion.arop_entidad");
                sSql.AppendLine("	  ,admi_tarbolopcion.arop_nombre, 'true' as checked");
                sSql.AppendLine("FROM  admi_tarbolopcion");
                sSql.AppendLine("inner join admi_tcontrolxrol	  ");
                sSql.AppendLine("on admi_tarbolopcion.arop_idoriginal = admi_tcontrolxrol.ctrl_control AND arop_entidad = 'C'");
                sSql.AppendLine("WHERE  admi_tcontrolxrol.rolm_rolm = " + rolId);





                if (filter != String.Empty && filter != null)
                {
                    sSql.AppendLine(" AND " + filter);
                }
                return MappingData<AdmiArbolOpcion>.MappingList(DBAccess.GetDataReader(oSessionManager, sSql.ToString().Trim(), CommandType.Text, null));
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
        
        public DataTable GetOptionsByUserAndModule(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows, string userLogon, int moduleId, int sistemaId)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT        admi_tarbolOpcion.arop_arbolOpcion, CASE WHEN arop_idUnicoPadre IN");
                sSql.AppendLine("(SELECT        arop_arbolOpcion");
                sSql.AppendLine("FROM            admi_tarbolOpcion");
                sSql.AppendLine("WHERE        arop_entidad = 'M') THEN NULL ELSE arop_idUnicoPadre END AS arop_idUnicoPadre, admi_tarbolOpcion.arop_nombre, ");
                sSql.AppendLine("'~/' + admi_tformulario.form_nombre AS form_nombre, admi_topcion.opci_hint");
                sSql.AppendLine("FROM            admi_topcion INNER JOIN");
                sSql.AppendLine("admi_tarbolOpcion ON admi_topcion.opci_opcion = admi_tarbolOpcion.arop_idOriginal LEFT OUTER JOIN");
                sSql.AppendLine("admi_tformulario ON admi_topcion.opci_opcion = admi_tformulario.opci_opcion");
                sSql.AppendLine("WHERE        (admi_tarbolOpcion.arop_entidad = 'O') AND (admi_tarbolOpcion.arop_idOriginal IN");
                sSql.AppendLine("(SELECT DISTINCT admi_topcionxrol.opci_opcion");
                sSql.AppendLine("FROM            admi_topcionxrol INNER JOIN");
                sSql.AppendLine("admi_trol ON admi_topcionxrol.rolm_rolm = admi_trol.rolm_rolm INNER JOIN");
                sSql.AppendLine("admi_trolxusuario ON admi_trol.rolm_rolm = admi_trolxusuario.rolm_rolm INNER JOIN");
                sSql.AppendLine("admi_topcion AS admi_topcion_1 ON admi_topcionxrol.opci_opcion = admi_topcion_1.opci_opcion");
                sSql.AppendLine("WHERE        (admi_trolxusuario.usua_usuario = @p0) AND (admi_topcionxrol.opci_opcion NOT IN");
                sSql.AppendLine("(SELECT        opci_opcion");
                sSql.AppendLine("FROM            admi_trestringeopcion");
                sSql.AppendLine("WHERE        (usua_usuario = @p0))) AND (admi_topcion_1.opci_estado = 1) AND (admi_trol.rolm_estado = 1) AND ");
                sSql.AppendLine("(admi_topcion_1.modu_modulo = @p1) AND (admi_topcion_1.sist_sistema = @p2))) AND (admi_tarbolOpcion.modu_modulo = @p1) AND ");
                sSql.AppendLine("(admi_topcion.opci_estado = 1) AND (admi_topcion.modu_modulo = @p1) AND (admi_tarbolOpcion.sist_sistema = @p2)");
                sSql.AppendLine("ORDER BY admi_topcion.opci_orden");
                if (filter != String.Empty && filter != null)
                {
                    sSql.AppendLine(" AND " + filter);
                }
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString().Trim(), CommandType.Text, userLogon, moduleId, sistemaId);
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
        public DataTable GetOptionsByUserAndModule(SessionManager oSessionManager, string userLogon, int moduleId, int sistemaId, int parentId)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT arop_arbolOpcion,arop_idUnicoPadre,arop_nombre,form_nombre,opci_hint FROM ");
                sSql.AppendLine("(");
                sSql.AppendLine("SELECT        admi_tarbolOpcion.arop_arbolOpcion, CASE WHEN arop_idUnicoPadre IN");
                sSql.AppendLine("(");
                sSql.AppendLine("	SELECT        arop_arbolOpcion");
                sSql.AppendLine("	FROM            admi_tarbolOpcion");
                sSql.AppendLine("	WHERE        arop_entidad = 'M') THEN 0 ELSE arop_idUnicoPadre END AS arop_idUnicoPadre, admi_tarbolOpcion.arop_nombre, ");
                sSql.AppendLine("	'~/' || admi_tformulario.form_nombre AS form_nombre, admi_topcion.opci_hint, admi_topcion.opci_orden");
                sSql.AppendLine("	FROM            admi_topcion INNER JOIN");
                sSql.AppendLine("	admi_tarbolOpcion ON admi_topcion.opci_opcion = admi_tarbolOpcion.arop_idOriginal LEFT OUTER JOIN");
                sSql.AppendLine("	admi_tformulario ON admi_topcion.opci_opcion = admi_tformulario.opci_opcion");
                sSql.AppendLine("	WHERE        (admi_tarbolOpcion.arop_entidad = 'O') AND (admi_tarbolOpcion.arop_idOriginal IN");
                sSql.AppendLine("	(");
                sSql.AppendLine("		SELECT DISTINCT admi_topcionxrol.opci_opcion");
                sSql.AppendLine("		FROM            admi_topcionxrol INNER JOIN");
                sSql.AppendLine("		admi_trol ON admi_topcionxrol.rolm_rolm = admi_trol.rolm_rolm INNER JOIN");
                sSql.AppendLine("		admi_trolxusuario ON admi_trol.rolm_rolm = admi_trolxusuario.rolm_rolm INNER JOIN");
                sSql.AppendLine("		admi_topcion AS admi_topcion_1 ON admi_topcionxrol.opci_opcion = admi_topcion_1.opci_opcion");
                sSql.AppendLine("		WHERE        (admi_trolxusuario.usua_usuario = @p0) AND (admi_topcionxrol.opci_opcion NOT IN");
                sSql.AppendLine("		(");
                sSql.AppendLine("			SELECT        opci_opcion");
                sSql.AppendLine("			FROM            admi_trestringeopcion");
                sSql.AppendLine("			WHERE        (usua_usuario = @p0)");
                sSql.AppendLine("		)");
                sSql.AppendLine("		) AND ");
                sSql.AppendLine("		(admi_topcion_1.opci_estado = 1) AND (admi_trol.rolm_estado = 1) AND ");
                sSql.AppendLine("			(admi_topcion_1.modu_modulo = @p1) AND (admi_topcion_1.sist_sistema = @p2)");
                sSql.AppendLine("	)");
                sSql.AppendLine("	) AND (admi_tarbolOpcion.modu_modulo = @p1) AND ");
                sSql.AppendLine("			(admi_topcion.opci_estado = 1) AND (admi_topcion.modu_modulo = @p1) AND (admi_tarbolOpcion.sist_sistema = @p2)");
                sSql.AppendLine(") AS T");
                sSql.AppendLine("WHERE T.arop_idUnicoPadre = @p3 ORDER BY opci_orden");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString().Trim(), CommandType.Text, userLogon, moduleId, sistemaId, parentId);
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
        public void DeleteControlsByIdForma(SessionManager oSessionManager, int idForma)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM admi_tarbolopcion");
                sSql.AppendLine("WHERE        (arop_idoriginal IN");
                sSql.AppendLine("(SELECT        admi_tcontrol.ctrl_control");
                sSql.AppendLine("FROM            admi_tcontrol");
                sSql.AppendLine("WHERE        (admi_tcontrol.form_formulario = @p0))) AND (arop_entidad = 'C')");
                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString().Trim(), CommandType.Text, idForma);
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
        public void DeleteByIdOriginalAndEntidad(SessionManager oSessionManager, int id, string entidad)
        {

            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM admi_tarbolOpcion");
                sSql.AppendLine("WHERE admi_tarbolOpcion.arop_entidad = @p0 AND admi_tarbolOpcion.arop_idOriginal= @p1");
                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString().Trim(), CommandType.Text, entidad, id);
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
        public int Add(SessionManager oSessionManager, AdmiArbolOpcion item)
        {
            StringBuilder sSql = new StringBuilder();
            object[] parametersSequence = new object[3];
            int seqBlog = -1;
            try
            {
                parametersSequence[0] = ((Table)new AdmiBlob().GetType().GetCustomAttributes(typeof(Table), true)[0]).EntityId;
                parametersSequence[1] = 0;
                parametersSequence[2] = 0;
                //seqBlog = DBAccess.GetSequence(oSessionManager, "admi_pGeneraSecuencia", 7,0,true);
                seqBlog = GenericaBD.GetSecuencia(oSessionManager,7,0);
                sSql.AppendLine("INSERT INTO admi_tarbolopcion");
                sSql.AppendLine("(arop_arbolopcion, sist_sistema, modu_modulo, arop_idoriginal, arop_idunicopadre, arop_entidad, arop_nombre)");
                sSql.AppendLine("VALUES        (@p0,@p1,@p2,@p3,@p4,@p5,@p6)");
                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString().Trim(), CommandType.Text, seqBlog, item.SistSistema, item.ModuModulo, item.AropIdOriginal, item.AropIdUnicoPadre, item.AropEntidad, item.AropNombre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sSql = null;
            }
            return seqBlog;
        }
        public AdmiArbolOpcion GetByIdAndEntidadAndSystem(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows, int idOriginal, string entidad, int idSistema)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT arop_arbolopcion");
                sSql.AppendLine("      ,sist_sistema");
                sSql.AppendLine("      ,modu_modulo");
                sSql.AppendLine("      ,arop_idoriginal");
                sSql.AppendLine("      ,arop_idunicopadre");
                sSql.AppendLine("      ,arop_entidad");
                sSql.AppendLine("      ,arop_nombre, 1 as checked");
                sSql.AppendLine("FROM  admi_tarbolopcion");
                sSql.AppendLine("WHERE arop_entidad=@p0 AND arop_idoriginal = @p1 AND sist_sistema=@p2");

                if (filter != String.Empty && filter != null)
                {
                    sSql.AppendLine(" AND " + filter);
                }
                using (IDataReader a = DBAccess.GetDataReader(oSessionManager, sSql.ToString().Trim(), CommandType.Text, entidad, idOriginal, idSistema))
                {
                    return MappingData<AdmiArbolOpcion>.Mapping(a);
                }
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
        public void Update(SessionManager oSessionManager, AdmiArbolOpcion item)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE admi_tarbolopcion");
                sSql.AppendLine("SET sist_sistema = @p0, modu_modulo = @p1, arop_idoriginal = @p2, arop_idunicopadre = @p3, arop_entidad = @p4, arop_nombre = @p5");
                sSql.AppendLine("WHERE (arop_arbolopcion = @p6)");
                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString().Trim(), CommandType.Text, item.SistSistema, item.ModuModulo, item.AropIdOriginal, item.AropIdUnicoPadre, item.AropEntidad, item.AropNombre, item.AropArbolOpcion);
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
        public DataTable GetOptionsByURLForm(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows, string urlForm)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT        admi_topcion.sist_sistema, admi_topcion.modu_modulo");
                sSql.AppendLine("FROM            admi_tformulario INNER JOIN");
                sSql.AppendLine("admi_topcion ON admi_tformulario.opci_opcion = admi_topcion.opci_opcion");
                sSql.AppendLine("WHERE        (upper(admi_tformulario.form_nombre) = upper(@p0)) ");
                if (filter != String.Empty && filter != null)
                {
                    sSql.AppendLine(" AND " + filter);
                }
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString().Trim(), CommandType.Text, urlForm);
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