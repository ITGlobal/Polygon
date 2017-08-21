function Print-Header($title) {
    Write-Host ""
    Write-Host "$title" -f Cyan
    Write-Host "$("-" * 80)" -f Yellow    
}

$ErrorActionPreference = "Stop"

# -----------------------------------------------------------------------------
#
# Detect build version
#
# -----------------------------------------------------------------------------
Print-Header "Detecting version number"
$version = $(git tag --list v*) | Select-Object -Last 1
if ([string]::IsNullOrWhiteSpace($version)) {
    Write-Host "No valid git version tag found, falling back to v0.0"
    $version = "v0.0"
}

$match = [regex]::Match($version, "^v(|\.)([0-9]+)\.([0-9]+)(|\.[0-9]+)$")
if ($match.Success) {
    $VER_MAJOR = [int]::Parse($match.Groups[2].Value)
    $VER_MINOR = [int]::Parse($match.Groups[3].Value)
}
else {
    Write-Host "Git version tag is malformed: `"$version`". Expected a `"v[0-9].[0-9]`" value" -f Red
    Wrire-Host "Will use fallback value v0.0"
    $VER_MAJOR = 0
    $VER_MINOR = 0
}

$VER_SUFFIX = ""
if ($env:APPVEYOR) {
    $VER_BUILD = [int]::Parse($env:APPVEYOR_BUILD_NUMBER)
    if (-not [string]::IsNullOrEmpty($env:APPVEYOR_PULL_REQUEST_NUMBER)) {
        Write-Host "It's a " -n
        Write-Host "PullRequest #$env:APPVEYOR_PULL_REQUEST_NUMBER" -n -f Green
        Write-Host " build"
        $VER_SUFFIX = "-pullrequest$env:APPVEYOR_PULL_REQUEST_NUMBER"
    }
    else {
        if ($env:APPVEYOR_REPO_BRANCH -ne "master") {
            Write-Host "It's a " -n
            Write-Host "branch $env:APPVEYOR_REPO_BRANCH" -n -f Green
            Write-Host " build"
            $safeBranchName = [regex]::Replace($env:APPVEYOR_REPO_BRANCH, "[^a-zA-Z0-9]", "")
            $VER_SUFFIX = "-$$safeBranchName"
        }
    }
}
else {
    Write-Host "Build is not running on AppVeyor, will use default build number 0"
    $VER_BUILD = 0
}

$VERSION = "$VER_MAJOR.$VER_MINOR.$VER_BUILD$VER_SUFFIX"
Write-Host "Version number: " -n
Write-Host "$VERSION" -f yellow

if ($env:APPVEYOR) { 
    appveyor UpdateBuild -Version "$VERSION"
}

# -----------------------------------------------------------------------------
#
# Clean build artifacts
#
# -----------------------------------------------------------------------------
Print-Header "Cleaning build artifacts"
& dotnet clean /nologo -v q
if ($LASTEXITCODE -ne 0) {
    Write-Host "`"dotnet clean`" failed with $LASTEXITCODE"
    exit $LASTEXITCODE
}


# -----------------------------------------------------------------------------
#
# Restore nuget package dependencies
#
# -----------------------------------------------------------------------------
Print-Header "Restoring dependencies"
& dotnet restore /nologo -v q /p:Version=$VERSION
if ($LASTEXITCODE -ne 0) {
    Write-Host "`"dotnet restore`" failed with $LASTEXITCODE"
    exit $LASTEXITCODE
}

# -----------------------------------------------------------------------------
#
# Compile projects
#
# -----------------------------------------------------------------------------
Print-Header "Compiling"
& dotnet build /nologo -v q -c Release /p:Version=$VERSION
if ($LASTEXITCODE -ne 0) {
    Write-Host "`"dotnet build`" failed with $LASTEXITCODE"
    exit $LASTEXITCODE
}

# -----------------------------------------------------------------------------
#
# Create nuget packages
#
# -----------------------------------------------------------------------------
Print-Header "Packaging artifacts"
$ARTIFACTS = Join-Path (Resolve-Path ".") "artifacts"
if (-not (Test-Path $ARTIFACTS)) {
    New-Item -Path $ARTIFACTS -ItemType Directory | Out-Null
}

Get-ChildItem $ARTIFACTS | Remove-Item -Recurse -Force

& dotnet pack /nologo -v q -c Release /p:Version=$VERSION --include-symbols --include-source --output $ARTIFACTS
if ($LASTEXITCODE -ne 0) {
    Write-Host "`"dotnet pack`" failed with $LASTEXITCODE"
    exit $LASTEXITCODE
}

Write-Host "Completed" -f Green