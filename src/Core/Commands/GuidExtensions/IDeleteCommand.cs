using System;

namespace Guidelines.Core.Commands.GuidExtensions
{
	public interface IDeleteCommand<TDomain> : IDeleteCommand<TDomain, Guid?>
		where TDomain : IIdentifiable<Guid?>
	{

	}
}