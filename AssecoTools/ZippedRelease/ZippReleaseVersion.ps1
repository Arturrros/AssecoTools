<#
    .DESCRIPTION
    Skrypt tylko do wewn�trznego u�ytku  
    Kompresuje wersj� Release do dw�ch lokalizacji 
#>

$ReleasePath = "C:\Users\Art\Dokumenty\Visual Studio 2026\Projects\AssecoTools\AssecoToolsApplication\AssecoTools\ZippedRelease\AssecoTools.zip"

if(Test-Path $ReleasePath)
{
    Remove-Item $ReleasePath 
    }

$compress2 = @{
Path = "C:\Users\Art\Dokumenty\Visual Studio 2026\Projects\AssecoTools\AssecoToolsApplication\AssecoTools\bin\x64\Release\*.dll",
       "C:\Users\Art\Dokumenty\Visual Studio 2026\Projects\AssecoTools\AssecoToolsApplication\AssecoTools\bin\x64\Release\*.config",
       "C:\Users\Art\Dokumenty\Visual Studio 2026\Projects\AssecoTools\AssecoToolsApplication\AssecoTools\bin\x64\Release\AssecoTools.exe" 
CompressionLevel = "Fastest"
DestinationPath = "C:\Users\Art\Dokumenty\Visual Studio 2026\Projects\AssecoTools\AssecoToolsApplication\AssecoTools\ZippedRelease\AssecoTools.zip"
}

Compress-Archive @compress2