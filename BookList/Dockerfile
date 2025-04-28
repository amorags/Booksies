# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy everything first
COPY . .

# Restore using the specific csproj
RUN dotnet restore ./BookList.csproj

# Publish
RUN dotnet publish ./BookList.csproj -c Release -o out

# Stage 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

COPY --from=build /app/out .

COPY .env .

EXPOSE 9090

ENTRYPOINT ["dotnet", "BookList.dll"]
