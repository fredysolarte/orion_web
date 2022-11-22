using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Web;
using BE.Administracion;
using DAL.Administracion;
using DataAccess;
namespace BLL.Administracion
{
	public class AdmiClaseBL
	{
		/// <summary>
		/// Obtiene una lista de AdmiClase 
		/// </summary>
		/// <param name="connection">cadena de conexi�n a la base de datos</param>
		/// <param name="filter">Sentencia de filtrado de la consulta</param>
		/// <param name="startRowIndex">�ndice de p�gina para generar la consulta paginada</param>
		/// <param name="maximumRows">M�ximo de registros a consultar</param>
		/// <returns>Coleccion generica de tipo lista de entidades </returns>
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public List<AdmiClase> GetList(string connection, string filter, int startRowIndex, int maximumRows)
		{
			AdmiClaseDB objDB = new AdmiClaseDB();
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
		/// M�todo para Inserci�n de datos
		/// </summary>
		/// <param name="objEntity">Entidad que se va a insertar</param>
		/// <param name="connection">Cadena de conexi�n a la base de datos</param>
		[DataObjectMethod(DataObjectMethodType.Insert, false)]
		public void Insert(AdmiClase objEntity)
		{
			AdmiClaseDB objDB = new AdmiClaseDB();
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
		/// M�todo para Actualizacion de datos
		/// </summary>
		/// <param name="objEntity">Entidad que se va a Modificar</param>
		/// <param name="connection">Cadena de conexi�n a la base de datos</param>
		[DataObjectMethod(DataObjectMethodType.Update, false)]
		public void Update(AdmiClase objEntity)
		{
			AdmiClaseDB objDB = new AdmiClaseDB();
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
		/// <param name="connection">cadena de conexi�n a la base de datos</param>
		/// <param name="filter">Sentencia de filtrado de la consulta</param>
		/// <returns>Cantidad de registros contados</returns>
		public int Count(string filter)
		{
			AdmiClaseDB objDB = new AdmiClaseDB();
			SessionManager oSessionManager = new SessionManager("");
			return objDB.CountAll(oSessionManager, filter);
		}
	}
}
