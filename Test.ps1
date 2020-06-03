Get-ChildItem Env:

dotnet test --configuration "Release" --logger "trx;LogFileName=TEST-results.xml"

#Push-Location -Path ".\tests\Common.Tests\"
#dotnet test --no-build --configuration "Release" --logger "trx;LogFileName=TEST-results.xml"
#if (-not $?) {throw "Failed Common.Tests"}
#Pop-Location

#Push-Location -Path ".\tests\OpenGraphTilemaker.Tests\"
#dotnet test --no-build --configuration "Release" --logger "trx;LogFileName=TEST-results.xml"
#if (-not $?) {throw "Failed OpenGraphTilemaker.Tests"}
#Pop-Location

#Push-Location -Path ".\tests\Experiment.Tests\"
#dotnet test --no-build --configuration "Release" --logger "trx;LogFileName=TEST-results.xml"
#if (-not $?) {throw "Failed Experiment.Tests"}
#Pop-Location
