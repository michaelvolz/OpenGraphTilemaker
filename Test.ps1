Push-Location -Path ".\tests\Common.Tests\"
dotnet test
Pop-Location

Push-Location -Path ".\tests\OpenGraphTilemaker.Tests\"
dotnet test
Pop-Location

Push-Location -Path ".\tests\OpenGraphTilemaker.Web.Client.Tests\"
dotnet test
Pop-Location