using UnityEngine;

public class HelpHint : Window
{
    public const string CLOSED = "closed";

    public override void Hide()
    {
        base.Hide();
        EventManager.TriggerEvent(CLOSED);
    }
}