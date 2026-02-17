FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

# Copy project file and restore dependencies
COPY JsonDemo.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish JsonDemo.csproj -c Release -o /app/output

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

WORKDIR /app

# Copy published output from build stage
COPY --from=build /app/output ./

# Expose port 8080
EXPOSE 8080

ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "JsonDemo.dll"]