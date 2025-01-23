# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0@sha256:3fcf6f1e809c0553f9feb222369f58749af314af6f063f389cbd2f913b4ad556 AS build
WORKDIR /App

# Copy the .csproj and restore dependencies (via layers for better caching)
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the source code and publish the application
COPY . ./
RUN dotnet publish -c Release -o /out

# Stage 2: Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0@sha256:b4bea3a52a0a77317fa93c5bbdb076623f81e3e2f201078d89914da71318b5d8
WORKDIR /App

# Copy the published output from the build stage
COPY --from=build /out ./

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "Kampus.dll"]   # Replace "Kampus.dll" with the actual DLL name of your project
