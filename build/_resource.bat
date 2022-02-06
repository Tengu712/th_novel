@echo off

"C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\Roslyn\csc.exe" ^
/out:resource.exe ^
/target:exe ^
/platform:x86 ^
/debug- ^
../res/ResGen.cs
