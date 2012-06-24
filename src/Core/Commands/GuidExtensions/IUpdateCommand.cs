using System;

namespace Guidelines.Core.Commands.GuidExtensions
{
	public interface IUpdateCommand<TDomain> : IUpdateCommand<TDomain, Guid?>
		where TDomain : IIdentifiable<Guid?>
	{

	}
}