# AzureAISearchPlayground

## Description

AzureAISearchPlayground is a Blazor application that demonstrates the use of Azure AI Search service to search for TV shows and Movies. The application showcases three main use cases:

1. Autocomplete search based on movie title
2. Filtered search based on AISearch filters
3. Multi-entity search with multiple grid results

## Setup

### .env.sample

Create a `.env.sample` file in the root directory of the project with the following structure:

```
AZURE_SEARCH_API_KEY=your-azure-search-api-key
AZURE_SEARCH_SERVICE_NAME=your-azure-search-service-name
AZURE_SEARCH_INDEX_NAME=your-azure-search-index-name
AZURE_SEARCH_ENDPOINT=your-azure-search-endpoint-url
```

Replace the placeholder values with your actual Azure Search credentials and endpoint information.

### Running the Project

1. Clone the repository:

```
git clone https://github.com/marconsilva/AzureAISearchPlayground.git
```

2. Navigate to the project directory:

```
cd AzureAISearchPlayground
```

3. Restore the dependencies:

```
dotnet restore
```

4. Build the project:

```
dotnet build
```

5. Run the project:

```
dotnet run
```

The application will be available at `https://localhost:7161` or `http://localhost:5007`.

## Pages

### Autocomplete Search

The Autocomplete Search page allows users to search for movies by title. As the user types in the search input, the application fetches autocomplete results from the Azure AI Search service and displays them as a grid of cover images.

### Filtered Search

The Filtered Search page allows users to search for movies based on AISearch filters such as genre and year. The application fetches filtered search results from the Azure AI Search service and displays them as a grid of cover images.

### Multi-Entity Search

The Multi-Entity Search page allows users to search for multiple entities such as movies, TV shows, and cast members. The application fetches multi-entity search results from the Azure AI Search service and displays them as multiple grids of cover images.
