# Experimental repository for Blazor (server)
[![Build Status](https://dev.azure.com/flynn-azure/OpenGraphTilemaker/_apis/build/status/michaelvolz.OpenGraphTilemaker?branchName=master)](https://dev.azure.com/flynn-azure/OpenGraphTilemaker/_build/latest?definitionId=3&branchName=master)
[![License: Unlicense](https://img.shields.io/badge/license-Unlicense-blue.svg)](http://unlicense.org/)
[![Dependabot enabled](https://img.shields.io/badge/Dependabot-enabled-blue.svg)](https://dependabot.com/)

## Work In Progress
## Why?
I want to try Blazor with a set of tools and patterns and libraries I personally find useful and evaluate it's complexity, usability and performance. Over simpified examples don't give me the answers I am looking for.

### Implemented Features

* [ASP.NET Core 3.1](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-3.1)
* [C# nullable reference types](https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references)
* [Blazor server-side](https://docs.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-3.1)
* State management => [Blazor.State](https://github.com/TimeWarpEngineering/blazor-state) and [MediatR](https://github.com/jbogard/MediatR)
* Formvalidation:
  * [Fluent Validation](https://fluentvalidation.net/)
  * ValidationError idea from [Blazor.Validation](https://github.com/PeterHimschoot/Blazor.Validation)
  * ValidationSummary idea from [Blazor.Validation](https://github.com/PeterHimschoot/Blazor.Validation)
* Tests
  * [xUnit](https://xunit.net/)
  * [Autofixture](https://github.com/AutoFixture/AutoFixture)
  * [nSubstitute](https://nsubstitute.github.io/)
* OpenGraph TileMaker:
  * RSS feedreader as source
  * Sorting 
  * Searching
  * Disc and memory caching
  * Simple loading indicator
  * Data initialisation from parent
  * Automatically generated basic TagCloud
  * TODO: paging
  * TODO: switch grid-layout
* Miscellaneous Utility Components
  * Visibility
  * TODO: FeatureFlag
  * TODO: Stopwatch
  * TODO: Caching
  * TODO: DataLoading
  * TODO: Logging
  * TODO: Html debug border/outline
* CryptoWatch DashBoard:
  * TODO: Create websocket source with fake data
  * TODO: Consume live websocket stream
  * TODO: Update in realtime
  * TODO: Use Reactive Extensions (Rx)
* [ZURB foundation 6.6.3](https://get.foundation/)
  * [Foundation Icons](https://zurb.com/playground/foundation-icon-fonts-3)
* Modified filestructure
  * Blazor Feature Folders adapted from [Feature Folder Structure in ASP.NET Core](https://scottsauber.com/2016/04/25/feature-folder-structure-in-asp-net-core/)
  * Removed "Pages" and "Shared"
* [Serilog](https://serilog.net/) for Logging
* Using as less JavaScript as possible
* [JetBrains Annotations](https://blog.jetbrains.com/dotnet/2018/05/03/what-are-jetbrains-annotations/)
* Custom Guard clauses, customised from [Ardalis.GuardClauses](https://github.com/ardalis/GuardClauses)
* [Feed Syndication](https://www.nuget.org/packages/Microsoft.SyndicationFeed.ReaderWriter/)
* [Azure Pipelines](https://azure.microsoft.com/en-us/services/devops/pipelines/)
  * Build release, including code analysis and stylecop
  * Run tests
  * Status badge
* [WebCompiler](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.WebCompiler)
  * [SCSS](https://sass-lang.com/) compilation and minification
  * JavaScript transformation and minification
* [Dependabot](https://dependabot.com/)
  * Include badge
 
License: [unlicense](http://unlicense.org/)
