FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DiscordElegiaBot/DiscordElegiaBot.csproj", "DiscordElegiaBot/"]
COPY ["DiscordElegiaBot.BLL/DiscordElegiaBot.BLL.csproj", "DiscordElegiaBot.BLL/"]
COPY ["DiscordElegiaBot.Common/DiscordElegiaBot.Common.csproj", "DiscordElegiaBot.Common/"]
COPY ["DiscordElegiaBot.DAL/DiscordElegiaBot.DAL.csproj", "DiscordElegiaBot.DAL/"]

RUN dotnet restore "DiscordElegiaBot/DiscordElegiaBot.csproj"
COPY . .
WORKDIR "/src/DiscordElegiaBot"
RUN dotnet build "DiscordElegiaBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DiscordElegiaBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY config.json /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DiscordElegiaBot.dll"]