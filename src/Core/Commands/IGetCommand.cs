﻿using System;

namespace Guidelines.Core.Commands
{
	public interface IGetCommand<TDomain, out TId>
		where TDomain : IIdentifiable<TId>
	{
		TId Id { get; }
	}
}