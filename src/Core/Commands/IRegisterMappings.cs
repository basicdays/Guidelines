using System;

namespace Guidelines.Core.Commands
{
	public interface IRegisterMappings
	{
		Type SourceType { get; }
		Type DestinationType { get; }

		KeyGenerationMethod KeyGenerationMethod { get; }
	}
}