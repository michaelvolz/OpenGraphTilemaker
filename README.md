# Some Blazor.Net Examples

[![Build Status](https://dev.azure.com/flynn-azure/BlazorExperiments/_apis/build/status/michaelvolz.BlazorExperiments)](https://dev.azure.com/flynn-azure/BlazorExperiments/_build/latest?definitionId=2)
[![License: Unlicense](https://img.shields.io/badge/license-Unlicense-blue.svg)](http://unlicense.org/)
[![Dependabot enabled](https://img.shields.io/badge/Dependabot-enabled-blue.svg)](https://dependabot.com/)

## Why?
I want to try Blazor with a set of tools and patterns and libraries I personally find useful and evaluate it's complexity, usability and performance. Over simpified examples don't give me the answers I am looking for.

At the moment I am really pleased with the state of Blazor!

### Implemented Features

* State management => [Blazor.State](https://github.com/TimeWarpEngineering/blazor-state) and [MediatR](https://github.com/jbogard/MediatR)
* Formvalidation:
  * [Fluent Validation](https://fluentvalidation.net/)
  * ValidationError idea from [Blazor.Validation](https://github.com/PeterHimschoot/Blazor.Validation)
  * ValidationSummary idea from [Blazor.Validation](https://github.com/PeterHimschoot/Blazor.Validation)
* Tests => xUnit, Autofixture, nSubstitute
* OpenGraph TileMaker:
  * RSS feedreader as source
  * Sorting 
  * Searching
  * Disc and memory caching
  * Simple loading indicator
  * Data initialisation from parent -> best practices
  * Basic TagCloud
  * TODO: paging
  * TODO: switch grid-layout
  * TODO: use API call for client version (feedreader is not client compatible)
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
* ZURB foundation 6.5
  * Foundation Icons
* Code Analysis
  * FxCopAnalyzers 
  * Aspnetcore.Mvc.Analyzers
  * AspNetCore.Mvc.Api.Analyzers
  * EntityFrameworkCore.Analyzers
  * StyleCop.Analyzers
* Switchable Blazor Modes adapted from [BlazorDualMode](https://github.com/Suchiman/BlazorDualMode)
  * Server side
  * Client side
* Modified filestructure
  * Blazor Feature Folders adapted from [Feature Folder Structure in ASP.NET Core](https://scottsauber.com/2016/04/25/feature-folder-structure-in-asp-net-core/)
  * Removed Pages, Shared
* Serilog for Logging
  * Blazor client-logging by NBlumhardt [serilog-blazor](https://github.com/nblumhardt/serilog-blazor)
* Using as less JavaScript as possible
* JetBrains Annotations
* Custom Guard clauses, customised from [Ardalis.GuardClauses](https://github.com/ardalis/GuardClauses)
* Feed Syndication = Microsoft.SyndicationFeed.ReaderWriter
* Azure Pipelines
  * Build release, including code analysis and stylecop
  * Run tests
  * Status badge
* Dependabot 
  * Include badge

License: [unlicense](http://unlicense.org/)
