
namespace AzureAISearchPlayground.Components.Services
{
    public class SearchResult
    {
        public string Title { get; set; } = string.Empty;
        public string CoverImageUrl { get; set; } = string.Empty;
        public string? Genre { get; set; }
        public string? Year { get; set; }
        public string? Description { get; set; }
    }
}