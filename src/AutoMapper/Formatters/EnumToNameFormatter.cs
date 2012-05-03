using System;
using AutoMapper;

namespace Guidelines.AutoMapper.Formatters
{
    public class EnumToNameFormatter : ValueFormatter<Enum>
    {
        protected override string FormatValueCore(Enum value)
        {
            return value.ToString();
        }
    }
}
