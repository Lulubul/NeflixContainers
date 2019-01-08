FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Subscription.API/Subscription.API.csproj", "Subscription.API/"]
RUN dotnet restore "Subscription.API/Subscription.API.csproj"
COPY . .
WORKDIR "/src/Subscription.API"
RUN dotnet build "Subscription.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Subscription.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Subscription.API.dll"]