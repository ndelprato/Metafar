# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Copiar los scripts necesarios
COPY init.sql /docker-entrypoint-initdb.d/init.sql

CMD ["/bin/bash", "-c", "/wait-for-it.sh localhost:1433 -- /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P 'YourStrong!Passw0rd' -d master -i /docker-entrypoint-initdb.d/init.sql"]

# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Metafar/Metafar.csproj", "Metafar/"]
COPY ["Metafar.Core/Metafar.Core.csproj", "Metafar.Core/"]
COPY ["Metafar.Infraestructura/Metafar.Infraestructura.csproj", "Metafar.Infraestructura/"]
RUN dotnet restore "./Metafar/Metafar.csproj"
COPY . .
WORKDIR "/src/Metafar"
RUN dotnet build "./Metafar.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Metafar.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Metafar.dll"]