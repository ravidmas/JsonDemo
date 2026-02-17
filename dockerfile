FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

COPY JsonDemo.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish JsonDemo.csproj -c Release -o /app/output

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

WORKDIR /app

COPY --from=build /app/output ./

EXPOSE 8080

ENTRYPOINT ["dotnet", "JsonDemo.dll"]