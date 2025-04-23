using System.Text.Json.Serialization;

namespace AzureAISearchPlayground.Components.Services
{
    public class MeoSearchResult
    {
        [JsonPropertyName("@search.score")]
        public double SearchScore { get; set; }
        
        [JsonPropertyName("Title")]
        public string Title { get; set; } = string.Empty;
        
        [JsonPropertyName("FriendlyUrlName")]
        public string FriendlyUrlName { get; set; } = string.Empty;
        
        [JsonPropertyName("Synopsis")]
        public string? Synopsis { get; set; }
        
        [JsonPropertyName("AssetType")]
        public string? AssetType { get; set; }
        
        [JsonPropertyName("ImageUri")]
        public string? ImageUri { get; set; }
        
        [JsonPropertyName("IsAdult")]
        public bool IsAdult { get; set; }
        
        [JsonPropertyName("TimeOfDay")]
        public List<string>? TimeOfDay { get; set; }
        
        [JsonPropertyName("Genres")]
        public List<string>? Genres { get; set; }
        
        [JsonPropertyName("Actors")]
        public List<string>? Actors { get; set; }
        
        [JsonPropertyName("Directors")]
        public List<string>? Directors { get; set; }
        
        [JsonPropertyName("AvailableOnChannels")]
        public List<string>? AvailableOnChannels { get; set; }
        
        [JsonPropertyName("Categories")]
        public List<string>? Categories { get; set; }
        
        [JsonPropertyName("StartDate")]
        public DateTime? StartDate { get; set; }
        
        [JsonPropertyName("EndDate")]
        public DateTime? EndDate { get; set; }
        
        [JsonPropertyName("NumberOfViews")]
        public int NumberOfViews { get; set; }
        
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        
        [JsonPropertyName("SeriesId")]
        public long? SeriesId { get; set; }
        
        [JsonPropertyName("Imdb")]
        public string? Imdb { get; set; }
        
        [JsonPropertyName("CallLetter")]
        public string? CallLetter { get; set; }
        
        [JsonPropertyName("TitleId")]
        public string? TitleId { get; set; }
        
        [JsonPropertyName("SvodOfferId")]
        public int? SvodOfferId { get; set; }
        
        [JsonPropertyName("SvodOfferName")]
        public string? SvodOfferName { get; set; }
        
        [JsonPropertyName("SeasonId")]
        public int? SeasonId { get; set; }
        
        [JsonPropertyName("Year")]
        public int? Year { get; set; }
        
        [JsonPropertyName("RatingIMDB")]
        public string? RatingIMDB { get; set; }
        
        [JsonPropertyName("PresentationKey")]
        public string? PresentationKey { get; set; }
        
        [JsonPropertyName("ParentalRating")]
        public string? ParentalRating { get; set; }
        
        [JsonPropertyName("SeriesName")]
        public string? SeriesName { get; set; }
        
        [JsonPropertyName("ContextName")]
        public string? ContextName { get; set; }
    }
}