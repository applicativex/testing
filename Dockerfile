# Build stage
FROM microsoft/dotnet:2.2-sdk AS build-env
WORKDIR /app


COPY . ./
RUN dotnet restore

RUN dotnet test Accounting.UnitTests/Accounting.UnitTests.csproj

# Dotnet Build and publish 
RUN dotnet publish Accounting.Host/Accounting.Host.csproj -c Release -o /app/out

# Clean up to free up spaces

WORKDIR /app/out
COPY wait_for_it.sh ./
RUN ls /app/out

# Runtime stage
FROM microsoft/dotnet:2.2-aspnetcore-runtime

WORKDIR /app
COPY --from=build-env /app/out .
RUN ls /app

CMD ["dotnet", "Accounting.Host.dll"]
