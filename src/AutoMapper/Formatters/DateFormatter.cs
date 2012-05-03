using System;
using AutoMapper;

namespace Guidelines.AutoMapper.Formatters
{
    public class DateFormatter : ValueFormatter<DateTime>
    {
        protected override string FormatValueCore(DateTime value)
        {
            return value.ToString("d");
        }
    }
}
