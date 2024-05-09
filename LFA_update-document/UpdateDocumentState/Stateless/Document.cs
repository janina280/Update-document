using System;
using Blazorise.States;
using System.Reflection.PortableExecutable;
using Stateless;

namespace UpdateDocumentState.Stateless;

public class Document
{
    private readonly StateMachine<DocumentState, DocumentCommand> _machine;
    public Document()
    {
        _machine = new StateMachine<DocumentState, DocumentCommand>(DocumentState.None);

        _machine.Configure(DocumentState.None)
            .Permit(DocumentCommand.MakeChanges, DocumentState.Draft);

        _machine.Configure(DocumentState.Draft)
            .Permit(DocumentCommand.BeginReview, DocumentState.Review)
            .Permit(DocumentCommand.Decline, DocumentState.Declined);


        _machine.Configure(DocumentState.Review)
            .Permit(DocumentCommand.Submit, DocumentState.Submitted)
            .Permit(DocumentCommand.ChangeNeeded, DocumentState.ChangeRequested);

        _machine.Configure(DocumentState.Submitted)
            .Permit(DocumentCommand.Decline, DocumentState.Declined)
            .Permit(DocumentCommand.Approve, DocumentState.Approved);
            
        _machine.Configure(DocumentState.Declined)
            .Permit(DocumentCommand.RestartReview, DocumentState.Review)
            .Permit(DocumentCommand.Close, DocumentState.Closed);

        _machine.Configure(DocumentState.ChangeRequested)
            .Permit(DocumentCommand.Reject, DocumentState.Review)
            .Permit(DocumentCommand.Accept, DocumentState.Draft);
    }
    public DocumentState ExecuteTransition(DocumentCommand command)
    {
        if (_machine.CanFire(command))
        {
            _machine.Fire(command);
        }
        else
        {
            throw new InvalidOperationException($"Cannot transition from {nameof(CurrentState)} via {command}");
        }

        return CurrentState();
    }

    public DocumentState CurrentState()
    {
        if (_machine != null)
            return _machine.State;
        else
            throw new InvalidOperationException("Car hasn't been configured yet.");
    }

    public bool CanFireCommand(DocumentCommand command)
    {
        return _machine.CanFire(command);
    }
}