FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Recommendation.API/Recommendation.API.csproj Recommendation.API/
RUN dotnet restore Recommendation.API/Recommendation.API.csproj
COPY . .
WORKDIR /src/Recommendation.API
RUN dotnet build Recommendation.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Recommendation.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Recommendation.API.dll"]
