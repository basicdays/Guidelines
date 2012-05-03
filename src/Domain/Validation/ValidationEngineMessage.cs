namespace Guidelines.Domain.Validation
{
    /// <summary>
    /// Contains the data on the part that failed validation, a message describing the reason, and severity of the error.
    /// </summary>
    public class ValidationEngineMessage
    {
        /// <summary>
        /// Creates a new validation message with <see cref="Severity"/> set to <see cref="ValidationSeverity.Error"/>.
        /// </summary>
        public ValidationEngineMessage()
        {
            Severity = ValidationSeverity.Error;
        }

        /// <summary>
        /// The name of the part that failed validation.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the failure.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The severity of the failure.
        /// </summary>
        public ValidationSeverity Severity { get; set; }
    }
}
