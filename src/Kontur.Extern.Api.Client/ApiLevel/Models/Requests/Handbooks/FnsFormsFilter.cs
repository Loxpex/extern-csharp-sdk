﻿using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Handbooks;

[PublicAPI]
public class FnsFormsFilter
{
    /// <summary>
    /// Код по КНД
    /// </summary>
    public string Knd { get; set; }

    /// <summary>
    /// Неотрицательное количество записей, которые нужно получить. Default = 100 (Если значение > 1000, оно понижается до 1000)
    /// </summary>
    public int Take { get; set; } = 100;

    /// <summary>
    /// Неотрицательное количество записей, которые нужно пропустить при считывании. Default = 0
    /// </summary>
    public int Skip { get; set; }
}