
$file1 = "@original\x_train.csv"
$file2 = "@original\y_train.csv"
$targetFile = "trainingData.csv"
$sep = ","

$content1 = Get-Content $file1
$content2 = Get-Content $file2
Remove-Item $targetFile

for($i=0; $i -lt $content1.Count; $i++){
    $line1 = $content1[$i];
    $line2 = $content2[$i];
    $line3 = $line1 + $sep + $line2;

#    $line3 = $sep + $line3;
#    97..122 | foreach-object {
#        $newcode = [int] [char]$_ - 97;
#        $line3 = $line3.Replace($sep+[char]$_+$sep, $sep+$newcode+$sep)
#        $line3 = $line3.Replace($sep+[char]$_+$sep, $sep+$newcode+$sep)
#        }
#    $line3 = $line3.Remove(0, 1);

    Add-Content $targetFile $line3
}
