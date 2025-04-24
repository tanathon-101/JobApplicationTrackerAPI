FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY JobApplicationTrackerAPI/*.csproj ./JobApplicationTrackerAPI/
RUN dotnet restore ./JobApplicationTrackerAPI/JobApplicationTrackerAPI.csproj
COPY . .
WORKDIR /src/JobApplicationTrackerAPI
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

RUN apt-get update && apt-get install -y netcat-openbsd

COPY --from=build /app/publish .
COPY wait-for-rabbitmq.sh /wait-for-rabbitmq.sh
RUN chmod +x /wait-for-rabbitmq.sh

ENTRYPOINT ["/wait-for-rabbitmq.sh"]
CMD ["dotnet", "JobApplicationTrackerAPI.dll"]
EXPOSE 80
