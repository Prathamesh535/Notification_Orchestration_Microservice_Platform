param(
    [Parameter(Mandatory=$true)]
    [string]$PackageVersion,

    [Parameter(Mandatory=$true)]
    [string]$SourceBranchName
)

$projectName = "Keka.Notifications.Client"

Write-Host "Executing after success scripts on branch $SourceBranchName"
Write-Host "Triggering Nuget package build"

Set-Location -Path "clients/$projectName/src/$projectName"
dotnet pack -c Release /p:PackageVersion=$PackageVersion --no-restore -o .

Write-Host "Uploading $projectName package to Nuget using branch $SourceBranchName"

dotnet nuget push *.nupkg --api-key AzureDevOps -s https://pkgs.dev.azure.com/kekahr/_packaging/kekahr/nuget/v3/index.json