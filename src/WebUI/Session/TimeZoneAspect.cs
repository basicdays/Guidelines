using System;
using System.Collections.ObjectModel;
using System.Web;
using System.Web.Mvc;
using Guidelines.WebUI.Tools;

namespace Guidelines.WebUI.Session
{
    public class TimeZoneAspect : IContextRegistrar
    {
        public const string TimeZoneCookieKey = "__timeZoneContext";
        private const string TimeZoneContextKey = "__timeZoneContext";
        private const string TimeZoneAttemptsKey = "__timeZoneAttempsContext";

        public void SetContext(ActionExecutingContext controllerContext)
        {
            HttpContextBase context = controllerContext.GetHttpContext();

            if (context.Session != null)
            {
                if (context.Session[TimeZoneContextKey] == null)
                {
                    var numberOfAttempts = (int)(context.Session[TimeZoneAttemptsKey] ?? 0);
                    string timeZoneIdString = GetTimeZoneIdString(numberOfAttempts, context.Request);
                    TimeZoneInfo timeZone = GetTimeZone(timeZoneIdString);

                    if (timeZone != null)
                    {
                        context.Session[TimeZoneContextKey] = timeZone;
                        context.Session[TimeZoneAttemptsKey] = null;
                    }
                    else
                    {
                        context.Session[TimeZoneAttemptsKey] = ++numberOfAttempts;
                    }
                }
            }
        }

        private static TimeZoneInfo GetTimeZone(string timeZoneIdString)
        {
            TimeZoneInfo timeZone = null;

            //Find The Timezone for this offset
            if (!string.IsNullOrEmpty(timeZoneIdString))
            {
                bool foundTimeZone = false;
                int index = 0;
                ReadOnlyCollection<TimeZoneInfo> timeZones = TimeZoneInfo.GetSystemTimeZones();
                while ((!foundTimeZone) && (index < timeZones.Count))
                {
                    if (timeZones[index].Id.ToLower().Trim() == timeZoneIdString.ToLower().Trim())
                    {
                        foundTimeZone = true;
                        timeZone = timeZones[index];
                    }
                    index++;
                }
            }

            return timeZone;
        }

        private static string GetTimeZoneIdString(int numberOfAttempts, HttpRequestBase request)
        {
            string timeZoneIdString;
            if (numberOfAttempts <= 3)
            {
                HttpCookie timeZoneIdCookie = request.Cookies[TimeZoneCookieKey];
                if (timeZoneIdCookie != null && (!string.IsNullOrEmpty(timeZoneIdCookie.Value)))
                {
                    timeZoneIdString = timeZoneIdCookie.Value;
                }
                else
                {
                    //Prevents conversion error
                    timeZoneIdString = "UTC";
                }
            }
            else
            {
                //Default to GMT
                timeZoneIdString = "GMT Standard Time";
            }
            return timeZoneIdString;
        }

        public static TimeZoneInfo GetTimeZone()
        {
            TimeZoneInfo timeZone = null;
            if (HttpContext.Current.Session != null)
            {
                timeZone = (TimeZoneInfo)HttpContext.Current.Session[TimeZoneContextKey];
            }
            return timeZone ?? GetTimeZone("GMT Standard Time");
        }
    }
}