# Use the official Microsoft .NET SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:5.0

# Set the working directory to the app folder
WORKDIR /app

# Copy the .csproj file into the working directory
COPY Group-2/PrinterApp/PrinterApp.csproj .

# Run dotnet restore
RUN dotnet restore

# Copy the rest of the application code
COPY . .

# Build the application
RUN dotnet build --no-restore -c Release -o /app/out

# Set the working directory for the runtime image
WORKDIR /app/out

# Set the entry point for the container
ENTRYPOINT ["dotnet", "PrinterApp.dll"]
