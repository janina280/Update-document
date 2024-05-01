using UpdateDocumentState.Stateless;

namespace UpdateDocumentState.Pages;

public partial class Index
{
    private DocumentState State => _document.CurrentState();
    private string _log = string.Empty;

    private readonly Document _document = new();

    private void FireEvent(DocumentCommand command)
    {
        _document.ExecuteTransition(command);
        _log += $"The document was {command.ToString()}!\n";
        StateHasChanged();
    }
}