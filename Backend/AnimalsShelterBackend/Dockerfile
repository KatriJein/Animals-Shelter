#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
RUN dotnet dev-certs https
RUN dotnet dev-certs https --trust
WORKDIR /src
COPY ["AnimalsShelterBackend/AnimalsShelterBackend.csproj", "AnimalsShelterBackend/"]
RUN dotnet restore "./AnimalsShelterBackend/AnimalsShelterBackend.csproj"
COPY . .
WORKDIR "/src/AnimalsShelterBackend"
RUN dotnet build "./AnimalsShelterBackend.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AnimalsShelterBackend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnimalsShelterBackend.dll"]