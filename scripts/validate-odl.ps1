$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $PSScriptRoot
$cssRoot = Join-Path $root 'src\Orizon.UI\wwwroot\css'
$components = @(Get-ChildItem (Join-Path $cssRoot 'components') -Filter '*.css') + @(Get-ChildItem (Join-Path $cssRoot 'layout') -Filter '*.css')

$fixedColors = $components | Select-String -Pattern '#[0-9a-fA-F]{3,8}|rgba?\('
if ($fixedColors) {
    $fixedColors | ForEach-Object { Write-Error "Cor fixa em $($_.Path):$($_.LineNumber): $($_.Line.Trim())" }
}

$required = @('design-tokens.css', 'themes\light.css', 'themes\dark.css', 'themes\construction.css', 'themes\real-estate.css', 'themes\corporate.css')
foreach ($file in $required) {
    if (-not (Test-Path (Join-Path $cssRoot $file))) { throw "Arquivo ODL ausente: $file" }
}

Write-Host 'ODL validation passed.'
