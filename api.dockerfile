FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy the csproj and restore all of the nugets
COPY API/*.csproj /app
RUN dotnet restore

# Copy everything else and build
COPY API/ /app
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/out .

# Run
ENTRYPOINT ["dotnet", "API.dll"]