@echo off
REM sc create "NAS_Service" binPath= "C:\Program Files (x86)\NAANSolution\ETL\NAS_ETLService.exe"
sc create "NAS_Service" binPath= ".\NAS_ETLService.exe" start= auto
sc start "NAS_Service"
