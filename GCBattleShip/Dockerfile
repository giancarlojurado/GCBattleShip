FROM mcr.microsoft.com/dotnet/core/sdk:2.1.607 AS build-env
WORKDIR /app

COPY ./ .

RUN dotnet restore GCBattleShip.sln
RUN dotnet build GCBattleShip.sln
RUN dotnet publish -c Release -o /app/out GCBattleShip.Api/GCBattleShip.Api.csproj

# build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.1.14


WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 80

ENTRYPOINT ["dotnet", "GCBattleShip.Api.dll"]
