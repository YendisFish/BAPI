#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BAPI/BAPI.csproj", "BAPI/"]
RUN dotnet restore "BAPI/BAPI.csproj"
COPY . .
WORKDIR "/src/BAPI"
RUN dotnet build "BAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BAPI.dll"]