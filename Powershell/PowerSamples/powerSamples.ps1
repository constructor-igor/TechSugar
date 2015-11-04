$battery = Get-WmiObject -Class Win32_Battery | Select-Object -First 1
$noAC = $battery -ne $null -and $battery.BatteryStatus -eq 1
 
if ($noAC)
{
    'On battery power'
}
else 
{
    'Connected to AC'
}