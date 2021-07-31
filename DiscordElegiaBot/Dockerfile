FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY ["DiscordElegiaBot.csproj", "/app"]
RUN dotnet restore "DiscordElegiaBot.csproj"
COPY . .
WORKDIR /app
RUN dotnet build "DiscordElegiaBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DiscordElegiaBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY config.json /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DiscordElegiaBot.dll"]
