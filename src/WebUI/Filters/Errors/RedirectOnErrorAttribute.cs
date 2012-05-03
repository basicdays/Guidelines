using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BarNapkin.Infrastructure.WebUI.Properties;

namespace BarNapkin.Infrastructure.WebUI.Filters.Errors
{
    public abstract class RedirectOnErrorAttribute : ActionFilterAttribute
    {
        public virtual List<Type> Types { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Check for errors
            if( !Validate(filterContext) )
            {
                return;
            }

            //If no exception occurred, stop processing this filter
            if( filterContext.Exception == null )
            {
                return;
            }

            //Get inner exception unless it is null (this should never happen?)
            Exception ex = filterContext.Exception.InnerException ?? filterContext.Exception;

            //Make sure the Type property is an Exception
            if( Types != null && !Types.All(typeof(Exception).IsAssignableFrom))
            {
				throw new ArgumentException(Resources.Error_NotAnException);
            }

            //If exception was thrown because of Response.Redirect, ignore it
            if( ex.GetType() == typeof(System.Threading.ThreadAbortException) )
            {
                return;
            }
            if (Types != null && Types.Contains(typeof(System.Threading.ThreadAbortException)))
            {
				throw new ArgumentException(Resources.Error_ThreadAbortCantBeCaught);
            }

            //If the specified Type matches the thrown exception, process it
            if( IsExactMatch(ex) )
            {
                Redirect( filterContext );
            }
            //If this attribute has no specified Type, investigate further (this attribute is a catch-all error handler)
            else if( Types == null || Types.Count == 0 )
            {
                //Loop through all other RedirectToUrlOnErrorAttribute on this method
                foreach( RedirectOnErrorAttribute att in GetAllAttributes( filterContext ) )
                {
                    //Ignore self
                    if( att.GetHashCode() == this.GetHashCode() )
                    {
                        continue;
                    }
                    //If another catch-all attribute is found, throw an exception
                    if (att.Types == null || att.Types.Count == 0)
                    {
						throw new ArgumentException(Resources.Error_OnlyOneTypelessRedirectOnErrorPerAction);
                    }
                        //If an exact match is found, stop processing the catch-all. that attribute has priority
                    if( att.IsExactMatch(ex) )
                    {
                        return;
                    }
                }

                //No exact matches were found. if the specified Type for the catch-all fits, process here
                Redirect(filterContext);
            }
            else
            {
                //Specified Type was not null, but did not match the thrown exception. don't process
                return;
            }
        }

        public bool IsExactMatch( Exception exception )
        {
            return Types != null && Types.Contains(exception.GetType());
        }

        private List<RedirectOnErrorAttribute> GetAllAttributes(ActionExecutedContext filterContext)
        {
            return filterContext.ActionDescriptor
                .GetCustomAttributes( typeof( RedirectOnErrorAttribute ), false )
                .Select( a => a as RedirectOnErrorAttribute )
                .ToList();
        }

        protected abstract bool Validate(ActionExecutedContext filterContext);
        protected abstract void Redirect(ActionExecutedContext filterContext);

        protected static void PlaceInItemDictionary(string keyName, object item, ActionExecutedContext filterContext)
        {
            var collection = filterContext.HttpContext.Items;

            if (collection.Contains(keyName))
            {
                collection[keyName] = item;
            }
            else
            {
                collection.Add(keyName, item);
            }
        }
    }
}