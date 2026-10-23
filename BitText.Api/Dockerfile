# Etapa 1: Compilação
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish BitText.Api/BitText.Api.csproj -c Release -o /app

# Etapa 2: Execução
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "BitText.Api.dll"]