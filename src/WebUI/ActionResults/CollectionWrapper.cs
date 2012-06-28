using System.Collections;

namespace Guidelines.WebUI.ActionResults
{
	/// <summary>
	/// Used when sending data in a Json response to prevent Json hijacking.
	/// </summary>
	/// <see cref="DynamicView"/>
	public class CollectionWrapper
	{
		public IEnumerable Collection { get; set; }
	}
}