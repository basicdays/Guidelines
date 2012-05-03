using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Guidelines.WebUI.Errors
{
	public class ErrorContext : HandleErrorInfo
    {
        public ErrorContext(Exception exception, string controllerName, string actionName, IEnumerable<string> additionalMessages, ModelStateDictionary modelState) 
			: base(exception, controllerName, actionName)
        {
            AdditionalMessages = additionalMessages;

            _modelState = modelState;
        }

        public ErrorContext(Exception exception, string controllerName, string actionName, IEnumerable<string> additionalMessages)
            : this(exception, controllerName, actionName, additionalMessages, null)
        { }

        public ErrorContext(Exception exception, string controllerName, string actionName)
            : this(exception, controllerName, actionName, null, null)
        { }

        public IEnumerable<string> AdditionalMessages { get; private set; }

        private readonly ModelStateDictionary _modelState;

        public bool HasMessages
        {
            get { return Exception != null || AdditionalMessages.Count() > 0; }
        }

        public string ErrorMessage
        {
            get
            {
                var baseMessage = Exception != null ? Exception.Message : string.Empty;

                if(AdditionalMessages != null && AdditionalMessages.Count() > 0)
                {
                    baseMessage += "<ul>";
                    baseMessage = AdditionalMessages.Aggregate(baseMessage, (current, message) => current + string.Format("<li>{0}</li>", message));
                    baseMessage += "</ul>";
                }

                return baseMessage;
            }
        }

        public void AddModelstateErrors(ModelStateDictionary modelState)
        {
            if (_modelState != null && !_modelState.IsValid)
            {
				//TODO: This errors now and then.
                modelState.Merge(_modelState);
            }
        }
    }
}