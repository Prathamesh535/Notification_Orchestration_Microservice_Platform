param(
    [Parameter(Mandatory=$true)]
    [string]$PackageVersion,

    [Parameter(Mandatory=$true)]
    [string]$SourceBranchName
)

# Base directory containing project folders
$baseDirectory = "clients/"

# Build solution file in release mode inside the base directory
Write-Host "Building solution in release mode"
$solutionPath = "./Keka.Notifications.sln" # Replace with your solution file path
dotnet build -c Release $solutionPath

# Get all project directories under the base directory
$projectDirectories = Get-ChildItem -Path $baseDirectory -Directory

foreach ($projectDir in $projectDirectories) {
    $projectPath = Join-Path $baseDirectory $projectDir
    $scriptPath = Join-Path $projectPath "scripts/dotnet-pack.ps1"
    Write-Host "Project: $($projectDir.Name)"
    Write-Host "Script: $scriptPath"
    # Check if the script file exists
    if (Test-Path -Path $scriptPath) {
        Write-Host "Running dotnet-pack.ps1 for project: $($projectDir.Name)"

        $initialLocation = Get-Location

        # Execute the PowerShell script for packaging and publishing
        . $scriptPath -PackageVersion $PackageVersion -SourceBranchName $SourceBranchName
        
        Write-Host "Completed: $($projectDir.Name)"
        
        Set-Location -Path $initialLocation
    } else {
        Write-Host "dotnet-pack.ps1 script not found for project: $($projectDir.Name)"
    }
}

Write-Host "Finished packaging all projects."