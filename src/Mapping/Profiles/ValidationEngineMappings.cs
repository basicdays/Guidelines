using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Guidelines.Domain.Validation;

namespace Guidelines.Mapping.Profiles
{
    public class ValidationEngineMappings : Profile
    {
        public override string ProfileName
        {
            get { return MethodBase.GetCurrentMethod().DeclaringType.Name; }
        }

        protected override void Configure()
        {
            CreateMap<ValidationResult, ValidationEngineMessage>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(dest => dest.MemberNames.FirstOrDefault()))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(dest => dest.ErrorMessage))
                .ForMember(dest => dest.Severity, opt => opt.Ignore());
        }
    }
}
