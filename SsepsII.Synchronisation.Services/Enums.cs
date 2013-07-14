using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SsepsII.Synchronisation.Services
{
    public enum SiteType : int
    {
        None = 0,
        Client = 1,
        Server = 2
    }

    public enum SyncEventType
    {
        None,
        Enrollment,
        Promotion,
        Demotion,
        Increment,
        Acting
    }

    public enum SyncType
    {
        None = 0,
        Consolidate = 1,
        Approval = 2
    }

    public enum SyncState : int
    {
        Normal = 0,
        Approved = 1,
        Rejected = 2,
        ConsolidateOnly = 3,
        SyncedConsolidateOnly = 4,
        Exported = 5
    }

    public enum TransferState : int
    {
        SourceCreatedTransfer = 0,
        MoLPSApproved = 1,
        MoLPSRejected = 2,
        EnrolledDestination = 3,
        RejectedDestination = 4

    }
}