rem
rem
rem /D:"C:\Program Files (x86)\Microsoft Visual Studio 12.0\Team Tools\Static Analysis Tools\FxCop"

"C:\Program Files (x86)\Microsoft Visual Studio 12.0\Team Tools\Static Analysis Tools\FxCop\FxCopCmd.exe" /out:"results.xml" /file:"..\SamplesForCodeAnalysis\bin\debug\SamplesForCodeAnalysis.dll" /rule:"..\UserDefinedRule\bin\debug\UserDefinedRule.dll" 

pause
