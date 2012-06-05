using System;
using AutoMapper;

namespace Guidelines.AutoMapper.Formatters
{
    public class NullableDateFormatter : ValueFormatter<DateTime?>
    {
        protected override string FormatValueCore(DateTime? value)
        {
            return value.HasValue ? value.Value.ToString("d") : string.Empty;
        }
    }
}
