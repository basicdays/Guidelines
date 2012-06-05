namespace Guidelines.Domain.Commands
{
    public interface IPostCommitCommandRegistrar
    {
        void RegisterPostCommitCommand<TCommand>(TCommand command, bool asThreadedAction = false, bool noUnitOfWork = false);
    }
}