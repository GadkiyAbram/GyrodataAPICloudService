using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GyrodataWebService.Models
{
    [DataContract]
    public class BatteryModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string BoxNumber { get; set; }
        [DataMember]
        public string BatteryCondition { get; set; }
        [DataMember]
        public string SerialOne { get; set; }
        [DataMember]
        public string SerialTwo { get; set; }
        [DataMember]
        public string SerialThr { get; set; }
        [DataMember]
        public string CCD { get; set; }
        [DataMember]
        public string Invoice { get; set; }
        [DataMember]
        public string BatteryStatus { get; set; }
        [DataMember]
        public string Arrived { get; set; }
        [DataMember]
        public string Container { get; set; }
        [DataMember]
        public string Comment { get; set; }
        [DataMember]
        public string Created_at { get; set; }
        [DataMember]
        public string Updated_at { get; set; }
    }
}