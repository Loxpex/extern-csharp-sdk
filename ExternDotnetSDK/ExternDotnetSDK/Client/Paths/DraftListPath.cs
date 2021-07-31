using System;
using Kontur.Extern.Client.Common;

namespace Kontur.Extern.Client.Paths
{
    public readonly struct DraftListPath
    {
        public DraftListPath(Guid accountId, IExternClientServices services)
        {
            AccountId = accountId;
            Services = services;
        }

        public Guid AccountId { get; }
        public IExternClientServices Services { get; }

        public DraftPath WithId(Guid draftId) => new(AccountId, draftId, Services);
    }
}