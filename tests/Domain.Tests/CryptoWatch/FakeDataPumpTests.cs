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

            var cryptoEntities = new List<CryptoEntity>();

            await foreach (var item in fakeDataPump.DataStreamAsync(cancellationTokenSource.Token))
            {
                cryptoEntities.Add(item);
                TestConsole.LogInformation("Item: {Item}", item.ReturnDumpFlat());
            }

            cryptoEntities.Should().NotBeEmpty();
            cryptoEntities.Count.Should().BeGreaterThan(0);
        }
    }
}
