cd Server
time /T
set productname=Server
del "bin\x64\Release\%productname%%.msi"
"C:\Program Files (x86)\WiX Toolset v3.6\bin\candle.exe" %productname%.wxs -ext WiXNetFxExtension 
"C:\Program Files (x86)\WiX Toolset v3.6\bin\light.exe" -sice:ICE20 %productname%.wixobj -out "bin\x64\Release\%productname%%.msi" -ext WiXNetFxExtension
cd ..