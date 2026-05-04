using Content.Shared._Maid.AuthPanel;

namespace Content.Client._Maid.AuthPanel;

public sealed class AuthPanelBoundUserInterface : BoundUserInterface
{
    private AuthPanelMenu? _menu;

    public AuthPanelBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey)
    {

    }

    protected override void Open()
    {
        base.Open();

        _menu = new AuthPanelMenu();

        _menu.OnRedButtonPressed(_ => SendButtonPressed(AuthPanelAction.ERTRecruit));

        _menu.OnClose += Close;
        _menu.OpenCentered();
    }

    public void SendButtonPressed(AuthPanelAction button)
    {
        SendMessage(new AuthPanelButtonPressedMessage(button, _menu?.GetReason()));
    }


    protected override void UpdateState(BoundUserInterfaceState state)
    {
        if (state is not AuthPanelConfirmationActionState confirmationActionState)
            return;

        var action = confirmationActionState.Action;

        if (action.Action is AuthPanelAction.ERTRecruit)
            _menu?.SetRedCount(action.ConfirmedPeopleCount, action.MaxConfirmedPeopleCount);

        _menu?.SetReason(action.Reason);
        if (action.ConfirmedPeopleCount == 0)
            _menu?.UnlockReason();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (!disposing)
            return;
        _menu?.Close();
    }
}
