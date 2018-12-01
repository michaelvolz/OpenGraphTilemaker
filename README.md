# Some Blazor.Net Examples

## Implemented Features

* State management => [Blazor.State](https://github.com/TimeWarpEngineering/blazor-state) and MediatR
* Formvalidation:
  * Fluent Validation
  * ValidationError idea from https://github.com/PeterHimschoot/Blazor.Validation
  * ValidationSummary idea from https://github.com/PeterHimschoot/Blazor.Validation
* Tests => xUnit, Autofixture, nSubstitute
* OpenGraph TileMaker:
  * RSS feedreader as source
  * Sorting 
  * Searching
  * Disc and memory caching
  * Loading indicator
  * TODO: paging
  * TODO: switch layouts
  * TODO: use API call for client version (feedreader is not compatible)
* ZURB foundation 6.5
* Code Analysis
  * FxCopAnalyzers
  * Aspnetcore.Mvc.Analyzers
  * AspNetCore.Mvc.Api.Analyzers
  * EntityFrameworkCore.Analyzers
  * StyleCop.Analyzers
* Switchable Blazor Modes
  * Server side
  * Client side
* Modified filestructure
  * Blazor Feature Folders adapted from https://scottsauber.com/2016/04/25/feature-folder-structure-in-asp-net-core/
  * Removed Pages, Shared
* Serilog for Logging
   * Blazor client-logging by NBlumhardt https://github.com/nblumhardt/serilog-blazor
* Using as less JavaScript as possible
* JetBrains Annotations
* Custom Guard clauses, customised from Ardalis.GuardClauses https://github.com/ardalis/GuardClauses
* Feed Syndication = Microsoft.SyndicationFeed.ReaderWriter
* Foundation Icons

License: http://unlicense.org/