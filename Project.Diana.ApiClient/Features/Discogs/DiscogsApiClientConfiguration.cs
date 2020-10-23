using FluentValidation;

namespace Project.Diana.ApiClient.Features.Discogs
{
    public class DiscogsApiClientConfiguration : IDiscogsApiClientConfiguration
    {
        public string BaseUrl { get; set; }
        public string DiscogsToken { get; set; }
        public string SearchResource { get; set; }
        public string UserAgent { get; set; }
    }

    public class DiscogsApiClientConfigurationValidator : AbstractValidator<DiscogsApiClientConfiguration>
    {
        public DiscogsApiClientConfigurationValidator()
        {
            RuleFor(r => r.BaseUrl).NotEmpty();
            RuleFor(r => r.DiscogsToken).NotEmpty();
            RuleFor(r => r.SearchResource).NotEmpty();
            RuleFor(r => r.UserAgent).NotEmpty();
        }
    }
}