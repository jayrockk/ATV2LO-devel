@echo off
call 1_____release.bat
echo on
call 2_____build_Server.bat
@echo off
call 3_____release_package.bat
call 4_____rename_package.bat
pause