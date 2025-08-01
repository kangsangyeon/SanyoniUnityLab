using System.Threading;
using Yarn.Unity;

namespace MyProject.Examples.YarnSpinnerExample.YarnSpinner
{
    public class VNDialoguePresenter : DialoguePresenterBase
    {
        public override YarnTask RunLineAsync(
            LocalizedLine line,
            LineCancellationToken token)
        {
            // ICharActor charActor = null;
            // if (line.CharacterName != null)
            //     charActor = _charActors[line.CharacterName];
            return YarnTask.CompletedTask;
        }

        public override YarnTask<DialogueOption> RunOptionsAsync(
            DialogueOption[] dialogueOptions,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public override YarnTask OnDialogueStartedAsync()
        {
            throw new System.NotImplementedException();
        }

        public override YarnTask OnDialogueCompleteAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}