FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY hackathon/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY hackathon ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM gcr.io/google-appengine/aspnetcore:2.0 
WORKDIR /app 
COPY --from=build-env /app/out .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
ENTRYPOINT ["dotnet", "Hackathon.dll"]