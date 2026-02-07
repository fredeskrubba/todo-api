# =======================
# Build stage
# =======================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy the solution file first
COPY TodoApi.sln ./

# Copy the project folder
COPY TodoApi/*.csproj ./TodoApi/

# Restore NuGet packages
RUN dotnet restore TodoApi/TodoApi.csproj

# Copy the rest of the code
COPY . .

# Set working directory to the project and publish
WORKDIR /app/TodoApi
RUN dotnet publish -c Release -o out

# =======================
# Runtime stage
# =======================
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

# Copy build output
COPY --from=build /app/TodoApi/out .

# Bind to all interfaces for Dokploy
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "TodoApi.dll"]

