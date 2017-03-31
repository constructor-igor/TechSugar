@echo on
echo "<<< OpenSplice HDE Release V6.4.140407OSS For x86_64.win64, Date 2014-04-15 >>>"
if "%SPLICE_ORB%"=="" set SPLICE_ORB=DDS_OpenFusion_1_6_1
set OSPL_HOME=%~dp0
set PATH=%OSPL_HOME%\bin;%OSPL_HOME%\lib;%OSPL_HOME%\examples\lib;%PATH%
set OSPL_TMPL_PATH=%OSPL_HOME%\etc\idlpp
set OSPL_URI=file://%OSPL_HOME%\etc\config\ospl.xml

rem D:\My\@gh\TechSugar\DDS\HelloWorld\sacs_helloworld_sub\bin\Debug\sacs_helloworld_sub.exe
D:\My\@gh\TechSugar\DDS\HelloWorld\sacs_helloworld_pub\bin\Debug\sacs_helloworld_pub.exe

pause
