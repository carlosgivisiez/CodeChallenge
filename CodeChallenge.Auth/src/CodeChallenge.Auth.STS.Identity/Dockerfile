FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/CodeChallenge.Auth.STS.Identity/CodeChallenge.Auth.STS.Identity.csproj", "src/CodeChallenge.Auth.STS.Identity/"]
COPY ["src/CodeChallenge.Auth.Admin.EntityFramework.Shared/CodeChallenge.Auth.Admin.EntityFramework.Shared.csproj", "src/CodeChallenge.Auth.Admin.EntityFramework.Shared/"]
COPY ["src/CodeChallenge.Auth.Shared/CodeChallenge.Auth.Shared.csproj", "src/CodeChallenge.Auth.Shared/"]
RUN dotnet restore "src/CodeChallenge.Auth.STS.Identity/CodeChallenge.Auth.STS.Identity.csproj"
COPY . .
WORKDIR "/src/src/CodeChallenge.Auth.STS.Identity"
RUN dotnet build "CodeChallenge.Auth.STS.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CodeChallenge.Auth.STS.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "CodeChallenge.Auth.STS.Identity.dll"]