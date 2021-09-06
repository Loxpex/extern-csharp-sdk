﻿using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Requests.Organizations
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class CreateOrganizationRequest
    {
        /// <summary>
        /// ИНН организации
        /// </summary>
        //[Required]
        public string Inn { get; set; }
        
        /// <summary>
        /// КПП организации
        /// </summary>
        public string Kpp { get; set; }
        
        /// <summary>
        /// Название организации
        /// </summary>
        //[Required]
        public string Name { get; set; }
    }
}