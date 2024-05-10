using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class CLIBMicrositeWS : ICLIBMicrositeWS
{
    GetList getList = new GetList();
    Upload upload = new Upload();
    RefCodeIsUsed refCodeIsUsed = new RefCodeIsUsed();
    public IList<String> GetPartnerList(TokenRequest token)
    {
        IList<String> partnerList;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                partnerList = getList.GetPartnerList();
            }
            else
            {
                partnerList = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            partnerList = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return partnerList;
    }

    public IList<String> GetProductList(TokenRequest token)
    {
        IList<String> productList;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                productList = getList.GetProductList();
                //productList.ResultStatus = ResultType.Success;
            }
            else
            {
                //productList.ResultStatus = ResultType.Failed;
                //productList.Message = "Incorrect Auth code.";
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
                productList = null;
            }
        }
        catch (Exception ex)
        {
            productList = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return productList;
    }

    public bool ifReferenceCodeExists(TokenRequest token)
    {
        bool bit = false;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                bit = getList.ifReferenceCodeExists(token.ReferenceCode);
            }
            else
            {
                bit = false;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            bit = false;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return bit;
    }

    public bool ifReferenceCodeIsUsed(TokenRequest token)
    {
        bool bit = false;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                bit = getList.ifReferenceCodeIsUsed(token);
            }
            else
            {
                bit = true;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            bit = true;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return bit;
    }


    public bool ifReferenceNumberExists(TokenRequest token)
    {
        bool bit = false;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                bit = getList.ifReferenceNumberExists(token);
            }
            else
            {
                bit = true;
            }
        }
        catch (Exception ex)
        {
            bit = true;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return bit;
    }


    public bool ifProductCodeBaseOnIntegrationMappingExists(TokenRequest token)
    {
        bool bit = false;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                bit = getList.ifProductCodeBaseOnIntegrationMappingExists(token);
            }
            else
            {
                bit = true;
            }
        }
        catch (Exception ex)
        {
            bit = true;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return bit;
    }


    public BaseResult UploadExcel(TokenRequest token)
    {
        BaseResult bit = new BaseResult();
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {
                bit = upload.UploadExcel(token);
            }
            else
            {
                bit.ResultStatus = ResultType.Failed;
                bit.Message = "Invalid token used.";
            }
        }
        catch (Exception ex)
        {
            bit.ResultStatus = ResultType.Success;
            bit.Message = "An error has occured while uploading entries. Please contact ict-affiliates@pjlhuillier.com.";
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return bit;
    }
    public IList<String> GetListRelationship(TokenRequest token)
    {
        IList<String> relationshipList;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                relationshipList = getList.GetListRelationship();
            }
            else
            {
                relationshipList = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            relationshipList = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return relationshipList;
    }

    public Int64 GetIntegrationId(TokenRequest token)
    {
        Int64 integrationId;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                integrationId = getList.GetIntegrationId(token.ProductCode, token.PartnerCode);
            }
            else
            {
                integrationId = 0;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            integrationId = 0;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return integrationId;
    }

    public IList<String> GetListValidIds(TokenRequest token)
    {
        IList<String> validIdList;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                validIdList = getList.GetListValidIds();
            }
            else
            {
                validIdList = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            validIdList = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return validIdList;
    }
    public string GetProductImagePath(TokenRequest token)
    {
        string productImagePath;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                productImagePath = getList.GetProductImagePath(token.ProductCode);
            }
            else
            {
                productImagePath = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            productImagePath = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return productImagePath;
    }

    public string GetPartnerImagePath(TokenRequest token)
    {
        string returnValue;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                returnValue = getList.GetPartnerImagePath(token);
            }
            else
            {
                returnValue = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            returnValue = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return returnValue;
    }
    public string GetCategory(TokenRequest token)
    {
        string returnValue;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                returnValue = getList.GetCategory(token);
            }
            else
            {
                returnValue = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            returnValue = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return returnValue;
    }
    public BaseResult TagRefCodeIsUsed(TokenRequest token)
    {
        BaseResult result = new BaseResult();
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                result = refCodeIsUsed.TagRefCodeIsUsed(token);
            }
            else
            {
                result.ResultStatus = ResultType.Failed;
                result.Message = "Incorrect Authentication code used.";
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            result.ResultStatus = ResultType.Failed;
            result.Message = "Incorrect Authentication code used.";
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return result;
    }



    public IList<CategoryResult> PopulateProductCategoryGridView(TokenRequest token)
    {
        IList<CategoryResult> result;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                result = getList.PopulateProductCategoryGridView(token);
            }
            else
            {
                result = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            result = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return result;
    }

    public IList<ProductList> PopulateProductGridView(TokenRequest token)
    {
        IList<ProductList> result;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                result = getList.PopulateProductGridView(token);
            }
            else
            {
                result = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            result = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return result;
    }

    public IList<ProductList> PopulateProductDetails(TokenRequest token)
    {
        IList<ProductList> result;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                result = getList.PopulateProductDetails(token);
            }
            else
            {
                result = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            result = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return result;
    }


    public IList<ProductList> PopulateProductByCodesAndIntegrationID(TokenRequest token)
    {
        IList<ProductList> result;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                result = getList.PopulateProductByCodesAndIntegrationID(token);
            }
            else
            {
                result = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            result = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return result;
    }

    #region ClientReferral
    public bool CheckIfBranchExists(TokenRequest token)
    {
        bool bit = false;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {
                bit = getList.CheckIfBranchExists(token);

            }
            else
            {
                bit = true;

                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            bit = true;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return bit;
    }

    public TokenRequest RetrieveBranchDetails(TokenRequest token)
    {
        TokenRequest result;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                result = getList.RetrieveBranchDetails(token);
            }
            else
            {
                result = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            result = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return result;
    }
    public IList<String> GetListProvince(TokenRequest token)
    {
        IList<String> provList;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                provList = getList.GetListProvince();
            }
            else
            {
                provList = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            provList = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return provList;
    }

    public IList<String> GetListCity(TokenRequest token)
    {
        IList<String> cityList;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                cityList = getList.GetListCity(token);
            }
            else
            {
                cityList = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            cityList = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return cityList;
    }


    // Existing Client in tbl_Client
    public bool CheckIfClientExistsIQR(TokenRequest token)
    {
        bool bit = false;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {
                bit = getList.CheckIfClientExistsIQR(token);

            }
            else
            {
                bit = true;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            bit = true;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return bit;
    }

    public bool CheckIfADCClientExists(TokenRequest token)
    {
        bool bit = false;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {
                bit = getList.CheckIfADCClientExists(token);

            }
            else
            {
                bit = true;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            bit = true;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return bit;
    }

    public TokenRequest RetrieveDetailsPerADCClient(TokenRequest token)
    {
        TokenRequest result;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                result = getList.RetrieveDetailsPerADCClient(token);
            }
            else
            {
                result = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            result = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return result;
    }

    public bool CheckIfADCGroupClientExists(TokenRequest token)
    {
        bool bit = false;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {
                bit = getList.CheckIfADCGroupClientExists(token);

            }
            else
            {
                bit = true;

                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            bit = true;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return bit;
    }
    public bool ClientReferralAgingValidation(TokenRequest token)
    {
        bool bit = false;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {
                bit = getList.ClientReferralAgingValidation(token);

            }
            else
            {
                bit = true;

                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            bit = true;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return bit;
    }

    public void ClientReferralIndividualTran(TokenRequest token)
    {
        GetList get = new GetList();
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {
                get.ClientReferralIndividualTran(token);
            }
            else
            {

                get = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {


            get = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }
    }
    public void ClientReferralTran(TokenRequest token)
    {
        GetList get = new GetList();
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {
                get.ClientReferralTran(token);
            }
            else
            {

                get = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {


            get = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }
    }

    public void ClientReferralGroupTran(TokenRequest token)
    {
        GetList get = new GetList();
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {
                get.ClientReferralGroupTran(token);
            }
            else
            {

                get = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {


            get = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }
    }

    #endregion

    #region MicroBiz


    public IList<String> GetListGender(TokenRequest token)
    {
        IList<String> genderList;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {
                
                genderList = getList.GetListGender(token);
            }
            else
            {
                genderList = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            genderList = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return genderList;
    }
    public IList<String> GetListCivilStatus(TokenRequest token)
    {
        IList<String> civilStatList;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                civilStatList = getList.GetListCivilStatus(token);
            }
            else
            {
                civilStatList = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            civilStatList = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return civilStatList;
    }
    public IList<String> GetListValidID(TokenRequest token)
    {
        IList<String> validIDList;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                validIDList = getList.GetListValidID(token);
            }
            else
            {
                validIDList = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            validIDList = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return validIDList;
    }
    public IList<String> GetListRelation(TokenRequest token)
    {
        IList<String> relationList;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                relationList = getList.GetListRelation(token);
            }
            else
            {
                relationList = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            relationList = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return relationList;
    }
    public void TranMBPClient(TokenRequest token)
    {
        GetList get = new GetList();
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {
               get.TranMBPClient(token);
            }
            else
            {

                get = null;
                SystemUtility.EventLog.SaveError("Error in TranMBPClient.");
            }
        }
        catch (Exception ex)
        {


            get = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }
        
    }
    public void MBPQuestionnaireTran(TokenRequest token)
    {
        GetList get = new GetList();
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {
                get.MBPQuestionnaireTran(token);
            }
            else
            {

                get = null;
                SystemUtility.EventLog.SaveError("Error in MBPQuestionnaireTran.");
            }
        }
        catch (Exception ex)
        {


            get = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }
    }

    public TokenRequest GetMBPClientID(TokenRequest token)
    {
        TokenRequest result;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                result = getList.GetMBPClientID(token);
            }
            else
            {
                result = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            result = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return result;
    }
    public void MBPBusinessDetailsTran(TokenRequest token)
    {
        GetList get = new GetList();
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {
                get.MBPBusinessDetailsTran(token);
            }
            else
            {

                get = null;
                SystemUtility.EventLog.SaveError("Error in MBPBussDetailsTran.");
            }
        }
        catch (Exception ex)
        {


            get = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }
    }
    public void MBPInsertAttachments(TokenRequest token)
    {
        GetList get = new GetList();
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {
                get.MBPInsertAttachments(token);
            }
            else
            {

                get = null;
                SystemUtility.EventLog.SaveError("Error in MBPInsertAttachments.");
            }
        }
        catch (Exception ex)
        {


            get = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }
    }
    public void MBPAddtlBusOwnerDetails(TokenRequest token)
    {
        GetList get = new GetList();
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {
                get.MBPAddtlBusOwnerDetails(token);
            }
            else
            {

                get = null;
                SystemUtility.EventLog.SaveError("Error in MBPAddtlBusOwnerDetails.");
            }
        }
        catch (Exception ex)
        {


            get = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }
    }
    public bool CheckIfMBPClientExists(TokenRequest token)
    {
        bool bit = false;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {
                bit = getList.CheckIfMBPClientExists(token);

            }
            else
            {
                bit = true;

                SystemUtility.EventLog.SaveError("Error in CheckIfMBPClientExists.");
            }
        }
        catch (Exception ex)
        {
            bit = true;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return bit;
    }

    public TokenRequest RetrieveMBPClientDetails(TokenRequest token)
    {
        TokenRequest result;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                result = getList.RetrieveMBPClientDetails(token);
            } 
            else
            {          
                result = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {           
            result = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }
        
        return result;
 
    }

    

    public void MBPInsertClientPhoto(TokenRequest token)
    {
        GetList get = new GetList();
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {
                get.MBPInsertClientPhoto(token);
            }
            else
            {               
                get = null;
                SystemUtility.EventLog.SaveError("Error in MBPInsertClientPhoto.");
            }
        }
        catch (Exception ex)
        {       
            get = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }
    }

    public void MBPDependentTran(TokenRequest token)
    {
        GetList get = new GetList();
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {
                get.MBPDependentTran(token);
            }
            else
            {      
                get = null;
                SystemUtility.EventLog.SaveError("Error in MBPDependentTran.");
            }
        }
        catch (Exception ex)
        {
            //get.MBPDependentTran(token);
            get = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }
    }
 
    public void MBPBeneficiaryTran(TokenRequest token)
    {
        GetList get = new GetList();
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {
                get.MBPBeneficiaryTran(token);
            }
            else
            {
                get = null;
                SystemUtility.EventLog.SaveError("Error in MBPBeneficiaryTran.");
            }
        }
        catch (Exception ex)
        {
           get = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }
    }
    public TokenRequest GetMBPAppDependentID(TokenRequest token)
    {
        TokenRequest result;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                result = getList.GetMBPAppDependentID(token);
            }
            else
            {
                result = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            result = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return result;
    }

    public BaseResult TestMBPMail(TokenRequest token)
    {
        BaseResult result = new BaseResult();
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                result = refCodeIsUsed.TestMBPMail(token);
            }
            else
            {
                result.ResultStatus = ResultType.Failed;
                result.Message = "Incorrect Authentication code used.";
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            result.ResultStatus = ResultType.Failed;
            result.Message = "Incorrect Authentication code used.";
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return result;
    }

    public BaseResult TestIQRMail(TokenRequest token)
    {
        BaseResult result = new BaseResult();
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                result = refCodeIsUsed.TestIQRMail(token);
            }
            else
            {
                result.ResultStatus = ResultType.Failed;
                result.Message = "Incorrect Authentication code used.";
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            result.ResultStatus = ResultType.Failed;
            result.Message = "Incorrect Authentication code used.";
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

            return result;
    }

    public void MBPDependentTran2(TokenRequest token)
    {
        GetList get = new GetList();
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token2, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {
                get.MBPDependentTran2(token);
            }
            else
            {
                get.MBPDependentTran2(token);
                //      get = null;
                SystemUtility.EventLog.SaveError("Error in MBPDependentTran.");
            }
        }
        catch (Exception ex)
        {
            get = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }
    }

    public void SaveIQRCodeKYC(TokenRequest token)
    {
        GetList get = new GetList();
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {
                get.SaveIQRCodeKYC(token);
            }
            else
            {
                get.SaveIQRCodeKYC(token);
                //      get = null;
                SystemUtility.EventLog.SaveError("Error in Saving IQR.");
            }
        }
        catch (Exception ex)
        {
            get = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }
    }

    public TokenRequest GetMBPAppDependentID2(TokenRequest token)
    {
        TokenRequest result;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                result = getList.GetMBPAppDependentID2(token);
            }
            else
            {
                result = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            result = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return result;
    }

     
    public IList<String> GetListSourceOfFunds(TokenRequest token)
    {
        IList<String> sourceofFundList;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                sourceofFundList = getList.GetListSourceOfFunds(token);
            }
            else
            {
                sourceofFundList = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            sourceofFundList = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return sourceofFundList;
    }

    public IList<String> GetListNatureOfWork(TokenRequest token)
    {
        IList<String> natureofWorkList;
        try
        {
            if (TokenAuth4.TokenAuth.IsValid(token.Token, System.Configuration.ConfigurationManager.AppSettings["PassKey"]))
            {

                natureofWorkList = getList.GetListNatureOfWork(token);
            }
            else
            {
                natureofWorkList = null;
                //SystemUtility.EventLog.SaveError("Incorrect auth code.");
            }
        }
        catch (Exception ex)
        {
            natureofWorkList = null;
            SystemUtility.EventLog.SaveError(ex.ToString());
        }

        return natureofWorkList;
    }
    #endregion

}
