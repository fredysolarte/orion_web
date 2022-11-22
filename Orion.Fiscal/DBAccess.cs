using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Orion.Fiscal
{
    public class DBAccess
    {
        private SqlConnection connection;
        private SqlTransaction transaction;
        private string connectionString;
        private SqlCommand _command = null;
        private const string ErrorNoOpen = "No se pudo conectar a la BD";
        private const string ErrorEjecutandoComando = "No se pudo ejecutar el comando";
        private const string ErrorNoConnectionString = "No hay una cadena de conexion asignada";

        #region Public Methods

        public DBAccess()
        {
        }

        public DBAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public string ConnectionString
        {
            get { return this.connectionString; }
            set { this.connectionString = value; }
        }

        public void BeginTransaction()
        {
            if (GetState() != ConnectionState.Open)
            {
                OpenConnection();
            }
            this.transaction = this.connection.BeginTransaction();
        }

        public void EndTransaction()
        {
            this.transaction.Dispose();
            this.transaction = null;
        }

        public void Commit()
        {
            if (this.transaction != null)
            {
                this.transaction.Commit();
            }
        }

        public void Rollback()
        {
            if (this.transaction != null)
            {
                this.transaction.Rollback();
            }
        }

        public ConnectionState GetState()
        {
            if (this.connection != null)
            {
                return this.connection.State;
            }
            else
            {
                return ConnectionState.Closed;
            }
        }

        public void CrearConnection(string connectionString)
        {
            this.connectionString = connectionString;
            CrearConnection();
        }

        public void CrearConnection()
        {
            if (string.IsNullOrWhiteSpace(this.connectionString))
            {
                throw new Exception(ErrorNoConnectionString);
            }
            if (this.connection != null)
            {
                if (this.connection.State == ConnectionState.Open)
                {
                    this.connection.Close();
                    this.connection = new SqlConnection(this.connectionString);
                    return;
                }
            }
            else
            {
                this.connection = new SqlConnection(this.connectionString);
                return;
            }
        }

        public bool OpenConnection()
        {
            if (this.connection == null)
            {
                CrearConnection();
            }
            try
            {
                if (this.connection.State == ConnectionState.Open)
                {
                    return true;
                }
                this.connection.Open();
                if (this.connection.State == ConnectionState.Open)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ExecuteNonQuery(string sql, params object[] parametersValues)
        {
            ValideConnection();
            using (SqlCommand command = new SqlCommand(sql, this.connection))
            {
                if (this.transaction != null)
                {
                    command.Transaction = this.transaction;
                }
                command.CommandTimeout = 0;
                CreateParameter(command, parametersValues);
                return command.ExecuteNonQuery();
            }
        }

        public object ExecuteScalar(string sql, params object[] parametersValues)
        {
            ValideConnection();
            using (SqlCommand command = new SqlCommand(sql, this.connection))
            {
                if (this.transaction != null)
                {
                    command.Transaction = this.transaction;
                }
                command.CommandTimeout = 0;
                CreateParameter(command, parametersValues);
                return command.ExecuteScalar();
            }
        }

        public SqlDataReader ExecuteReader(string sql, params object[] parametersValues)
        {
            ValideConnection();
            using (SqlCommand command = new SqlCommand(sql, this.connection))
            {
                if (this.transaction != null)
                {
                    command.Transaction = this.transaction;
                }
                command.CommandTimeout = 0;
                CreateParameter(command, parametersValues);
                return command.ExecuteReader();
            }
        }

        public SqlDataReader ExecuteReader()
        {
            ValideConnection();
            if (_command == null)
            {
                throw new Exception(ErrorEjecutandoComando);
            }

            SqlDataReader dato = _command.ExecuteReader();
            _command.Dispose();
            _command = null;
            return dato;
        }

        public void CreateCommand()
        {
            ValideConnection();
            _command = new SqlCommand();
            _command.Connection = this.connection;
        }

        public void SetSQlCommand(string sql)
        {
            if (_command == null)
            {
                CreateCommand();
            }
            _command.CommandText = sql;
        }

        public void AddParametro(string nombre, object val)
        {
            if (_command == null)
            {
                CreateCommand();
            }
            SqlParameter parametro = new SqlParameter(nombre, val);
            _command.Parameters.Add(parametro);
        }

        public DataTable ExecuteDataTable(string sql, params object[] parametersValues)
        {
            SqlDataReader reader;
            DataTable dataTable = new DataTable();
            using (reader = ExecuteReader(sql, parametersValues))
            {
                dataTable.Load(reader);
                return dataTable;
            }
        }

        public bool CloseConnection()
        {
            if (GetState() == ConnectionState.Open)
            {
                this.connection.Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataSet ExecuteDataSet(string sql, params object[] parametersValues)
        {
            ValideConnection();
            using (SqlCommand command = new SqlCommand(sql, this.connection))
            {
                CreateParameter(command, parametersValues);
                using (SqlDataAdapter sdaAdapter = new SqlDataAdapter(command))
                {
                    DataSet dst = new DataSet();
                    sdaAdapter.Fill(dst);
                    return dst;
                }
            }
        }

        #endregion

        #region Private Methods

        private void CreateParameter(SqlCommand command, params object[] parametersValues)
        {
            SqlParameter parameter;
            int i = 0;
            if (parametersValues != null)
            {
                foreach (object param in parametersValues)
                {
                    parameter = command.CreateParameter();
                    parameter.Direction = ParameterDirection.Input;
                    //parameter.DbType = GetDbType(param);
                    if (param != null)
                    {
                        parameter.Value = param;
                    }
                    else
                    {
                        parameter.Value = DBNull.Value;
                    }
                    parameter.ParameterName = "@p" + i;
                    i++;
                    command.Parameters.Add(parameter);
                }
            }
        }

        private void ValideConnection()
        {
            if (GetState() != ConnectionState.Open)
            {
                if (!OpenConnection())
                {
                    throw new Exception(ErrorNoOpen);
                }
            }
        }

        private DbType GetDbType(object parameter)
        {
            if (parameter is double)
            {
                return DbType.Double;
            }
            else
            {
                if (parameter is string)
                {
                    return DbType.String;
                }
                else
                {
                    if (parameter is int)
                    {
                        return DbType.Int32;
                    }
                    else
                    {
                        if (parameter is DateTime)
                        {
                            return DbType.DateTime;
                        }
                        else
                        {
                            if (parameter is TimeSpan)
                            {
                                return DbType.Time;
                            }
                            else
                            {
                                if (parameter is long)
                                {
                                    return DbType.Int64;
                                }
                                else
                                {
                                    if (parameter is bool)
                                    {
                                        return DbType.Boolean;
                                    }
                                    else
                                    {
                                        return DbType.String;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
