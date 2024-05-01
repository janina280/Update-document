namespace UpdateDocumentState.Stateless;

public enum DocumentState
{
    None,
    Draft,
    Review,
    ChangeRequested,
    Submitted,
    Declined,
    Approved,
}