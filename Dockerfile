FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_ENVIRONMENT=Production

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["Ecommerce.Api.csproj", "./"]
RUN dotnet restore "./Ecommerce.Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Ecommerce.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ecommerce.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "Ecommerce.Api.dll"]