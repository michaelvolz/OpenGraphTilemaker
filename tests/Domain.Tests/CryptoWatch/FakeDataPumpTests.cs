using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BaseTestCode;
using Common.Extensions;
using Domain.CryptoWatch;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace Domain.Tests.CryptoWatch
{
    public class FakeDataPumpTests : BaseTest<FakeDataPumpTests>
    {
        public FakeDataPumpTests(ITestOutputHelper output)
            : base(output)
        {
        }

        [Fact]
        public async Task Get_Some_Data()
        {
            var fakeDataPump = new FakeDataPump();

            using var cancellationTokenSource = new CancellationTokenSource(300);

            var result = new List<CryptoEntity>();

            await foreach (var cryptoEntity in fakeDataPump.DataStreamAsync(cancellationTokenSource.Token))
            {
                result.Add(cryptoEntity);
                TestConsole.LogInformation("{Item}", cryptoEntity.ReturnDumpFlat());
            }

            result.Should().NotBeEmpty();
            result.Count.Should().BeGreaterThan(0);
        }
    }
}
