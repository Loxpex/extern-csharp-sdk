﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Drafts.Meta
{
    [DataContract]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class Certificate
    {
        [DataMember]
        [Required]
        public string Content { get; set; }
    }
}