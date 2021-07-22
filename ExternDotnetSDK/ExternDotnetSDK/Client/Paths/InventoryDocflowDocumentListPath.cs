using System;
using Kontur.Extern.Client.Common;

namespace Kontur.Extern.Client.Paths
{
    public readonly struct InventoryDocflowDocumentListPath
    {
        public InventoryDocflowDocumentListPath(Guid accountId, Guid docflowId, Guid documentId, Guid inventoryId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            DocumentId = documentId;
            InventoryId = inventoryId;
            Services = services;
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        public Guid DocumentId { get; }
        public Guid InventoryId { get; }
        public IExternClientServices Services { get; }

        public InventoryDocflowDocumentPath WithId(Guid inventoryDocumentId) => new(AccountId, DocflowId, DocumentId, InventoryId, inventoryDocumentId, Services);
    }
}