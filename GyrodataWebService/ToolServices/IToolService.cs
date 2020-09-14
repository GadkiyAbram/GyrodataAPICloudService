using GyrodataWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GyrodataWebService.ToolServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IToolService" in both code and config file together.
    [ServiceContract]
    public interface IToolService
    {
        [OperationContract]
        [WebGet(UriTemplate = "GetCustomItem/{id}",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        List<ItemModel> GetCustomItem(string id);

        [OperationContract]
        [WebGet(UriTemplate = "GetItemsComponents",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<string> GetItemsComponents();

        [OperationContract]
        [WebGet(UriTemplate = "GetItemsStoring",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        List<string> GetItemsStoring();

        [OperationContract]
        [WebGet(
            UriTemplate = "GetCustomItems?what={item}&where={where}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<ItemModel> GetCustomItems(string item, string where);

        // POST Method //
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddNewItem",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        int AddNewItem(ItemModel model);

        // POST Method //
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Check",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ItemModel AddCheck(ItemModel model);

        // TODO - remove this if no need
        // POST Method //
        //[OperationContract]
        //[WebInvoke(Method = "POST", UriTemplate = "AddNewItemImage",
        //    BodyStyle = WebMessageBodyStyle.Bare,
        //    RequestFormat = WebMessageFormat.Json,
        //    ResponseFormat = WebMessageFormat.Json)]
        //int AddNewItemImage(ItemModelImage model);

        //[OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "EditItem/{id}",
        BodyStyle = WebMessageBodyStyle.Bare,
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        void EditItem(string id, ItemModel model);

        // POST Method //
        [OperationContract]
        [WebGet(UriTemplate = "GetSelectedItem?item={selectedItem}&asset={selectedAsset}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<ItemModel> GetSelectedItem(string selectedItem, string selectedAsset);

        //LARAVEL
        [OperationContract]
        [WebGet(UriTemplate = "GetSelectedItemLRL?item={id}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<ItemModel> GetSelectedItem_lrl(int id);

        [OperationContract]
        [WebGet(UriTemplate = "GetJobsInvolvedIn?item={id}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<JobModel> GetJobsInvolvedIn(string id);
    }
}
