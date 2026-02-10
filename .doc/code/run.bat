@echo off
REM Usage: run.bat [ExcelFilePath] [ExportPath]
REM Defaults: ..\XLSX\Data.xlsx ..\csv\

set /p EXCEL_PATH=Enter Excel file path (default: ../XLSX/Data.xlsx): 
set /p EXPORT_PATH=Enter export path (default: ../../csvdata/): 

if "%EXCEL_PATH%"=="" set "EXCEL_PATH=../XLSX/Data.xlsx"
if "%EXPORT_PATH%"=="" set "EXPORT_PATH=../../csvdata/"

REM Call the Python script with relative paths
python code.py "%EXCEL_PATH%" "%EXPORT_PATH%"