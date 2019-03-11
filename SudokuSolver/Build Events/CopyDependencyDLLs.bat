set "OutputDir=%1"
set "Platform=%2"

rem Debugging
rem echo %OutputDir% > log.txt
rem echo %OutputDir%..\..\..\..\3rdParty\DLL >> log.txt

cd %OutputDir%

(robocopy ..\..\..\..\3rdParty\DLL\%Platform% .\ /IS /IT) ^& exit 0