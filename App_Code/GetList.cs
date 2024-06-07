using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for GetList
/// </summary>
/// 

//Comment from ryan
public class GetList
{
    AuditTrail auditTrail = new AuditTrail();
    public IList<String> GetPartnerList()
    {
        var partnerList = new List<String>();
        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;
        var selectSQL = "SELECT fld_PartnerCode FROM Reference.tbl_Partner";
        using (SqlConnection conn = new SqlConnection(connString))//@"Data Source=LPD-0012\SQLEXPRESS; Initial Catalog=db_NAV24K; Integrated Security=True;"))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            using (SqlCommand cmd = new SqlCommand(selectSQL, conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //ListItem category = new ListItem();
                    string partnerCode;
                    partnerCode = reader["fld_PartnerCode"].ToString();
                    partnerList.Add(partnerCode);
                }
            }
            
        }
        return partnerList;
    }

    public IList<String> GetProductList()
    {
        var productList = new List<String>();

        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;
        var selectSQL = "SELECT fld_ProductCode FROM Reference.tbl_Products";
        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            using (SqlCommand cmd = new SqlCommand(selectSQL, conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    string product;
                    
                    product = reader["fld_ProductCode"].ToString();
                    //product.IconPath= reader["fld_IconPath"].ToString();
                    productList.Add(product);
                    
                }
                
            }
        }
        return productList;
    }

    public bool ifReferenceCodeExists(string referenceCode)
    {
        bool isExist = false;
        
        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;
        
        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            using (SqlCommand cmd = new SqlCommand("Reader.usp_ReferenceCodeIfExists", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@referenceCode", SqlDbType.VarChar).Value = referenceCode;
                
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    isExist = (reader.GetBoolean(0));
                }
            }
        }
        return isExist;
    }

    public bool ifReferenceNumberExists(TokenRequest token)
    {
        bool isExist = false;

        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            using (SqlCommand cmd = new SqlCommand("Reader.usp_ReferenceNumberIfExists", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@referenceNumber", SqlDbType.VarChar).Value = token.ReferenceNumber;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    isExist = (reader.GetBoolean(0));
                }
            }
        }
        return isExist;
    }

    public bool ifReferenceCodeIsUsed(TokenRequest token)
    {
        bool returnVal = false;

        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            using (SqlCommand cmd = new SqlCommand("Reader.usp_ReferenceCodeIsUsed", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@referenceCode", SqlDbType.VarChar).Value = token.ReferenceCode;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    returnVal = (reader.GetBoolean(0));
                }
            }
        }
        return returnVal;
    }


    public bool ifProductCodeBaseOnIntegrationMappingExists(TokenRequest token)
    {
        bool returnVal = false;

        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            using (SqlCommand cmd = new SqlCommand("Reader.usp_GetProductCodeWithIntegrationMapping", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PlatformName", SqlDbType.VarChar).Value = token.PlatformName;
                cmd.Parameters.AddWithValue("@PartnerCode", SqlDbType.VarChar).Value = token.PartnerCode;
                cmd.Parameters.AddWithValue("@ProductCode", SqlDbType.VarChar).Value = token.ProductCode;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    returnVal = (reader.GetBoolean(0));
                }
            }
        }
        return returnVal;
    }

    public IList<String> GetListRelationship()
    {
        var relationshipList = new List<String>();
        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;
        var selectSQL = "select [fld_Id], [fld_Relationship] from [Reference].[tbl_Relationship]";
        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            using (SqlCommand cmd = new SqlCommand(selectSQL, conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                relationshipList.Add("Select");
                while (reader.Read())
                {
                    
                    string relationship;
                    string id;
                    relationship = reader["fld_Relationship"].ToString();
                    id = Convert.ToString(reader["fld_Id"]);
                    relationshipList.Add(relationship);
                }
            }


        }
        return relationshipList;
    }

    public IList<String> GetListValidIds()
    {
        var validIdList = new List<String>();
        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;
        var selectSQL = "select [fld_Id], [fld_ValidId] from [Reference].[tbl_ValidId]";
        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            using (SqlCommand cmd = new SqlCommand(selectSQL, conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                validIdList.Add("Select");
                while (reader.Read())
                {
                    //ListItem category = new ListItem();
                    string validId;
                    string id;
                    validId = reader["fld_ValidId"].ToString();
                    id = Convert.ToString(reader["fld_Id"]);
                    validIdList.Add(validId);
                }
            }


        }
        return validIdList;
    }

    public Int64 GetIntegrationId(string productCode,string partnerCode)
    {
        Int64 integrationId=0;
        
        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;
        var selectSQL = "Reader.usp_GetListIntegrationId";
        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            using (SqlCommand cmd = new SqlCommand(selectSQL, conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productCode", SqlDbType.VarChar).Value = productCode;
                cmd.Parameters.AddWithValue("@partnerCode", SqlDbType.VarChar).Value = partnerCode;
                SqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    
                    integrationId = Convert.ToInt64(reader["fld_IntegrationId"]);
                    
                }
            }


        }

        return integrationId;
    }

    public string GetProductImagePath(string productCode)
    {
        string productImagePath="";

        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;
        var selectSQL = "Reader.usp_GetProductIconPath";
        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            using (SqlCommand cmd = new SqlCommand(selectSQL, conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productCode", SqlDbType.VarChar).Value = productCode;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    productImagePath = reader["fld_IconPath"].ToString();

                }

            }
        }
        return productImagePath;
    }
    public string GetPartnerImagePath(TokenRequest token)
    {
        string returnValue = "";

        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;
        var selectSQL = "Reader.usp_GetPartnerIconPath";
        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            using (SqlCommand cmd = new SqlCommand(selectSQL, conn))    
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@partnerCode", SqlDbType.VarChar).Value = token.PartnerCode;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    returnValue = reader["fld_IconPath"].ToString();
                }

            }
        }
        return returnValue;
    }

    public string[] GetProductDescription(string productCode)
    {
        string[] returnValue = new string[2];

        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;
        var selectSQL = "Reader.usp_GetProductDescription";
        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            using (SqlCommand cmd = new SqlCommand(selectSQL, conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productCode", SqlDbType.VarChar).Value = productCode;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    
                    returnValue[0] = reader["fld_ProductDescription"].ToString();
                    returnValue[1] = reader["fld_ProductName"].ToString();
                }

            }
        }
        return returnValue;
    }

    public IList<CategoryResult> PopulateProductCategoryGridView(TokenRequest token)
        {
        string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;
        var returnValue = new List<CategoryResult>();

        //var selectSQL = "SELECT DISTINCT fld_CategoryName, fld_CategoryCode, fld_IconPath FROM [db_MicroInsurance].[Reference].[tbl_Category][Prod]";//"SELECT DISTINCT Cat.fld_CategoryName, Cat.fld_CategoryCode, Cat.fld_IconPath FROM [db_MicroInsurance].[Reference].[tbl_Products][Prod]"; //LEFT JOIN [db_MicroInsurance].[Reference].[tbl_Category][Cat] ON [Cat].[fld_CategoryCode]=[Prod].[fld_CategoryCode]";// WHERE [Prod].[fld_ProductCode] IN('CLER5', 'CLER1', 'CLER2', 'CLHDP', 'CLPAB', 'CLPRM')
        //var selectSQL = "SELECT DISTINCT b.fld_CategoryId ,a.fld_CategoryName ,a.fld_DisplayName ,a.fld_CategoryCode ,a.fld_COCorPC ,a.fld_Remarks ,a.fld_IconPath ,a.fld_CreatedBy ,a.fld_DateCreated ,a.fld_ModifiedBy ,a.fld_DateModified ,a.fld_DeletedBy ,a.fld_DateDeleted ,a.fld_IsDeleted FROM [Reference].[tbl_Category] a INNER JOIN [Reference].[tbl_IntegrationMapping] b on b.fld_CategoryId = a.fld_CategoryId INNER JOIN [Reference].[tbl_Platform] tp on  tp.fld_PlatformId =b.fld_PlatformId INNER JOIN [Reference].[tbl_Partner] tr on tr.fld_PartnerId = b.fld_PartnerId where  tp.fld_PlatformId = '9' and tr.fld_PartnerId= '42'";//"SELECT DISTINCT Cat.fld_CategoryName, Cat.fld_CategoryCode, Cat.fld_IconPath FROM [db_MicroInsurance].[Reference].[tbl_Products][Prod]"; //LEFT JOIN [db_MicroInsurance].[Reference].[tbl_Category][Cat] ON [Cat].[fld_CategoryCode]=[Prod].[fld_CategoryCode]";// WHERE [Prod].[fld_ProductCode] IN('CLER5', 'CLER1', 'CLER2', 'CLHDP', 'CLPAB', 'CLPRM')

        var selectSQL = "Reader.usp_GetProductCategoryBaseOnIntegrationMapping";
        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(selectSQL, conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PlatformName", SqlDbType.VarChar).Value = token.PlatformName;
                cmd.Parameters.AddWithValue("@PartnerCode", SqlDbType.VarChar).Value = token.ProductCode;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())   
                {
                    CategoryResult value = new CategoryResult();
                    value.IconPath = reader["fld_IconPath"].ToString();
                    value.CategoryCode = reader["fld_CategoryCode"].ToString();
                    value.CategoryName = reader["fld_CategoryName"].ToString();
                    returnValue.Add(value);
                }

            }
        }
        return returnValue;
    }
    public IList<ProductList> PopulateProductGridView(TokenRequest token)
    {

        string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;
        var returnValue = new List<ProductList>();
        
        // var selectSQL = "SELECT fld_ProductCode,fld_IconPath,fld_ProductDescription FROM Reference.tbl_Products WHERE [fld_CategoryCode] = '"+ token.CategoryCode + "'";
        //var selectSQL = "SELECT DISTINCT b.fld_ProductId ,b.fld_IntegrationId ,p.fld_ProviderCode ,a.fld_ProductName ,a.fld_ProductCode ,a.fld_IconPath ,a.fld_ProductDescription ,b.fld_SRP FROM [Reference].[tbl_Products] a INNER JOIN [Reference].[tbl_IntegrationMapping] b INNER JOIN [Reference].[tbl_Platform] tp on  tp.fld_PlatformId =b.fld_PlatformId  INNER JOIN [Reference].[tbl_Partner] tr on tr.fld_PartnerId = b.fld_PartnerId on b.fld_ProductId = a.fld_ProductId INNER JOIN [Reference].[tbl_Provider] p on p.fld_ProviderId = b.fld_ProviderId WHERE a.[fld_CategoryCode] = '" + token.CategoryCode + "' and tp.fld_PlatformName = '" + token.PlatformName + "' and tr.fld_PartnerCode= '" + token.PartnerCode + "' order by b.fld_ProductId";

        var selectSQL = "Reader.usp_GetProductGridView";

        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(selectSQL, conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoryCode", SqlDbType.VarChar).Value = token.CategoryCode;
                cmd.Parameters.AddWithValue("@PlatformName", SqlDbType.VarChar).Value = token.PlatformName;
                cmd.Parameters.AddWithValue("@PartnerCode", SqlDbType.VarChar).Value = token.PartnerCode;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ProductList value = new ProductList();
                    value.IntegrrationId = reader["fld_IntegrationId"].ToString();
                    value.ProviderCode = reader["fld_ProviderCode"].ToString();
                    value.ProductName = reader["fld_ProductName"].ToString();
                    value.ProductCode= reader["fld_ProductCode"].ToString();
                    value.IconPath = reader["fld_IconPath"].ToString();
                    value.ProductDescription = reader["fld_ProductDescription"].ToString();
                    value.SRP = (decimal)reader["fld_SRP"];
                    returnValue.Add(value);
                }

            }
        }
            return returnValue;
    }


    public IList<ProductList> PopulateProductDetails(TokenRequest token)
    {

        string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;
        var returnValue = new List<ProductList>();
        // var selectSQL = "SELECT fld_ProductCode,fld_IconPath,fld_ProductDescription FROM Reference.tbl_Products WHERE [fld_CategoryCode] = '"+ token.CategoryCode + "'";
        //var selectSQL = "SELECT DISTINCT b.fld_ProductId ,b.fld_IntegrationId ,p.fld_ProviderCode ,a.fld_ProductName ,a.fld_ProductCode ,a.fld_IconPath ,a.fld_ProductDescription ,b.fld_SRP ,a.fld_COCEffectiveDateBasis ,a.fld_CoverageDurationInDays FROM [Reference].[tbl_Products] a INNER JOIN [Reference].[tbl_IntegrationMapping] b INNER JOIN [Reference].[tbl_Platform] tp on  tp.fld_PlatformId =b.fld_PlatformId  INNER JOIN [Reference].[tbl_Partner] tr on tr.fld_PartnerId = b.fld_PartnerId on b.fld_ProductId = a.fld_ProductId INNER JOIN [Reference].[tbl_Provider] p on p.fld_ProviderId = b.fld_ProviderId WHERE p.[fld_ProviderCode] = '" + token.CategoryCode + "' and tp.fld_PlatformName = '" + token.PlatformName + "' and tr.fld_PartnerCode= '" + token.PartnerCode + "' and a.fld_ProductCode= '" + token.ProductCode + "' order by b.fld_ProductId";

        var selectSQL = "Reader.usp_GetProductDetailsBaseOnIntegrationMapping";

        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(selectSQL, conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProviderCode", SqlDbType.VarChar).Value = token.CategoryCode;
                cmd.Parameters.AddWithValue("@PlatformName", SqlDbType.VarChar).Value = token.PlatformName;
                cmd.Parameters.AddWithValue("@PartnerCode", SqlDbType.VarChar).Value = token.PartnerCode;
                cmd.Parameters.AddWithValue("@ProductCode", SqlDbType.VarChar).Value = token.ProductCode;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ProductList value = new ProductList();
                    value.IntegrrationId = reader["fld_IntegrationId"].ToString();
                    value.ProviderCode = reader["fld_ProviderCode"].ToString();
                    value.ProductName = reader["fld_ProductName"].ToString();
                    value.ProductCode = reader["fld_ProductCode"].ToString();
                    value.IconPath = reader["fld_IconPath"].ToString();
                    value.ProductDescription = reader["fld_ProductDescription"].ToString();
                    value.SRP = (decimal)reader["fld_SRP"];
                    value.COCEffectiveDateBasis = reader["fld_COCEffectiveDateBasis"].ToString();
                    value.CoverageDurationInDays = reader["fld_CoverageDurationInDays"].ToString();
                    value.CategoryId = (Int64)reader["fld_CategoryId"];
                    value.ProductId = (Int64)reader["fld_ProductId"];
                    value.ProviderId = (Int64)reader["fld_ProviderId"];
                    value.PartnerId = (Int64)reader["fld_PartnerId"];
                    value.PlatformId = (Int64)reader["fld_PlatformId"];

                    returnValue.Add(value);
                }

            }
        }
        return returnValue;
    }


    public IList<ProductList> PopulateProductByCodesAndIntegrationID(TokenRequest token)
    {

        string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;
        var returnValue = new List<ProductList>();

        var selectSQL = "Reader.usp_GetProductByCodesAndIntegrationID";

        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(selectSQL, conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PlatformName", SqlDbType.VarChar).Value = token.PlatformName;
                cmd.Parameters.AddWithValue("@PartnerCode", SqlDbType.VarChar).Value = token.PartnerCode;
                cmd.Parameters.AddWithValue("@ProductCode", SqlDbType.VarChar).Value = token.ProductCode;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ProductList value = new ProductList();
                    value.PartnerCode = reader["fld_PartnerCode"].ToString();
                    value.ProductCode = reader["fld_ProductCode"].ToString();
                    value.IntegrrationId = reader["fld_IntegrationId"].ToString();
                    value.CategoryCode = reader["fld_CategoryCode"].ToString();
                    value.ProviderCode = reader["fld_ProviderCode"].ToString();
                    value.ProductName = reader["fld_ProductName"].ToString();
                    returnValue.Add(value);
                }

            }
        }
        return returnValue;
        }



    public string GetCategory(TokenRequest token)
    {
        string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;

        string returnValue="";

        var selectSQL = "SELECT fld_CategoryCode FROM Reference.tbl_Products WHERE [fld_ProductCode] = '" + token.ProductCode + "'";
        
        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(selectSQL, conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    
                    returnValue = reader["fld_CategoryCode"].ToString();
                    
                }

            }
        }
        return returnValue;
    }

    #region Client Referral

    public bool CheckIfBranchExists(TokenRequest token)
    {
        bool isExist = false;

        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbBDS_MicroinsuranceConnStringReader"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            using (SqlCommand cmd = new SqlCommand("Reader.usp_CheckIfBranchExists", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchCode", SqlDbType.VarChar).Value = token.BranchCode;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    isExist = (reader.GetBoolean(0));

                }
            }
            return isExist;
        }
    }
    public TokenRequest RetrieveBranchDetails(TokenRequest token)
    {
        try
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbBDS_MicroinsuranceConnStringReader"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
                using (SqlCommand cmd = new SqlCommand("Reader.usp_RetrieveBranchDetails", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    TokenRequest tokenValues = new TokenRequest();
                    cmd.Parameters.AddWithValue("@BranchCode", SqlDbType.VarChar).Value = token.BranchCode;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        tokenValues.BranchCode = reader["fld_BranchCode"].ToString();
                        tokenValues.BranchName = reader["fld_BranchName"].ToString();
                        tokenValues.AreaCode = reader["fld_AreaCode"].ToString();
                        tokenValues.Region = reader["fld_RegionCode"].ToString();
                    }
                    reader.Close();
                    return tokenValues;
                }
            }

        }
        catch (Exception error)
        {
            throw error;
        }
    }

    public IList<String> GetListProvince()
    {
        var provList = new List<String>();
        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;
        var selectSQL = "select distinct [fld_Province] from [db_MicroInsurance].[Reference].[tbl_Location] order by fld_Province ASC";

        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            using (SqlCommand cmd = new SqlCommand(selectSQL, conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                provList.Add("Select");
                while (reader.Read())
                {
                    //ListItem category = new ListItem();
                    string province;

                    province = reader["fld_Province"].ToString();

                    provList.Add(province);
                }
            }


        } 
        return provList;
    }

    public IList<String> GetListCity(TokenRequest token)
    {



        var cityList = new List<String>();
        string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;
        var selectSQL = "Select fld_City from [db_MicroInsurance].[Reference].[tbl_Location] where [fld_Province] = '" + token.Province + "' order by fld_City asc";

        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(selectSQL, conn))
            {
             
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                cityList.Add("Select");
                while (reader.Read())
                {
                    
                    //ListItem category = new ListItem();
                    string city;
                    city = reader["fld_City"].ToString();  

                    byte[] bytes = System.Text.Encoding.GetEncoding(28592).GetBytes(city);                 
                    string outputText = System.Text.Encoding.ASCII.GetString(bytes);

                

                    cityList.Add(outputText);
                }
            }
        }
        return cityList;
    }

    public bool CheckIfClientExistsIQR(TokenRequest token)
    {
        bool isExist = false;

        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;
                
        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            using (SqlCommand cmd = new SqlCommand("Reader.usp_CheckIfClientExistsIQR", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@firstName", SqlDbType.VarChar).Value = token.FirstName;
                cmd.Parameters.AddWithValue("@lastName", SqlDbType.VarChar).Value = token.LastName;
                cmd.Parameters.AddWithValue("@birthDate", SqlDbType.DateTime).Value = Convert.ToDateTime(token.DOB);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    isExist = (reader.GetBoolean(0));

                }
            }
            return isExist;
        }
    }

    public bool CheckIfADCClientExists(TokenRequest token)
    {
        bool isExist = false;

        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            using (SqlCommand cmd = new SqlCommand("Reader.usp_CheckIfADCClientExists", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@firstName", SqlDbType.VarChar).Value = token.FirstName;
                cmd.Parameters.AddWithValue("@lastName", SqlDbType.VarChar).Value = token.LastName;
                cmd.Parameters.AddWithValue("@birthDate", SqlDbType.DateTime).Value = Convert.ToDateTime(token.DOB);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    isExist = (reader.GetBoolean(0));

                }
            }
            return isExist;
        }
    }
    public TokenRequest RetrieveDetailsPerADCClient(TokenRequest token)
    {
        try
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
                using (SqlCommand cmd = new SqlCommand("Reader.usp_RetrieveDetailsPerADCClient", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    TokenRequest tokenValues = new TokenRequest();
                    cmd.Parameters.AddWithValue("@firstName", SqlDbType.VarChar).Value = token.FirstName;
                    cmd.Parameters.AddWithValue("@lastName", SqlDbType.VarChar).Value = token.LastName;
                    cmd.Parameters.AddWithValue("@birthDate", SqlDbType.SmallDateTime).Value = Convert.ToDateTime(token.DOB);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        tokenValues.ClientID = reader["fld_ADCClientId"].ToString();
                        tokenValues.FirstName = reader["fld_ADCFirstName"].ToString();
                        tokenValues.MiddleName = reader["fld_ADCMiddleName"].ToString();
                        tokenValues.LastName = reader["fld_ADCLastName"].ToString();
                        tokenValues.Suffix = reader["fld_ADCSuffix"].ToString();
                        tokenValues.DOB = reader["fld_ADCDateOfBirth"].ToString();
                        tokenValues.Address = reader["fld_Address"].ToString();
                        tokenValues.City = reader["fld_ADCCity"].ToString();
                        tokenValues.Province = reader["fld_ADCProvince"].ToString();
                        tokenValues.ZipCode = reader["fld_ADCZipCode"].ToString();
                        tokenValues.Email = reader["fld_ADCEmailAddress"].ToString();
                        tokenValues.ContactNumber = reader["fld_ADCMobileNumber"].ToString();
                        tokenValues.NatureofWork = reader["fld_ADCNatureOfWork"].ToString();
                        tokenValues.Employer = reader["fld_ADCEmployer"].ToString();
                        tokenValues.ValidID = reader["fld_ADCValidIDPresented"].ToString();
                        tokenValues.ValidIDNum = reader["fld_ADCValidIDNumber"].ToString();
                        tokenValues.Photo = reader["fld_IDPhoto"].ToString();
                        tokenValues.Interests = reader["fld_Interests"].ToString();
                        tokenValues.Appointments = reader["fld_Appointment"].ToString();
                        tokenValues.GroupName = reader["fld_GroupName"].ToString();
                        tokenValues.GroupContactPerson = reader["fld_GroupContactPerson"].ToString();
                        tokenValues.ClientType = reader["fld_ClientType"].ToString();
                    }
                    reader.Close();
                    return tokenValues;
                }
            }

        }
        catch (Exception error)
        {
            throw error;
        }
    }
    public bool CheckIfADCGroupClientExists(TokenRequest token)
    {
        bool isExist = false;

        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            using (SqlCommand cmd = new SqlCommand("Reader.usp_CheckIfADCGroupClientExists", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@groupName", SqlDbType.VarChar).Value = token.GroupName;
                cmd.Parameters.AddWithValue("@groupCP", SqlDbType.VarChar).Value = token.GroupContactPerson;
                cmd.Parameters.AddWithValue("@birthDate", SqlDbType.DateTime).Value = Convert.ToDateTime(token.DOB);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    isExist = (reader.GetBoolean(0));

                }
            }
            return isExist;
        }
    }
    public bool ClientReferralAgingValidation(TokenRequest token)
    {
        bool isExist = false;

        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            using (SqlCommand cmd = new SqlCommand("Reader.usp_ClientReferralAgingValidation", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClientID", SqlDbType.Int).Value = token.ClientID;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    isExist = (reader.GetBoolean(0));
                }
            }
            return isExist;
        }
    }
    public void ClientReferralIndividualTran(TokenRequest token)
    {
        BaseResult resultValue = new BaseResult();
        try
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroinsuranceConnStringWriter"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
                //TokenRequest tokenValues = new TokenRequest();
                using (SqlCommand cmd = new SqlCommand("Updater.usp_Tran_ClientReferralIndividual", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    byte[] img = Convert.FromBase64String(token.Photo);

                    cmd.Parameters.AddWithValue("@Type", SqlDbType.Int).Value = Convert.ToInt32(token.Type);
                    cmd.Parameters.AddWithValue("@ADCFirstName", SqlDbType.VarChar).Value = token.FirstName;
                    cmd.Parameters.AddWithValue("@ADCLastName", SqlDbType.VarChar).Value = token.LastName;
                    cmd.Parameters.AddWithValue("@ADCDateOfBirth", SqlDbType.DateTime).Value = Convert.ToDateTime(token.DOB);
                    cmd.Parameters.AddWithValue("@Address", SqlDbType.VarChar).Value = token.Address;
                    cmd.Parameters.AddWithValue("@ADCCity", SqlDbType.VarChar).Value = token.City;
                    cmd.Parameters.AddWithValue("@ADCProvince", SqlDbType.VarChar).Value = token.Province;
                    cmd.Parameters.AddWithValue("@ADCZipCode", SqlDbType.VarChar).Value = token.ZipCode;
                    cmd.Parameters.AddWithValue("@ADCEmailAddress", SqlDbType.VarChar).Value = token.Email;
                    cmd.Parameters.AddWithValue("@ADCMobileNumber", SqlDbType.VarChar).Value = token.ContactNumber;
                    cmd.Parameters.AddWithValue("@Region", SqlDbType.VarChar).Value = token.Region;
                    cmd.Parameters.AddWithValue("@AreaCode", SqlDbType.VarChar).Value = token.AreaCode;
                    cmd.Parameters.AddWithValue("@BranchCode", SqlDbType.VarChar).Value = token.BranchCode;
                    cmd.Parameters.AddWithValue("@BranchName", SqlDbType.VarChar).Value = token.BranchName;
                    cmd.Parameters.AddWithValue("@UserID", SqlDbType.VarChar).Value = token.AgentID;
                    cmd.Parameters.AddWithValue("@ADCPhoto", SqlDbType.VarBinary).Value = img;
                    cmd.ExecuteNonQuery();

                    #region Audit Trail
                    auditTrail.IpAddress = token.IpAddress;
                    auditTrail.ActionTaken = "Upload to tbl_ADCClient";
                    auditTrail.ActionDetails = "CLIBMicrositeWS: " + resultValue.Message;
                    auditTrail.InsertAuditTrailEntry();
                    #endregion
                }


            }

        }
        catch (Exception error)
        {
            throw error;
        }

    }
    public void ClientReferralTran(TokenRequest token)
    {
        BaseResult resultValue = new BaseResult();

        try
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroinsuranceConnStringWriter"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
                //TokenRequest tokenValues = new TokenRequest();
                using (SqlCommand cmd = new SqlCommand("Updater.usp_ClientReferralTran", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //byte[] img = Convert.FromBase64String(token.Photo);

                    cmd.Parameters.AddWithValue("@ADCClientID", SqlDbType.Int).Value = Convert.ToInt32(token.ClientID);
                    cmd.Parameters.AddWithValue("@RTNCode", SqlDbType.VarChar).Value = token.ReferenceCode;
                    cmd.Parameters.AddWithValue("@Interests", SqlDbType.VarChar).Value = token.Interests;
                    cmd.Parameters.AddWithValue("@Appointment", SqlDbType.VarChar).Value = token.Appointments;
                    cmd.Parameters.AddWithValue("@UserID", SqlDbType.VarChar).Value = token.BranchCode;
                   
                    //cmd.Parameters.AddWithValue("@ADCPhoto", SqlDbType.VarBinary).Value = img;
                    cmd.ExecuteNonQuery();

                    #region Audit Trail
                    auditTrail.IpAddress = token.IpAddress;
                    auditTrail.ActionTaken = "Upload to tbl_ClientReferral";
                    auditTrail.ActionDetails = "CLIBMicrositeWS: " + resultValue.Message;
                    auditTrail.InsertAuditTrailEntry();
                    #endregion
                }


            }

        }
        catch (Exception error)
        {
            throw error;
        }

    }
    public void ClientReferralGroupTran(TokenRequest token)
    {
        BaseResult resultValue = new BaseResult();

        try
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroinsuranceConnStringWriter"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
                //TokenRequest tokenValues = new TokenRequest();
                using (SqlCommand cmd = new SqlCommand("Updater.usp_Tran_ClientReferralGroup", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    byte[] img = Convert.FromBase64String(token.Photo);
                    

                    cmd.Parameters.AddWithValue("@Type", SqlDbType.Int).Value = Convert.ToInt32(token.Type);
                    cmd.Parameters.AddWithValue("@ADCFirstName", SqlDbType.VarChar).Value = token.FirstName;
                    cmd.Parameters.AddWithValue("@ADCLastName", SqlDbType.VarChar).Value = token.LastName;
                    cmd.Parameters.AddWithValue("@ADCDateOfBirth", SqlDbType.DateTime).Value = Convert.ToDateTime(token.DOB);
                    cmd.Parameters.AddWithValue("@Address", SqlDbType.VarChar).Value = token.Address;
                    cmd.Parameters.AddWithValue("@ADCCity", SqlDbType.VarChar).Value = token.City;
                    cmd.Parameters.AddWithValue("@ADCProvince", SqlDbType.VarChar).Value = token.Province;
                    cmd.Parameters.AddWithValue("@ADCZipCode", SqlDbType.VarChar).Value = token.ZipCode;
                    cmd.Parameters.AddWithValue("@ADCEmailAddress", SqlDbType.VarChar).Value = token.Email;
                    cmd.Parameters.AddWithValue("@ADCMobileNumber", SqlDbType.VarChar).Value = token.ContactNumber;
                    cmd.Parameters.AddWithValue("@Region", SqlDbType.VarChar).Value = token.Region;
                    cmd.Parameters.AddWithValue("@AreaCode", SqlDbType.VarChar).Value = token.AreaCode;
                    cmd.Parameters.AddWithValue("@BranchCode", SqlDbType.VarChar).Value = token.BranchCode;
                    cmd.Parameters.AddWithValue("@BranchName", SqlDbType.VarChar).Value = token.BranchName;
                    cmd.Parameters.AddWithValue("@UserID", SqlDbType.VarChar).Value = token.AgentID;
                    cmd.Parameters.AddWithValue("@ADCPhoto", SqlDbType.VarBinary).Value = img;
                    cmd.Parameters.AddWithValue("@GroupContactPerson", SqlDbType.VarChar).Value = token.GroupContactPerson;
                    cmd.Parameters.AddWithValue("@GroupName", SqlDbType.VarChar).Value = token.GroupName;

                    
                        
                    cmd.ExecuteNonQuery();

                    #region Audit Trail
                    auditTrail.IpAddress = token.IpAddress;
                    
                    auditTrail.ActionTaken = "Upload to tbl_ADCClient";
                    auditTrail.ActionDetails = "CLIBMicrositeWS: " + resultValue.Message;
                    auditTrail.InsertAuditTrailEntry();
                    #endregion
                }


            }

        }
        catch (Exception error)
        {
            throw error;
        }

    }

    #endregion

    #region  MicroBizPJr
    public IList<String> GetListGender(TokenRequest token)
    {
        var genderList = new List<String>();
        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;
        var selectSQL = "select distinct [fld_DisplayText] from [db_MicroInsurance].[Reference].[tbl_IMSSelectionList]  where fld_DefinitionID = 6 order by [fld_DisplayText] desc";

        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            using (SqlCommand cmd = new SqlCommand(selectSQL, conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                genderList.Add("Select");
                while (reader.Read())
                {
                    //ListItem category = new ListItem();
                    string gender;

                    gender = reader["fld_DisplayText"].ToString();

                    genderList.Add(gender);
                }
            }
        }
        return genderList;
    }

    public IList<String> GetListCivilStatus(TokenRequest token)
    {
        var civilStatList = new List<String>();
        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;
        var selectSQL = "select distinct [fld_DisplayText] from [db_MicroInsurance].[Reference].[tbl_IMSSelectionList] where fld_DefinitionID = 2 order by [fld_DisplayText] asc";

        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            using (SqlCommand cmd = new SqlCommand(selectSQL, conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                civilStatList.Add("Select");
                while (reader.Read())
                {
                    //ListItem category = new ListItem();
                    string gender;

                    gender = reader["fld_DisplayText"].ToString();

                    civilStatList.Add(gender);
                }
            }
        }
        return civilStatList;
    }

    public IList<String> GetListValidID(TokenRequest token)
    {
        var validIDList = new List<String>();
        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;
        var selectSQL = "select distinct [fld_DisplayText] from [db_MicroInsurance].[Reference].[tbl_IMSSelectionList] where fld_DefinitionID = 9 order by [fld_DisplayText] asc";

        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            using (SqlCommand cmd = new SqlCommand(selectSQL, conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                validIDList.Add("Select");
                while (reader.Read())
                {
                    //ListItem category = new ListItem();
                    string validid;

                    validid = reader["fld_DisplayText"].ToString();

                    validIDList.Add(validid);
                }
            }
        }
        return validIDList;
    }

    public IList<String> GetListRelation(TokenRequest token)
    {
        var relationList = new List<String>();
        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;
        var selectSQL = "select distinct [fld_DisplayText] from [db_MicroInsurance].[Reference].[tbl_IMSSelectionList] where fld_DefinitionID = 18 order by [fld_DisplayText] asc";

        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            using (SqlCommand cmd = new SqlCommand(selectSQL, conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                relationList.Add("Select");
                while (reader.Read())
                {
                    //ListItem category = new ListItem();
                    string rel;

                    rel = reader["fld_DisplayText"].ToString();

                    relationList.Add(rel);
                }
            }
        }
        return relationList;
    }

    public void TranMBPClient (TokenRequest token)
    {
        BaseResult resultValue = new BaseResult();

        try
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroinsuranceConnStringWriter"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
                
                using (SqlCommand cmd = new SqlCommand("Updater.usp_Tran_MBPClient", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@FirstName", SqlDbType.VarChar).Value = token.FirstName;
                    cmd.Parameters.AddWithValue("@LastName", SqlDbType.VarChar).Value = token.LastName;
                    cmd.Parameters.AddWithValue("@DateOfBirth", SqlDbType.DateTime).Value = Convert.ToDateTime(token.DOB);
                    cmd.Parameters.AddWithValue("@MiddleName", SqlDbType.VarChar).Value = token.MiddleName;
                    cmd.Parameters.AddWithValue("@Suffix", SqlDbType.VarChar).Value = token.Suffix;
                    cmd.Parameters.AddWithValue("@EmailAddress", SqlDbType.VarChar).Value = token.Email;
                    cmd.Parameters.AddWithValue("@MobileNumber", SqlDbType.VarChar).Value = token.ContactNumber;

                    cmd.ExecuteNonQuery();

                    #region Audit Trail
                    auditTrail.IpAddress = token.IpAddress;
                    auditTrail.ActionTaken = "Upload Initial Data to tbl_Client";
                    auditTrail.ActionDetails = "CLIBMicrositeWS: " + resultValue.Message;
                    auditTrail.InsertAuditTrailEntry();
                    #endregion
                }
            }

        }
        catch (Exception error)
        {
            throw error;
        }

    }
    public void MBPQuestionnaireTran(TokenRequest token)
    {
        BaseResult resultValue = new BaseResult();

        try
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroinsuranceConnStringWriter"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
                //TokenRequest tokenValues = new TokenRequest();
                using (SqlCommand cmd = new SqlCommand("Updater.usp_Tran_MBPQuestionnaire", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //byte[] img = Convert.FromBase64String(token.Photo);

                    cmd.Parameters.AddWithValue("@ClientID", SqlDbType.Int).Value = Convert.ToInt32(token.ClientID);
                    cmd.Parameters.AddWithValue("@RTNCode", SqlDbType.VarChar).Value = token.ReferenceCode;
                    cmd.Parameters.AddWithValue("@Q1", SqlDbType.VarChar).Value = token.Q1;
                    cmd.Parameters.AddWithValue("@Q2", SqlDbType.VarChar).Value = token.Q2;
                    cmd.Parameters.AddWithValue("@Q3", SqlDbType.VarChar).Value = token.Q3;
                    cmd.Parameters.AddWithValue("@Q4", SqlDbType.VarChar).Value = token.Q4;
                    cmd.Parameters.AddWithValue("@Q5", SqlDbType.VarChar).Value = token.Q5;
                    cmd.Parameters.AddWithValue("@ServicingUnit", SqlDbType.VarChar).Value = token.ServicingUnit; 


                    //cmd.Parameters.AddWithValue("@ADCPhoto", SqlDbType.VarBinary).Value = img;
                    cmd.ExecuteNonQuery();

                    #region Audit Trail
                    auditTrail.IpAddress = token.IpAddress;
                    auditTrail.ActionTaken = "Upload Questionnaires to tbl_Applications";
                    auditTrail.ActionDetails = "CLIBMicrositeWS: " + resultValue.Message;
                    auditTrail.InsertAuditTrailEntry();
                    #endregion
                }


            }

        }
        catch (Exception error)
        {
            throw error;
        }

    }

    public TokenRequest GetMBPClientID(TokenRequest token)
    {
        try
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
                using (SqlCommand cmd = new SqlCommand("Reader.usp_GetMBPClientID", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    TokenRequest tokenValues = new TokenRequest();
                    cmd.Parameters.AddWithValue("@firstName", SqlDbType.VarChar).Value = token.FirstName;
                    cmd.Parameters.AddWithValue("@lastName", SqlDbType.VarChar).Value = token.LastName;
                    cmd.Parameters.AddWithValue("@DOB", SqlDbType.SmallDateTime).Value = Convert.ToDateTime(token.DOB);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        tokenValues.ClientID = reader["fld_ClientId"].ToString();                       
                    }
                    reader.Close();
                    return tokenValues;
                }
            }

        }
        catch (Exception error)
        {
            throw error;
        }
    }

    public void MBPBusinessDetailsTran(TokenRequest token)
    {
        BaseResult resultValue = new BaseResult();

        try
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroinsuranceConnStringWriter"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
                //TokenRequest tokenValues = new TokenRequest();
                using (SqlCommand cmd = new SqlCommand("Updater.usp_Tran_MBPBusinessDetails", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //byte[] img = Convert.FromBase64String(token.Photo);

                    cmd.Parameters.AddWithValue("@ClientID", SqlDbType.Int).Value = Convert.ToInt32(token.ClientID);
                    cmd.Parameters.AddWithValue("@RTNCode", SqlDbType.VarChar).Value = token.ReferenceCode;
                    cmd.Parameters.AddWithValue("@InsuredAmount", SqlDbType.VarChar).Value = token.InsuredAmount;
                    cmd.Parameters.AddWithValue("@BusinessName", SqlDbType.VarChar).Value = token.BusinessName;
                    cmd.Parameters.AddWithValue("@BusinessType", SqlDbType.VarChar).Value = token.BusinessType;
                    cmd.Parameters.AddWithValue("@StartOfBusiness", SqlDbType.DateTime).Value = Convert.ToDateTime(token.StartOfBusiness);
                    
                    cmd.Parameters.AddWithValue("@BusinessAddress", SqlDbType.VarChar).Value = token.Address;
                    cmd.Parameters.AddWithValue("@BusinessProvince", SqlDbType.VarChar).Value = token.Province;
                    cmd.Parameters.AddWithValue("@BusinessCity", SqlDbType.VarChar).Value = token.City;
                    cmd.Parameters.AddWithValue("@BusinessZipCode", SqlDbType.VarChar).Value = token.ZipCode;
                    cmd.Parameters.AddWithValue("@PropertyOwnership", SqlDbType.VarChar).Value = token.PropOwner;
                    //cmd.Parameters.AddWithValue("@BusinessPhoto", SqlDbType.VarChar).Value = token.Photo;


                    //cmd.Parameters.AddWithValue("@ADCPhoto", SqlDbType.VarBinary).Value = img;
                    cmd.ExecuteNonQuery();

                    #region Audit Trail
                    auditTrail.IpAddress = token.IpAddress;
                    auditTrail.ActionTaken = "Upload Business Details to tbl_Applications";
                    auditTrail.ActionDetails = "CLIBMicrositeWS: " + resultValue.Message;
                    auditTrail.InsertAuditTrailEntry();
                    #endregion
                }


            }

        }
        catch (Exception error)
        {
            throw error;
        }

    }

    public void MBPInsertAttachments(TokenRequest token)
    {
        BaseResult resultValue = new BaseResult();

        try
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroinsuranceConnStringWriter"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
                //TokenRequest tokenValues = new TokenRequest();
                using (SqlCommand cmd = new SqlCommand("Updater.usp_Insert_MBPAttachments", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    byte[] img = Convert.FromBase64String(token.Photo);

                    cmd.Parameters.AddWithValue("@ClientID", SqlDbType.Int).Value = Convert.ToInt32(token.ClientID);
                    cmd.Parameters.AddWithValue("@RTNCode", SqlDbType.VarChar).Value = token.ReferenceCode;
                    cmd.Parameters.AddWithValue("@BusinessPhoto", SqlDbType.VarBinary).Value = img;
                    cmd.Parameters.AddWithValue("@Category", SqlDbType.VarChar).Value = token.AttachmentCategory;

                    
                    cmd.ExecuteNonQuery();

                    #region Audit Trail
                    auditTrail.IpAddress = token.IpAddress;
                    auditTrail.ActionTaken = "Upload Business Details to tbl_Applications";
                    auditTrail.ActionDetails = "CLIBMicrositeWS: " + resultValue.Message;
                    auditTrail.InsertAuditTrailEntry();
                    #endregion
                }


            }

        }
        catch (Exception error)
        {
            throw error;
        }

    }
    public void MBPAddtlBusOwnerDetails(TokenRequest token)
    {
        BaseResult resultValue = new BaseResult();

        try
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroinsuranceConnStringWriter"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
                //TokenRequest tokenValues = new TokenRequest();
                using (SqlCommand cmd = new SqlCommand("Updater.usp_Tran_MBPAddtlBusOwnerDetails", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@ClientID", SqlDbType.Int).Value = Convert.ToInt32(token.ClientID);                    
                    cmd.Parameters.AddWithValue("@Gender", SqlDbType.VarChar).Value = token.Gender;
                    cmd.Parameters.AddWithValue("@CivilStatus", SqlDbType.VarChar).Value = token.CivilStat;
                    cmd.Parameters.AddWithValue("@Address", SqlDbType.VarChar).Value = token.Address;
                    cmd.Parameters.AddWithValue("@Province", SqlDbType.VarChar).Value = token.Province;
                    cmd.Parameters.AddWithValue("@City", SqlDbType.VarChar).Value = token.City;
                    cmd.Parameters.AddWithValue("@ZipCode", SqlDbType.VarChar).Value = token.ZipCode;
                    cmd.Parameters.AddWithValue("@ValidID", SqlDbType.VarChar).Value = token.ValidID;
                    cmd.Parameters.AddWithValue("@ValidIDNumber", SqlDbType.VarChar).Value = token.ValidIDNum;
                    cmd.Parameters.AddWithValue("@Nationality", SqlDbType.VarChar).Value = token.Nationality;
                    cmd.Parameters.AddWithValue("@SourceOfFunds", SqlDbType.VarChar).Value = token.SourceOfFunds;
                    cmd.Parameters.AddWithValue("@NatureOfWork", SqlDbType.VarChar).Value = token.NatureofWork;
                    cmd.ExecuteNonQuery();

                    #region Audit Trail
                    auditTrail.IpAddress = token.IpAddress;
                    auditTrail.ActionTaken = "Update Owner Details to tbl_Clients";
                    auditTrail.ActionDetails = "CLIBMicrositeWS: " + resultValue.Message;
                    auditTrail.InsertAuditTrailEntry();
                    #endregion
                }


            }

        }
        catch (Exception error)
        {
            throw error;
        }

    }

    public bool CheckIfMBPClientExists(TokenRequest token)
    {
        bool isExist = false;

        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            using (SqlCommand cmd = new SqlCommand("Reader.usp_CheckIfMBPClientExists", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClientID", SqlDbType.Int).Value = Convert.ToInt32(token.ClientID);                

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    isExist = (reader.GetBoolean(0));
                }
            }
            return isExist;
        }
    }
    public TokenRequest RetrieveMBPClientDetails(TokenRequest token)
    {
        try
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
                using (SqlCommand cmd = new SqlCommand("Reader.usp_RetrieveMBPClientDetails", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClientID", SqlDbType.Int).Value = Convert.ToInt32(token.ClientID); 
                    TokenRequest tokenValues = new TokenRequest();
                     
                  
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        tokenValues.FirstName = reader["fld_FirstName"].ToString();
                        tokenValues.LastName = reader["fld_LastName"].ToString();
                        tokenValues.MiddleName = reader["fld_MiddleName"].ToString();
                        tokenValues.Suffix = reader["fld_Suffix"].ToString();
                        tokenValues.DOB = reader["fld_DateOfBirth"].ToString();
                        tokenValues.CivilStat = reader["fld_CivilStatus"].ToString();
                        tokenValues.Gender = reader["fld_Gender"].ToString();
                        tokenValues.Address = reader["fld_Address"].ToString();
                        tokenValues.Province = reader["fld_Province"].ToString();
                        tokenValues.City = reader["fld_City"].ToString();
                        tokenValues.ZipCode = reader["fld_ZipCode"].ToString();
                        tokenValues.ValidID = reader["fld_ValidIDPresented"].ToString();
                        tokenValues.ValidIDNum = reader["fld_ValidIDNumber"].ToString();
                        tokenValues.Email = reader["fld_EmailAddress"].ToString();
                        tokenValues.Nationality = reader["fld_Nationality"].ToString();
                        tokenValues.SourceOfFunds = reader["fld_SourceOfFunds"].ToString();
                        tokenValues.NatureofWork = reader["fld_NatureOfWork"].ToString();
                    }
                    //reader.Close();
                    return tokenValues;
                }
            }

        }
        catch (Exception error)
        {
            throw error;
        }
    }

    public void MBPInsertClientPhoto(TokenRequest token)
    {
        BaseResult resultValue = new BaseResult();

        try
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroinsuranceConnStringWriter"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
                //TokenRequest tokenValues = new TokenRequest();
                using (SqlCommand cmd = new SqlCommand("Updater.usp_Insert_ClientPhoto", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    byte[] img = Convert.FromBase64String(token.Photo);

                    cmd.Parameters.AddWithValue("@ClientID", SqlDbType.Int).Value = Convert.ToInt32(token.ClientID);
                    cmd.Parameters.AddWithValue("@Photo", SqlDbType.VarBinary).Value = img;
                    cmd.Parameters.AddWithValue("@Category", SqlDbType.VarChar).Value = token.AttachmentCategory;
                    cmd.Parameters.AddWithValue("@ReferenceCode", SqlDbType.VarChar).Value = token.ReferenceCode;
                    cmd.ExecuteNonQuery();

                    #region Audit Trail
                    auditTrail.IpAddress = token.IpAddress;
                    auditTrail.ActionTaken = "Upload Photo to tbl_Clients";
                    auditTrail.ActionDetails = "CLIBMicrositeWS: " + resultValue.Message;
                    auditTrail.InsertAuditTrailEntry();
                    #endregion
                }


            }

        }
        catch (Exception error)
        {
            throw error;
        }

    }
    public void MBPDependentTran(TokenRequest token)
    {
        BaseResult resultValue = new BaseResult();

        try
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroinsuranceConnStringWriter"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
                //TokenRequest tokenValues = new TokenRequest();
                using (SqlCommand cmd = new SqlCommand("Updater.usp_Tran_MBPDependent", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                
                    cmd.Parameters.AddWithValue("@ClientID", SqlDbType.Int).Value = Convert.ToInt32(token.ClientID);
                    cmd.Parameters.AddWithValue("@ReferenceCode", SqlDbType.VarChar).Value = token.ReferenceCode;
                    cmd.Parameters.AddWithValue("@FullName", SqlDbType.VarChar).Value = token.FullName;
                    cmd.Parameters.AddWithValue("@FirstName", SqlDbType.VarChar).Value = token.FirstName;
                    cmd.Parameters.AddWithValue("@MiddleName", SqlDbType.VarChar).Value = token.MiddleName;
                    cmd.Parameters.AddWithValue("@LastName", SqlDbType.VarChar).Value = token.LastName;
                    cmd.Parameters.AddWithValue("@Suffix", SqlDbType.VarChar).Value = token.Suffix;
                    cmd.Parameters.AddWithValue("@DateOfBirth", SqlDbType.DateTime).Value = Convert.ToDateTime(token.DOB);
                    cmd.Parameters.AddWithValue("@Gender", SqlDbType.VarChar).Value = token.Gender;

                    
                    cmd.ExecuteNonQuery();

                    #region Audit Trail
                    auditTrail.IpAddress = token.IpAddress;
                    auditTrail.ActionTaken = "Upload Photo to tbl_Clients";
                    auditTrail.ActionDetails = "CLIBMicrositeWS: " + resultValue.Message;
                    auditTrail.InsertAuditTrailEntry();
                    #endregion
                }


            }

        }
        catch (Exception error)
        {
            throw error;
        }

    }

    public void MBPBeneficiaryTran(TokenRequest token)
    {
        BaseResult resultValue = new BaseResult();

        try
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroinsuranceConnStringWriter"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
                //TokenRequest tokenValues = new TokenRequest();
                using (SqlCommand cmd = new SqlCommand("Updater.usp_Tran_MBPBeneficiary", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                
                    cmd.Parameters.AddWithValue("@ClientID", SqlDbType.Int).Value = Convert.ToInt32(token.ClientID);
                    cmd.Parameters.AddWithValue("@ReferenceCode", SqlDbType.VarChar).Value = token.ReferenceCode;
                    cmd.Parameters.AddWithValue("@AppDependentID", SqlDbType.Int).Value = Convert.ToInt32(token.AppDependentID);
                    cmd.Parameters.AddWithValue("@FullName", SqlDbType.VarChar).Value = token.FullName;
                    cmd.Parameters.AddWithValue("@Relationship", SqlDbType.VarChar).Value = token.Relationship;                 
                    cmd.Parameters.AddWithValue("@DateOfBirth", SqlDbType.DateTime).Value = Convert.ToDateTime(token.DOB);
                    cmd.Parameters.AddWithValue("@AppDepRel", SqlDbType.VarChar).Value = token.AppDepRelationship;

                    cmd.ExecuteNonQuery();

                    #region Audit Trail
                    auditTrail.IpAddress = token.IpAddress;
                    auditTrail.ActionTaken = "Insert into tbl_benetable";
                    auditTrail.ActionDetails = "CLIBMicrositeWS: " + resultValue.Message;
                    auditTrail.InsertAuditTrailEntry();
                    #endregion
                }


            }

        }
        catch (Exception error)
        {
            throw error;
        }

    }
    public TokenRequest GetMBPAppDependentID(TokenRequest token)
    {
        try
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
                using (SqlCommand cmd = new SqlCommand("Reader.usp_GetMBPAppDependentID", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    TokenRequest tokenValues = new TokenRequest();
                    cmd.Parameters.AddWithValue("@firstName", SqlDbType.VarChar).Value = token.FirstName;
                    cmd.Parameters.AddWithValue("@lastName", SqlDbType.VarChar).Value = token.LastName;
                    cmd.Parameters.AddWithValue("@DOB", SqlDbType.SmallDateTime).Value = Convert.ToDateTime(token.DOB);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        tokenValues.AppDependentID = reader["fld_AppDependentID"].ToString();
                    }
                    reader.Close();
                    return tokenValues;
                }
            }

        }
        catch (Exception error)
        {
            throw error;
        }
    }



    public void SaveIQRCodeKYC(TokenRequest token)
    {
        BaseResult resultValue = new BaseResult();

        try
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroinsuranceConnStringWriter"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
                //TokenRequest tokenValues = new TokenRequest();
                using (SqlCommand cmd = new SqlCommand("Updater.usp_InsertUpdateClientInformationIQR", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@firstName", SqlDbType.Int).Value = token.FirstName;
                    cmd.Parameters.AddWithValue("@middleName", SqlDbType.VarChar).Value = token.MiddleName;
                    cmd.Parameters.AddWithValue("@lastName", SqlDbType.VarChar).Value = token.LastName;
                    cmd.Parameters.AddWithValue("@suffix", SqlDbType.VarChar).Value = token.Suffix;
                    cmd.Parameters.AddWithValue("@gender", SqlDbType.VarChar).Value = token.Gender;
                    cmd.Parameters.AddWithValue("@birthDate", SqlDbType.DateTime).Value = Convert.ToDateTime(token.DOB);
                    cmd.Parameters.AddWithValue("@civilStatus", SqlDbType.VarChar).Value = token.CivilStat;
                    cmd.Parameters.AddWithValue("@sourceOfFunds", SqlDbType.VarChar).Value = token.SourceOfFunds;
                    cmd.Parameters.AddWithValue("@mobileNumber", SqlDbType.DateTime).Value = token.ContactNumber;
                    cmd.Parameters.AddWithValue("@emailAddress", SqlDbType.VarChar).Value = token.Email;
                    cmd.Parameters.AddWithValue("@validIDPresented", SqlDbType.VarChar).Value = token.ValidID;
                    cmd.Parameters.AddWithValue("@validIDNumber", SqlDbType.VarChar).Value = token.ValidIDNum;
                    cmd.Parameters.AddWithValue("@address", SqlDbType.VarChar).Value = token.Address;
                    cmd.Parameters.AddWithValue("@province", SqlDbType.VarChar).Value = token.Province;
                    cmd.Parameters.AddWithValue("@city", SqlDbType.VarChar).Value = token.City;
                    cmd.Parameters.AddWithValue("@guardianName", SqlDbType.VarChar).Value = token.GuardianName;
                    cmd.Parameters.AddWithValue("@guardianBirthdate", SqlDbType.VarChar).Value = Convert.ToDateTime(token.GuardianBirthday);
                    cmd.Parameters.AddWithValue("@guardianRelationship", SqlDbType.VarChar).Value = token.GuardianRelationship;
                    cmd.Parameters.AddWithValue("@referenceNumber", SqlDbType.VarChar).Value = token.ReferenceNumber;
                    cmd.Parameters.AddWithValue("@issueDate", SqlDbType.VarChar).Value = Convert.ToDateTime(token.IssueDateTime);
                    cmd.Parameters.AddWithValue("@effectiveDate", SqlDbType.VarChar).Value = Convert.ToDateTime(token.EffectiveDateTime);
                    cmd.Parameters.AddWithValue("@terminationDate", SqlDbType.VarChar).Value = Convert.ToDateTime(token.TerminationDate);
                    cmd.Parameters.AddWithValue("@beneficiaryName", SqlDbType.VarChar).Value = token.BeneficiaryName;
                    cmd.Parameters.AddWithValue("@beneficiaryRelationship", SqlDbType.VarChar).Value = token.BeneficiaryRelationship;
                    cmd.Parameters.AddWithValue("@categoryId", SqlDbType.BigInt).Value = token.CategoryId;
                    cmd.Parameters.AddWithValue("@productId", SqlDbType.BigInt).Value = token.ProductId;
                    cmd.Parameters.AddWithValue("@providerId", SqlDbType.BigInt).Value = token.ProviderId;
                    cmd.Parameters.AddWithValue("@partnerId", SqlDbType.BigInt).Value = token.PartnerId;
                    cmd.Parameters.AddWithValue("@platformId", SqlDbType.BigInt).Value = token.PlatformId;



                    cmd.ExecuteNonQuery();

                    #region Audit Trail
                    auditTrail.IpAddress = token.IpAddress;
                    auditTrail.ActionTaken = "InsertUpdateClientInformationIQR";
                    auditTrail.ActionDetails = "CLIBMicrositeWS: " + resultValue.Message;
                    auditTrail.InsertAuditTrailEntry();
                    #endregion
                }


            }

        }
        catch (Exception error)
        {
            throw error;
        }

    }


    public void MBPDependentTran2(TokenRequest token)
    {
        BaseResult resultValue = new BaseResult();

        try
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroinsuranceConnStringWriter"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
                //TokenRequest tokenValues = new TokenRequest();
                using (SqlCommand cmd = new SqlCommand("Updater.usp_Tran_MBPDependent2", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ClientID", SqlDbType.Int).Value = Convert.ToInt32(token.ClientID);
                    cmd.Parameters.AddWithValue("@ReferenceCode", SqlDbType.VarChar).Value = token.ReferenceCode;
                    cmd.Parameters.AddWithValue("@FullName2", SqlDbType.VarChar).Value = token.FullName2;
                    cmd.Parameters.AddWithValue("@FirstName2", SqlDbType.VarChar).Value = token.FirstName2;
                    cmd.Parameters.AddWithValue("@MiddleName2", SqlDbType.VarChar).Value = token.MiddleName2;
                    cmd.Parameters.AddWithValue("@LastName2", SqlDbType.VarChar).Value = token.LastName2;
                    cmd.Parameters.AddWithValue("@Suffix2", SqlDbType.VarChar).Value = token.Suffix2;
                    cmd.Parameters.AddWithValue("@DateOfBirth2", SqlDbType.DateTime).Value = Convert.ToDateTime(token.DOB2);
                    cmd.Parameters.AddWithValue("@Gender2", SqlDbType.VarChar).Value = token.Gender2;


                    cmd.ExecuteNonQuery();

                    #region Audit Trail
                    auditTrail.IpAddress = token.IpAddress;
                    auditTrail.ActionTaken = "Insert into MBPDependentTable";
                    auditTrail.ActionDetails = "CLIBMicrositeWS: " + resultValue.Message;
                    auditTrail.InsertAuditTrailEntry();
                    #endregion
                }


            }

        }
        catch (Exception error)
        {
            throw error;
        }

    }

    public TokenRequest GetMBPAppDependentID2(TokenRequest token)
    {
        try
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
                using (SqlCommand cmd = new SqlCommand("Reader.usp_GetMBPAppDependentID2", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    TokenRequest tokenValues = new TokenRequest();
                    cmd.Parameters.AddWithValue("@firstName", SqlDbType.VarChar).Value = token.FirstName2;
                    cmd.Parameters.AddWithValue("@lastName", SqlDbType.VarChar).Value = token.LastName2;
                    cmd.Parameters.AddWithValue("@DOB", SqlDbType.SmallDateTime).Value = Convert.ToDateTime(token.DOB2);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        tokenValues.AppDependentID = reader["fld_AppDependentID"].ToString();
                    }
                    reader.Close();
                    return tokenValues;
                }
            }

        }
        catch (Exception error)
        {
            throw error;
        }
    }

    public IList<String> GetListSourceOfFunds(TokenRequest token)
    {
        var sourceStatList = new List<String>();
        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;
        var selectSQL = "select distinct [fld_DisplayText] from [db_MicroInsurance].[Reference].[tbl_IMSSelectionList] where fld_DefinitionID = 19 order by [fld_DisplayText] asc";

        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            using (SqlCommand cmd = new SqlCommand(selectSQL, conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                sourceStatList.Add("Select");
                while (reader.Read())
                {
                    //ListItem category = new ListItem();
                    string sourceOfFunds;

                    sourceOfFunds = reader["fld_DisplayText"].ToString();

                    sourceStatList.Add(sourceOfFunds);
                }
            }
        }
        return sourceStatList;
    }
  
    public IList<String> GetListNatureOfWork(TokenRequest token)
    {
        var workStatList = new List<String>();
        var connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMicroInsuranceConnStringReader"].ConnectionString;
        var selectSQL = "select distinct [fld_DisplayText] from [db_MicroInsurance].[Reference].[tbl_IMSSelectionList] where fld_DefinitionID = 1 order by [fld_DisplayText] asc";

        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open(); // no need of try/catch or using, as if this fails, no resources are leaked
            using (SqlCommand cmd = new SqlCommand(selectSQL, conn))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                workStatList.Add("Select");
                while (reader.Read())
                {
                    //ListItem category = new ListItem();
                    string natureOfwork;

                    natureOfwork = reader["fld_DisplayText"].ToString();

                    workStatList.Add(natureOfwork);
                }
            }
        }
        return workStatList;
    }
    #endregion

}