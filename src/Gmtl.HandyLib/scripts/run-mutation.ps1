param(
  [string]$TestProject = "Gmtl.HandyLib.Tests"
)

Write-Host "Running mutation tests for $TestProject"

$scriptRoot = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent
$repoRoot = Split-Path -Path $scriptRoot -Parent

Push-Location $repoRoot
Set-Location $TestProject

if (-not (Test-Path "../dotnet-tools.json")) {
  dotnet new tool-manifest
}

try {
  dotnet tool install dotnet-stryker
} catch {
  Write-Host "Local install failed; attempting global install..."
  dotnet tool install -g dotnet-stryker
}

dotnet stryker --open-report

Pop-Location
