using System;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class FssSickReportDescription : DocflowDescription
    {
        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion FormVersion { get; set; }
        /// <summary>
        /// Регистрационный номер
        /// </summary>
        public string RegistrationNumber { get; set; }
        /// <summary>
        /// Идентификатор реестра на портале ФСС
        /// </summary>
        public string FssId { get; set; }
        /// <summary>
        /// Описание стадии обработки, на которой находится реестр
        /// </summary>
        public string FssStageDescription { get; set; }
        /// <summary>
        /// Код ошибки
        /// </summary>
        public string FssStageErrorCode { get; set; }
        /// <summary>
        /// Описание ошибки
        /// </summary>
        public string FssStageErrorExtend { get; set; }
        /// <summary>
        ///  Текущая стадия обработки реестра
        /// </summary>
        public string FssStageType { get; set; }
        /// <summary>
        /// Статус текущей стадии обработки отчета
        /// </summary>
        public string FssStageStatus { get; set; }
        /// <summary>
        /// Дата перехода реестра в текущую стадию обработки
        /// </summary>
        public DateTime? FssStageDate { get; set; }
    }
}