FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app 
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY ["PracticalTest.Endpoint/PracticalTest.Endpoint.csproj", "PracticalTest.Endpoint/"]
RUN dotnet restore "PracticalTest.Endpoint/PracticalTest.Endpoint.csproj"
COPY . .

RUN dotnet build "PracticalTest.Endpoint/PracticalTest.Endpoint.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PracticalTest.Endpoint/PracticalTest.Endpoint.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PracticalTest.Endpoint.dll"]
