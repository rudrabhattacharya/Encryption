using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace QuickType
{
    

    public partial class CallObject
    {
        [JsonProperty("Request")]
        public Request Request { get; set; }
    }

    public partial class Request
    {
        [JsonProperty("Header")]
        public Header Header { get; set; }

        [JsonProperty("Body")]
        public Body Body { get; set; }
    }

    public partial class Body
    {
        [JsonProperty("UUID")]
        public string Uuid { get; set; }

        [JsonProperty("AccountNumber")]
        public string AccountNumber { get; set; }

        [JsonProperty("Amount")]
        public long Amount { get; set; }

        [JsonProperty("LienID")]
        public string LienId { get; set; }

        [JsonProperty("Remarks")]
        public string Remarks { get; set; }
    }

    public partial class Header
    {
        [JsonProperty("Timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("ChannelDetails")]
        public ChannelDetails ChannelDetails { get; set; }

        [JsonProperty("DeviceDetails")]
        public DeviceDetails DeviceDetails { get; set; }
    }

    public partial class ChannelDetails
    {
        [JsonProperty("ChannelID")]
        public string ChannelId { get; set; }

        [JsonProperty("ChannelType")]
        public string ChannelType { get; set; }

        [JsonProperty("ChannelSubClass")]
        public string ChannelSubClass { get; set; }

        [JsonProperty("BranchCode")]
        public string BranchCode { get; set; }

        [JsonProperty("ChannelCusHdr")]
        public ChannelCusHdr ChannelCusHdr { get; set; }
    }

    public partial class ChannelCusHdr
    {
        [JsonProperty("ChannelProtocol")]
        public string ChannelProtocol { get; set; }
    }

    public partial class DeviceDetails
    {
        [JsonProperty("DeviceID")]
        public string DeviceId { get; set; }

        [JsonProperty("IMEINumber")]
        public string ImeiNumber { get; set; }

        [JsonProperty("ClientIP")]
        public string ClientIp { get; set; }

        [JsonProperty("OS")]
        public string Os { get; set; }

        [JsonProperty("BrowserType")]
        public string BrowserType { get; set; }

        [JsonProperty("MobileNumber")]
        public string MobileNumber { get; set; }

        [JsonProperty("GeoLocation")]
        public GeoLocation GeoLocation { get; set; }
    }

    public partial class GeoLocation
    {
        [JsonProperty("Latitude")]
        public string Latitude { get; set; }

        [JsonProperty("Longitude")]
        public string Longitude { get; set; }
    }
}
