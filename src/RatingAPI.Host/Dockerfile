FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base

WORKDIR /build

COPY src/ /build/src

RUN dotnet publish /build/src/RatingAPI.Host/ -c Release -o /build/app

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime

WORKDIR /app
COPY --from=base /build/app /app

ENTRYPOINT [ "dotnet", "RatingAPI.Host.dll" ]