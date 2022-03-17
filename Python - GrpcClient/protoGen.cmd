echo OFF
echo Proto Generator from proto file
echo.


IF "%~1" == "" GOTO :EOF
set ProtoDir=%~1
echo Proto Dir : %ProtoDir%


for %%x in (%ProtoDir%\*.proto) do (
	echo Processing Proto File : %%x  
	python -m grpc_tools.protoc -I%ProtoDir% --python_out=. --grpc_python_out=. %%x
)

:EOF
EXIT /B 0