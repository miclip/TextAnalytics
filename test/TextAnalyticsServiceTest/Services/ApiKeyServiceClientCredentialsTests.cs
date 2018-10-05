using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Rest;
using Moq;
using TextAnalytics.Service.Services;
using Xunit;

namespace TextAnalytics.Service.Services.Test
{
    public class ApiKeyServiceClientCredentialsTests
    {
        Mock<IConfiguration> configurationMock = new Mock<IConfiguration>();
        ApiKeyServiceClientCredentials apiKey;

        Mock<ILogger<TextAnalyticsService>> loggerMock = new Mock<ILogger<TextAnalyticsService>>();

        public ApiKeyServiceClientCredentialsTests()
        {
            configurationMock.SetupGet(p => p[It.IsAny<string>()]).Returns("SubscriptionKeyValue");
        }

        [Fact]
        public async Task ProcessHttpRequestAsyncHasSubscriptionKey()
        {   
            var apiKeyServiceClientCredentials = new ApiKeyServiceClientCredentials(configurationMock.Object);
            var httpRequestMessage = new HttpRequestMessage();
            var t = apiKeyServiceClientCredentials.ProcessHttpRequestAsync(httpRequestMessage, CancellationToken.None);
            t.Wait();
            var key = httpRequestMessage.Headers.GetValues("Ocp-Apim-Subscription-Key").FirstOrDefault();
            Assert.Equal("SubscriptionKeyValue",key);
        }

       
    }
}