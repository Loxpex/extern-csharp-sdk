using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Helpers;
using Kontur.Extern.Api.Client.Model;
using Kontur.Extern.Api.Client.Model.DocflowFiltering;
using Kontur.Extern.Api.Client.Models.Docflows;
using Kontur.Extern.Api.Client.Models.Docflows.Documents;
using Kontur.Extern.Api.Client.Models.Docflows.Documents.Enums;
using Kontur.Extern.Api.Client.Models.Docflows.Documents.Replies;
using Kontur.Extern.Api.Client.Paths;
using Kontur.Extern.Api.Client.Primitives;

namespace Kontur.Extern.Api.Client
{
    [PublicAPI]
    public static class DocumentPathExtension
    {
        public static Task<Document?> TryGetAsync(this in DocumentPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Docflows.TryGetDocumentAsync(path.AccountId, path.DocflowId, path.DocumentId, timeout);
        }
        
        public static Task<Document> GetAsync(this in DocumentPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Docflows.GetDocumentAsync(path.AccountId, path.DocflowId, path.DocumentId, timeout);
        }
        
        public static IEntityList<IDocflow> RelatedDocflowsList(this in DocumentPath path, DocflowFilterBuilder? filterBuilder = null)
        {
            return DocflowListsHelper.DocflowsList(
                path.Services.Api,
                path.AccountId,
                path.DocflowId,
                path.DocumentId,
                filterBuilder,
                (apiClient, accountId, relatedDocflowId, relatedDocumentId, filter, tm) => apiClient.Docflows.GetRelatedDocflows(accountId, relatedDocflowId, relatedDocumentId, filter, tm)
            );
        }

        public static Task<ReplyDocument> GenerateReplyAsync(this in DocumentPath path, DocumentType documentType, CertificateContent certificate, TimeSpan? timeout = null)
        {
            if (documentType.IsEmpty)
                throw Errors.ValueShouldNotBeEmpty(nameof(documentType));
            var apiClient = path.Services.Api;
            return apiClient.Replies.GenerateReplyAsync(
                path.AccountId,
                path.DocflowId,
                path.DocumentId,
                documentType.ToUrn(),
                certificate.ToBytes(),
                timeout);
        }
    }
}