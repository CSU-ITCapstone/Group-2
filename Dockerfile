# Use the official Microsoft .NET 5.0 SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

# Set the working directory
WORKDIR /app

# Copy the entire Group-2 folder into the working directory
COPY . .

# Set the working directory to Group-2/PrinterApp
WORKDIR /app/PrinterApp

# Restore dependencies
RUN dotnet restore

# Build the application
RUN dotnet build -c Release -o out

# Use the official Microsoft .NET 5.0 Runtime image as the base image for the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:5.0

# Set the working directory
WORKDIR /app

# Copy the build output from the build environment
COPY --from=build-env /app/PrinterApp/out .

# Expose port 80 for the application to listen on
EXPOSE 80

# Run the application
ENTRYPOINT ["dotnet", "PrinterApp.dll"]
