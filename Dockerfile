FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

# Copy all projects
COPY DeliveryHero ./DeliveryHero
COPY DeliveryHero.AppDomain ./DeliveryHero.AppDomain
COPY DeliveryHero.Data ./DeliveryHero.Data
COPY DeliveryHero.Api ./DeliveryHero.Api
COPY DeliveryHero.Tests ./DeliveryHero.Tests
COPY *.sln ./

# Copy everything else, build, test and publish
RUN dotnet restore
RUN dotnet build -c Release --no-restore
RUN dotnet test -c Release --no-build ./DeliveryHero.Tests/DeliveryHero.Tests.csproj
RUN dotnet publish -c Release -o ../out --no-build ./DeliveryHero.Api/DeliveryHero.Api.csproj

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
ENV MONGO_URL "mongodb://127.0.0.1:27017"
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "DeliveryHero.Api.dll"]
