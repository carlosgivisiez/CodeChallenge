FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/CodeChallenge.Auth.Admin.Api/CodeChallenge.Auth.Admin.Api.csproj", "src/CodeChallenge.Auth.Admin.Api/"]
COPY ["src/CodeChallenge.Auth.Admin.EntityFramework.Shared/CodeChallenge.Auth.Admin.EntityFramework.Shared.csproj", "src/CodeChallenge.Auth.Admin.EntityFramework.Shared/"]
COPY ["src/CodeChallenge.Auth.Admin.EntityFramework.SqlServer/CodeChallenge.Auth.Admin.EntityFramework.SqlServer.csproj", "src/CodeChallenge.Auth.Admin.EntityFramework.SqlServer/"]
COPY ["src/CodeChallenge.Auth.Shared/CodeChallenge.Auth.Shared.csproj", "src/CodeChallenge.Auth.Shared/"]
COPY ["src/CodeChallenge.Auth.Admin.EntityFramework.PostgreSQL/CodeChallenge.Auth.Admin.EntityFramework.PostgreSQL.csproj", "src/CodeChallenge.Auth.Admin.EntityFramework.PostgreSQL/"]
COPY ["src/CodeChallenge.Auth.Admin.EntityFramework.MySql/CodeChallenge.Auth.Admin.EntityFramework.MySql.csproj", "src/CodeChallenge.Auth.Admin.EntityFramework.MySql/"]
RUN dotnet restore "src/CodeChallenge.Auth.Admin.Api/CodeChallenge.Auth.Admin.Api.csproj"
COPY . .
WORKDIR "/src/src/CodeChallenge.Auth.Admin.Api"
RUN dotnet build "CodeChallenge.Auth.Admin.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CodeChallenge.Auth.Admin.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "CodeChallenge.Auth.Admin.Api.dll"]