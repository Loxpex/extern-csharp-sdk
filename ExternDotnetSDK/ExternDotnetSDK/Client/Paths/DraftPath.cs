using System;
using Kontur.Extern.Client.Common;

namespace Kontur.Extern.Client.Paths
{
    public readonly struct DraftPath
    {
        public DraftPath(Guid accountId, Guid draftId, IExternClientServices services)
        {
            AccountId = accountId;
            DraftId = draftId;
            Services = services;
        }

        public Guid AccountId { get; }
        public Guid DraftId { get; }
        public IExternClientServices Services { get; }
    }
}