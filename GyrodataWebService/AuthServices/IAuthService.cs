using GyrodataWebService.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace GyrodataWebService.AuthServices
{
    [ServiceContract]
    public interface IAuthService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
             UriTemplate = "/authenticate",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        string Authenticate(Credentials creds);

        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "/sentconfig",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        Dictionary<string, string> sentConfig();

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate ="/testtoken",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        bool TestToken(string token);
    }
}
