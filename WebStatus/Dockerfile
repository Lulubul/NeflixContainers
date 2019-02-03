FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY WebStatus/WebStatus.csproj WebStatus/
RUN dotnet restore WebStatus/WebStatus.csproj
COPY . .
WORKDIR /src/WebStatus
RUN dotnet build WebStatus.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WebStatus.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebStatus.dll"]
