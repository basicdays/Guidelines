using System;

namespace Guidelines.Core.Commands.GuidExtensions
{
	public interface ICreateCommand<TDomain> : ICreateCommand<TDomain, Guid?>
		where TDomain : IIdentifiable<Guid?>
	{ }
}