#
# Initialization
#
Clear-Host
#
# Parameters Definition
#
# 2 - Removable drive (include USB)
# 3 - Local hard disk
# 4 - Network disk
# 5 - Compact disk (include DVD)
# 6 - RAM disk
#
$driveType = 3
#
# Print "Disks space status" table
#
$allDidks = GET-WMIOBJECT –query ([String]::Format(“SELECT * from win32_logicaldisk where DriveType = {0}”, $driveType))
$allDidks | Format-table -Property `
    @{name='Disk name'; e={$_.Name}; align='right'; formatstring='N2'}, `
    @{name='Free space(in GB)'; e={$_.Freespace/1GB}; align='right'; formatstring='N2'}, `
    @{name='Free space (in %)'; e={$_.size/$_.Freespace}; align='right'; formatstring='N2'}, `
    @{name='Total Size (in GB)'; e={$_.size/1GB}; align='right'; formatstring='N2'} `
     -autosize

#
# other options
# 
#$allDidks = gdr -PSProvider 'FileSystem'
#$allDidks

<#
    References
    - https://msdn.microsoft.com/en-us/library/aa394173(v=vs.85).aspx
    - http://stackoverflow.com/questions/1663565/list-all-devices-partitions-and-volumes-in-powershell
#>