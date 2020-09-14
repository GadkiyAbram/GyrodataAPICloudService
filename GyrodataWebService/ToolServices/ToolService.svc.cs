using Dapper;
using GyrodataWebService.Business;
using GyrodataWebService.Database;
using GyrodataWebService.Interfaces;
using GyrodataWebService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace GyrodataWebService.ToolServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ToolService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ToolService.svc or ToolService.svc.cs at the Solution Explorer and start debugging.
    public class ToolService : IToolService
    {
        public ItemModel AddCheck(ItemModel model)
        {
            return model;
        }

        public int AddNewItem(ItemModel model)
        {
            var headers = OperationContext.Current.IncomingMessageProperties["httpRequest"];
            var token = ((HttpRequestMessageProperty)headers).Headers["Token"];

            using (var dbContext = new TokenDbContext())
            {
                ITokenValidator validator = new DatabaseTokenValidator(dbContext);
                if (validator.IsValid(token) == true)
                {
                    using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("GyrodataTracker")))
                    {
                        var p = new DynamicParameters();
                        p.Add("@Item", model.Item);
                        p.Add("@Asset", model.Asset);
                        p.Add("@Arrived", model.Arrived);
                        p.Add("@Invoice", model.Invoice);
                        p.Add("@CCD", model.CCD);
                        p.Add("@NameRus", model.NameRus);
                        p.Add("@PositionCCD", model.PositionCCD);
                        p.Add("@ItemStatus", model.ItemStatus);
                        p.Add("@Box", model.Box);
                        p.Add("@Container", model.Container);
                        p.Add("@Comment", model.Comment);
                        p.Add("@ItemImage", model.ItemImage);
                        p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                        connection.Execute("dbo.spItems_Insert", p, commandType: CommandType.StoredProcedure);

                        model.Id = p.Get<int>("@Id");
                    }
                }
            }
            return model.Id;
        }

        public void EditItem(string Id, ItemModel model)
        {
            int id = int.Parse(Id);
            var headers = OperationContext.Current.IncomingMessageProperties["httpRequest"];
            var token = ((HttpRequestMessageProperty)headers).Headers["Token"];

            using (var dbContext = new TokenDbContext())
            {
                ITokenValidator validator = new DatabaseTokenValidator(dbContext);
                if (validator.IsValid(token) == true)
                {
                    using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("GyrodataTracker")))
                    {
                        var p = new DynamicParameters();
                        p.Add("@Id", id);
                        p.Add("@ItemItem", model.Item);
                        p.Add("@ItemAsset", model.Asset);
                        p.Add("@ItemArrived", model.Arrived);
                        p.Add("@ItemInvoice", model.Invoice);
                        p.Add("@ItemCCD", model.CCD);
                        p.Add("@ItemNameRus", model.NameRus);
                        p.Add("@PositionCCD", model.PositionCCD);
                        p.Add("@ItemStatus", model.ItemStatus);
                        p.Add("@ItemBox", model.Box);
                        p.Add("@Container", model.Container);
                        p.Add("@Comment", model.Comment);
                        p.Add("@ItemImage", model.ItemImage);

                        connection.Execute("dbo.spUpdate_Item", p, commandType: CommandType.StoredProcedure);
                    }
                }
            }
        }

        // REMOVE IF NO NEED
        public List<ItemModel> GetCustomItem(string id)
        {
            int Id = Int32.Parse(id);
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString("GyrodataTracker")))
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

                var p = new DynamicParameters();

                p.Add("@itemId", Id);

                return connection.Query<ItemModel>("spGetCustomItemWithCirculation", p, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public List<ItemModel> GetCustomItems(string what, string where)
        {
            var headers = OperationContext.Current.IncomingMessageProperties["httpRequest"];
            var token = ((HttpRequestMessageProperty)headers).Headers["Token"];

            using (var dbContext = new TokenDbContext())
            {
                ITokenValidator validator = new DatabaseTokenValidator(dbContext);
                if (validator.IsValid(token) == true)
                {
                    using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString("GyrodataTracker")))
                    {
                        if (connection.State == ConnectionState.Closed) connection.Open();

                        var p = new DynamicParameters();

                        p.Add("@SearchWhat", what);
                        p.Add("@SearchWhere", where);

                        return connection.Query<ItemModel>("spGetCustomEquipmentData", p, commandType: CommandType.StoredProcedure).ToList();
                    }
                }
                return null;
            }
        }

        public List<string> GetItemsComponents()
        {
            var headers = OperationContext.Current.IncomingMessageProperties["httpRequest"];
            var token = ((HttpRequestMessageProperty)headers).Headers["Token"];

            using (var dbContext = new TokenDbContext())
            {
                ITokenValidator validator = new DatabaseTokenValidator(dbContext);
                if (validator.IsValid(token) == true)
                {
                    using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString("GyrodataTracker")))
                    {
                        if (connection.State == ConnectionState.Closed) connection.Open();

                        return connection.Query<string>("spGetItemsComponents", commandType: CommandType.StoredProcedure).ToList();
                    }
                }
                return null;
            }
        }

        public List<string> GetItemsStoring()
        {
            var headers = OperationContext.Current.IncomingMessageProperties["httpRequest"];
            var token = ((HttpRequestMessageProperty)headers).Headers["Token"];

            using (var dbContext = new TokenDbContext())
            {
                ITokenValidator validator = new DatabaseTokenValidator(dbContext);
                if (validator.IsValid(token) == true)
                {
                    using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString("GyrodataTracker")))
                    {
                        return connection.Query<string>("spGetItemsStoring", commandType: CommandType.StoredProcedure).ToList();
                    }
                }
                return null;
            }
        }

        public List<JobModel> GetJobsInvolvedIn(string id)
        {
            var headers = OperationContext.Current.IncomingMessageProperties["httpRequest"];
            var token = ((HttpRequestMessageProperty)headers).Headers["Token"];

            using (var dbContext = new TokenDbContext())
            {
                ITokenValidator validator = new DatabaseTokenValidator(dbContext);
                if (validator.IsValid(token) == true)
                {
                    using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString("GyrodataTracker")))
                    {
                        if (connection.State == ConnectionState.Closed) connection.Open();

                        var p = new DynamicParameters();

                        p.Add("@ItemId", id);

                        return connection.Query<JobModel>("spGetJobsInvolvedInData", p, commandType: CommandType.StoredProcedure).ToList();
                    }
                }
                return null;
            }
        }

        public List<ItemModel> GetSelectedItem(string selectedItem, string selectedAsset)
        {
            var headers = OperationContext.Current.IncomingMessageProperties["httpRequest"];
            var token = ((HttpRequestMessageProperty)headers).Headers["Token"];

            using (var dbContext = new TokenDbContext())
            {
                ITokenValidator validator = new DatabaseTokenValidator(dbContext);
                if (validator.IsValid(token) == true)
                {
                    using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString("GyrodataTracker")))
                    {
                        var p = new DynamicParameters();

                        p.Add("@SelectedItem", selectedItem);
                        p.Add("@SelectedAsset", selectedAsset);

                        return connection.Query<ItemModel>("spGetSelectedItemData", p, commandType: CommandType.StoredProcedure).ToList();
                    }
                }
                return null;
            }
        }

        //LARAVEL
        public List<ItemModel> GetSelectedItem_lrl(int id)
        {
            var headers = OperationContext.Current.IncomingMessageProperties["httpRequest"];
            var token = ((HttpRequestMessageProperty)headers).Headers["Token"];

            using (var dbContext = new TokenDbContext())
            {
                ITokenValidator validator = new DatabaseTokenValidator(dbContext);
                if (validator.IsValid(token) == true)
                {
                    using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString("GyrodataTracker")))
                    {
                        var p = new DynamicParameters();

                        p.Add("@Id", id);

                        return connection.Query<ItemModel>("spGetSelectedItemData_LRL", p, commandType: CommandType.StoredProcedure).ToList();
                    }
                }
                return null;
            }
        }
    }
}
