function GetDateTime(){
    $result = ""
    Try{
        $result = Invoke-RestMethod -Uri "https://timeapi.io/api/Time/current/zone?timeZone=Asia/Kolkata"
        return [DateTime]$result.dateTime
    }
    Catch{
        #Output Error details: Exception message
        Write-Host $_.Exception.Message

        # Check if $result is empty. If empty, generate Local date time and return result
        if([string]::IsNullOrEmpty($result)){
			return [DateTime]::Now
		}
    }
}

# Read Migration name from user input and create sql migration file with that name
$MigrationName = Read-Host -Prompt 'Enter Migration Name'

# Replace migration name in file name with only alphanumeric characters
$MigrationName = $MigrationName -replace '[^a-zA-Z0-9]', ''

# Get current date time
$DateTime = GetDateTime

# Generate File Prefix
$FilePrefix = '{0:yyMMddHHmmssff}' -f $dateTime

# Generate Folder Path by Year, Month
$FolderPath = "Scripts\$($DateTime.Year)\$($DateTime.Month)"

# Create SQL migration file name by appending date time and migration name
$MigrationFileName = "$($FilePrefix)_$($MigrationName).sql"

# Create SQL migration file
New-Item -Path "$FolderPath\$MigrationFileName" -ItemType File -Force
