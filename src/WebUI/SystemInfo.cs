using System;
using System.Threading;
using Guidelines.Domain;
using Guidelines.WebUI.Session;

namespace Guidelines.WebUI
{
    public class SystemInfo : ISystemInfo
    {
        private TimeZoneInfo _timeZone;

        public TimeZoneInfo GetCurrentTimeZone()
        {
            return _timeZone ?? (_timeZone = TimeZoneAspect.GetTimeZone());
        }

        public string GetCurrentLanguageCode()
        {
            return Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
        }
    }
}