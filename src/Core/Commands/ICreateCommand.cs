using System;

namespace Guidelines.Core.Commands
{
	public interface ICreateCommand<TDomain, out TId>
		where TDomain : IIdentifiable<TId>
	{ }
}