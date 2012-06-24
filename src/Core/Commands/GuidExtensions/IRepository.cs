using System;

namespace Guidelines.Core.Commands.GuidExtensions
{
	public interface IRepository<TDomain> : IRepository<TDomain, Guid?>
		where TDomain : IIdentifiable<Guid?>
	{ }
}
