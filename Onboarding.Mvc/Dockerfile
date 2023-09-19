FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Onboarding.Mvc/Onboarding.Mvc.csproj", "Onboarding.Mvc/"]
RUN dotnet restore "Onboarding.Mvc/Onboarding.Mvc.csproj"
COPY . .
WORKDIR "/src/Onboarding.Mvc"
RUN dotnet build "Onboarding.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Onboarding.Mvc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Onboarding.Mvc.dll"]
