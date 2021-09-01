﻿using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests
{
    /// <summary>
    /// Реквизиты, специфичные для ЮЛ
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class OrganizationInfoRequest
    {
        private string kpp;

        /// <summary>
        /// КПП
        /// </summary>
        public string Kpp
        {
            get => kpp;
            set => kpp = string.IsNullOrEmpty(value) ? null : value;
        }
    }
}