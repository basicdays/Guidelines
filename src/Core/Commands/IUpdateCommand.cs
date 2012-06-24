using System;

namespace Guidelines.Core.Commands
{
	public interface IUpdateCommand<TDomain, out TId>
		where TDomain : IIdentifiable<TId>
	{
		TId Id { get; }
	}
}