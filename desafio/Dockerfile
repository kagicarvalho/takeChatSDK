#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["desafio/desafio.csproj", "desafio/"]
RUN dotnet restore "desafio/desafio.csproj"
COPY . .
WORKDIR "/src/desafio"
RUN dotnet build "desafio.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "desafio.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "desafio.dll"]
