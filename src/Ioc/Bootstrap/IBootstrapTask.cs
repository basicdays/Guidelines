namespace Guidelines.Ioc.Bootstrap
{
	public interface IBootstrapTask
	{
		void Bootstrap();

		int Order { get; }
	}
}
