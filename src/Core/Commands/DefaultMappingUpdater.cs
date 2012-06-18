namespace Guidelines.Core.Commands
{
	public class DefaultMappingUpdater<TCommand, TDomain> : IUpdateCommandHandler<TCommand, TDomain>
	{
		private readonly IMapper<TCommand, TDomain> _mapper;

		public DefaultMappingUpdater(IMapper<TCommand, TDomain> mapper)
		{
			_mapper = mapper;
		}

		public TDomain Update(TCommand command, TDomain workOn)
		{
			return _mapper.Map(command, workOn);
		}
	}
}