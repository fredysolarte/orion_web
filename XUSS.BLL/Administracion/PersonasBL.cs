using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using BE.Administracion;
using DAL.Administracion;
using DataAccess;


namespace BLL.Administracion
{
    [DataObject(true)]
    public class PersonasBL
    {
        /// <summary>
        /// Obtiene una lista 
        /// </summary>
        /// <param name="connection">cadena de conexión a la base de datos</param>
        /// <param name="filter">Sentencia de filtrado de la consulta</param>
        /// <param name="startRowIndex">Índice de página para generar la consulta paginada</param>
        /// <param name="maximumRows">Máximo de registros a consultar</param>
        /// <returns>Coleccion generica de tipo lista de entidades </returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Personas> GetList(string connection, string filter, int startRowIndex, int maximumRows)
        {
            PersonasDB objDB = new PersonasDB();
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return objDB.GetAllList(oSessionManager, filter, startRowIndex, maximumRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDB = null;
                oSessionManager = null;
            }
        }

        /// <summary>
        /// Método para Inserción de datos
        /// </summary>
        /// <param name="objEntity">Entidad que se va a insertar</param>
        /// <param name="connection">Cadena de conexión a la base de datos</param>
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void Insert(Personas objEntity)
        {
            PersonasDB objDB = new PersonasDB();
            SessionManager oSessionManager = new SessionManager("");
            try
            {
                oSessionManager.CreateConnection();
                oSessionManager.BeginTransaction();
                objDB.Add(oSessionManager, objEntity);
                oSessionManager.CommitTranstaction();
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager.CloseConnection();
                objDB = null;
                oSessionManager = null;
            }
        }

        /// <summary>
        /// Método para Actualizacion de datos
        /// </summary>
        /// <param name="objEntity">Entidad que se va a insertar</param>
        /// <param name="connection">Cadena de conexión a la base de datos</param>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void Update(Personas objEntity)
        {
            PersonasDB objDB = new PersonasDB();
            SessionManager oSessionManager = new SessionManager("");
            try
            {
                oSessionManager.CreateConnection();
                oSessionManager.BeginTransaction();
                objDB.Update(oSessionManager, objEntity);
                oSessionManager.CommitTranstaction();
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager.CloseConnection();
                objDB = null;
                oSessionManager = null;
            }
        }

        /// <summary>
        /// Método para Eliminación de datos
        /// </summary>
        /// <param name="connection">Cadena de conexión a la base de datos</param>
        /// <param name="objEntity">Entidad que se va a Eliminar</param>
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void Delete(Personas objEntity)
        {
            PersonasDB objDB = new PersonasDB();
            SessionManager oSessionManager = new SessionManager("");
            try
            {
                oSessionManager.CreateConnection();
                oSessionManager.BeginTransaction();
                objDB.Delete(oSessionManager, objEntity);
                oSessionManager.CommitTranstaction();
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager.CloseConnection();
                objDB = null;
                oSessionManager = null;
            }
        }
    }
}
