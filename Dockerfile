
# These ARGs allow for swapping out the base used to make the final image when debugging from VS
ARG LAUNCHING_FROM_VS
# This sets the base image for final, but only if LAUNCHING_FROM_VS has been defined
ARG FINAL_BASE_IMAGE=${LAUNCHING_FROM_VS:+aotdebug}

# =========================================================
# 1. Base Runtime Stage (Visual Studio Fast Mode uses this)
# =========================================================
# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
USER root

# Install tools, configure Microsoft production repo, and install PowerShell
RUN apt-get update && apt-get install -y --no-install-recommends wget curl libunwind8 nano inotify-tools procps -y && \
    apt-get install -y wget ca-certificates && \
    wget -q https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && \
    dpkg -i packages-microsoft-prod.deb && \
    apt-get update && apt-get install -y powershell && \
    rm -rf /var/lib/apt/lists/*

#install node npm nvm etc
COPY --from=node:20 /usr/local/bin/node /usr/local/bin/
COPY --from=node:20 /usr/local/lib/node_modules /usr/local/lib/node_modules

# Re-link npm and npx so they work globally
RUN ln -s /usr/local/lib/node_modules/npm/bin/npm-cli.js /usr/local/bin/npm \
    && ln -s /usr/local/lib/node_modules/npm/bin/npx-cli.js /usr/local/bin/npx \
    && npx playwright install-deps

# Revert to the standard non-root user for default security
USER $APP_UID


# =========================================================
# 2. Build Stage
# =========================================================
# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# Install clang/zlib1g-dev dependencies for publishing to native
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    clang zlib1g-dev
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SpeedApply.csproj", "."]
RUN dotnet restore "./SpeedApply.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./SpeedApply.csproj" -c $BUILD_CONFIGURATION -o /app/build


# =========================================================
# 3. Publish Stage
# =========================================================
# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SpeedApply.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=true


# =========================================================
# 4. Final Stage (Production Execution)
# =========================================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
USER root
COPY --from=publish /app/publish .

# Run the .NET Playwright PowerShell script to download headless browsers
# Since dependencies were installed in step 1, this step downloads browser files
RUN pwsh playwright.ps1 install

# Secure the container runtime environment before execution
USER $APP_UID

CMD ["/bin/sh", "-c", "/app/SpeedApply"]
