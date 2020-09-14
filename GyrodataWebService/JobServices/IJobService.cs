using GyrodataWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GyrodataWebService.JobServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IJobServices" in both code and config file together.
    [ServiceContract]
    public interface IJobService
    {
        // Get Method 
        [OperationContract]
        [WebGet(UriTemplate = "GetAllJobs")]
        List<JobModel> GetAllJobs();

        [OperationContract]
        [WebGet(UriTemplate = "GetSelectedJobData/{jobnumber}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        List<JobModel> GetSelectedJobData(string jobnumber);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    RequestFormat = WebMessageFormat.Json,
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Bare)]
        //List<JobModel> GetAllJobs();

        // Getting All Job information, load to Array
        // Get Method 
        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
                ResponseFormat = WebMessageFormat.Json,
                BodyStyle = WebMessageBodyStyle.Bare)]
        List<List<string>> GetAllDataForJobCreate();
        //Dictionary<string, List<string>> GetAllDataForJobCreate();

        [OperationContract]
        [WebGet(UriTemplate = "GetCustomJobData?what={what}&where={where}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<JobModel> GetCustomJobData(string what, string where);

        [OperationContract]
        [WebInvoke(Method = "POST", 
            UriTemplate = "AddNewJob",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        int AddNewJob(JobModel model);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "EditJob/{id}",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        //int EditJob(JobModel model, float circul);
        void EditJob(string id, JobModel model);
    }
}
