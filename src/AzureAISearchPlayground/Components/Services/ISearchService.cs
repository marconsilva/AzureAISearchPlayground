using Azure.Search.Documents.Models;

namespace AzureAISearchPlayground.Components.Services
{
    public interface ISearchService
    {
        Task<List<SearchResult<MeoSearchResult>>> MeoSearchAsync(string searchQuery);
        Task<List<SearchResult<MeoSearchResult>>> MeoAutocompleteAsync(string searchQuery);
        Task<List<SearchResult<MeoSearchResult>>> MeoFilteredSearchAsync(string searchQuery, string genre, string year);

        Task<List<string>> GetAutocompleteSuggestionsAsync(string searchText);
        
        Task<Dictionary<string, List<string>>> GetFacetsAsync();
        Task<IEnumerable<SearchResult<MeoSearchResult>>> FacetedSearchAsync(string searchText, Dictionary<string, string> filters);
    }
}