namespace Guidelines.Core.Commands
{
    public interface IPostCommitCommandRegistrar
    {
        void RegisterPostCommitCommand<TCommand>(TCommand command, bool asThreadedAction = false, bool noUnitOfWork = false);
    }
}