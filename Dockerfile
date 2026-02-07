# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy the project files
COPY TodoApi/*.csproj ./TodoApi/

# Restore packages
RUN dotnet restore TodoApi/TodoApi.csproj

# Copy everything
COPY . .

WORKDIR /app/TodoApi
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build /app/TodoApi/out .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "TodoApi.dll"]


