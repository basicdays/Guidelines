using System.Collections.Generic;
using System.Linq;

namespace Guidelines.Core.Commands
{
	public class UpdateHandlerFactory<TCommand, TDomain> : IUpdateHandlerFactory<TCommand, TDomain>
	{
		private readonly IEnumerable<IUpdateCommandHandler<TCommand, TDomain>> _updater;

		public UpdateHandlerFactory(IEnumerable<IUpdateCommandHandler<TCommand, TDomain>> updater)
		{
			_updater = updater;
		}

		public IUpdateCommandHandler<TCommand, TDomain> BuildUpdater()
		{
			return _updater.Count() > 1 
			       	? _updater.FirstOrDefault(updater => !(updater is DefaultMappingUpdater<TCommand, TDomain>)) 
			       	: _updater.FirstOrDefault();
		}
	}
}