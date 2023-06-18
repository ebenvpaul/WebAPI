# Initialize a new build stage and set the Base Image 
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
# Set the working directory inside the container
WORKDIR /app
# Expose the port 5000
EXPOSE 5000
# Sets the environment variable
ENV ASPNETCORE_URLS=http://+:5000






# Initialize the new build stage and set the Build Image
FROM mcr.microsoft.com/dotnet/sdk:7.0  AS build
# Set the working directory for subsequent COPY and RUN commands
WORKDIR /src
# Copy the project file to the container filesystem /src folder
COPY ["WebAPI/WebAPI.csproj", "./app/"]
# Build the application80
RUN dotnet  restore "./app/WebAPI.csproj" --disable-parallel
# Copy everything to the container filesystem
COPY ./WebAPI/ ./app
# Set the working directory for subsequent RUN commands
WORKDIR "/src/app"
# Execute project build with all of its dependencies
RUN dotnet build "WebAPI.csproj" -c Release -o /app/build
# Pick up where a previous build stage left off
FROM build AS publish
RUN dotnet  publish "WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false --no-restore

# Pick up where a previous base stage left off
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production

# Start the application
ENTRYPOINT ["dotnet", "WebAPI.dll", "--workers=4"]
