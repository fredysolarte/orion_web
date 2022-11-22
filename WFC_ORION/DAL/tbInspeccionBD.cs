using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFC_ORION.DAL
{
    public class tbInspeccionBD
    {
		public static int InsertInspeccion(DBAccess ObjDB, int IP_CODIGO,string IP_CODEMP,int TRCODTER,string IP_TIPO,string IP_PREDIO,string IP_SERVICIO,DateTime? IP_FECHA,string IP_OBSERVACIONES,string IP_ESTADO, string IP_USUARIO,int? AT_CODIGO)
		{
			StringBuilder sql = new StringBuilder();
			
			try
			{
				sql.AppendLine("INSERT INTO TB_INSPECCION (IP_CODIGO,IP_CODEMP,TRCODTER,IP_TIPO,IP_PREDIO,IP_SERVICIO,IP_FECHA,IP_OBSERVACIONES,IP_ESTADO, AT_CODIGO, IP_USUARIO,IP_FECING) VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,GETDATE())");
				return ObjDB.ExecuteNonQuery(sql.ToString(), IP_CODIGO, IP_CODEMP, TRCODTER, IP_TIPO, IP_PREDIO, IP_SERVICIO, IP_FECHA, IP_OBSERVACIONES, IP_ESTADO, AT_CODIGO, IP_USUARIO);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				sql = null;
			}
		}
		public static int InsertEvidenciaInspeccion(DBAccess ObjDB, int IP_CODIGO, int EI_TIPO, object EI_FOTO, DateTime? EI_FECHA)
		{
			StringBuilder sql = new StringBuilder();

			try
			{
				sql.AppendLine("INSERT INTO TB_EVIDENCIAS_INSPECCION (IP_CODIGO, EI_TIPO, EI_FOTO, EI_FECHA) VALUES (@p0,@p1,@p2,@p3)");
				return ObjDB.ExecuteNonQuery(sql.ToString(), IP_CODIGO, EI_TIPO, EI_FOTO, EI_FECHA);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				sql = null;
			}
		}
		public static SqlDataReader getInspeccion(DBAccess ObjDB, string inFiltro)
		{
			StringBuilder sSql = new StringBuilder();
			try
			{
				sSql.AppendLine("");

				return ObjDB.ExecuteReader(sSql.ToString());
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
		public static SqlDataReader getAtencionClienteInspeccion(DBAccess ObjDB, string inFiltro)
		{
			StringBuilder sSql = new StringBuilder();
			try
			{
				sSql.AppendLine("SELECT * ");
				sSql.AppendLine("FROM TB_ATENCIONCLIENTE WITH(NOLOCK)");
				sSql.AppendLine("LEFT OUTER JOIN TERCEROS WITH(NOLOCK) ON(TERCEROS.TRCODTER = TB_ATENCIONCLIENTE.TRCODTER)");
				sSql.AppendLine(" WHERE "+inFiltro);

				return ObjDB.ExecuteReader(sSql.ToString());
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

		public static int insertAtencionCliente(DBAccess ObjDB, int AT_CODIGO, string AT_CODEMP, int TRCODTER, string AT_TIPOINSPECCION, string AT_TIPOPREDIO, string AT_TIPO, string AT_ESTADO, string AT_USUARIO,
												DateTime AT_FECPROGRAMACION, string AT_RESPONSABLE, string AT_CTACONTRATO, string AT_DISISOMETRICO, string AT_PLANOAPROBADO, string AT_CERTLABORAL, string AT_COMPETENCIAS,
												string AT_ADMINISTRADOR, string AT_CONTACTO, string AT_IDCONSTRUCTOR, string AT_NOMCONSTRUCTOR, string AT_NEWUSD,
												string AT_ALMATRIZ, string AT_CLMATRIZ, string AT_CUINSPECCION, DateTime? AT_FECUINSPECCION, string AT_ZONA, string AT_TIPOGAS)
		{
			StringBuilder sSql = new StringBuilder();
			try
			{
				sSql.AppendLine("INSERT INTO TB_ATENCIONCLIENTE (AT_CODIGO, AT_CODEMP, TRCODTER, AT_TIPOINSPECCION, AT_TIPOPREDIO, AT_TIPO, AT_FECPROGRAMACION, AT_RESPONSABLE, AT_CTACONTRATO, ");
				sSql.AppendLine("AT_DISISOMETRICO,AT_PLANOAPROBADO,AT_CERTLABORAL,AT_COMPETENCIAS, AT_ADMINISTRADOR,AT_CONTACTO,AT_IDCONSTRUCTOR,AT_NOMCONSTRUCTOR,AT_NEWUSD, ");
				sSql.AppendLine("AT_ALMATRIZ,AT_CLMATRIZ,AT_CUINSPECCION, AT_FECUINSPECCION,AT_ZONA,AT_ESTADO, AT_USUARIO, AT_TIPOGAS, AT_FECING, AT_FECMOD)");
				sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,GETDATE(),GETDATE())");

				return ObjDB.ExecuteNonQuery(sSql.ToString(), AT_CODIGO, AT_CODEMP, TRCODTER, AT_TIPOINSPECCION, AT_TIPOPREDIO, AT_TIPO, AT_FECPROGRAMACION, AT_RESPONSABLE,
					AT_CTACONTRATO, AT_DISISOMETRICO, AT_PLANOAPROBADO, AT_CERTLABORAL, AT_COMPETENCIAS, AT_ADMINISTRADOR, AT_CONTACTO, AT_IDCONSTRUCTOR, AT_NOMCONSTRUCTOR, AT_NEWUSD,
					AT_ALMATRIZ, AT_CLMATRIZ, AT_CUINSPECCION, AT_FECUINSPECCION, AT_ZONA, AT_ESTADO, AT_USUARIO, AT_TIPOGAS);
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
