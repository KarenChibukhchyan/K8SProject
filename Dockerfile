# Use official .NET runtime as base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/K8SProject/K8SProject.csproj", "K8SProject/"]
RUN dotnet restore "K8SProject/K8SProject.csproj"
COPY . .
WORKDIR /src/K8SProject
RUN dotnet build "K8SProject.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "K8SProject.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Copy appsettings.json to output directory
COPY src/K8SProject/appsettings.json .
ENTRYPOINT ["dotnet", "K8SProject.dll"]