#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Host/Api/Api.csproj", "Host/Api/"]
COPY ["Modules/Catalog/Module.Catalog/Module.Catalog.csproj", "Modules/Catalog/Module.Catalog/"]
COPY ["Modules/Catalog/Module.Catalog.Core/Module.Catalog.Core.csproj", "Modules/Catalog/Module.Catalog.Core/"]
COPY ["Shared/Shared.Core/Shared.Core.csproj", "Shared/Shared.Core/"]
COPY ["Shared/Shared.Models/Shared.Models.csproj", "Shared/Shared.Models/"]
COPY ["Shared/Shared.Infrastructure/Shared.Infrastructure.csproj", "Shared/Shared.Infrastructure/"]
COPY ["Modules/Users/Module.Users.Shared/Module.Users.Shared.csproj", "Modules/Users/Module.Users.Shared/"]
COPY ["Modules/Catalog/Module.Catalog.Shared/Module.Catalog.Shared.csproj", "Modules/Catalog/Module.Catalog.Shared/"]
COPY ["Modules/Catalog/Module.Catalog.Infrastructure/Module.Catalog.Infrastructure.csproj", "Modules/Catalog/Module.Catalog.Infrastructure/"]
COPY ["Modules/Employees/Module.Employees/Module.Employees.csproj", "Modules/Employees/Module.Employees/"]
COPY ["Modules/Employees/Module.Employees.Core/Module.Employees.Core.csproj", "Modules/Employees/Module.Employees.Core/"]
COPY ["Modules/Employees/Module.Employees.Shared/Module.Employees.Shared.csproj", "Modules/Employees/Module.Employees.Shared/"]
COPY ["Modules/Users/Module.Users.Infrastructure/Module.Users.Infrastructure.csproj", "Modules/Users/Module.Users.Infrastructure/"]
COPY ["Modules/Users/Module.Users.Core/Module.Users.Core.csproj", "Modules/Users/Module.Users.Core/"]
COPY ["Modules/Employees/Module.Employees.Infrastructure/Module.Employees.Infrastructure.csproj", "Modules/Employees/Module.Employees.Infrastructure/"]
COPY ["Modules/Users/Module.Users/Module.Users.csproj", "Modules/Users/Module.Users/"]
RUN dotnet restore "Host/Api/Api.csproj"
COPY . .
WORKDIR "/src/Host/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]