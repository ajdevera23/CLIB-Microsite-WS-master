using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Data.SqlClient;
using System.Configuration;
/// <summary>
/// Summary description for Upload
/// </summary>
public class Upload
{
    AuditTrail auditTrail = new AuditTrail();
    public Upload()
    {
        //
        // TODO: Add constructor logic here
        //
        
    }

    public BaseResult UploadExcel(TokenRequest token)
    {
        DataTable dt = ConvertListToDataTable(token.UploadCollection);
        BaseResult success;
        BaseResult resultValue = new BaseResult();
                
        int startCountEntries = CountEntriesInTblUpload();
        success = SaveToTable(dt);    
        int endCountEntries = CountEntriesInTblUpload();
        int totalEntriesAdded = endCountEntries - startCountEntries;

        if (success.ResultStatus == ResultType.Success)
        {
            resultValue.ResultStatus = ResultType.Success;

            if (totalEntriesAdded > 0)
            {
                resultValue.Message = "Successfully uploaded " + totalEntriesAdded + " entries to database.";
            }
            else
            {
                resultValue.Message = "No entries added to database.";
            }

            #region Audit Trail
            auditTrail.IpAddress = token.IpAddress;
            auditTrail.ActionTaken = "Upload to tbl_Upload";
            auditTrail.ActionDetails = "CLIBMicrositeWS: " + resultValue.Message;
            auditTrail.InsertAuditTrailEntry();
            #endregion

        }
        else
        {
            resultValue.ResultStatus = ResultType.Failed;
            resultValue.Message = success.Message;
        }
       
        return resultValue;
    }

    static DataTable ConvertListToDataTable(List<UploadCollection> list)
    {
        // New table.
        DataTable table = new DataTable(typeof(UploadCollection).Name);

        PropertyInfo[] Props = typeof(UploadCollection).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (PropertyInfo prop in Props)
        {
            //Defining type of data column gives proper data table 
            var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
            //Setting column names as Property names
            table.Columns.Add(prop.Name, type);
        }

        foreach (UploadCollection item in list)
        {
            var values = new object[Props.Length];
            for(int i = 0; i< Props.Length;i++)
            {
                values[i] = Props[i].GetValue(item, null);
            }
            table.Rows.Add(values);
        }
        return table;
    }

    private int CountEntriesInTblUpload()
    {
        int result = 0;

        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            string script = "SELECT COUNT([fld_ReferenceNumber]) FROM [db_MicroInsurance].[Transaction].[tbl_Upload]";
            using (SqlCommand cmd = new SqlCommand(script, conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                //cmd.Parameters.AddWithValue("@referenceCode", SqlDbType.VarChar).Value = token.ReferenceNumber;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result = (reader.GetInt32(0));
                }
            }
        }
        return result;

    }

    

    private BaseResult SaveToTable(DataTable dt)
    {
        BaseResult resultValue = new BaseResult();
        string connString = ConfigurationManager.ConnectionStrings["dbMicroinsuranceConnStringWriter"].ToString();
        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open();
            //create temp tbl
            string script = "Updater.usp_CreateTempUploadTbl";
            using (SqlCommand cmd = new SqlCommand(script, conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                //copy dt to temp table
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                {
                    foreach (DataColumn column in dt.Columns)
                        bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                    string referenceNumber = dt.Rows[0].ToString();
                    bulkCopy.DestinationTableName = "##temp_tblUpload";
                    try
                    {
                        bulkCopy.WriteToServer(dt);
                        resultValue.ResultStatus = ResultType.Success;
                    }
                    catch (Exception ex)
                    {
                        SystemUtility.EventLog.SaveError(ex.ToString());
                        resultValue.ResultStatus = ResultType.Error;
                        resultValue.Message = ex.Message;

                    }
                }
                //save unique entries to tbl_upload
                string script2 = "Updater.usp_SaveToUploadTbl";
                using (SqlCommand cmd2 = new SqlCommand(script2, conn))
                {
                    cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd2.ExecuteNonQuery();
                }
            }
            

        }
        return resultValue;
    }

    
}