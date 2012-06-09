using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Guidelines.Core;

namespace Guidelines.DataAccess.Mongo
{
	/// <summary>
	/// Mongo does not support this right now.  Could look into figuring out some kind of batching process.
	/// </summary>
	public class UnitOfWork : IUnitOfWork
	{
		public void Dispose()
		{
			
		}

		public void Begin()
		{
			
		}

		public void Commit()
		{
			
		}

		public void RollBack()
		{
			
		}
	}
}
