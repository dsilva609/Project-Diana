using FluentValidation;

namespace Project.Diana.ApiClient.Features.ComicVine
{
    public class ComicVineApiClientConfiguration : IComicVineApiClientConfiguration
    {
        public string ApiKey { get; set; }
        public string BaseUrl { get; set; }
        public string Format { get; set; }
        public int Limit { get; set; }
        public string ResourceType { get; set; }
        public string SearchResource { get; set; }
    }

    public class ComicVineApiClientConfigurationValidator : AbstractValidator<ComicVineApiClientConfiguration>
    {
        public ComicVineApiClientConfigurationValidator()
        {
            RuleFor(r => r.BaseUrl).NotEmpty();
            RuleFor(r => r.ApiKey).NotEmpty();
            RuleFor(r => r.Format).NotEmpty();
            RuleFor(r => r.Limit).GreaterThan(0);
            RuleFor(r => r.ResourceType).NotEmpty();
            RuleFor(r => r.SearchResource).NotEmpty();
        }
    }
}