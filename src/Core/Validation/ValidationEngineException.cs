using System;
using System.Collections.Generic;
using System.Text;
using Guidelines.Core.Properties;

namespace Guidelines.Core.Validation
{
    public class ValidationEngineException : Exception
    {
        public ValidationEngineException(IEnumerable<ValidationEngineMessage> errors)
			:base(GetMessage(errors))
        {
            Errors = errors;
        }

        public ValidationEngineException(IEnumerable<ValidationEngineMessage> errors, Exception innerException)
            : base(GetMessage(errors), innerException)
        {
            Errors = errors;
        }

        private IEnumerable<ValidationEngineMessage> _errors;
        public IEnumerable<ValidationEngineMessage> Errors
        {
            get { return _errors; }
            set { _errors = value ?? new List<ValidationEngineMessage>(); }
        }

		private static string GetMessage(IEnumerable<ValidationEngineMessage> errors)
		{
			var builder = new StringBuilder();
			builder.AppendLine(Resources.Error_ValidationError);
			foreach (var error in errors) {
				builder.AppendFormat("\t[{0}] {1} - {2}\n", error.Severity, error.Name, error.Message);
			}
			return builder.ToString();
		}
    }
}
