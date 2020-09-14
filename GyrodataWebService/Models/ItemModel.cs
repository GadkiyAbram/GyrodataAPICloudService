using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GyrodataWebService.Models
{
    [DataContract]
    public class ItemModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Item { get; set; }
        [DataMember]
        public string Asset { get; set; }
        [DataMember]
        public string Arrived { get; set; }
        [DataMember]
        public string Invoice { get; set; }
        [DataMember]
        public string CCD { get; set; }
        [DataMember]
        public float Circulation { get; set; }
        [DataMember]
        public string NameRus { get; set; }
        [DataMember]
        public string PositionCCD { get; set; }
        [DataMember]
        public string ItemStatus { get; set; }
        [DataMember]
        public string Box { get; set; }
        [DataMember]
        public string Container { get; set; }
        [DataMember]
        public string Comment { get; set; }
        [DataMember]
        public string ItemImage { get; set; }
        [DataMember]
        public string Created_at { get; set; }
        [DataMember]
        public string Updated_at { get; set; }
    }
}