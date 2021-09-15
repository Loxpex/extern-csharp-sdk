﻿using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class SenderRequest
    {
        /// <summary>
        /// ИНН
        /// </summary>
        //[Required]
        public string Inn { get; set; } = null!;

        /// <summary>
        /// КПП
        /// </summary>
        public string? Kpp { get; set; }

        /// <summary>
        /// Сертификат для отправки
        /// </summary>
        // [Required]
        public CertificateRequest Certificate { get; set; } = null!;

        /// <summary>
        /// Отправитель является представителем
        /// </summary>
        //[Required]
        public bool IsRepresentative { get; set; }

        /// <summary>
        /// IP адрес отправителя отчета
        /// </summary>
        [JsonPropertyName("ipaddress")]
        public string? IpAddress { get; set; }
    }
}