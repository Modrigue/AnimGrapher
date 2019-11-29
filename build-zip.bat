@echo off

set ZipAppPath="C:\Program Files\7-Zip"

For /f "tokens=1-3 delims=/ " %%a in ('date /t') do (set CurDate=%%c-%%b-%%a)
echo Current date: %CurDate%
echo:

set SolutionDir=%cd%
set BuildDir=%SolutionDir%\AnimGrapher\bin\Release
set OutputDir=%SolutionDir%\build
::echo %SolutionDir%

:: NOTE: strings.exe requires to be executed once for its use to be agreed
set StringsFileName=%SolutionDir%\dependencies\strings.exe


REM ################################ ARCHIVES #################################

IF NOT EXIST %OutputDir% (
	echo CREATING output directory
	echo:
	mkdir %OutputDir%
)

echo: 
echo ARCHIVING Anim Grapher...
echo: 

cd %BuildDir%
set AppFileName=AnimGrapher.exe
echo: 
echo Get version number...
call:getFileVersion %AppFileName% NumVersion
echo ...Version number found: %NumVersion%
echo:

cd %BuildDir%
:: set ZipFileName=%CurDate%-AnimGrapher-%NumVersion%.zip
set ZipFileName=AnimGrapher_%NumVersion%.zip
:: Remove spaces in filename
set ZipFileName=%ZipFileName: =%
:: cd setup
%ZipAppPath%\7z.exe a -tzip %ZipFileName% %AppFileName% %AppFileName%.config AnimGrapher.pdb AnimGrapherEquations.txt
move %ZipFileName% %OutputDir%
cd %SolutionDir%

goto:eof

:: ############################################################################
:: #              Function returning the executable version                   #
:: ############################################################################

:getFileVersion    - [FileName] [&Version]
SET FileVer=
FOR /F "tokens=1 delims=[]" %%A IN ('%StringsFileName% %AppFileName% ^| FIND /N /V "" ^| FIND /I "FileVersion"') DO SET LineNum=%%A
SET /A LineNum += 1
FOR /F "tokens=1* delims=[]" %%A IN ('%StringsFileName% %AppFileName% ^| FIND /N /V "" ^| FIND "[%LineNum%]"') DO SET FileVer=%%B
REM SET FileVer
for /f "tokens=1 delims=." %%A in ("%FileVer%") DO SET NumBuild=%%A
for /f "tokens=2 delims=." %%B in ("%FileVer%") DO SET NumMajor=%%B
for /f "tokens=3 delims=." %%C in ("%FileVer%") DO SET NumMinor=%%C
set %~2=%NumBuild%.%NumMajor%.%NumMinor%
goto:eof