using System;

namespace Guidelines.Core.Commands.GuidExtensions
{
	public interface IGetCommand<TDomain> : IGetCommand<TDomain, Guid?>
		where TDomain : IIdentifiable<Guid?>
	{

	}
}