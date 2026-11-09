FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EventManagementAPI/EventManagementAPI.csproj", "EventManagementAPI/"]
RUN dotnet restore "EventManagementAPI/EventManagementAPI.csproj"
COPY . .
WORKDIR "/src/EventManagementAPI"
RUN dotnet build "EventManagementAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventManagementAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventManagementAPI.dll"]
