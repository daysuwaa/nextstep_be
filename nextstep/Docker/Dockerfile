# Stage 1: Build the app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy solution file
COPY ../nextstep.sln ./

# Copy all project files
COPY ../nextstep/*.csproj ./nextstep/
COPY ../nextstep.application/*.csproj ./nextstep.application/
COPY ../nextstep.domain/*.csproj ./nextstep.domain/
COPY ../nextstep.infrastructure/*.csproj ./nextstep.infrastructure/

# Restore dependencies
RUN dotnet restore ./nextstep/nextstep.csproj

# Copy everything
COPY ../. .

# Build and publish
WORKDIR /src/nextstep
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Create runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "nextstep.dll"]