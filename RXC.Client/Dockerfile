FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

# Copy the project file and restore dependencies
COPY RXC.Client.csproj ./
RUN dotnet restore

# Copy all source files and publish
COPY . ./
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Use a lightweight image to serve the frontend static files (e.g. nginx)
FROM nginx:alpine

# Clean default nginx static files
    RUN rm -rf /usr/share/nginx/html/*

    # Copy published files to nginx wwwroot folder
    COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

    EXPOSE 80

    CMD ["nginx", "-g", "daemon off;"]
