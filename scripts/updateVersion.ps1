param(
    [Parameter(Mandatory=$true)]
    [string]$PackageVersion
)

# Update package version in build props file.
$propsFilePath = "./Directory.Build.props"
$content = Get-Content $propsFilePath -Raw
$content = $content -replace '<Version>.*?</Version>', "<Version>$PackageVersion</Version>"
$content | Set-Content $propsFilePath