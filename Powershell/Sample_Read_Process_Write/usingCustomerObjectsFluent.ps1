#
# Pay attention, $root contains absolute path where execution of the script starts
#
$root = "D:\My\MyProjects\@TechSugar\TechSugar.Samples\trunk\Powershell\Sample_Read_Process_Write"
$reportFile = "D:\My\MyProjects\@TechSugar\TechSugar.Samples\trunk\Powershell\reportFile.txt"

$absolutePathInterfaces = Join-Path $root "CustomerObjects\4Binaries\bin\Debug\Customer.Interfaces.dll"
$absolutePathTextData = Join-Path $root "CustomerObjects\4Binaries\bin\Debug\Customer.TextData.dll"
$absolutePathDataProcessing = Join-Path $root "CustomerObjects\4Binaries\bin\Debug\Customer.DataProcessing.dll"
$absolutePathMainConsole = Join-Path $root "CustomerObjects\4Binaries\bin\Debug\MainConsole.exe"
$absolutePathTPL = Join-Path $root "CustomerObjects\packages\Microsoft.Tpl.Dataflow.4.5.23\lib\portable-net45+win8+wpa81\System.Threading.Tasks.Dataflow.dll"

[System.Reflection.Assembly]::LoadFile($absolutePathInterfaces)
[System.Reflection.Assembly]::LoadFile($absolutePathTextData)
[System.Reflection.Assembly]::LoadFile($absolutePathDataProcessing)
[System.Reflection.Assembly]::LoadFile($absolutePathMainConsole)
[System.Reflection.Assembly]::LoadFile($absolutePathTPL)

$dataStorage = ".\DataFiles"
$dataFilter = "*.txt"
$minimalNumberOfSymbols = 10
$pathToInput = New-Object MainConsole.PathToFolder -Args $dataStorage
$reportFileObject = New-Object MainConsole.PathToFile -ArgumentList $reportFile

$factory = New-Object MainConsole.FlowBuilderFactory_v3
$factory.DataFromFolder($dataFilter)
$factory.LoadData()
$factory.Filter($minimalNumberOfSymbols)
$factory.Processing()
$factory.ToReport()
$factory.ReportToFile($reportFileObject)

$factory.CreateSequentialFlow()
$factory.Post($pathToInput)
$factory.Wait()
