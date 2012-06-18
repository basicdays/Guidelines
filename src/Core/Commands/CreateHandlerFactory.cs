using System.Collections.Generic;
using System.Linq;

namespace Guidelines.Core.Commands
{
	public class CreateHandlerFactory<TCommand, TDomain> : ICreateHandlerFactory<TCommand, TDomain>
	{
		private readonly IEnumerable<ICreateCommandHandler<TCommand, TDomain>> _creators;

		public CreateHandlerFactory(IEnumerable<ICreateCommandHandler<TCommand, TDomain>> creator)
		{
			_creators = creator;
		}

		public ICreateCommandHandler<TCommand, TDomain> BuildCreator()
		{
			return _creators.Count() > 1
			       	? _creators.FirstOrDefault(updater => !(updater is DefaultMappingCreator<TCommand, TDomain>))
			       	: _creators.FirstOrDefault();
		}
	}
}