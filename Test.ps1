Get-ChildItem Env:

Push-Location -Path ".\tests\Common.Tests\"
dotnet test --no-build --configuration "Release" --logger "trx;LogFileName=TEST-results.xml"
Pop-Location

Push-Location -Path ".\tests\OpenGraphTilemaker.Tests\"
dotnet test --no-build --configuration "Release" --logger "trx;LogFileName=TEST-results.xml"
Pop-Location

Push-Location -Path ".\tests\Experiment.Web.Client.Tests\"
dotnet test --no-build --configuration "Release" --logger "trx;LogFileName=TEST-results.xml"
Pop-Location
