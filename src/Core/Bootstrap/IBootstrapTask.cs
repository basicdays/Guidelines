namespace Guidelines.Core.Bootstrap
{
	public interface IBootstrapTask
	{
		void Bootstrap();

		int Order { get; }
	}
}
