#
#
#
$capacity = 360*10
$file = [string]::Format("sin-{0}-training-data.csv", $capacity)
remove-item -path $file -force -ErrorAction:Ignore

0..$capacity | 
#    % {$_/10} |
    % {($_/180)*[math]::PI} |
#    select @{Name="X";Expression={$_}}, @{Name="Y";Expression={[math]::Sin($_)}} | 
    select @{Name="Y";Expression={[math]::Sin($_)}}, @{Name="X";Expression={$_}} | #google style Y, X
    convertto-csv -NoTypeInformation -Delimiter "," | 
    % {$_ -replace '"',''} |
    Out-File $file
