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
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;

namespace GyrodataWebService.BatteryServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BatteryService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BatteryService.svc or BatteryService.svc.cs at the Solution Explorer and start debugging.
    public class BatteryService : IBatteryService
    {
        // NO TOKEN, JUST FOR TEST!!!
        public List<BatteryModel> GetAllBatteriesNotToken()
        {
            List<BatteryModel> batteryList = new List<BatteryModel>();

            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString("GyrodataTracker")))
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

                return connection.Query<BatteryModel>("spGetAllBatteries", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public List<BatteryModel> GetSelectedBatteries(string serial, string where)
        {
            List<BatteryModel> batteryList = new List<BatteryModel>();

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

                        p.Add("@SearchWhat", serial);
                        p.Add("@SearchWhere", where);

                        return connection.Query<BatteryModel>("spGetCustomBatteryData", p, commandType: CommandType.StoredProcedure).ToList();
                    }
                }
                return null;
            }
        }

        public int AddBattery(BatteryModel model)
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
                        p.Add("@BoxNumber", model.BoxNumber);
                        p.Add("@BatteryCondition", model.BatteryCondition);
                        p.Add("@SerialOne", model.SerialOne);
                        p.Add("@SerialTwo", model.SerialTwo);
                        p.Add("@SerialThr", model.SerialThr);
                        p.Add("@CCD", model.CCD);
                        p.Add("@Invoice", model.Invoice);
                        p.Add("@BatteryStatus", model.BatteryStatus);
                        p.Add("@Arrived", model.Arrived);
                        p.Add("@Container", model.Container);
                        p.Add("@Comment", model.Comment);
                        p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                        connection.Execute("dbo.spBatteries_Insert", p, commandType: CommandType.StoredProcedure);

                        model.Id = p.Get<int>("@Id");

                    }
                }
            }
            return model.Id;
        }

        public void EditBattery(string id, BatteryModel model)
        {
            int Id = int.Parse(id);
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

                        p.Add("@Id", Id);
                        p.Add("@BoxNumber", model.BoxNumber);
                        p.Add("@BatteryCondition", model.BatteryCondition);
                        p.Add("@SerialOne", model.SerialOne);
                        p.Add("@SerialTwo", model.SerialTwo);
                        p.Add("@SerialThr", model.SerialThr);
                        p.Add("@CCD", model.CCD);
                        p.Add("@Invoice", model.Invoice);
                        p.Add("@BatteryStatus", model.BatteryStatus);
                        p.Add("@Arrived", model.Arrived);
                        p.Add("@Container", model.Container);
                        p.Add("@Comment", model.Comment);

                        connection.Execute("spBattery_Update", p, commandType: CommandType.StoredProcedure);
                    }
                }
            }
        }

        public List<BatteryModel> GetSelectedBatteryData(string id)
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
                        //if (connection.State == ConnectionState.Closed) connection.Open();
                        var p = new DynamicParameters();

                        p.Add("@Id", id);

                        return connection.Query<BatteryModel>("spGetSelectedBatteryData", p, commandType: CommandType.StoredProcedure).ToList();
                    }
                }
                return null;
            }
        }

        public List<BatteryModel> GetSelectedBatteryDataBySerial(string serial)
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
                        //if (connection.State == ConnectionState.Closed) connection.Open();
                        var p = new DynamicParameters();

                        p.Add("@Serial", serial);

                        return connection.Query<BatteryModel>("spGetSelectedBatteryDataBySerial", p, commandType: CommandType.StoredProcedure).ToList();
                    }
                }
                return null;
            }
        }
    }
}