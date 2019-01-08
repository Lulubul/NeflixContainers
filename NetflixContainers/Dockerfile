FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["NetflixContainers/NetflixContainers.csproj", "NetflixContainers/"]
RUN dotnet restore "NetflixContainers/NetflixContainers.csproj"
COPY . .
WORKDIR "/src/NetflixContainers"
RUN dotnet build "NetflixContainers.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "NetflixContainers.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NetflixContainers.dll"]