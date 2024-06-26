using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;



namespace Utilities
{
    public class DataAccess
    {


        private static string AnonConnectionString = /*Security.DecryptData(*/ConfigurationManager.ConnectionStrings["AnonConnection"].ConnectionString/*)*/;

        //private static string ConnectionString { get; set; }


        public static string getConnectionString(string ConnectionName)
        {
            string esProduccion = ConfigurationManager.AppSettings["esProduccion"];
            string connectionStr = "";
            if (esProduccion == "1")
            {
                connectionStr = ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString;
            }
            else
            {
                connectionStr = ConfigurationManager.ConnectionStrings["UAT_" + ConnectionName].ConnectionString;
            }

            return  /*Security.DecryptData(*/connectionStr/*)*/;
        }

        public static string DataBaseProvider
        {
            get
            {
                string dataProvider = ConfigurationManager.AppSettings["DatabaseProvider"] != null ?
                     ConfigurationManager.AppSettings["DatabaseProvider"] : "SQL";

                return dataProvider;
            }
        }

        public static string DataBaseProvider_O
        {
            get
            {
                string dataProvider = ConfigurationManager.AppSettings["DatabaseProvider"] != null ?
                     ConfigurationManager.AppSettings["DatabaseProvider"] : "Oracle";

                return dataProvider;
            }
        }

        public static string DataBaseProvider_M
        {
            get
            {
                string dataProvider = ConfigurationManager.AppSettings["DatabaseProvider"] != null ?
                     ConfigurationManager.AppSettings["DatabaseProvider"] : "MySQL";

                return dataProvider;
            }
        }

        /// <summary>
        /// create a new connection to cviper application database
        /// </summary>
        /// <returns></returns>
        public static SqlConnection CreateConnection(string ConnectionName)
        {
            try
            {
                SqlConnection newConnection = new SqlConnection(getConnectionString(ConnectionName));
                return newConnection;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Obsolete]
        public static OracleConnection CreateConnection_O(string ConnectionName)
        {
            try
            {
                OracleConnection newConnection = new OracleConnection(getConnectionString(ConnectionName));
                return newConnection;
            }
            catch (OracleException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static MySqlConnection CreateConnection_M(string ConnectionName)
        {
            try
            {
                MySqlConnection newConnection = new MySqlConnection(getConnectionString(ConnectionName));
                return newConnection;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private DataSet CreateDataset(string selectCommandText, string ConnectionName)
        {
            SqlConnection conn = null;
            try
            {
                string connString = getConnectionString(ConnectionName);

                DataSet dsSql = new DataSet();
                conn = new SqlConnection(connString);
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(selectCommandText, conn);
                    adapter.Fill(dsSql);
                    return dsSql;
                }
                finally
                {
                    conn.Close();
                    conn = null;
                }

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null)
                    conn.Close();

                conn = null;
            }
        }


        public DataTable CreateDataTable(SqlCommand sqlSelectCommand, String ConnectionName)
        {
            DataSet dset = new DataSet();
            sqlSelectCommand.CommandTimeout = 0;

            if (DataBaseProvider.ToUpper() == "SQL")
            {
                dset = CreateDataset(sqlSelectCommand, getConnectionString(ConnectionName));
            }

            return (dset != null && dset.Tables.Count == 1) ? dset.Tables[0] : null;
        }

        [Obsolete]
        public DataTable CreateDataTable_O(OracleCommand sqlSelectCommand, String ConnectionName)
        {
            DataSet dset = new DataSet();
            sqlSelectCommand.CommandTimeout = 0;

            if (DataBaseProvider_O.ToUpper() == "ORACLE")
            {
                dset = CreateDataset_O(sqlSelectCommand, getConnectionString(ConnectionName));
            }

            return (dset != null && dset.Tables.Count == 1) ? dset.Tables[0] : null;
        }

        public DataTable CreateDataTable_M(MySqlCommand sqlSelectCommand, String ConnectionName)
        {
            DataSet dset = new DataSet();
            sqlSelectCommand.CommandTimeout = 0;

            if (DataBaseProvider_M.ToUpper() == "MYSQL")
            {
                dset = CreateDataset_M(sqlSelectCommand, getConnectionString(ConnectionName));
            }

            return (dset != null && dset.Tables.Count == 1) ? dset.Tables[0] : null;
        }

        public DataTable CreateDataTable(SqlTransaction transaction, SqlCommand sqlSelectCommand)
        {
            DataSet dset;

            dset = CreateDataset(transaction, sqlSelectCommand);

            return (dset != null && dset.Tables.Count == 1) ? dset.Tables[0] : null;
        }

        public DataTable CreateAnonDataTable(SqlCommand sqlSelectCommand)
        {
            DataSet dset;
            dset = CreateDataset(sqlSelectCommand, AnonConnectionString);

            if (dset != null && dset.Tables.Count > 1)
                throw new Exception("the query return more than one result");
            return (dset != null && dset.Tables.Count == 1) ? dset.Tables[0] : null;
        }

        public object DoExecuteScalar(SqlCommand command, String ConnectionName)
        {
            object dr;

            try
            {
                command.Connection = new SqlConnection(getConnectionString(ConnectionName));
                command.Connection.Open();
                dr = command.ExecuteScalar();
                command.Connection.Close();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (command.Connection != null)
                {
                    command.Connection.Close();
                }

                if (command != null)
                {
                    command.Dispose();
                }
            }

            return dr;
        }

        public DataSet CreateDataset(SqlCommand sqlSelectCommand, string ConectionStr)
        {
            DataSet dsSql;

            try
            {
                sqlSelectCommand.Connection = new SqlConnection(ConectionStr);
                sqlSelectCommand.Connection.Open();
                SqlDataAdapter daSql = new SqlDataAdapter(sqlSelectCommand);
                dsSql = new DataSet();
                daSql.Fill(dsSql);
                sqlSelectCommand.Connection.Close();
            }
            catch (SqlException ex)
            {
                if (sqlSelectCommand != null && sqlSelectCommand.Parameters != null)
                    throw new Exception(ex.Message);
                else
                    throw new Exception(ex.Message);
            }
            finally
            {
                if (sqlSelectCommand.Connection != null)
                {
                    sqlSelectCommand.Connection.Close();
                    sqlSelectCommand.Connection = null;
                }


                if (sqlSelectCommand != null)
                {
                    sqlSelectCommand.Dispose();
                }
            }

            return dsSql;
        }

        [Obsolete]
        public DataSet CreateDataset_O(OracleCommand sqlSelectCommand, string ConectionStr)
        {
            DataSet dsSql;

            try
            {
                sqlSelectCommand.Connection = new OracleConnection(ConectionStr);
                sqlSelectCommand.Connection.Open();
                OracleDataAdapter daSql = new OracleDataAdapter(sqlSelectCommand);
                dsSql = new DataSet();
                daSql.Fill(dsSql);
                if (dsSql.Tables.Count == 0)
                {


                    int rowsInserted = sqlSelectCommand.ExecuteNonQuery();
                    foreach (var currentParam in sqlSelectCommand.Parameters)
                    {
                        //if (currentParam.GetType().GenericParameterAttributes[11] != "")
                        //{

                        //}
                    }

                }

                sqlSelectCommand.Connection.Close();
                OracleConnection.ClearPool(sqlSelectCommand.Connection);
            }
            catch (OracleException ex)
            {
                if (sqlSelectCommand != null && sqlSelectCommand.Parameters != null)
                    throw new Exception(ex.Message);
                else
                    throw new Exception(ex.Message);
            }
            finally
            {
                if (sqlSelectCommand.Connection != null)
                {
                    sqlSelectCommand.Connection.Close();
                    OracleConnection.ClearPool(sqlSelectCommand.Connection);
                    sqlSelectCommand.Connection = null;
                }


                if (sqlSelectCommand != null)
                {
                    sqlSelectCommand.Dispose();

                }
            }

            return dsSql;
        }

        public DataSet CreateDataset_M(MySqlCommand sqlSelectCommand, string ConnectionStr)
        {
            DataSet dsSql;

            try
            {
                sqlSelectCommand.Connection = new MySqlConnection(ConnectionStr);
                sqlSelectCommand.Connection.Open();
                MySqlDataAdapter daSql = new MySqlDataAdapter(sqlSelectCommand);
                dsSql = new DataSet();
                daSql.Fill(dsSql);
                sqlSelectCommand.Connection.Close();
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (sqlSelectCommand.Connection != null)
                {
                    sqlSelectCommand.Connection.Close();
                    sqlSelectCommand.Connection = null;
                }

                if (sqlSelectCommand != null)
                {
                    sqlSelectCommand.Dispose();
                }
            }

            return dsSql;
        }


        public DataSet CreateDataset(SqlTransaction transaction, SqlCommand sqlSelectCommand)
        {
            DataSet dsSql;

            try
            {
                sqlSelectCommand.Transaction = transaction;
                sqlSelectCommand.Connection = transaction.Connection;
                SqlDataAdapter daSql = new SqlDataAdapter(sqlSelectCommand);
                dsSql = new DataSet();
                daSql.Fill(dsSql);
                //sqlSelectCommand.Connection.Close();
            }
            catch (SqlException ex)
            {
                if (sqlSelectCommand != null && sqlSelectCommand.Parameters != null)
                    throw new Exception(ex.Message);
                else
                    throw new Exception(ex.Message);
            }
            return dsSql;
        }

        [Obsolete]
        public DataSet CreateDataset_O(OracleTransaction transaction, OracleCommand sqlSelectCommand)
        {
            DataSet dsSql;

            try
            {
                sqlSelectCommand.Transaction = transaction;
                sqlSelectCommand.Connection = transaction.Connection;
                OracleDataAdapter daSql = new OracleDataAdapter(sqlSelectCommand);
                dsSql = new DataSet();
                daSql.Fill(dsSql);
                //sqlSelectCommand.Connection.Close();
            }
            catch (OracleException ex)
            {
                if (sqlSelectCommand != null && sqlSelectCommand.Parameters != null)
                    throw new Exception(ex.Message);
                else
                    throw new Exception(ex.Message);
            }
            return dsSql;
        }

        public void CreateBulkCopy(DataTable source, String DestinationTable, bool MapByIndex, bool FitBulkTable, string ConnectionName, Dictionary<String, String> MappingColumns = null)
        {
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(getConnectionString(ConnectionName)))
            {
                bulkCopy.DestinationTableName = DestinationTable;

                try
                {
                    if (MapByIndex)
                    {
                        if (MappingColumns == null)
                        {
                            bulkCopy.WriteToServer(source);
                        }
                        else
                        {
                            foreach (var item in MappingColumns)
                            {
                                bulkCopy.ColumnMappings.Add(Convert.ToInt32(item.Key.Replace("Column", "")) - 1, item.Value);
                            }
                            bulkCopy.BulkCopyTimeout = 3600;
                            bulkCopy.WriteToServer(source);
                        }
                    }
                    else if (FitBulkTable)
                    {
                        bulkCopy.WriteToServer(source);
                    }
                    else
                    {
                        if (MappingColumns == null)
                        {
                            bulkCopy.WriteToServer(source);
                        }
                        else
                        {
                            foreach (var item in MappingColumns)
                            {
                                bulkCopy.ColumnMappings.Add(item.Key, item.Value);
                            }
                            bulkCopy.BulkCopyTimeout = 3600;
                            bulkCopy.WriteToServer(source);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void CreateBulkCopyByIndex(DataTable source, String DestinationTable, string ConnectionName, Dictionary<String, String> MappingColumns = null)
        {
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(getConnectionString(ConnectionName)))
            {
                bulkCopy.DestinationTableName = DestinationTable;

                try
                {
                    if (MappingColumns == null)
                    {
                        bulkCopy.WriteToServer(source);
                    }
                    else
                    {
                        foreach (var item in MappingColumns)
                        {
                            bulkCopy.ColumnMappings.Add(Convert.ToInt32(item.Key), item.Value);
                        }
                        bulkCopy.BulkCopyTimeout = 3600;
                        bulkCopy.WriteToServer(source);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

    }
}