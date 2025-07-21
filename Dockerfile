FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

# Copy everything in solution folder to container
COPY . .

# Restore solution (includes backend and frontend projects)
RUN dotnet restore RenewXControl.sln

# Publish backend project only
RUN dotnet publish RenewXControl/RenewXControl.csproj -c Release -r linux-x64 --self-contained true -o /out /p:PublishTrimmed=false

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /app

COPY --from=build /out .

ENTRYPOINT ["./RenewXControl"]
