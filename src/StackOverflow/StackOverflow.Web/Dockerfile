FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
RUN apt-get update && apt-get install -y nodejs
COPY ["StackOverflow.Web/*.csproj", "StackOverflow.Web/"]
COPY ["StackOverflow.Application/*.csproj", "StackOverflow.Application/"]
COPY ["StackOverflow.Domain/*.csproj", "StackOverflow.Domain/"]
COPY ["StackOverflow.Infrastructure/*.csproj", "StackOverflow.Infrastructure/"]
RUN dotnet restore "StackOverflow.Web/StackOverflow.Web.csproj"
COPY . .
WORKDIR "/src/StackOverflow.Web"
RUN dotnet build "StackOverflow.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "StackOverflow.Web.csproj" -c Release -o /app

FROM build AS final
WORKDIR /app
COPY --from=publish /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "StackOverflow.Web.dll"]