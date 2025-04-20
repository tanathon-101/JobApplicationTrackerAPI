# Use the official .NET SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore
COPY JobApplicationTrackerAPI/*.csproj ./JobApplicationTrackerAPI/
RUN dotnet restore ./JobApplicationTrackerAPI/JobApplicationTrackerAPI.csproj

# Copy the rest and build
COPY . .
WORKDIR /src/JobApplicationTrackerAPI
RUN dotnet publish -c Release -o /app/publish

# Use runtime image for final container
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expose port
EXPOSE 80

# Run the app
ENTRYPOINT ["dotnet", "JobApplicationTrackerAPI.dll"]
