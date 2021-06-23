﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Client.Models.Api;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Docflows;
using Kontur.Extern.Client.Models.Documents;

namespace Kontur.Extern.Client.Clients.Docflows
{
    public interface IDocflowsClient
    {
        /// <summary>
        /// Поиск документооборотов по заданным параметрам
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="filter">Параметры поиска</param>
        /// <param name="timeout"></param>
        /// <returns>Список документооборотов</returns>
        Task<DocflowPage> GetDocflowsAsync(Guid accountId, DocflowFilter filter = null, TimeSpan? timeout = null);

        /// <summary>
        /// Получение связанных документооборотов
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="relatedDocflowId">Документооборот, к которому относится связанный документооборот</param>
        /// <param name="relatedDocumentId">Документ, к которому относится связанный документооборот</param>
        /// <param name="filter">Параметры поиска</param>
        /// <param name="timeout"></param>
        /// <returns>Список документооборотов</returns>
        Task<DocflowPage> GetRelatedDocflows(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            DocflowFilter filter = null,
            TimeSpan? timeout = null);

        /// <summary>
        /// Получение документооборота по идентификатору
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="timeout"></param>
        /// <returns>Документооборот</returns>
        Task<Docflow> GetDocflowAsync(Guid accountId, Guid docflowId, TimeSpan? timeout = null);

        /// <summary>
        /// Получение всех документов в документообороте
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="timeout"></param>
        /// <returns>Список документов из документооборота</returns>
        Task<List<Document>> GetDocumentsAsync(Guid accountId, Guid docflowId, TimeSpan? timeout = null);

        /// <summary>
        /// Получение документа из документооборота по его идентификатору
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="timeout"></param>
        /// <returns>Документ из документооборота</returns>
        Task<Document> GetDocumentAsync(Guid accountId, Guid docflowId, Guid documentId, TimeSpan? timeout = null);

        /// <summary>
        /// Получение описания документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="timeout"></param>
        /// <returns>Описание документа</returns>
        Task<DocflowDocumentDescription> GetDocumentDescriptionAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Получение списка подписей документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="timeout"></param>
        /// <returns>Подписи документа</returns>
        Task<List<Signature>> GetDocumentSignaturesAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Получение подписи документа по идентификатору
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="signatureId">Идентификатор подписи</param>
        /// <param name="timeout"></param>
        /// <returns>Подпись документа</returns>
        Task<Signature> GetSignatureAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Получение контента подписи документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="signatureId">Идентификатор подписи</param>
        /// <param name="timeout"></param>
        /// <returns>Контент подписи документа</returns>
        Task<byte[]> GetSignatureContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Печать документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="contentId">Идентификатор расшифрованного документа в сервисе контентов</param>
        /// <param name="timeout"></param>
        /// <returns>Результат печати с идентификатором печатной формы документа в сервисе контентов</returns>
        Task<PrintDocumentResult> PrintDocumentAsync(Guid accountId, Guid docflowId, Guid documentId, Guid contentId, TimeSpan? timeout = null);

        /// <summary>
        /// Печать документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="contentId">Идентификатор расшифрованного документа в сервисе контентов</param>
        /// <param name="timeout"></param>
        /// <returns>Задача печати документа</returns>
        Task<ApiTaskResult<PrintDocumentResult>> StartPrintDocumentAsync(Guid accountId, Guid docflowId, Guid documentId, Guid contentId, TimeSpan? timeout = null);

        /// <summary>
        /// Проверка статуса задачи печати по TaskId
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="taskId">Идентификатор задачи</param>
        /// <param name="timeout"></param>
        /// <returns>Задача печати документа</returns>
        Task<ApiTaskResult<PrintDocumentResult>> GetPrintTaskAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid taskId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Дешифрование контента документа Контур.Сертификатом
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="certificate">Сертификат</param>
        /// <param name="timeout"></param>
        /// <returns>Инициировано дешифрование документа</returns>
        Task<CloudDecryptionInitResult> StartCloudDecryptDocumentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            byte[] certificate,
            TimeSpan? timeout = null);

        /// <summary>
        /// Подтверждение дешифрования документа Контур.Сертификатом
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="requestId">Идентификатор запроса, который инициировал дешифрование контента документа</param>
        /// <param name="code">Код из смс</param>
        /// <param name="unzip">Флаг распаковки архива. По умолчанию false, контент возвращается сжатым</param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        Task<DecryptDocumentResult> ConfirmDocumentDecryptionAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            string requestId,
            string code,
            bool? unzip = null,
            TimeSpan? timeout = null);

        /// <summary>
        /// Дешифрование контента документа сертификатом DSS
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="certificate">Сертификат</param>
        /// <param name="unzip">Флаг необходимости распаковки архива. По умолчанию false, контент возвращается сжатым</param>
        /// <param name="timeout"></param>
        /// <returns>Инициировано дешифрование документа</returns>
        Task<DssDecryptionInitResult> StartDssDecryptDocumentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            byte[] certificate,
            bool? unzip = null,
            TimeSpan? timeout = null);

        /// <summary>
        /// Проверка статуса задачи дешифрования по TaskId
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="taskId">Идентификатор задачи</param>
        /// <param name="timeout"></param>
        /// <returns>Задача дешифрования документа</returns>
        Task<ApiTaskResult<DecryptDocumentResult>> GetDssDecryptDocumentTaskAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid taskId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Распознавание требования
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа требования с типом fns534-demand-attachment</param>
        /// <param name="contentId"></param>
        /// <param name="timeout"></param>
        /// <returns>Распознанные данные из файла требования</returns>
        Task<RecognizeResult> RecognizeDocumentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid contentId,
            TimeSpan? timeout = null);
    }
}