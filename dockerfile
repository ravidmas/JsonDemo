FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Install required dependencies
RUN apt-get update && apt-get install -y \
    curl \
    unzip \
    openjdk-17-jdk \
    && rm -rf /var/lib/apt/lists/*

ENV JAVA_HOME=/usr/lib/jvm/java-17-openjdk-amd64
ENV PATH="$JAVA_HOME/bin:$PATH"

# Install Android SDK
ENV ANDROID_SDK_ROOT=/opt/android-sdk
ENV PATH="$ANDROID_SDK_ROOT/cmdline-tools/latest/bin:$ANDROID_SDK_ROOT/platform-tools:$PATH"

RUN mkdir -p $ANDROID_SDK_ROOT/cmdline-tools && \
    curl -o /tmp/cmdline-tools.zip https://dl.google.com/android/repository/commandlinetools-linux-11076708_latest.zip && \
    unzip /tmp/cmdline-tools.zip -d $ANDROID_SDK_ROOT/cmdline-tools && \
    mv $ANDROID_SDK_ROOT/cmdline-tools/cmdline-tools $ANDROID_SDK_ROOT/cmdline-tools/latest && \
    rm /tmp/cmdline-tools.zip

# Accept licenses and install Android SDK packages
RUN yes | sdkmanager --licenses && \
    sdkmanager \
    "platform-tools" \
    "platforms;android-35" \
    "build-tools;35.0.0"

# Install .NET MAUI workload
RUN dotnet workload install maui-android

# Set working directory
WORKDIR /app

# Copy project files
COPY MauiApp8/MauiApp8.csproj ./MauiApp8/

# Restore dependencies
RUN dotnet restore MauiApp8/MauiApp8.csproj -f net9.0-android

# Copy all source files
COPY MauiApp8/ ./MauiApp8/

# Build and publish the Android APK
RUN dotnet publish MauiApp8/MauiApp8.csproj \
    -f net9.0-android \
    -c Release \
    -o /app/output \
    --no-restore

FROM alpine:latest AS output

WORKDIR /output

# Copy the APK from build stage
COPY --from=build /app/output/*.apk ./

CMD ["ls", "-la", "/output"]