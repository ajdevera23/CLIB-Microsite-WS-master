using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface ICLIBMicrositeWS
{

    [OperationContract]
    [WebInvoke(Method = "POST"
            , ResponseFormat = WebMessageFormat.Json
            , UriTemplate = "GetPartnerList"
            , BodyStyle = WebMessageBodyStyle.Bare)]
    IList<String> GetPartnerList(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
            , ResponseFormat = WebMessageFormat.Json
            , UriTemplate = "GetProductList"
            , BodyStyle = WebMessageBodyStyle.Bare)]
    IList<String> GetProductList(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
            , ResponseFormat = WebMessageFormat.Json
            , UriTemplate = "ifReferenceCodeExists"
            , BodyStyle = WebMessageBodyStyle.Bare)]
    bool ifReferenceCodeExists(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
            , ResponseFormat = WebMessageFormat.Json
            , UriTemplate = "ifReferenceCodeIsUsed"
            , BodyStyle = WebMessageBodyStyle.Bare)]
    bool ifReferenceCodeIsUsed(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
            , ResponseFormat = WebMessageFormat.Json
            , UriTemplate = "ifReferenceNumberExists"
            , BodyStyle = WebMessageBodyStyle.Bare)]
    bool ifReferenceNumberExists(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
        , ResponseFormat = WebMessageFormat.Json
        , UriTemplate = "ifProductCodeBaseOnIntegrationMappingExists"
        , BodyStyle = WebMessageBodyStyle.Bare)]
    bool ifProductCodeBaseOnIntegrationMappingExists(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
            , ResponseFormat = WebMessageFormat.Json
            , UriTemplate = "GetListRelationship"
            , BodyStyle = WebMessageBodyStyle.Bare)]
    IList<String> GetListRelationship(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
            , ResponseFormat = WebMessageFormat.Json
            , UriTemplate = "GetListValidIds"
            , BodyStyle = WebMessageBodyStyle.Bare)]
    IList<String> GetListValidIds(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
            , ResponseFormat = WebMessageFormat.Json
            , UriTemplate = "GetIntegrationId"
            , BodyStyle = WebMessageBodyStyle.Bare)]
    Int64 GetIntegrationId(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
            , ResponseFormat = WebMessageFormat.Json
            , UriTemplate = "GetProductImagePath"
            , BodyStyle = WebMessageBodyStyle.Bare)]
    string GetProductImagePath(TokenRequest token);

  

    [OperationContract]
    [WebInvoke(Method = "POST"
            , ResponseFormat = WebMessageFormat.Json
            , UriTemplate = "GetPartnerImagePath"
            , BodyStyle = WebMessageBodyStyle.Bare)]
    string GetPartnerImagePath(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
            , ResponseFormat = WebMessageFormat.Json
            , UriTemplate = "GetCategory"
            , BodyStyle = WebMessageBodyStyle.Bare)]
    string GetCategory(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
            , ResponseFormat = WebMessageFormat.Json
            , UriTemplate = "TagRefCodeIsUsed"
            , BodyStyle = WebMessageBodyStyle.Bare)]
    BaseResult TagRefCodeIsUsed(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
            , ResponseFormat = WebMessageFormat.Json
            , UriTemplate = "PopulateProductCategoryGridView"
            , BodyStyle = WebMessageBodyStyle.Bare)]
    IList<CategoryResult> PopulateProductCategoryGridView(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
           , ResponseFormat = WebMessageFormat.Json
           , UriTemplate = "PopulateProductGridView"
           , BodyStyle = WebMessageBodyStyle.Bare)]
    IList<ProductList> PopulateProductGridView(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
       , ResponseFormat = WebMessageFormat.Json
       , UriTemplate = "PopulateProductDetails"
       , BodyStyle = WebMessageBodyStyle.Bare)]
    IList<ProductList> PopulateProductDetails(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
   , ResponseFormat = WebMessageFormat.Json
   , UriTemplate = "PopulateProductByCodesAndIntegrationID"
   , BodyStyle = WebMessageBodyStyle.Bare)]
    IList<ProductList> PopulateProductByCodesAndIntegrationID(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
           , ResponseFormat = WebMessageFormat.Json
           , UriTemplate = "UploadExcel"
           , BodyStyle = WebMessageBodyStyle.Bare)]
    BaseResult UploadExcel(TokenRequest token);
    // TODO: Add your service operations here

    #region Client REferral

    [OperationContract]
    [WebInvoke(Method = "POST"
          , ResponseFormat = WebMessageFormat.Json
          , UriTemplate = "CheckIfBranchExists"
          , BodyStyle = WebMessageBodyStyle.Bare)]
    bool CheckIfBranchExists(TokenRequest token);

    [WebInvoke(Method = "POST"
            , ResponseFormat = WebMessageFormat.Json
            , UriTemplate = "RetrieveBranchDetails"
            , BodyStyle = WebMessageBodyStyle.Bare)]
    TokenRequest RetrieveBranchDetails(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
        , ResponseFormat = WebMessageFormat.Json
        , UriTemplate = "GetListProvince"
        , BodyStyle = WebMessageBodyStyle.Bare)]
    IList<String> GetListProvince(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
          , ResponseFormat = WebMessageFormat.Json
          , UriTemplate = "GetListCity"
          , BodyStyle = WebMessageBodyStyle.Bare)]
    IList<String> GetListCity(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
        , ResponseFormat = WebMessageFormat.Json
        , UriTemplate = "CheckIfADCClientExists"
        , BodyStyle = WebMessageBodyStyle.Bare)]
    bool CheckIfADCClientExists(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
            , ResponseFormat = WebMessageFormat.Json
            , UriTemplate = "CheckIfClientExistsIQR"
            , BodyStyle = WebMessageBodyStyle.Bare)]
    bool CheckIfClientExistsIQR(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
            , ResponseFormat = WebMessageFormat.Json
            , UriTemplate = "RetrieveDetailsPerADCClient"
            , BodyStyle = WebMessageBodyStyle.Bare)]
    TokenRequest RetrieveDetailsPerADCClient(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
            , ResponseFormat = WebMessageFormat.Json
            , UriTemplate = "CheckIfADCGroupClientExists"
            , BodyStyle = WebMessageBodyStyle.Bare)]
    bool CheckIfADCGroupClientExists(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
            , ResponseFormat = WebMessageFormat.Json
            , UriTemplate = "ClientReferralAgingValidation"
            , BodyStyle = WebMessageBodyStyle.Bare)]
    bool ClientReferralAgingValidation(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
          , ResponseFormat = WebMessageFormat.Json
          , UriTemplate = "ClientReferralIndividualTran"
          , BodyStyle = WebMessageBodyStyle.Bare)]
    void ClientReferralIndividualTran(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
         , ResponseFormat = WebMessageFormat.Json
         , UriTemplate = "ClientReferralTran"
         , BodyStyle = WebMessageBodyStyle.Bare)]
    void ClientReferralTran(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
          , ResponseFormat = WebMessageFormat.Json
          , UriTemplate = "ClientReferralGroupTran"
          , BodyStyle = WebMessageBodyStyle.Bare)]
    void ClientReferralGroupTran(TokenRequest token);

    #endregion



    #region MicroBIz
    [OperationContract]
    [WebInvoke(Method = "POST"
         , ResponseFormat = WebMessageFormat.Json
         , UriTemplate = "GetListGender"
         , BodyStyle = WebMessageBodyStyle.Bare)]
    IList<String> GetListGender(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
        , ResponseFormat = WebMessageFormat.Json
        , UriTemplate = "GetListCivilStatus"
        , BodyStyle = WebMessageBodyStyle.Bare)]
    IList<String> GetListCivilStatus(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
        , ResponseFormat = WebMessageFormat.Json
        , UriTemplate = "GetListValidID"
        , BodyStyle = WebMessageBodyStyle.Bare)]
    IList<String> GetListValidID(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
        , ResponseFormat = WebMessageFormat.Json
        , UriTemplate = "GetListRelation"
        , BodyStyle = WebMessageBodyStyle.Bare)]
    IList<String> GetListRelation(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
         , ResponseFormat = WebMessageFormat.Json
         , UriTemplate = "TranMBPClient"
         , BodyStyle = WebMessageBodyStyle.Bare)]
    void TranMBPClient(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
         , ResponseFormat = WebMessageFormat.Json
         , UriTemplate = "MBPQuestionnaireTran"
         , BodyStyle = WebMessageBodyStyle.Bare)]
    void MBPQuestionnaireTran(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
            , ResponseFormat = WebMessageFormat.Json
            , UriTemplate = "GetMBPClientID"
            , BodyStyle = WebMessageBodyStyle.Bare)]
    TokenRequest GetMBPClientID(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
         , ResponseFormat = WebMessageFormat.Json
         , UriTemplate = "MBPBusinessDetailsTran"
         , BodyStyle = WebMessageBodyStyle.Bare)]
    void MBPBusinessDetailsTran(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
         , ResponseFormat = WebMessageFormat.Json
         , UriTemplate = "MBPInsertAttachments"
         , BodyStyle = WebMessageBodyStyle.Bare)]
    void MBPInsertAttachments(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
         , ResponseFormat = WebMessageFormat.Json
         , UriTemplate = "MBPAddtlBusOwnerDetails"
         , BodyStyle = WebMessageBodyStyle.Bare)]
    void MBPAddtlBusOwnerDetails(TokenRequest token);



    [OperationContract]
    [WebInvoke(Method = "POST"
          , ResponseFormat = WebMessageFormat.Json
          , UriTemplate = "CheckIfMBPClientExists"
          , BodyStyle = WebMessageBodyStyle.Bare)]
    bool CheckIfMBPClientExists(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
          , ResponseFormat = WebMessageFormat.Json
          , UriTemplate = "RetrieveMBPClientDetails"
          , BodyStyle = WebMessageBodyStyle.Bare)]
    TokenRequest RetrieveMBPClientDetails(TokenRequest token);
    
    [OperationContract]
    [WebInvoke(Method = "POST"
         , ResponseFormat = WebMessageFormat.Json
         , UriTemplate = "MBPInsertClientPhoto"
         , BodyStyle = WebMessageBodyStyle.Bare)]
    void MBPInsertClientPhoto(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
         , ResponseFormat = WebMessageFormat.Json
         , UriTemplate = "MBPDependentTran"
         , BodyStyle = WebMessageBodyStyle.Bare)]
    void MBPDependentTran(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
        , ResponseFormat = WebMessageFormat.Json
        , UriTemplate = "MBPBeneficiaryTran"
        , BodyStyle = WebMessageBodyStyle.Bare)]
    void MBPBeneficiaryTran(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
            , ResponseFormat = WebMessageFormat.Json
            , UriTemplate = "GetMBPAppDependentID"
            , BodyStyle = WebMessageBodyStyle.Bare)]
    TokenRequest GetMBPAppDependentID(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
          , ResponseFormat = WebMessageFormat.Json
          , UriTemplate = "TestMBPMail"
          , BodyStyle = WebMessageBodyStyle.Bare)]
    BaseResult TestMBPMail(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
      , ResponseFormat = WebMessageFormat.Json
      , UriTemplate = "TestIQRMail"
      , BodyStyle = WebMessageBodyStyle.Bare)]
    BaseResult TestIQRMail(TokenRequest token);


    [OperationContract]
    [WebInvoke(Method = "POST"
         , ResponseFormat = WebMessageFormat.Json
         , UriTemplate = "MBPDependentTran2"
         , BodyStyle = WebMessageBodyStyle.Bare)]
    void MBPDependentTran2(TokenRequest token);


    [OperationContract]
    [WebInvoke(Method = "POST"
     , ResponseFormat = WebMessageFormat.Json
     , UriTemplate = "SaveIQRCodeKYC"
     , BodyStyle = WebMessageBodyStyle.Bare)]
    void SaveIQRCodeKYC(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
            , ResponseFormat = WebMessageFormat.Json
            , UriTemplate = "GetMBPAppDependentID2"
            , BodyStyle = WebMessageBodyStyle.Bare)]
    TokenRequest GetMBPAppDependentID2(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
         , ResponseFormat = WebMessageFormat.Json
         , UriTemplate = "GetListSourceOfFunds"
         , BodyStyle = WebMessageBodyStyle.Bare)]
    IList<String> GetListSourceOfFunds(TokenRequest token);

    [OperationContract]
    [WebInvoke(Method = "POST"
         , ResponseFormat = WebMessageFormat.Json
         , UriTemplate = "GetListNatureOfWork"
         , BodyStyle = WebMessageBodyStyle.Bare)]
    IList<String> GetListNatureOfWork(TokenRequest token);
    #endregion
}

// Use a data contract as illustrated in the sample below to add composite types to service operations.
[DataContract]
public class CompositeType
{
	bool boolValue = true;
	string stringValue = "Hello ";

	[DataMember]
	public bool BoolValue
	{
		get { return boolValue; }
		set { boolValue = value; }
	}

	[DataMember]
	public string StringValue
	{
		get { return stringValue; }
		set { stringValue = value; }
	}
}
