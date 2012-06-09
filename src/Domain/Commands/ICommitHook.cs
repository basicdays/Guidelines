namespace Guidelines.Core.Commands
{
    public interface ICommitHook
    {
        void OnSuccessfulCommit(object commandMessage);
        bool CommandIsEligible(object command);
    }

    public interface ICommitHook<TCommand>
    {
        void OnSuccessfulCommit(TCommand command);
    }
}
