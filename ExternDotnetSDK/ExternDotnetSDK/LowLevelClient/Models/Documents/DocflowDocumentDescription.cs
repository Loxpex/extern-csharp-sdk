﻿using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Documents.Requisites;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Documents
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DocflowDocumentDescription
    {
        public Urn Type { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public bool Compressed { get; set; }
        public DocflowDocumentRequisites Requisites { get; set; }
        public long? RelatedDocflowsCount { get; set; }
        public bool SupportRecognition { get; set; }
    }
}