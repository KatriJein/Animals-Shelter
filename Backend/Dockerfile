# Используем официальный образ SDK .NET Core для сборки и публикации приложения
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Устанавливаем рабочую директорию внутри контейнера
WORKDIR /app

# Копируем файл решения проекта и восстанавливаем зависимости
COPY *.sln ./
COPY AnimalsShelterBackend/*.csproj ./AnimalsShelterBackend/
RUN dotnet restore AnimalsShelterBackend/AnimalsShelterBackend.csproj

# Копируем остальные файлы и публикуем приложение
COPY AnimalsShelterBackend/. ./AnimalsShelterBackend/
WORKDIR /app/AnimalsShelterBackend
RUN dotnet publish -c Release -o out

# Используем образ ASP.NET Core Runtime для запуска приложения
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime

# Устанавливаем рабочую директорию
WORKDIR /app

# Копируем опубликованные файлы из стадии сборки
COPY --from=build /app/AnimalsShelterBackend/out ./

# Открываем порт 5000 для доступа к приложению
EXPOSE 5000

# Устанавливаем переменные окружения для запуска ASP.NET Core
ENV ASPNETCORE_URLS=http://+:5000

# Команда для запуска приложения
ENTRYPOINT ["dotnet", "AnimalsShelterBackend.dll"]
