#
#
#
$capacity = 10
$file = "sin-2000-training-data.csv"
remove-item -path $file -force -ErrorAction:Ignore

0..$capacity | 
    % {$_/10} |
    select @{Name="X";Expression={$_}}, @{Name="Y";Expression={[math]::Sin($_)}} | 
    convertto-csv -NoTypeInformation -Delimiter "," | 
    % {$_ -replace '"',''} |
    Out-File $file

#$csvContent | Out-File $file
#| select-object @{Name="X";Expression={$_.'$_, [math]::Sin($_)'}}
# {Name="Y";Expression={[math]::Sin($_)}}