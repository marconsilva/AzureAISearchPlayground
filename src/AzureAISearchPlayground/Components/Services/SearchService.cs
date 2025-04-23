using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure;
using System.Text.Json;
using Azure.Search.Documents.Models;
using System.Reflection.Metadata.Ecma335;

namespace AzureAISearchPlayground.Components.Services
{
    public class SearchService : ISearchService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        private SearchClient _searchClient;
        private SearchIndexClient _indexClient;
        private string _endpoint;
        private string _apiKey;
        private string _indexName;

        public SearchService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;

            // Initialize the SearchIndexClient and SearchClient with the configuration settings.
             _endpoint = _configuration["AzureAISearch:Endpoint"];
             _apiKey = _configuration["AzureAISearch:ApiKey"];
             _indexName = _configuration["AzureAISearch:IndexName"];

            if (string.IsNullOrEmpty(_endpoint) || string.IsNullOrEmpty(_apiKey) || string.IsNullOrEmpty(_indexName))
            {
                throw new ArgumentException("Azure Search configuration is not set properly.");
            }

            // Create a service and index client.
             _indexClient = new SearchIndexClient(new Uri(_endpoint), new AzureKeyCredential(_apiKey));
             _searchClient = _indexClient.GetSearchClient(_indexName);
        }

        public async Task<List<SearchResult<MeoSearchResult>>> MeoSearchAsync(string searchQuery)
        {
            var options = new SearchOptions
            {
                Filter = "IsAdult eq false",
                Size = 50,
                OrderBy = { "NumberOfViews desc" },
                QueryType = SearchQueryType.Full,
                IncludeTotalCount = true
            };

            var searchResult = await _searchClient.SearchAsync<MeoSearchResult>(searchQuery, options);
            
            return searchResult.Value.GetResults().ToList();
        }

        public async Task<List<SearchResult<MeoSearchResult>>> MeoAutocompleteAsync(string searchQuery)
        {
            var options = new SearchOptions
            {
                Filter = "IsAdult eq false",
                Size = 50,
                OrderBy = { "NumberOfViews desc" },
                QueryType = SearchQueryType.Full,
                IncludeTotalCount = true
            };

            var searchResult = await _searchClient.SearchAsync<MeoSearchResult>(searchQuery, options);
            
            return searchResult.Value.GetResults().ToList();
        }

        public async Task<List<SearchResult<MeoSearchResult>>> MeoFilteredSearchAsync(string searchQuery, string genre, string year)
        {
            var options = new SearchOptions
            {
                Filter = "IsAdult eq false",
                Size = 50,
                OrderBy = { "NumberOfViews desc" },
                QueryType = SearchQueryType.Full,
                IncludeTotalCount = true
            };

            if (!string.IsNullOrEmpty(genre))
            {
                options.Filter += $" and Genres/any(g: g eq '{genre}')";
            }

            if (!string.IsNullOrEmpty(year))
            {
                options.Filter += $" and StartDate ge {year}-01-01 and StartDate le {year}-12-31";
            }

            var searchResult = await _searchClient.SearchAsync<MeoSearchResult>(searchQuery, options);
            
            return searchResult.Value.GetResults().ToList();
        }

        public async Task<List<string>> GetAutocompleteSuggestionsAsync(string searchText)
        {
            var options = new AutocompleteOptions
            {
                Mode = AutocompleteMode.OneTermWithContext
            };

            var response = await _searchClient.AutocompleteAsync(searchText, "sg", options);
            return response.Value.Results.Select(r => r.Text).ToList();
        }

        
        public async Task<Dictionary<string, List<string>>> GetFacetsAsync()
        {
            var options = new SearchOptions
            {
                Facets = { "AssetType", "TimeOfDay", "Genres", "Actors", "Directors", "AvailableOnChannels", "Categories", "Imdb", "CallLetter"  } 
            };

            var response = await _searchClient.SearchAsync<MeoSearchResult>("*", options);
            var facets = new Dictionary<string, List<string>>();

            foreach (var facet in response.Value.Facets)
            {
                facets[facet.Key] = facet.Value.Select(v => v.Value.ToString()).ToList();
            }

            return facets;
        }

        public async Task<IEnumerable<SearchResult<MeoSearchResult>>> FacetedSearchAsync(string searchText, Dictionary<string, string> filters)
        {
            var options = new SearchOptions
            {
                IncludeTotalCount = true
            };

            foreach (var filter in filters)
            {
                options.Filter = $"{filter.Key} eq '{filter.Value}'";
            }

            var response = await _searchClient.SearchAsync<MeoSearchResult>(searchText, options);
            return response.Value.GetResults();
        }
    }
}