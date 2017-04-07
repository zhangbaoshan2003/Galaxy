using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Galaxy.DAL2
{
    public class SQLSession : IDisposable
    {
        #region Private varibles

        private SqlConnection sqlConnection = null;
        public SqlConnection SqlConnection
        {
            get { return sqlConnection; }
        }
        private SqlTransaction sqlTransaction = null;

        public SqlTransaction SQLTransaction
        {
            get { return sqlTransaction; }
        }

        #endregion

        #region Public constructor

        public SQLSession(string connStr)
        {
            string connectionString = "";
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings[connStr].ConnectionString;
            }
            catch
            {
                connectionString = connStr;
            }
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Start a database transaction and save the transaction instance.
        /// </summary>
        public void BeginTransaction()
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlTransaction = sqlConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            }
        }

        /// <summary>
        /// Commit the transaction saved in private variables.
        /// </summary>
        public void CommitTransaction()
        {
            if (sqlTransaction != null)
            {
                sqlTransaction.Commit();
                sqlTransaction = null;
            }
        }

        /// <summary>
        /// Rollback the transaction saved in private variables.
        /// </summary>
        public void RollbackTransaction()
        {
            if (sqlTransaction != null)
            {
                sqlTransaction.Rollback();
                sqlTransaction = null;
            }
        }

        /// <summary>
        /// Create a SQLcommand object for class consumer to execute SQL command.
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public SqlCommand CreateSQLCommand(string commandText)
        {
            SqlCommand sqlCommand;

            if (sqlTransaction != null)
                sqlCommand = new SqlCommand(commandText, sqlConnection, sqlTransaction);
            else
                sqlCommand = new SqlCommand(commandText, sqlConnection);

            return sqlCommand;
        }

        /// <summary>
        /// Execute a SQL statement and returns a DataTable object.
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public DataTable SQLQuery(string commandText)
        {
            SqlCommand sqlCommand = CreateSQLCommand(commandText);
            sqlCommand.CommandTimeout = 10*60;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();

            sqlDataAdapter.Fill(dataTable);

            return dataTable;
        }

        /// <summary>
        /// Execute a SQL statement and returns a DataTable object.
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public DataSet DataSetQuery(string commandText)
        {
            SqlCommand sqlCommand = CreateSQLCommand(commandText);
            sqlCommand.CommandTimeout = 60 * 1;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet ds = new DataSet();

            sqlDataAdapter.Fill(ds);

            return ds;
        }

        /// <summary>
        /// Execute a SQL statement and returns a DataTable object.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="paraList"></param>
        /// <returns></returns>
        public DataTable SQLQuery(string commandText, SqlParameter[] paraList)
        {
            SqlCommand sqlCommand = CreateSQLCommand(commandText);
            sqlCommand.Parameters.Clear();
            if (paraList != null)
            {
                sqlCommand.Parameters.AddRange(paraList);
            }

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();

            sqlDataAdapter.Fill(dataTable);

            return dataTable;
        }

        /// <summary>
        /// Close the sql connection if it is opened when the instance of SQLSession class is disposed.
        /// </summary>
        public void Dispose()
        {
            if (sqlConnection != null)
                sqlConnection.Close();
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Indicate whether the session is in a database transaction.
        /// </summary>
        public bool IsTransaction
        {
            get { return sqlTransaction != null; }
        }

        #endregion
    }
}
