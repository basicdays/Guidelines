﻿using System.Collections.Generic;

namespace Guidelines.Core.Validation
{
	public interface IValidationEngine
	{
        bool IsValid(object instance);

        bool IsValid(object instance, out IEnumerable<ValidationEngineMessage> results);

        void Validate(object instance);

	    void Validate(IEnumerable<object> instances);
	}
}
