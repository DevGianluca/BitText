# Etapa 1: Compilação com Node.js para o Tailwind
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Instala o Node.js e o npm
RUN apt-get update && apt-get install -y nodejs npm

# Copia tudo e compila apontando para o nome antigo
COPY . .
RUN dotnet publish BlazorSample/BlazorSample.csproj -c Release -o /app

# Etapa 2: Execução leve
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "BlazorSample.dll"]