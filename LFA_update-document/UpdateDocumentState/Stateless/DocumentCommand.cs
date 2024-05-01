namespace UpdateDocumentState.Stateless;

public enum DocumentCommand
{
    ToNone,
    MakeChanges,
    BeginReview,
    Submit,
    Approve,
    ChangeNeeded,
    Reject,
    RestartReview,
    Decline,
    Accept
}