using System;
using System.Collections.Generic;

namespace Guidelines.Core.Validation
{
    public class ValidationEngineException : Exception
    {
        public ValidationEngineException(IEnumerable<ValidationEngineMessage> errors)
        {
            Errors = errors;
        }

        public ValidationEngineException(IEnumerable<ValidationEngineMessage> errors, string message)
            : base(message)
        {
            Errors = errors;
        }

        public ValidationEngineException(IEnumerable<ValidationEngineMessage> errors, string message, Exception innerException)
            : base(message, innerException)
        {
            Errors = errors;
        }

        private IEnumerable<ValidationEngineMessage> _errors;
        public IEnumerable<ValidationEngineMessage> Errors
        {
            get { return _errors; }
            set { _errors = value ?? new List<ValidationEngineMessage>(); }
        }
    }
}
