using GyrodataWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GyrodataWebService.BatteryServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBatteryService" in both code and config file together.
    [ServiceContract]
    public interface IBatteryService
    {
        // FOR TEST PURPOSE SEE IF THE REQUEST WORKS, NO AUTH REQUIRED
        [OperationContract]
        [WebGet(
            UriTemplate = "GetAllBatteries",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<BatteryModel> GetAllBatteriesNotToken();

        // FOR DISPLAYING ALL THE BATTERIES BASED ON CRITERIA SERIAL & WHERE (SERIAL, INVOICE, CCD, LOCATION)
        [OperationContract]
        [WebGet(
            UriTemplate = "GetSelectedBatteries?what={serial}&where={where}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<BatteryModel> GetSelectedBatteries(string serial, string where);

        // FOR DISPLAYING ALL THE BATTERIES BASED ON CRITERIA SERIAL & WHERE (SERIAL, INVOICE, CCD, LOCATION)
        //[OperationContract]
        //[WebInvoke(
        //    Method = "GET",
        //    UriTemplate = "GetSelectedBatteries?what={serial}&where={where}")]
        //List<BatteryModel> GetSelectedBatteries(string serial, string where);

        // POST Method ADDING BATTERY //
        [OperationContract]
        [WebInvoke(
            Method = "POST", 
            UriTemplate = "AddBattery",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        int AddBattery(BatteryModel model);

        // EDITING CURRENT BATTERY
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "EditBattery/{id}",
           BodyStyle = WebMessageBodyStyle.Bare,
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        //int EditJob(JobModel model, float circul);
        void EditBattery(string id, BatteryModel model);

        // GET CURRENT BATTERY, SEE PRECISE DETAILS
        [OperationContract]
        [WebGet(
            UriTemplate = "GetSelectedBatteryData/{id}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<BatteryModel> GetSelectedBatteryData(string id);

        // GET CURRENT BATTERY BY SERIAL ONE
        [OperationContract]
        [WebGet(
            UriTemplate = "GetSelectedBatteryDataBySerial/{serial}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<BatteryModel> GetSelectedBatteryDataBySerial(string serial);
    }
}
