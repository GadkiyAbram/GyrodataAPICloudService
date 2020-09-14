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
using System.Web;

namespace GyrodataWebService.JobServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "JobServices" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select JobServices.svc or JobServices.svc.cs at the Solution Explorer and start debugging.
    public class JobService : IJobService
    {
        public List<List<string>> GetAllDataForJobCreate()
        {
            List<List<string>> initialJobData = new List<List<string>>();

            var headers = OperationContext.Current.IncomingMessageProperties["httpRequest"];
            var token = ((HttpRequestMessageProperty)headers).Headers["Token"];

            using (var dbContext = new TokenDbContext())
            {
                ITokenValidator validator = new DatabaseTokenValidator(dbContext);
                if (validator.IsValid(token) == true)
                {
                    initialJobData.Add(GetClientsData());
                    initialJobData.Add(GetGdpData());
                    initialJobData.Add(GetModemData());
                    initialJobData.Add(GetBbpData());
                    initialJobData.Add(GetEngineerData());
                    initialJobData.Add(GetBatteriesData());
                }
            }
            return initialJobData;
        }

        //public Dictionary<string, List<string>> GetAllDataForJobCreate()
        //{
        //    Dictionary<string, List<string>> initialJobData = new Dictionary<string, List<string>>();
        //    initialJobData.Add("Clients", GetClientsData());
        //    //initialJobData.Add("GDP", GetGdpData());
        //    //initialJobData.Add(GetModemData());
        //    //initialJobData.Add(GetBbpData());
        //    //initialJobData.Add(GetEngineerData());
        //    //initialJobData.Add(GetBatteriesData());

        //    return initialJobData;
        //}

        //******* LOADING DATA FOR JOB CREATION
        // Loading Clients
        private List<string> GetClientsData()
        {
            List<ClientModel> clientModels = new List<ClientModel>();
            List<string> clients = new List<string>();
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("GyrodataTracker")))
            {
                clientModels = connection.Query<ClientModel>("spGetClientsData", commandType: CommandType.StoredProcedure).ToList();
            }
            foreach (ClientModel model in clientModels)
            {
                clients.Add(model.ClientName);
            }
            return clients;
        }

        // Loading GDP Data
        private List<string> GetGdpData()
        {
            List<ItemModel> gdpModels = new List<ItemModel>();
            List<string> gdps = new List<string>();
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("GyrodataTracker")))
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

                gdpModels = connection.Query<ItemModel>("spGetAllGdpData", commandType: CommandType.StoredProcedure).ToList();
            }
            foreach (ItemModel model in gdpModels)
            {
                gdps.Add(model.Asset);
            }
            return gdps;
        }

        // Loading Modem from DB
        private List<string> GetModemData()
        {
            List<ItemModel> modemModels = new List<ItemModel>();
            List<string> modems = new List<string>();
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("GyrodataTracker")))
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

                modemModels = connection.Query<ItemModel>("spGetAllModemData", commandType: CommandType.StoredProcedure).ToList();
            }
            foreach (ItemModel model in modemModels)
            {
                modems.Add(model.Asset);
            }
            return modems;
        }

        // Loading Bbp from DB
        private List<string> GetBbpData()
        {
            List<ItemModel> bbpModels = new List<ItemModel>();
            List<string> bbps = new List<string>();
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("GyrodataTracker")))
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

                bbpModels = connection.Query<ItemModel>("spGetAllBbpData", commandType: CommandType.StoredProcedure).ToList();
            }
            foreach (ItemModel model in bbpModels)
            {
                bbps.Add(model.Asset);
            }
            return bbps;
        }

        // Loading Engineers from DB
        private List<string> GetEngineerData()
        {
            List<EngineerModel> engineerModels = new List<EngineerModel>();
            List<string> engineers = new List<string>();
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("GyrodataTracker")))
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

                engineerModels = connection.Query<EngineerModel>("spGetEngineerData", commandType: CommandType.StoredProcedure).ToList();
            }
            foreach (EngineerModel model in engineerModels)
            {
                engineers.Add(model.EngineerName);
            }
            return engineers;
        }
        // TODO - place BatteryModel to Models folder
        // Loading Batteries Records from DB
        private List<string> GetBatteriesData()
        {
            List<BatteryModel> batteryModels = new List<BatteryModel>();
            List<string> batteries = new List<string>();
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("GyrodataTracker")))
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

                batteryModels = connection.Query<BatteryModel>("spGetAllBatteriesNew", commandType: CommandType.StoredProcedure).ToList();
            }
            foreach (BatteryModel model in batteryModels)
            {
                batteries.Add(model.SerialOne);
            }
            return batteries;
        }
        // END LOADING DATA FOR JOB CREATION *******

        // Loading Custom Jobs based on what and where
        public List<JobModel> GetCustomJobData(string what, string where)
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
                        if (connection.State == ConnectionState.Closed) connection.Open();

                        var p = new DynamicParameters();

                        p.Add("@SearchWhat", what);
                        p.Add("@SearchWhere", where);

                        return connection.Query<JobModel>("spGetCustomJobData", p, commandType: CommandType.StoredProcedure).ToList();
                        // TODO - remove further
                        //return connection.Query<JobModel>("spGetCustomJobsDataANDROID", p, commandType: CommandType.StoredProcedure).ToList();
                    }
                }
                return null;
            }
        }

        // Add New Job
        public int AddNewJob(JobModel model)
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
                        p.Add("@JobNumber", model.JobNumber);
                        p.Add("@Client", model.ClientName);
                        p.Add("@GDPAsset", model.GDP);
                        p.Add("@ModemAsset", model.Modem);
                        p.Add("@ModemVersion", model.ModemVersion);
                        p.Add("@BullplugAsset", model.Bullplug);
                        p.Add("@CirculationHours", model.CirculationHours);
                        p.Add("@Battery", model.Battery);
                        p.Add("@MaxTemp", model.MaxTemp);
                        p.Add("@EngineerOne", model.EngineerOne);
                        p.Add("@EngineerTwo", model.EngineerTwo);
                        p.Add("@EngineerOneArrived", model.eng_one_arrived);
                        p.Add("@EngineerTwoArrived", model.eng_two_arrived);
                        p.Add("@EngineerOneLeft", model.eng_one_left);
                        p.Add("@EngineerTwoLeft", model.eng_two_left);
                        p.Add("@Container", model.Container);
                        p.Add("@ContainerArrived", model.ContainerArrived);
                        p.Add("@ContainerLeft", model.ContainerLeft);
                        p.Add("@Rig", model.Rig);
                        p.Add("@Issues", model.Issues);
                        p.Add("@Comment", model.Comment);
                        p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                        connection.Execute("dbo.spJob_Insert", p, commandType: CommandType.StoredProcedure);

                        model.Id = p.Get<int>("@Id");
                    }
                }
            }
            return model.Id;
        }

        public List<JobModel> GetAllJobs()
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString("GyrodataTracker")))
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

                return connection.Query<JobModel>("spGetAllJobsTEST", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public void EditJob(string Id, JobModel model)
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
                        p.Add("@JobNumber", model.JobNumber);
                        p.Add("@Client", model.ClientName);
                        p.Add("@gdp", model.GDP);
                        p.Add("@modem", model.Modem);
                        p.Add("@ModemVersion", model.ModemVersion);
                        p.Add("@bbp", model.Bullplug);
                        p.Add("@newJobCirculation", model.CirculationHours);
                        p.Add("@Battery", model.Battery);
                        p.Add("@MaxTemp", model.MaxTemp);
                        p.Add("@EngineerOne", model.EngineerOne);
                        p.Add("@EngineerTwo", model.EngineerTwo);
                        p.Add("@EngineerOneArrived", model.eng_one_arrived);
                        p.Add("@EngineerTwoArrived", model.eng_two_arrived);
                        p.Add("@EngineerOneLeft", model.eng_one_left);
                        p.Add("@EngineerTwoLeft", model.eng_two_left);
                        p.Add("@Container", model.Container);
                        p.Add("@ContainerArrived", model.ContainerArrived);
                        p.Add("@ContainerLeft", model.ContainerLeft);
                        p.Add("@Rig", model.Rig);
                        p.Add("@Issues", model.Issues);
                        p.Add("@Comment", model.Comment);

                        connection.Execute("dbo.spUpdate_Job", p, commandType: CommandType.StoredProcedure);
                    }
                }
            }
        }

        public List<JobModel> GetSelectedJobData(string jobnumber)
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

                        p.Add("@SelectedJob", jobnumber);

                        return connection.Query<JobModel>("spGetSelectedJobData", p, commandType: CommandType.StoredProcedure).ToList();
                    }
                }
                return null;
            }
        }
    }
}
