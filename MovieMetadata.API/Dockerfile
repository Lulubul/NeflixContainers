FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY MovieMetadata.API/MovieMetadata.API.csproj MovieMetadata.API/
COPY MovieMetadata.Domain/MovieMetadata.Infrastructure.csproj MovieMetadata.Domain/
RUN dotnet restore MovieMetadata.API/MovieMetadata.API.csproj
COPY . .
WORKDIR /src/MovieMetadata.API
RUN dotnet build MovieMetadata.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish MovieMetadata.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MovieMetadata.API.dll"]
