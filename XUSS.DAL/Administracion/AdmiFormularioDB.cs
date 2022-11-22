using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using Mapping;
using BE.Administracion;
using XUSS.DAL.Genericas;
namespace DAL.Administracion
{
    public class AdmiFormularioDB :GenericBaseDB<AdmiFormulario>
    {
        public AdmiFormularioDB()
            : base("admi_pGeneraSecuencia")
        {
        }


        override public List<AdmiFormulario> GetAllList(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();            
            try
            {
                //sSql.AppendLine("SELECT admi_tformulario.form_formulario, admi_tformulario.blob_ayuda, admi_tformulario.form_nombre, admi_tformulario.form_descripcion,");
                //sSql.AppendLine("admi_tformulario.form_tablabase, admi_tformulario.form_estado, admi_tformulario.logs_usuario, admi_tformulario.logs_fecha, admi_tblob.blob_nombre,admi_tformulario.opci_opcion");
                //sSql.AppendLine("FROM admi_tformulario LEFT OUTER JOIN");
                //sSql.AppendLine("admi_tblob ON admi_tformulario.blob_ayuda = admi_tblob.blob_blob");
                sSql.AppendLine("SELECT        admi_tformulario.form_formulario, admi_tformulario.form_nombre, admi_tformulario.form_descripcion, ");
                sSql.AppendLine("admi_tformulario.form_tablabase, admi_tformulario.form_estado, admi_tformulario.logs_usuario, admi_tformulario.logs_fecha, ");
                sSql.AppendLine("admi_tformulario.opci_opcion, admi_topcion.sist_sistema, admi_topcion.modu_modulo, NUll blob_nombre, NULL blob_ayuda");
                sSql.AppendLine("FROM            admi_tformulario ");
                sSql.AppendLine("INNER JOIN admi_topcion ON admi_tformulario.opci_opcion = admi_topcion.opci_opcion ");
                //sSql.AppendLine("LEFT OUTER JOIN admi_tblob ON admi_tformulario.blob_ayuda = admi_tblob.blob_blob");
                if (filter != String.Empty && filter != null)
                {
                    sSql.AppendLine(" WHERE " + filter);
                }

                return MappingData<AdmiFormulario>.MappingList(DBAccess.GetDataReader(oSessionManager, sSql.ToString().Trim(), CommandType.Text, null));
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
        override public int Add(SessionManager oSessionManager, AdmiFormulario item)
        {            
            StringBuilder sSql = new StringBuilder();
            int seqFormulario=-1;
            try
            {
                object[] parametersSequence = new object[2];
                parametersSequence[0] = ((Table)item.GetType().GetCustomAttributes(typeof(Table),true)[0]).EntityId;
                parametersSequence[1] = 0;
                //parametersSequence[2] = 0;
                //seqFormulario = DBAccess.GetSequence(oSessionManager, "admi_pGeneraSecuencia", parametersSequence[1], 0, true);
                seqFormulario = GenericaBD.GetSecuencia(oSessionManager, 5, 0);
                sSql = new StringBuilder();
                sSql.AppendLine("INSERT INTO admi_tformulario");
                sSql.AppendLine("(form_formulario, blob_ayuda, form_nombre, form_descripcion, form_tablabase, form_estado, logs_usuario, logs_fecha,admi_tformulario.opci_opcion)");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)");
                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString().Trim(), CommandType.Text, seqFormulario, item.BlobAyuda, item.FormNombre, item.FormDescripcion, item.FormTablabase, item.FormEstado, item.LogsUsuario, item.LogsFecha, item.OpciOpcion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {                
                sSql = null;
            }
            return seqFormulario;
        }

        public List<AdmiFormulario> GetAllListByIdOpcion(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows, int idOpcion)
        {
            StringBuilder sSql = new StringBuilder();            
            try
            {
                //sSql.AppendLine("SELECT admi_tformulario.form_formulario, admi_tformulario.blob_ayuda, admi_tformulario.form_nombre, admi_tformulario.form_descripcion, ");
                //sSql.AppendLine("admi_tformulario.form_tablabase, admi_tformulario.form_estado, admi_tformulario.logs_usuario, admi_tformulario.logs_fecha, admi_tblob.blob_nombre, admi_tformulario.opci_opcion");
                //sSql.AppendLine("FROM admi_tformulario");
                //sSql.AppendLine("LEFT OUTER JOIN");
                //sSql.AppendLine("admi_tblob ON admi_tformulario.blob_ayuda = admi_tblob.blob_blob");
                //sSql.AppendLine("WHERE (admi_tformulario.opci_opcion = @p0)");
                sSql.AppendLine("SELECT        admi_tformulario.form_formulario, admi_tformulario.blob_ayuda, admi_tformulario.form_nombre, admi_tformulario.form_descripcion, ");
                sSql.AppendLine("admi_tformulario.form_tablabase, admi_tformulario.form_estado, admi_tformulario.logs_usuario, admi_tformulario.logs_fecha, admi_tblob.blob_nombre, ");
                sSql.AppendLine("admi_tformulario.opci_opcion, admi_topcion.sist_sistema, admi_topcion.modu_modulo");
                sSql.AppendLine("FROM            admi_tformulario INNER JOIN");
                sSql.AppendLine("admi_topcion ON admi_tformulario.opci_opcion = admi_topcion.opci_opcion LEFT OUTER JOIN");
                sSql.AppendLine("admi_tblob ON admi_tformulario.blob_ayuda = admi_tblob.blob_blob");
                sSql.AppendLine("WHERE        (admi_tformulario.opci_opcion = @p0)");
                if (filter != String.Empty && filter != null)
                {
                    sSql.AppendLine(" AND " + filter);
                }

                return MappingData<AdmiFormulario>.MappingList(DBAccess.GetDataReader(oSessionManager, sSql.ToString().Trim(), CommandType.Text, idOpcion));
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

        override public void Update(SessionManager oSessionManager, AdmiFormulario item)
        {            
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql = new StringBuilder();
                sSql.AppendLine("UPDATE admi_tformulario");
                sSql.AppendLine("SET blob_ayuda = @p0, form_nombre = @p1, form_descripcion = @p2,");
                sSql.AppendLine("form_tablabase = @p3, form_estado = @p4, logs_usuario = @p5, logs_fecha = @p6");
                sSql.AppendLine("WHERE (form_formulario = @p7)");
                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString().Trim(), CommandType.Text, item.BlobAyuda, item.FormNombre, item.FormDescripcion, item.FormTablabase, item.FormEstado, item.LogsUsuario, item.LogsFecha, item.FormFormulario);
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

        public void Delete(SessionManager oSessionManager, int original_FormFormulario)
        {            
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM admi_tformulario");
                sSql.AppendLine("WHERE (form_formulario = @p0)");
                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString().Trim(), CommandType.Text, original_FormFormulario);
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