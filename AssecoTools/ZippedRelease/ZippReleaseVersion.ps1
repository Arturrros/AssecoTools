<#
    .DESCRIPTION
    Skrypt tylko do wewnêtrznego u¿ytku  
    Kompresuje wersjê Release do dwóch lokalizacji 
#>

$SharePath = "C:\vm_share\AssecoTools.zip"
If(Test-Path $SharePath)
{
    Remove-Item $sharePath
    }

$compress = @{
Path = "C:\Users\Artur.Balon\Documents\GitLocal\AssecoTools\AssecoToolsApplication\AssecoTools\bin\x64\Release\*.dll",
       "C:\Users\Artur.Balon\Documents\GitLocal\AssecoTools\AssecoToolsApplication\AssecoTools\bin\x64\Release\*.config",
       "C:\Users\Artur.Balon\Documents\GitLocal\AssecoTools\AssecoToolsApplication\AssecoTools\bin\x64\Release\AssecoTools.exe" 
CompressionLevel = "Fastest"
DestinationPath = "C:\vm_share\AssecoTools.zip"
}
Compress-Archive @compress

$ReleasePath = "C:\Users\Artur.Balon\Documents\GitLocal\AssecoTools\AssecoToolsApplication\AssecoTools\ZippedRelease\AssecoTools.zip"

if(Test-Path $ReleasePath)
{
    Remove-Item $ReleasePath 
    }

$compress2 = @{
Path = "C:\Users\Artur.Balon\Documents\GitLocal\AssecoTools\AssecoToolsApplication\AssecoTools\bin\x64\Release\*.dll",
       "C:\Users\Artur.Balon\Documents\GitLocal\AssecoTools\AssecoToolsApplication\AssecoTools\bin\x64\Release\*.config",
       "C:\Users\Artur.Balon\Documents\GitLocal\AssecoTools\AssecoToolsApplication\AssecoTools\bin\x64\Release\AssecoTools.exe" 
CompressionLevel = "Fastest"
DestinationPath = "C:\Users\Artur.Balon\Documents\GitLocal\AssecoTools\AssecoToolsApplication\AssecoTools\ZippedRelease\AssecoTools.zip"
}
Compress-Archive @compress2