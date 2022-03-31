FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NftFaucet/NftFaucet.csproj", "NftFaucet/"]
RUN dotnet restore "NftFaucet/NftFaucet.csproj"
COPY . .
WORKDIR "/src/NftFaucet"
RUN dotnet build "NftFaucet.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NftFaucet.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NftFaucet.dll"]
