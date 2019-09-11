using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using BaseTestCode;
using Common.Extensions;
using Common.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Websocket.Client;
using Xunit;
using Xunit.Abstractions;

namespace Common.Tests
{
    public class WebSocketTest : BaseTest<WebSocketTest>
    {
        public WebSocketTest(ITestOutputHelper output) : base(output) { }

        private static readonly int FiveSeconds = (int)TimeSpan.FromSeconds(5).TotalMilliseconds;
        private static readonly int ThirtySeconds = (int)TimeSpan.FromSeconds(30).TotalMilliseconds;
        private static readonly ManualResetEvent ExitEvent = new ManualResetEvent(true);
        private static readonly Uri Url = new Uri("wss://ws-feed.shrimpy.io/");

        private const string Subscribe_CoinbasePro_LTC_BTC_BBO =
            "{ \"type\": \"subscribe\", \"exchange\": \"coinbasepro\", \"pair\": \"eth-btc\", \"channel\": \"bbo\" }";


        //[Fact]
        public async Task WebSocket_Connection_Successful()
        {
            using var client = new WebsocketClient(Url) {ReconnectTimeoutMs = ThirtySeconds};

            client.ReconnectionHappened.Subscribe(type => TestConsole.LogInformation("Reconnection happened, type: {Type}", type));
            var messageStore = new MessageStore(ApplicationLogging.CreateLogger<MessageStore>());

            client.MessageReceived.Subscribe(msg => messageStore.ProcessMessage(msg));
            await client.Start();

            await Task.Run(() => client.Send(Subscribe_CoinbasePro_LTC_BTC_BBO));

            await Task.Delay(FiveSeconds);
            if (messageStore.BestBidOffer == null) await Task.Delay(FiveSeconds);

            ExitEvent.WaitOne();

            messageStore.BestBidOffer.Should().NotBeNull();
            messageStore!.BestBidOffer!.channel.Should().NotBeNullOrWhiteSpace();
            TestConsole.WriteLine(messageStore.BestBidOffer.ReturnDump());
        }
    }

    public class MessageStore
    {
        private readonly ILogger<MessageStore> _logger;

        public MessageStore(ILogger<MessageStore> logger) => _logger = logger;

        public Shrimpy.BestBidOffer? BestBidOffer { get; set; }

        public void ProcessMessage(ResponseMessage msg)
        {
            if (msg.MessageType != WebSocketMessageType.Text) return;
            if (!msg.Text.StartsWith("{\"exchange\"")) return;

            var bbo = msg.Text.JSONUnserialize<Shrimpy.BestBidOffer>();

            if (bbo.channel == "bbo")
            {
                if (BestBidOffer == null || bbo.content.sequence > BestBidOffer.content.sequence)
                {
                    BestBidOffer = bbo;
                    return;
                }

                BestBidOffer.content.asks.AddRange(bbo.content.asks);
                BestBidOffer.content.bids.AddRange(bbo.content.bids);
            }
            else
            {
                _logger.LogInformation("Wrong channel: {Channel}", bbo.channel);
            }
        }
    }


    public class Shrimpy
    {
        public class BestBidOffer
        {
            // ReSharper disable InconsistentNaming
            public string exchange { get; set; } = string.Empty;
            public string pair { get; set; } = string.Empty;
            public string channel { get; set; } = string.Empty;

            public Content content { get; set; } = new Content();
            // ReSharper restore InconsistentNaming
        }

        public class Content
        {
            // ReSharper disable InconsistentNaming
            public int sequence { get; set; }

            public List<Ask> asks { get; set; } = new List<Ask>();

            public List<Bid> bids { get; set; } = new List<Bid>();
            // ReSharper restore InconsistentNaming
        }

        public class Ask
        {
            // ReSharper disable InconsistentNaming
            public string price { get; set; } = string.Empty;

            public string quantity { get; set; } = string.Empty;
            // ReSharper restore InconsistentNaming
        }

        public class Bid
        {
            // ReSharper disable InconsistentNaming
            public string price { get; set; } = string.Empty;

            public string quantity { get; set; } = string.Empty;
            // ReSharper restore InconsistentNaming
        }

        public class Pong
        {
            // ReSharper disable InconsistentNaming
            public string type { get; set; } = string.Empty;

            public int data { get; set; }
            // ReSharper restore InconsistentNaming
        }

        public class MarketData
        {
            // ReSharper disable InconsistentNaming
            public Ticker[]? tickers { get; set; }
            // ReSharper restore InconsistentNaming
        }

        public class Ticker
        {
            // ReSharper disable InconsistentNaming
            public string name { get; set; } = string.Empty;
            public string symbol { get; set; } = string.Empty;
            public string priceUsd { get; set; } = string.Empty;
            public string priceBtc { get; set; } = string.Empty;
            public string percentChange24hUsd { get; set; } = string.Empty;

            public DateTime lastUpdated { get; set; }
            // ReSharper restore InconsistentNaming
        }
    }
}