﻿using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.DraftsBuilders.Builders.Data
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public abstract class DraftsBuilderData
    {
    }
}