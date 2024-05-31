FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Parnain.Template.Blazor/Parnain.Template.Blazor.csproj", "Parnain.Template.Blazor/"]
RUN dotnet restore "Parnain.Template.Blazor/Parnain.Template.Blazor.csproj"
COPY . .
WORKDIR "/src/Parnain.Template.Blazor"
RUN dotnet build "Parnain.Template.Blazor.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Parnain.Template.Blazor.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Parnain.Template.Blazor.dll"]
