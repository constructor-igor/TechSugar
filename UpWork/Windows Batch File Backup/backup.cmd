rem
rem backup script
rem 

set backupFolder="test"

for %%G in (.txt, .doc) do forfiles /P %backupFolder% /S /M *%%G /C "cmd /c rename @file @file.backup"

pause