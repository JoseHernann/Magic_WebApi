using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BLL.Proyect.WebAPI_NGK
{
    public class DinamicData
    {
        public DataTable WebAPI_GetDinamicData(Entities.Request.WebAPI_NGK.DinamicData RequestObj)
        {
            try
            {
                string dencryptedSP = Encryption.DencryptData(RequestObj.encryptedSP);
                string dencryptedConnection = Encryption.DencryptData(RequestObj.encryptedConnection);
                var paramValues = RequestObj.paramValues;

                DAL.Projects.WebAPI.DinamicData DAL_DinamicData = new DAL.Projects.WebAPI.DinamicData();
                DataTable dt = DAL_DinamicData.WebAPI_GetDinamicData(dencryptedSP, paramValues, dencryptedConnection);


                return dt;
            }
            catch (Exception exe)
            {
                DataTable table = new DataTable();
                table.Columns.Add("HasError");
                table.Columns.Add("Error");
                table.Rows.Add(true, exe.Message);
                return table;
            }
        }


        public DataTable WebAPI_GetDinamicDataQRY(Entities.Request.WebAPI_NGK.DinamicData RequestObj)
        {
            try
            {
                string dataString = Encryption.DencryptData(RequestObj.dataString);
                string dencryptedConnection = Encryption.DencryptData(RequestObj.encryptedConnection);
                var paramValues = RequestObj.paramValues;
                DAL.Projects.WebAPI.DinamicData DAL_DinamicData = new DAL.Projects.WebAPI.DinamicData();
                DataTable dt = DAL_DinamicData.WebAPI_GetDinamicData_Select(dataString, dencryptedConnection, paramValues);
                return dt;
            }
            catch (Exception exe)
            {
                DataTable table = new DataTable();
                table.Columns.Add("HasError");
                table.Columns.Add("Error");
                table.Rows.Add(true, exe.Message);
                return table;
            }
        }

        [Obsolete]
        public DataTable WebAPI_GetDinamicData_O(Entities.Request.WebAPI_NGK.DinamicData RequestObj)
        {
            try
            {
                string dencryptedSP = Encryption.DencryptData(RequestObj.encryptedSP);
                string dencryptedConnection = Encryption.DencryptData(RequestObj.encryptedConnection);
                var paramValues = RequestObj.paramValues;

                DAL.Projects.WebAPI.DinamicData DAL_DinamicData = new DAL.Projects.WebAPI.DinamicData();
                DataTable dt = DAL_DinamicData.WebAPI_GetDinamicData_O(dencryptedSP, paramValues, dencryptedConnection);



                return dt;
            }
            catch (Exception exe)
            {
                DataTable table = new DataTable();
                table.Columns.Add("HasError");
                table.Columns.Add("Error");
                table.Rows.Add(true, exe.Message);
                return table;
            }
        }

        [Obsolete]
        public DataTable WebAPI_GetDinamicDataQRY_O(Entities.Request.WebAPI_NGK.DinamicData RequestObj)
        {
            try
            {
                string dataString = Encryption.DencryptData(RequestObj.dataString);
                string dencryptedConnection = Encryption.DencryptData(RequestObj.encryptedConnection);
                var paramValues = RequestObj.paramValues;

                DAL.Projects.WebAPI.DinamicData DAL_DinamicData = new DAL.Projects.WebAPI.DinamicData();
                DataTable dt = DAL_DinamicData.WebAPI_GetDinamicData_Select_O(dataString, dencryptedConnection, paramValues);


                return dt;
            }
            catch (Exception exe)
            {
                DataTable table = new DataTable();
                table.Columns.Add("HasError");
                table.Columns.Add("Error");
                table.Rows.Add(true, exe.Message);
                return table;
            }
        }
        public DataTable WebAPI_GetDinamicData_M(Entities.Request.WebAPI_NGK.DinamicData RequestObj)
        {
            try
            {
                string dencryptedSP = Encryption.DencryptData(RequestObj.encryptedSP);
                string dencryptedConnection = Encryption.DencryptData(RequestObj.encryptedConnection);
                var paramValues = RequestObj.paramValues;

                DAL.Projects.WebAPI.DinamicData DAL_DinamicData = new DAL.Projects.WebAPI.DinamicData();
                DataTable dt = DAL_DinamicData.WebAPI_GetDinamicData_M(dencryptedSP, paramValues, dencryptedConnection);


                return dt;
            }
            catch (Exception exe)
            {
                DataTable table = new DataTable();
                table.Columns.Add("HasError");
                table.Columns.Add("Error");
                table.Rows.Add(true, exe.Message);
                return table;
            }
        }

        public DataTable WebAPI_GetDinamicData_Login(Entities.Request.WebAPI_NGK.DinamicData RequestObj)
        {
            try
            {
                string dencryptedSP = Encryption.DencryptData(RequestObj.encryptedSP);
                string dencryptedConnection = Encryption.DencryptData(RequestObj.encryptedConnection);

                string sUsr = RequestObj.User;
                string sPass = RequestObj.Pass;
                var paramValues = RequestObj.paramValues;

                DAL.Projects.WebAPI.DinamicData DAL_DinamicData = new DAL.Projects.WebAPI.DinamicData();
                DataTable dt = DAL_DinamicData.WebAPI_GetDinamicDataLogin(dencryptedSP, paramValues, dencryptedConnection, sUsr, sPass, "N");
                return dt;

            }
            catch (Exception exe)
            {
                DataTable table = new DataTable();
                table.Columns.Add("HasError");
                table.Columns.Add("Error");
                table.Rows.Add(true, exe.Message);
                return table;
            }
        }


        public DataTable WebAPI_GetDinamicData_Login_V(Entities.Request.WebAPI_NGK.DinamicData RequestObj)
        {
            try
            {
                string dencryptedSP = Encryption.DencryptData(RequestObj.encryptedSP);
                string dencryptedConnection = Encryption.DencryptData(RequestObj.encryptedConnection);

                string sUsr = RequestObj.User;
                string sPass = RequestObj.Pass;
                var paramValues = RequestObj.paramValues;

                DataTable table = new DataTable();
                table.Columns.Add("User");
                table.Columns.Add("Validate");
                table.Rows.Add(sUsr, "0");
                return table;

            }
            catch (Exception exe)
            {
                DataTable table = new DataTable();
                table.Columns.Add("HasError");
                table.Columns.Add("Error");
                table.Rows.Add(true, exe.Message);
                return table;
            }
        }

        public List<Dictionary<string, string>> GetDataTableDictionaryList(DataTable dt)
        {
            return dt.AsEnumerable().Select(
                row => dt.Columns.Cast<DataColumn>().ToDictionary(
                    column => column.ColumnName,
                    column => row[column].ToString().Trim()
                )).ToList();
        }

    }
}