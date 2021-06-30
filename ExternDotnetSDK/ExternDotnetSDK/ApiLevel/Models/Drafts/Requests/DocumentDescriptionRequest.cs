﻿using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DocumentDescriptionRequest
    {
        /// <summary>
        /// Тип документа
        /// </summary>
        public Urn Type { get; set; }

        /// <summary>
        /// Имя файла
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Тип контента
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Код документа по справочнику СВДРЕГ
        /// </summary>
        public string SvdregCode { get; set; }

        /// <summary>
        /// Наименование файла документа
        /// </summary>
        public string OriginalFilename { get; set; }
    }
}