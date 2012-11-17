wix_tool "Server\Server.wxs" "TopLevelTab\AddIn.xml"

rem copy "SubTab\bin\x64\Release\FTR2LO_Vail.SubTab.dll" "_out"
copy "TopLevelTab\bin\x64\Release\ATV2LO.TopLevelTab.dll" "_out"
copy "TopLevelTab\AddIn.xml" "_out"
copy "TopLevelTab\Package.ddf" "_out"
copy "TopLevelTab\ATV2LO.addin" "_out"
