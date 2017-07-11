$targetDir = Resolve-Path "."
$solutionDir = $targetDir

$i = 0
while($true) {
    if(Join-Path $solutionDir ".solution" | Test-Path) {
        break
    }

    $solutionDir = Split-Path $solutionDir
    $i++
    if($i -gt 1000) {
        Write-Error "Unable to find solution root directory"
        exit -1
    }
}

xcopy "$(Join-Path $solutionDir "libs/x86/*.dll")" "$targetDir" /Y
xcopy "$(Join-Path $solutionDir "ini")" "$targetDir" /Y /S