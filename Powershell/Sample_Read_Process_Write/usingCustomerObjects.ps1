#
# Pay attention, $root contains absolute path where execution of the script start
#
$root = "D:\My\MyProjects\@TechSugar\TechSugar.Samples\trunk\Powershell\Sample_Read_Process_Write"
$absolutePathInterfaces = Join-Path $root "CustomerObjects\4Binaries\bin\Debug\Customer.Interfaces.dll"
$absolutePathTextData = Join-Path $root "CustomerObjects\4Binaries\bin\Debug\Customer.TextData.dll"
$absolutePathDataProcessing = Join-Path $root "CustomerObjects\4Binaries\bin\Debug\Customer.DataProcessing.dll"
[System.Reflection.Assembly]::LoadFile($absolutePathInterfaces)
[System.Reflection.Assembly]::LoadFile($absolutePathTextData)
[System.Reflection.Assembly]::LoadFile($absolutePathDataProcessing)

$dataStorage = ".\DataFiles"
$dataFilter = "*.txt"

$loadTextData = New-Object Customer.TextData.CustomerTextDataFactory
$filter = New-Object Customer.DataProcessing.FilterTextData -argumentList 10
$processing = New-Object Customer.DataProcessing.WeightTextData

$dataFiles = Get-ChildItem -Recurse -Path $dataStorage -Filter $dataFilter
foreach ($dataFile in $dataFiles)
{
    $textData = $loadTextData.LoadFromFile($dataFile.FullName)
    if ($filter.Run($textData))
    {
        "passed: " + $dataFile.FullName
        "result: " + $processing.Run($textData)
    } else
    {
        "ignored: " + $dataFile.FullName
    }
}
