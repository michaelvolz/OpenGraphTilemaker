Push-Location -Path ".\tests\Common.Tests\"
dotnet test --configuration "Release" --logger "trx;LogFileName=TEST-results.xml"
Pop-Location

Push-Location -Path ".\tests\OpenGraphTilemaker.Tests\"
dotnet test --configuration "Release" --logger "trx;LogFileName=TEST-results.xml"
Pop-Location

Push-Location -Path ".\tests\OpenGraphTilemaker.Web.Client.Tests\"
dotnet test --configuration "Release" --logger "trx;LogFileName=TEST-results.xml"
Pop-Location