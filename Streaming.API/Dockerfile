FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Streaming.API/Streaming.API.csproj", "Streaming.API/"]
RUN dotnet restore "Streaming.API/Streaming.API.csproj"
COPY . .
WORKDIR "/src/Streaming.API"
RUN dotnet build "Streaming.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Streaming.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Streaming.API.dll"]