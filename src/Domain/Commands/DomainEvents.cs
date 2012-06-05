using System;
using System.Collections.Generic;
using System.Linq;
using Guidelines.Domain.Threading;

namespace Guidelines.Domain.Commands
{
    public static class DomainEvents
	{ 
		[ThreadStatic] //so that each thread has its own callbacks
		private static List<Delegate> _actions;

        public static IApplicationServiceProvider Container { get; set; }

		//Registers a callback for the given domain event
		public static void Register<T>(Action<T> callback)
		{
			if (_actions == null)
			{
				_actions = new List<Delegate>();
			}

			_actions.Add(callback);
		}

		//Clears callbacks passed to Register on the current thread
		public static void ClearCallbacks ()
		{
		   _actions = null;
		}

		//Raises the given domain event
        public static void Raise<T>(T args)
		{
			if (Container != null)
			{
                foreach (var handler in Container.GetServices<ICommandHandler<T>>())
				{
					handler.Execute(args);
				}
			}

			if (_actions != null)
			{
				foreach (var action in _actions)
				{
					if (action is Action<T>)
					{
						((Action<T>)action)(args);
					}
				}
			}
		}

        //Raises the given domain event
        public static IEnumerable<TResult> Raise<TInput, TResult>(TInput args)
        {
            IEnumerable<TResult> results = null;

            if (Container != null)
            {
                var queryHandler = Container.GetServices<IQueryHandler<TInput, TResult>>();

                results = queryHandler.Select(handler => handler.Execute(args));
            }

            return results ?? new List<TResult>();
        }

        //Raises the given domain event in its own thread
        public static void RaiseThreadedEvent<T>(T args)
        {
            if (Container != null)
            {
                foreach (var handler in Container.GetServices<ICommandHandler<T>>())
                {
                    ICommandHandler<T> threadHandler = handler;

                    ThreadLauncher.Launch(threadHandler.Execute, args);
                }
            }

            if (_actions != null)
            {
                foreach (var action in _actions.OfType<Action<T>>())
                {
                    var threadHandler = action;

                    ThreadLauncher.Launch(threadHandler, args);
                }
            }
        }
	} 
}
