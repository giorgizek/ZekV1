@echo off
cls
setlocal

REM -------------------------------- Folder to compress---------------------------------
set dir=%~dp0
REM ------------------------------------------------------------------------------------

REM change to directory
cd %dir%

REM Path to WinRAR executable in Program Files
set path="C:\Program Files\WinRAR\";%path%

REM Replace space in hour with zero if it's less than 10
SET hr=%time:~0,2%
IF %hr% lss 10 SET hr=0%hr:~1,1%

REM This sets the date like this: mm-dd-yr-hrminsecs1/100secs
REM Set TODAY=%date:~4,2%-%date:~7,2%-%date:~10,4%-%hr%%time:~3,2%%time:~6,2%%time:~9,2%


echo.
echo Folder to compress in *.RAR format:
echo %dir%
echo.
echo.

REM Compress files in directory individually (no subdirectories)
:indiv
echo.
echo.
FOR %%i IN (*.BAK) do (
rar a -m5 "%%~ni" "%%i"
del "%%i"
)
goto eof


:eof
endlocal

echo.
echo "Task Completed"
echo.

pause
exit
