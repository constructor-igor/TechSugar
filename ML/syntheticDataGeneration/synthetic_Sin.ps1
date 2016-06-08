#
#
#
$capacity = 10
$file = [string]::Format("sin-{0}-training-data.csv", $capacity)
remove-item -path $file -force -ErrorAction:Ignore

0..$capacity | 
    % {$_/10} |
    select @{Name="X";Expression={$_}}, @{Name="Y";Expression={[math]::Sin($_)}} | 
    convertto-csv -NoTypeInformation -Delimiter "," | 
    % {$_ -replace '"',''} |
    Out-File $file
