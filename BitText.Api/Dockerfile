# Etapa 1: Compilação
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Instala o Node.js e o npm para o Tailwind funcionar
RUN apt-get update && apt-get install -y nodejs npm

# Copia toda a pasta raiz para o contêiner
COPY . .

# Compila o projeto apontando para a pasta do frontend
RUN dotnet publish BitText.Web/BitText.Web.csproj -c Release -o /app

# Etapa 2: Execução (O Node.js não vai para cá, mantendo tudo leve)
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "BitText.Web.dll"]