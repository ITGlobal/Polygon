$ErrorActionPreference = "Stop"

function protogen-cqgc() {
    Write-Host "Generating source code for CQGC... "  -f cyan -n
    $protogen = Resolve-Path "./tools/protogen/protogen.exe"  
    pushd "./src/Polygon.Connector.CQGContinuum"
    & $protogen -i:WebAPI.proto -o:WebAPI.cs -ns:Polygon.Connector.CQGContinuum.WebAPI -q -p:fixCase=false -p:lightFramework=true
    $source = Get-Content "./WebAPI.cs"
    $source = $source.Replace("WebAPI_1", "Polygon.Connector.CQGContinuum.WebAPI")
    $source = $source.Replace("WebAPI1", "Polygon.Connector.CQGContinuum.WebAPI") 
    $source = $source.Replace("public class", "internal class")
    $source = $source.Replace("public partial class", "internal partial class")
    Set-Content "./WebAPI.cs" $source 
    popd
    Write-Host "Done" -f Green
}

function protogen-spimex() {
    Write-Host "Generating source code for SPIMEX... "  -f cyan -n
    $protogen = Resolve-Path "./tools/protogen/protogen.exe"  
    pushd "./src/spimex/SpimexAdapter"
    & $protogen -i:proto/FTEMessages.proto -o:proto/FTEMessages.cs -ns:SpimexAdapter.FTE -q -p:fixCase=false -p:lightFramework=true
    $source = Get-Content "./proto/FTEMessages.cs"
    $source = $source.Replace("FTE", "SpimexAdapter.FTE")
    Set-Content "./proto/FTEMessages.cs" $source
    popd
    Write-Host "Done" -f Green
}

protogen-cqgc
protogen-spimex