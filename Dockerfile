# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy the project files
COPY todo-api/*.csproj ./todo-api/

# Restore packages
RUN dotnet restore todo-api/todo-api.csproj

# Copy everything
COPY . .

WORKDIR /app/todo-api
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build /app/todo-api/out .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "todo-api.dll"]


