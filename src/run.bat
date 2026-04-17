@echo off
cd \d "%dp0"
:loop
dotnet run
if %errorlevel% == 4 goto loop