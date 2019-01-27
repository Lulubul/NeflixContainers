FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Marketing.API/Marketing.API.csproj Marketing.API/
COPY Marketing.Infrastructure/Marketing.Infrastructure.csproj Marketing.Infrastructure/
COPY Marketing.Domain/Marketing.Domain.csproj Marketing.Domain/
RUN dotnet restore Marketing.API/Marketing.API.csproj
COPY . .
WORKDIR /src/Marketing.API
RUN dotnet build Marketing.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Marketing.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Marketing.API.dll"]
