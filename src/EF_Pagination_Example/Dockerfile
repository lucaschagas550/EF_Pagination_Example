#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY "EF_Pagination_Example/EF_Pagination_Example.csproj" "EF_Pagination_Example/"
RUN dotnet restore "EF_Pagination_Example/EF_Pagination_Example.csproj"

COPY . .
WORKDIR "/src/EF_Pagination_Example"
RUN dotnet build "EF_Pagination_Example.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EF_Pagination_Example.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EF_Pagination_Example.dll"]