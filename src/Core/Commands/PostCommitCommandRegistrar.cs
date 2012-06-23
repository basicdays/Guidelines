using System;
using System.Collections.Generic;
using System.Linq;
using Guidelines.Core.Threading;

namespace Guidelines.Core.Commands
{
    public class PostCommitCommandRegistrar : IPostCommitCommandRegistrar, ICommitHook
    {
        [ThreadStatic] //so that each thread has its own callbacks
        private static Queue<Action> _actions;

        private readonly IApplicationServiceProvider _serviceProvider;

        public PostCommitCommandRegistrar(IApplicationServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private static Queue<Action> Actions
        {
            get { return _actions ?? (_actions = new Queue<Action>()); }
        } 

        private void HandleCommand<TCommand>(TCommand command)
        {
            foreach (var handler in _serviceProvider.GetServices<ICommandHandler<TCommand>>())
            {
                handler.Execute(command);
            }
        }

        private void ProcessCommand<TCommand>(TCommand command)
        {
            foreach (var handler in _serviceProvider.GetServices<ICommandProcessor<TCommand>>())
            {
                handler.Process(command);
            }
        }

        private void ExecuteCommand<TCommand>(TCommand command, bool noUnitOfWork)
        {
            if(noUnitOfWork)
            {
                HandleCommand(command);
            }
            else
            {
                ProcessCommand(command);
            }
        }

        private void ProcessCommand<TCommand>(TCommand command, bool asThreadedAction, bool noUnitOfWork)
        {
            if (asThreadedAction)
            {
                ThreadLauncher.Launch(() => ExecuteCommand(command, noUnitOfWork));
            }
            else
            {
                ExecuteCommand(command, noUnitOfWork);
            }
        }

        public void RegisterPostCommitCommand<TCommand>(TCommand command, bool asThreadedAction = false, bool noUnitOfWork = false)
        {
            Actions.Enqueue(() => ProcessCommand(command, asThreadedAction, noUnitOfWork));
        }

        public void OnSuccessfulCommit(object commandMessage)
        {
            //create a new list to loop through.
            var actions = new List<Action>(Actions);

            //clear the master list so we don't wind up re-running a post action from inside the post action.
            Actions.Clear();

            //take the actions.
            foreach (var action in actions)
            {
                action();
            }
        }

        public bool CommandIsEligible(object command)
        {
            return Actions.Any();
        }
    }
}