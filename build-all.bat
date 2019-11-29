@echo off

call:StartTimer

call build-app
call build-zip

call:StopTimer
echo:
call:DisplayTimerResult
echo:
pause

goto:eof


:: ############################################################################
:: #                             Timer functions                              #
:: ############################################################################

:StartTimer
:: Store start time
set StartTIME=%TIME%
for /f "usebackq tokens=1-4 delims=:., " %%f in (`echo %StartTIME: =0%`) do set /a Start100S=1%%f*360000+1%%g*6000+1%%h*100+1%%i-36610100
goto:eof

:StopTimer
:: Get the end time
set StopTIME=%TIME%
for /f "usebackq tokens=1-4 delims=:., " %%f in (`echo %StopTIME: =0%`) do set /a Stop100S=1%%f*360000+1%%g*6000+1%%h*100+1%%i-36610100
:: Test midnight rollover. If so, add 1 day=8640000 1/100ths secs
if %Stop100S% LSS %Start100S% set /a Stop100S+=8640000
set /a TookTime=%Stop100S%-%Start100S%
set TookTimePadded=0%TookTime%
goto:eof

:DisplayTimerResult
:: Show timer start/stop/delta
::echo Started: %StartTime%
::echo Stopped: %StopTime%
::echo Compilation time: %TookTime:~0,-2%.%TookTimePadded:~-2% seconds
echo Compilation time: %TookTime:~0,-2% s
goto:eof