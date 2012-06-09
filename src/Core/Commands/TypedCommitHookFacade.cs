namespace Guidelines.Core.Commands
{
    public class TypedCommitHookFacade<TCommand> : ICommitHook
    {
        private readonly ICommitHook<TCommand> _commitHook;

        public TypedCommitHookFacade(ICommitHook<TCommand> commitHook)
        {
            _commitHook = commitHook;
        }

        public bool CommandIsEligible(object command)
        {
            return command is TCommand;
        }

        public void OnSuccessfulCommit(object command)
        {
            if (CommandIsEligible(command))
            {
                _commitHook.OnSuccessfulCommit((TCommand)command);
            }
        }
    }
}