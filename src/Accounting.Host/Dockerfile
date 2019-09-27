FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy proj files needed for package restore
COPY . .

# restore packages
RUN dotnet restore

# copy everything else
COPY . .

# run tests
RUN dotnet test test/Accounting.UnitTests/Accounting.UnitTests.csproj
RUN dotnet test test/Accounting.IntegrationTests/Accounting.IntegrationTests.csproj

# build app
WORKDIR /app/src/Accounting.Host/
RUN dotnet publish -c Release -o out

# runtime stage
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/src/Accounting.Host/out ./
RUN ls

ENTRYPOINT ["dotnet", "Accounting.Host.dll"]