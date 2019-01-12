FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Profiles.API/Profiles.API.csproj", "Profiles.API/"]
RUN dotnet restore "Profiles.API/Profiles.API.csproj"
COPY . .
WORKDIR "/src/Profiles.API"
RUN dotnet build "Profiles.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Profiles.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Profiles.API.dll"]