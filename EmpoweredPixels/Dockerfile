FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
 
FROM mjibrandl/dotnetcore-angular:latest AS build
WORKDIR /src
COPY ["EmpoweredPixels.csproj", ""]
RUN dotnet restore "EmpoweredPixels.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "EmpoweredPixels.csproj" -c Release -o /app
 
FROM build AS publish
RUN dotnet publish "EmpoweredPixels.csproj" -c Release -o /app
 
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
HEALTHCHECK --interval=30s --timeout=3s \
  CMD curl -f http://localhost/ || exit 1
ENTRYPOINT ["dotnet", "EmpoweredPixels.dll"]