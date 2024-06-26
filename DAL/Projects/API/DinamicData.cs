using Entities.Request.WebAPI_NGK;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.OracleClient;
using System.Data.SqlClient;
using Utilities;
using System.Configuration;


namespace DAL.Projects.WebAPI
{
    public class DinamicData
    {
        DataAccess DA = new DataAccess();

        public DataTable WebAPI_GetDinamicData(string dencryptedSP, List<paramValues> paramValues, string dencryptedConnection)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = dencryptedSP;
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (var currentParam in paramValues)
                    {
                        if (currentParam.type_out == null)
                        {
                            cmd.Parameters.Add(currentParam.name, GetSQLDBType(currentParam.type)).Value = currentParam.value;

                        }
                        else
                        {
                            if (currentParam.type_out.Equals("1"))
                            {
                                cmd.Parameters.Add(currentParam.name, GetSQLDBType(currentParam.type)).Direction = ParameterDirection.Output;
                            }
                            else
                            {
                                cmd.Parameters.Add(currentParam.name, GetSQLDBType(currentParam.type)).Value = currentParam.value;
                            }
                        }
                    }

                    return DA.CreateDataTable(cmd, dencryptedConnection);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable WebAPI_GetDinamicData_Select(string dataString, string dencryptedConnection, List<paramValues> paramValues)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = dataString;
                    cmd.CommandType = CommandType.Text;

                    foreach (var currentParam in paramValues)
                    {
                        if (currentParam.type_out == null)
                        {
                            cmd.Parameters.Add(currentParam.name, GetSQLDBType(currentParam.type)).Value = currentParam.value;

                        }
                        else
                        {
                            if (currentParam.type_out.Equals("1"))
                            {
                                cmd.Parameters.Add(currentParam.name, GetSQLDBType(currentParam.type)).Direction = ParameterDirection.Output;
                            }
                            else
                            {
                                cmd.Parameters.Add(currentParam.name, GetSQLDBType(currentParam.type)).Value = currentParam.value;
                            }
                        }
                    }

                    return DA.CreateDataTable(cmd, dencryptedConnection);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Obsolete]
        public DataTable WebAPI_GetDinamicData_O(string dencryptedSP, List<paramValues> paramValues, string dencryptedConnection)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.CommandText = dencryptedSP;
                    cmd.CommandType = CommandType.StoredProcedure;


                    foreach (var currentParam in paramValues)
                    {

                        if (currentParam.type_out == null)
                        {
                            cmd.Parameters.Add(currentParam.name, GetORACLEDBType(currentParam.type)).Value = currentParam.value;

                        }
                        else
                        {
                            if (currentParam.type_out.Equals("1"))
                            {
                                if (currentParam.type.Equals("varchar") || currentParam.type.Equals("varchar2"))
                                {
                                    cmd.Parameters.Add(currentParam.name, GetORACLEDBType(currentParam.type), 500).Direction = ParameterDirection.Output;
                                }
                                else
                                {
                                    cmd.Parameters.Add(currentParam.name, GetORACLEDBType(currentParam.type)).Direction = ParameterDirection.Output;
                                }

                            }
                            else
                            {
                                cmd.Parameters.Add(currentParam.name, GetORACLEDBType(currentParam.type)).Value = currentParam.value;
                            }
                        }

                    }

                    return DA.CreateDataTable_O(cmd, dencryptedConnection);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Obsolete]
        public DataTable WebAPI_GetDinamicData_Select_O(string dataString, string dencryptedConnection, List<paramValues> paramValues)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.CommandText = dataString;
                    cmd.CommandType = CommandType.Text;
                    foreach (var currentParam in paramValues)
                    {

                        if (currentParam.type_out == null)
                        {
                            cmd.Parameters.Add(currentParam.name, GetORACLEDBType(currentParam.type)).Value = currentParam.value;

                        }
                        else
                        {
                            if (currentParam.type_out.Equals("1"))
                            {
                                if (currentParam.type.Equals("varchar") || currentParam.type.Equals("varchar2"))
                                {
                                    cmd.Parameters.Add(currentParam.name, GetORACLEDBType(currentParam.type), 500).Direction = ParameterDirection.Output;
                                }
                                else
                                {
                                    cmd.Parameters.Add(currentParam.name, GetORACLEDBType(currentParam.type)).Direction = ParameterDirection.Output;
                                }

                            }
                            else
                            {
                                cmd.Parameters.Add(currentParam.name, GetORACLEDBType(currentParam.type)).Value = currentParam.value;
                            }
                        }

                    }



                    return DA.CreateDataTable_O(cmd, dencryptedConnection);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public DataTable WebAPI_GetDinamicData_M(string dencryptedSP, List<paramValues> paramValues, string dencryptedConnection)
        //{

        //    try
        //    {
        //        using (MySqlConnection conn = new MySqlConnection())
        //        {
        //            using (MySqlCommand cmd = new MySqlCommand(dencryptedSP, conn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                foreach (var currentParam in paramValues)
        //                {
        //                    var sqlParam = cmd.Parameters.Add(currentParam.name, GetMySQLDBType(currentParam.type));
        //                    if (currentParam.type_out == null)
        //                    {
        //                        sqlParam.Value = currentParam.value;
        //                    }
        //                    else
        //                    {
        //                        if (currentParam.type_out.Equals("1"))
        //                        {
        //                            sqlParam.Direction = ParameterDirection.Output;
        //                        }
        //                        else
        //                        {
        //                            sqlParam.Value = currentParam.value;
        //                        }
        //                    }
        //                }

        //                conn.Open();
        //                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
        //                {
        //                    DataTable dt = new DataTable();
        //                    da.Fill(dt);
        //                    return dt;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Error executing stored procedure '{dencryptedSP}': {ex.Message}", ex);
        //    }
        //}
        public DataTable WebAPI_GetDinamicData_M(string dencryptedSP, List<paramValues> paramValues, string dencryptedConnection)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.CommandText = dencryptedSP;
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (var currentParam in paramValues)
                    {
                        if (currentParam.type_out == null)
                        {
                            cmd.Parameters.Add(currentParam.name, GetMySQLDBType(currentParam.type)).Value = currentParam.value;

                        }
                        else
                        {
                            if (currentParam.type_out.Equals("1"))
                            {
                                cmd.Parameters.Add(currentParam.name, GetMySQLDBType(currentParam.type)).Direction = ParameterDirection.Output;
                            }
                            else
                            {
                                cmd.Parameters.Add(currentParam.name, GetMySQLDBType(currentParam.type)).Value = currentParam.value;
                            }
                        }
                    }

                    return DA.CreateDataTable_M(cmd, dencryptedConnection);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public DataTable WebAPI_GetDinamicDataLogin(string dencryptedSP, List<paramValues> paramValues, string dencryptedConnection, string User, string pass, string Type)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = dencryptedSP;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("PARAM", SqlDbType.VarChar).Value = Type;
                    cmd.Parameters.Add("USR", SqlDbType.VarChar).Value = User;
                    cmd.Parameters.Add("PASS", SqlDbType.VarChar).Value = pass;
                    return DA.CreateDataTable(cmd, dencryptedConnection);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private SqlDbType GetSQLDBType(string SQLDBTypeName)
        {
            switch (SQLDBTypeName)
            {
                case "varchar":
                    return SqlDbType.VarChar;
                case "nvarchar":
                    return SqlDbType.NVarChar;
                case "tinyint":
                    return SqlDbType.TinyInt;
                case "smallint":
                    return SqlDbType.SmallInt;
                case "int":
                    return SqlDbType.Int;
                case "bigint":
                    return SqlDbType.BigInt;
                case "decimal":
                    return SqlDbType.Decimal;
                case "bool":
                    return SqlDbType.Bit;
                case "bit":
                    return SqlDbType.Bit;
                case "datetime":
                    return SqlDbType.DateTime;
                case "datetime2":
                    return SqlDbType.DateTime2;
                case "money":
                    return SqlDbType.Money;
                case "smallmoney":
                    return SqlDbType.SmallMoney;
                case "time":
                    return SqlDbType.Time;
                case "varbinary":
                    return SqlDbType.VarBinary;
                default:
                    return SqlDbType.VarChar;
            }
        }
        private OracleType GetORACLEDBType(string SQLDBTypeName)
        {
            switch (SQLDBTypeName)
            {
                case "varchar":
                    return OracleType.VarChar;
                case "varchar2":
                    return OracleType.VarChar;
                case "nvarchar":
                    return OracleType.NVarChar;
                case "Int16":
                    return OracleType.Int16;
                case "Int32":
                    return OracleType.Int32;
                case "decimal":
                    return OracleType.Double;
                case "bit":
                    return OracleType.Byte;
                case "datetime":
                    return OracleType.DateTime;
                case "time":
                    return OracleType.Timestamp;
                case "LongVarChar":
                    return OracleType.LongVarChar;
                case "CURSOR":
                    return OracleType.Cursor;
                case "RowId":
                    return OracleType.RowId;
                default:
                    return OracleType.VarChar;
            }
        }
        private MySqlDbType GetMySQLDBType(string SQLDBTypeName)
        {
            switch (SQLDBTypeName)
            {
                case "varchar":
                    return MySqlDbType.VarChar;
                case "nvarchar":
                    return MySqlDbType.VarString;
                case "tinyint":
                    return MySqlDbType.Byte;
                case "smallint":
                    return MySqlDbType.Int16;
                case "int":
                    return MySqlDbType.Int32;
                case "bigint":
                    return MySqlDbType.Int64;
                case "decimal":
                    return MySqlDbType.Decimal;
                case "bool":
                    return MySqlDbType.Bit;
                case "bit":
                    return MySqlDbType.Bit;
                case "datetime":
                    return MySqlDbType.DateTime;
                case "datetime2":
                    return MySqlDbType.DateTime;
                case "money":
                    return MySqlDbType.Decimal;
                case "smallmoney":
                    return MySqlDbType.Decimal;
                case "time":
                    return MySqlDbType.Time;
                case "varbinary":
                    return MySqlDbType.VarBinary;
                default:
                    return MySqlDbType.VarChar;
            }
        }
    }
}