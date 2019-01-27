FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY History.API/History.API.csproj History.API/
RUN dotnet restore History.API/History.API.csproj
COPY . .
WORKDIR /src/History.API
RUN dotnet build History.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish History.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "History.API.dll"]
