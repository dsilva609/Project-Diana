using FluentValidation;

namespace Project.Diana.Data.Features.Settings
{
    public class GlobalSettings
    {
        //--TODO: add validation
        public string Issuer { get; set; }

        public string JwtKey { get; set; }
    }

    public class GlobalSettingsValidator : AbstractValidator<GlobalSettings>
    {
        public GlobalSettingsValidator()
        {
            RuleFor(s => s.Issuer).NotEmpty();
            RuleFor(s => s.JwtKey).NotEmpty();
        }
    }
}