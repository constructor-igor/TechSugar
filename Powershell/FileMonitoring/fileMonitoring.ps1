#
#
#
<#
    Refernces:
    http://stackoverflow.com/questions/29066742/watch-file-for-changes-and-run-command-with-powershell
    http://programmingthisandthat.blogspot.de/2014/04/create-delegates-that-will-be-used-when.html

#>

Clear-Host

$filePath = "."
$fileName = "test.txt"
$pattern = "pattern"

$updated = { 
    $r = Select-String -Pattern $pattern -Path test.txt
    if ($r.Matches.Count -gt 0)
    {
        $now = Get-Date
        Write-Host "File: " $EventArgs.FullPath " " $EventArgs.ChangeType " " $now ;
    }
}

#$global:UpdateEvent = $EventArgs

$watcher = New-Object System.IO.FileSystemWatcher
$watcher.Path = $filePath
$watcher.Filter = $fileName
$watcher.IncludeSubdirectories = $false
$watcher.EnableRaisingEvents = $true
$watcher.NotifyFilter = [System.IO.NotifyFilters]::LastWrite

if($ChangedEvent) {$ChangedEvent.Dispose()}
$ChangedEvent = Register-ObjectEvent $watcher "Changed" -Action $updated
$watcher.EnableRaisingEvents = $true

do
{
} while ($true)