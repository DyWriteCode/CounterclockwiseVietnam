@echo off

set "PROTOC_EXE=%cd%\tool\protoc.exe"
set "WORK_DIR=%cd%\ProtoFile"
set "CS_OUT_PATH=%cd%\cs"
::if not exist %CS_OUT_PATH% md %CS_OUT_PATH%

for /f "delims=" %%i in ('dir /b protoFile "ProtoFile/*.proto"') do (
   echo gen protoFile/%%i...
   %PROTOC_EXE%  --proto_path="%WORK_DIR%" --csharp_out="%CS_OUT_PATH%" "%WORK_DIR%\%%i"
   )
echo finish... 

pause