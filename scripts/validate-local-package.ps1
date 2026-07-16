param(
    [string] $Configuration = "Release"
)

$ErrorActionPreference = "Stop"

$repoRoot = Resolve-Path (Join-Path $PSScriptRoot "..")
$packageDir = Join-Path $repoRoot "artifacts/packages"
$consumerDir = Join-Path $repoRoot "artifacts/consumer-test"
$consumerConfig = Join-Path $consumerDir "NuGet.Config"

if (Test-Path $consumerDir) {
    Remove-Item $consumerDir -Recurse -Force
}

New-Item -ItemType Directory -Force -Path $packageDir | Out-Null
New-Item -ItemType Directory -Force -Path $consumerDir | Out-Null

dotnet clean (Join-Path $repoRoot "Orizon.UI.sln")
dotnet restore (Join-Path $repoRoot "Orizon.UI.sln")
dotnet build (Join-Path $repoRoot "Orizon.UI.sln") --configuration $Configuration --no-restore
dotnet test (Join-Path $repoRoot "Orizon.UI.sln") --configuration $Configuration --no-build
dotnet pack (Join-Path $repoRoot "src/Orizon.UI/Orizon.UI.csproj") --configuration $Configuration --output $packageDir --no-build

dotnet new mvc --framework net8.0 --output $consumerDir
dotnet new nugetconfig --output $consumerDir
dotnet nuget add source $packageDir --name OrizonLocalConsumer --configfile $consumerConfig
dotnet add $consumerDir package Orizon.UI --version 1.0.0-mvp --source $packageDir
dotnet restore $consumerDir
dotnet build $consumerDir --configuration $Configuration

Remove-Item $consumerDir -Recurse -Force
