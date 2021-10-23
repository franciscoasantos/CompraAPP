FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy the csproj and restore all of the nugets
COPY KafkaConsumer/*.csproj /app
RUN dotnet restore

# Copy everything else and build
COPY KafkaConsumer/ /app
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /app
COPY --from=build-env /app/out .
COPY scripts/consumer-entrypoint.sh /app

# Run
ENTRYPOINT ["/bin/bash", "consumer-entrypoint.sh"]