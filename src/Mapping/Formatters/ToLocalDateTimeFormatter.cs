using System;
using AutoMapper;
using Guidelines.Domain;

namespace Guidelines.Mapping.Formatters
{
    public class ToLocalDateTimeFormatter : ValueFormatter<DateTime?>
    {
        private readonly ISystemInfo _systemInfo;

        public ToLocalDateTimeFormatter(ISystemInfo systemInfo)
        {
            _systemInfo = systemInfo;
        }

        protected override string FormatValueCore(DateTime? value)
        {
            var timeZone = _systemInfo.GetCurrentTimeZone();
            string result = null;

            if(value.HasValue)
            {
                var timeToConvert = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);

                DateTime convertedDate = timeToConvert > DateTime.MinValue
                    ? TimeZoneInfo.ConvertTimeFromUtc(timeToConvert, timeZone)
                    : DateTime.MinValue;

                DateTime localizedDate = timeZone != null ? convertedDate : timeToConvert;

                result = localizedDate.ToString("d");
            }
            
            return result;
        }
    }
}
