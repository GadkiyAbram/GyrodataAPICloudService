using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GyrodataWebService.Models
{
    [DataContract]
    public class JobModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string JobNumber { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public string GDP { get; set; }
        [DataMember]
        public string Modem { get; set; }
        [DataMember]
        public string ModemVersion { get; set; }
        [DataMember]
        public string Bullplug { get; set; }
        [DataMember]
        public float CirculationHours { get; set; }
        [DataMember]
        public float OldCirculation { get; set; }
        [DataMember]
        public string Battery { get; set; }
        [DataMember]
        public string MaxTemp { get; set; }
        [DataMember]
        public string EngineerOne { get; set; }
        [DataMember]
        public string EngineerTwo { get; set; }
        [DataMember]
        public string eng_one_arrived { get; set; }
        [DataMember]
        public string eng_two_arrived { get; set; }
        [DataMember]
        public string eng_one_left { get; set; }
        [DataMember]
        public string eng_two_left { get; set; }
        [DataMember]
        public string Container { get; set; }
        [DataMember]
        public string ContainerArrived { get; set; }
        [DataMember]
        public string ContainerLeft { get; set; }
        [DataMember]
        public string Rig { get; set; }
        [DataMember]
        public string Issues { get; set; }
        [DataMember]
        public string Comment { get; set; }
        [DataMember]
        public string Created_at { get; set; }
        [DataMember]
        public string Updated_at { get; set; }
    }
}