#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Presentation/EmiSoft.CleanArchitecture.Api/EmiSoft.CleanArchitecture.Api.csproj", "src/Presentation/EmiSoft.CleanArchitecture.Api/"]
COPY ["src/Shared/EmiSoft.CleanArchitecture.SharedKernel/EmiSoft.CleanArchitecture.SharedKernel.csproj", "src/Shared/EmiSoft.CleanArchitecture.SharedKernel/"]
COPY ["src/Core/EmiSoft.CleanArchitecture.Application/EmiSoft.CleanArchitecture.Application.csproj", "src/Core/EmiSoft.CleanArchitecture.Application/"]
COPY ["src/Core/EmiSoft.CleanArchitecture.Domain/EmiSoft.CleanArchitecture.Domain.csproj", "src/Core/EmiSoft.CleanArchitecture.Domain/"]
COPY ["src/Infrastructure/EmiSoft.CleanArchitecture.Infrastructure/EmiSoft.CleanArchitecture.Infrastructure.csproj", "src/Infrastructure/EmiSoft.CleanArchitecture.Infrastructure/"]
COPY ["src/Infrastructure/EmiSoft.CleanArchitecture.Infrastructure.Persistence/EmiSoft.CleanArchitecture.Infrastructure.Persistence.csproj", "src/Infrastructure/EmiSoft.CleanArchitecture.Infrastructure.Persistence/"]
RUN dotnet restore "src/Presentation/EmiSoft.CleanArchitecture.Api/EmiSoft.CleanArchitecture.Api.csproj"
COPY . .
WORKDIR "/src/src/Presentation/EmiSoft.CleanArchitecture.Api"
RUN dotnet build "EmiSoft.CleanArchitecture.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmiSoft.CleanArchitecture.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmiSoft.CleanArchitecture.Api.dll"]
