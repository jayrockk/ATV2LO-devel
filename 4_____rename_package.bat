REM z:
REM cd "\Visual Studio 2010\Projects\FTR2LO\ftr2loservice - Vail - 0.5.0 version"

set productname=ATV2LO
call setvars.bat
cd _out\disk1
ren %productname%.wssx %productname%_%productversion%.wssx
cd ..\..
del setvars.bat
