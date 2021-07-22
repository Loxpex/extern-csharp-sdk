using System;
using Kontur.Extern.Client.Common;

namespace Kontur.Extern.Client.Paths
{
    public readonly struct InventoryDocflowListPath
    {
        public InventoryDocflowListPath(Guid accountId, Guid docflowId, Guid documentId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            DocumentId = documentId;
            Services = services;
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        public Guid DocumentId { get; }
        public IExternClientServices Services { get; }

        public InventoryDocflowPath WithId(Guid inventoryId) => new(AccountId, DocflowId, DocumentId, inventoryId, Services);
    }
}