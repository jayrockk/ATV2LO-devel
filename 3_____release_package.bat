REM  z:
REM cd "\Visual Studio 2010\Projects\FTR2LO\ftr2loservice - Vail - 0.5.0 version"

copy "Server\bin\x64\Release\Server.msi" "_out"
copy "eula.rtf" "_out"

cd _out
makecab /f package.ddf
cd ..