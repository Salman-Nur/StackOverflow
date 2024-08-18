FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
RUN apt-get update && apt-get install -y nodejs
COPY ["StackOverflow.Worker/*.csproj", "StackOverflow.Worker/"]
COPY ["StackOverflow.Infrastructure/*.csproj", "StackOverflow.Infrastructure/"]
COPY ["StackOverflow.Application/*.csproj", "StackOverflow.Application/"]
COPY ["StackOverflow.Domain/*.csproj", "StackOverflow.Domain/"]
RUN dotnet restore "StackOverflow.Worker/StackOverflow.Worker.csproj"
COPY . .
WORKDIR "/src/StackOverflow.Worker"
RUN dotnet build "StackOverflow.Worker.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "StackOverflow.Worker.csproj" -c Release -o /app

FROM build AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "StackOverflow.Worker.dll"]