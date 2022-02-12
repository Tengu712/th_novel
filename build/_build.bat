@echo off

"C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\Roslyn\csc.exe" ^
/out:novel.exe ^
/target:winexe ^
/platform:x86 ^
/debug- ^
/recurse:..\src\*.cs ^
/res:resource.resx
