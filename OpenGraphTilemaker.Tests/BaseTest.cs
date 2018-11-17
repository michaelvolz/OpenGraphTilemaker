using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Microsoft.Extensions.Options;
using Xunit.Abstractions;
using Options = Microsoft.Extensions.Options.Options;

// ReSharper disable ArgumentsStyleLiteral
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CheckNamespace

public class BaseTest
{
    protected BaseTest(ITestOutputHelper testConsole) {
        TestConsole = testConsole;
    }

    protected ITestOutputHelper TestConsole { get; }
}

