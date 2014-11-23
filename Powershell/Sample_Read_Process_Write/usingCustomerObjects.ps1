#
# Pay attention, $root contains absolute path where execution of the script starts
#
$root = "D:\My\MyProjects\@TechSugar\TechSugar.Samples\trunk\Powershell\Sample_Read_Process_Write"
$reportFile = "D:\My\MyProjects\@TechSugar\TechSugar.Samples\trunk\Powershell\reportFile.txt"

$absolutePathInterfaces = Join-Path $root "CustomerObjects\4Binaries\bin\Debug\Customer.Interfaces.dll"
$absolutePathTextData = Join-Path $root "CustomerObjects\4Binaries\bin\Debug\Customer.TextData.dll"
$absolutePathDataProcessing = Join-Path $root "CustomerObjects\4Binaries\bin\Debug\Customer.DataProcessing.dll"
[System.Reflection.Assembly]::LoadFile($absolutePathInterfaces)
[System.Reflection.Assembly]::LoadFile($absolutePathTextData)
[System.Reflection.Assembly]::LoadFile($absolutePathDataProcessing)

$dataStorage = ".\DataFiles"
$dataFilter = "*.txt"
$minimalNumberOfSymbols = 10

$loadTextData = New-Object Customer.TextData.CustomerTextDataFactory
$filter = New-Object Customer.DataProcessing.FilterTextData -argumentList $minimalNumberOfSymbols
$processing = New-Object Customer.DataProcessing.WeightTextData
$processingReport = New-Object Customer.DataProcessing.ProcessingReport
$processingReportHelper = New-Object Customer.DataProcessing.ProcessingReportHelper

$dataFiles = Get-ChildItem -Recurse -Path $dataStorage -Filter $dataFilter
foreach ($dataFile in $dataFiles)
{
    $textData = $loadTextData.LoadFromFile($dataFile.FullName)
    if ($filter.Run($textData))
    {
        $processingResult = $processing.Run($textData)
        $processingReport.AddEnabled($dataFile.FullName, $processingResult)
    } else
    {
        $processingReport.AddDisabled($dataFile.FullName)
    }
}
$processingReportHelper.WriteTo($processingReport, $reportFile)

"report in file" + $reportFile
