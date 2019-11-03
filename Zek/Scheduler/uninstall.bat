@ECHO OFF
set DOTNETFX2=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319
set PATH=%PATH%;%DOTNETFX2%
echo uninstalling WindowsService…
echo —————————————————
InstallUtil /u MyWindowsService.exe
echo —————————————————
echo Done.