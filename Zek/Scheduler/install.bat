@ECHO OFF
set DOTNETFX2=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319
set PATH=%PATH%;%DOTNETFX2%
echo Installing WindowsService…
echo —————————————————
InstallUtil /i MyWindowsService.exe
echo —————————————————
echo Done.