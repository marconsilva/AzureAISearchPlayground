# Use the official .NET image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy the project files
COPY AzureAISearchPlayground.csproj ./
RUN dotnet restore

# Copy the rest of the application files
COPY . ./
RUN dotnet publish -c Release -o /out

# Use the official ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /out ./

# Expose the port the app runs on
EXPOSE 8080

# Set the entry point for the container
ENTRYPOINT ["dotnet", "AzureAISearchPlayground.dll"]