namespace Guidelines.DataAccess.Mongo
{
	public interface IMongoSetup
	{
		/// <summary>
		/// Call to begin setup. Can only be run once.
		/// </summary>
		void TrySettingUp();
	}
}
