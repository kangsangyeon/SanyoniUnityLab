namespace MyProject.Examples.YarnSpinnerExample.YarnSpinner.Abstraction
{
    public interface IActorService
    {
        void CharCommand(string id, string appearance);
        void BackCommand();
        void DespawnCommand();
        void AnimateCommand();
        void ArrangeCommand();
    }
}