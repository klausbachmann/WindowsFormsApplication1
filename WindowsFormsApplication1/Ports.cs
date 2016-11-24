using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApplication1
{
    internal class Trips
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("MarketingName")]
        public string MarketingName { get; set; }

        public string actual { get; set; }
        public string verify { get; set; }

    }


    internal class Routes {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("ParentRegionCode")]
        public string ParentRegionCode { get; set; }
    }

    internal class Cabin
    {

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("CabinNo")]
        public string CabinNo { get; set; }

        [JsonProperty("CategoryId")]
        public int CategoryId { get; set; }

        [JsonProperty("Equipment")]
        public Equipment2[] Equipment { get; set; }

        [JsonProperty("MaximumOccupation")]
        public int MaximumOccupation { get; set; }

        [JsonProperty("View")]
        public string View { get; set; }
    }

    internal class Equipment2
    {

        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("Group")]
        public int Group { get; set; }

        [JsonProperty("Value")]
        public string Value { get; set; }
    }

    internal class Port
    {

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Airports")]
        public string Airports { get; set; }

        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("CountryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("CountryRegion")]
        public string CountryRegion { get; set; }

        [JsonProperty("GeoLatitude")]
        public string GeoLatitude { get; set; }

        [JsonProperty("GeoLongitude")]
        public string GeoLongitude { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
    }

}





