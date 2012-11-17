set SOLUTIONDIR=..\..\..\..
set PROJECTDIR=..\..\..

%SOLUTIONDIR%\UpdateVersion.exe -r Increment -i %PROJECTDIR%\Properties\AssemblyInfo.cs -o %PROJECTDIR%\Properties\assemblyinfo.cs
%SOLUTIONDIR%\UpdateVersion.exe -v File -r Increment -i %PROJECTDIR%\Properties\AssemblyInfo.cs -o %PROJECTDIR%\Properties\assemblyinfo.cs

echo Update done >updateversion.log

@echo off
rem $(SolutionDir)\UpdateVersion.exe -b Increment -i $(ProjectDir)\Properties\AssemblyInfo.cs -o $(ProjectDir)\Properties\assemblyinfo.cs
rem $(SolutionDir)\UpdateVersion.exe -v File -b Increment -i $(ProjectDir)\Properties\AssemblyInfo.cs -o $(ProjectDir)\Properties\assemblyinfo.cs
