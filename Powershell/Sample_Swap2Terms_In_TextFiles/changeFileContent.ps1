$dataStorage = ".\DataFiles"
$dataFilter = "*.xml"
$term1 = '<DataItem Polarization="Black">'
$term2 = '<DataItem Polarization="White">'
$temporaryTerm = [guid]::NewGuid().ToString()
$date = Get-Date -format H_mm_ss
$resultStorage = $dataStorage + "_RESULT_"+$date


#
# copy data files from source to destination folder (by filter)
#
Copy-Item -Path $dataStorage -Filter $dataFilter -Destination $resultStorage –Recurse -Force

#
# get data files from destination folder
#
$dataFiles = Get-ChildItem -Recurse -Path $resultStorage -Filter $dataFilter

foreach ($dataFile in $dataFiles)
{
    #$dataFile.Name
    #$dataFile.DirectoryName
    #$dataFile.Length
    
    $dataFileFileName = Join-Path $dataFile.DirectoryName $dataFile.Name
    #$resultDirectory = Join-Path $dataFile.Directory.Parent.FullName "RESULT"    
    #$resultDataFile = Join-Path $resultDirectory $dataFile.Name    

    #New-Item -ItemType Directory -Force -Path $resultDirectory | Out-Null

    #"name: " + $dataFile.Name
    #"result directory: " + $resultDirectory        

   (Get-Content $dataFileFileName) | 
   ForEach-Object {$_ -replace $term1, $temporaryTerm} | 
   ForEach-Object {$_ -replace $term2, $term1} | 
   ForEach-Object {$_ -replace $temporaryTerm, $term2} | 
   Out-File -Force -FilePath $dataFileFileName   
}