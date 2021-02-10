using FluentValidation;

namespace Project.Diana.Data.Features.Settings
{
    public class GlobalSettings
    {
        public string Issuer { get; set; }
        public string JwtKey { get; set; }
        public int RefreshTokenExpirationDays { get; set; }
        public int TokenExpirationMinutes { get; set; }
    }

    public class GlobalSettingsValidator : AbstractValidator<GlobalSettings>
    {
        public GlobalSettingsValidator()
        {
            RuleFor(s => s.Issuer).NotEmpty();
            RuleFor(s => s.JwtKey).NotEmpty();
            RuleFor(s => s.RefreshTokenExpirationDays).GreaterThan(0);
            RuleFor(s => s.TokenExpirationMinutes).GreaterThan(0);
        }
    }
}