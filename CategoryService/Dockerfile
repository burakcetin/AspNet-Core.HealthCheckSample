FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["CategoryService/CategoryService.csproj", "CategoryService/"]
RUN dotnet restore "CategoryService/CategoryService.csproj"
COPY . .
WORKDIR "/src/CategoryService"
RUN dotnet build "CategoryService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CategoryService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CategoryService.dll"]
