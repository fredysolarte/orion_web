using System;
using System.Collections.Generic;
using System.ComponentModel;
using BE.Administracion;
using System.Web;
using DAL.Administracion;
using DataAccess;
namespace BLL.Administracion
{
	[DataObject(true)]
	public class AdmiParametroBL
	{
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public List<AdmiParametro> GetListByIdClass(string connection, string filter, int startRowIndex, int maximumRows, int idClass)
		{
			AdmiParametroDB objDB = new AdmiParametroDB();
			SessionManager oSessionManager = new SessionManager(connection);
			try
			{
				return objDB.GetListByIdClass(oSessionManager, filter, startRowIndex, maximumRows,idClass);
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
		public void Insert(AdmiParametro objEntity)
		{
			AdmiParametroDB objDB = new AdmiParametroDB();
			SessionManager oSessionManager = new SessionManager("");
			try
			{
				objEntity.LogsUsuario = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
				objEntity.LogsFecha = DateTime.Now;
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
		/// <param name="objEntity">Entidad que se va a Modificar</param>
		/// <param name="connection">Cadena de conexión a la base de datos</param>
		[DataObjectMethod(DataObjectMethodType.Update, false)]
		public void Update(AdmiParametro objEntity)
		{
			AdmiParametroDB objDB = new AdmiParametroDB();
			SessionManager oSessionManager = new SessionManager("");

			try
			{
				objEntity.LogsUsuario = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
				objEntity.LogsFecha = DateTime.Now;
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
		/// Obtiene la cantidad de registros
		/// </summary>
		/// <param name="connection">cadena de conexión a la base de datos</param>
		/// <param name="filter">Sentencia de filtrado de la consulta</param>
		/// <returns>Cantidad de registros contados</returns>
		public int Count(string filter)
		{
			AdmiParametroDB objDB = new AdmiParametroDB();
			SessionManager oSessionManager = new SessionManager("");
			oSessionManager.CreateConnection();
			return objDB.CountAll(oSessionManager, filter);

		}
	}
}
