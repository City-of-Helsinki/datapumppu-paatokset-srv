FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY AhjoApiService/AhjoApiService.csproj .
RUN dotnet restore "AhjoApiService.csproj"

COPY . .
RUN dotnet publish "AhjoApiService/AhjoApiService.csproj" -c Release -o /publish
    
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "AhjoApiService.dll"]