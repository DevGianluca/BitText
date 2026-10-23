# Etapa 1: Fábrica com Node.js
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Instalação do Node.js e npm para processar o Tailwind CSS
RUN DEBIAN_FRONTEND=noninteractive apt-get update && apt-get install -y nodejs npm

COPY . .
RUN dotnet publish BlazorSample/BlazorSample.csproj -c Release -o /app

# Etapa 2: Servidor Leve
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "BlazorSample.dll"]