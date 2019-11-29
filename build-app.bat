@echo off

:: NOTE: Windows environment variable "Platform" requires to be disabled

set SolutionDir=%cd%

REM ################## VISUAL STUDIO VERSION DETECTION ########################

IF NOT "%VS100COMNTOOLS%"=="" (
	ECHO Visual Studio 2010 detected
	set CompilerToolPath="%VS100COMNTOOLS%"
	set CompilerToolScript=vsvars32.bat
	set CompilerAppPath=%WINDIR%\Microsoft.NET\Framework\v4.0.30319
	set Param=x86
)
IF NOT "%VS110COMNTOOLS%"=="" (
	ECHO Visual Studio 2012 detected
	set CompilerToolPath="%VS110COMNTOOLS%"
	set CompilerToolScript=vsvars32.bat
	set CompilerAppPath=%WINDIR%\Microsoft.NET\Framework\v4.0.30319
	set Param=x86
)
IF NOT "%VS120COMNTOOLS%"=="" (
	ECHO Visual Studio 2013 detected
	set CompilerToolPath="%VS120COMNTOOLS%"
	set CompilerToolScript=vsvars32.bat
	set CompilerAppPath=%WINDIR%\Microsoft.NET\Framework\v4.0.30319
	set Param=x86
)
IF NOT "%VS130COMNTOOLS%"=="" (
	ECHO Visual Studio 2014 detected
	set CompilerToolPath="%VS130COMNTOOLS%"
	set CompilerToolScript=vsvars32.bat
	set CompilerAppPath=%WINDIR%\Microsoft.NET\Framework\v4.0.30319
	set Param=x86
)
IF NOT "%VS140COMNTOOLS%"=="" (
	ECHO Visual Studio 2015 detected
	set CompilerToolPath="%VS140COMNTOOLS%"
	set CompilerToolScript=vsvars32.bat
	set CompilerAppPath=%WINDIR%\Microsoft.NET\Framework\v4.0.30319
	set Param=
)

:: Visual Studio 2017 detection
::    not functional yet
::    must be manually disabled if not installed

set VS2017Path=C:\Program Files (x86)\Microsoft Visual Studio\2017
::set "VS2017Exe=C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\IDE\devenv.exe"
::IF EXIST "%SolutionDir%\.vs" (
	ECHO Visual Studio 2017 detected
	set CompilerToolPath=%VS2017Path%\Professional\VC\Auxiliary\Build
	set CompilerToolScript=vcvars32.bat
	set CompilerAppPath="%VS2017Path%\Professional\MSBuild\15.0\Bin"
	set Param=
::)


REM ECHO Compiler Tool Path: %CompilerToolPath%
echo:


REM ############################## APPLICATIONS ###############################

call "%CompilerToolPath%\%CompilerToolScript%" %Param%
echo:

echo COMPILING Anim Grapher...
echo:     
%CompilerAppPath%\MSBuild.exe AnimGrapher.sln /nologo /p:Configuration=Release;Platform="Any CPU";WarningLevel=0 /v:q
echo:
