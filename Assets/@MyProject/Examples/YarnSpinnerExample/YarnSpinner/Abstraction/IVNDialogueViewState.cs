using UnityEngine;

namespace MyProject.Examples.YarnSpinnerExample.YarnSpinner
{
    public interface IVNDialogueViewState
    {
        string NameText { get; }
        string DialogueText { get; }
        Color NameColor { get; }
    }
}