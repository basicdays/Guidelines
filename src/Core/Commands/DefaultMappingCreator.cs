namespace Guidelines.Core.Commands
{
	public class DefaultMappingCreator<TCommand, TDomain> : ICreateCommandHandler<TCommand, TDomain>
	{
		private readonly IMapper<TCommand, TDomain> _mapper;

		public DefaultMappingCreator(IMapper<TCommand, TDomain> mapper)
		{
			_mapper = mapper;
		}

		public TDomain Create(TCommand command)
		{
			return _mapper.Map(command);
		}
	}
}