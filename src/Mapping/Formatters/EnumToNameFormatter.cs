using System;
using AutoMapper;

namespace Guidelines.Mapping.Formatters
{
    public class EnumToNameFormatter : ValueFormatter<Enum>
    {
        protected override string FormatValueCore(Enum value)
        {
            return value.ToString();
        }
    }
}
