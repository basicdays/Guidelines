using System;

namespace Guidelines.Core.Bootstrap
{
	/// <summary>
	/// Represents errors that occur during bootstrapping.
	/// </summary>
	[Serializable]
	public class BootstrapException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BootstrapException"/>
		/// class.
		/// </summary>
		public BootstrapException() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="BootstrapException"/>
		/// class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public BootstrapException(string message) : base(message) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="BootstrapException"/>
		/// class with a specified error message and a reference to the inner
		/// exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause of the
		///		current exception, or a null reference (Nothing in Visual Basic)
		///		if no inner exception is specified.</param>
		public BootstrapException(string message, Exception innerException)
			: base(message, innerException) { }
	}
}