using System;
using System.Collections.Generic;
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
    public class TextAnalyticsServiceTests
    {
        Mock<IConfiguration> configurationMock = new Mock<IConfiguration>();
        ApiKeyServiceClientCredentials apiKey;
        Mock<ITextAnalyticsClient> clientMock;

        Mock<ILogger<TextAnalyticsService>> loggerMock = new Mock<ILogger<TextAnalyticsService>>();

        public TextAnalyticsServiceTests()
        {
            configurationMock.SetupGet(p => p[It.IsAny<string>()]).Returns("somevalue");
            apiKey = new ApiKeyServiceClientCredentials(configurationMock.Object);
            clientMock = new Mock<ITextAnalyticsClient>();
        }

        [Fact]
        public async Task KeyPhrasesResult()
        {   
            var httpResult = new HttpOperationResponse<KeyPhraseBatchResult>();
            httpResult.Body = new KeyPhraseBatchResult(new List<KeyPhraseBatchResultItem>(new List<KeyPhraseBatchResultItem>(){new KeyPhraseBatchResultItem(new List<string>(){"phrase1", "phrase2"})}),null);
            clientMock.Setup(s=>s.KeyPhrasesWithHttpMessagesAsync(It.IsAny<MultiLanguageBatchInput>(),null, 
                CancellationToken.None)).ReturnsAsync(httpResult);
            TextAnalyticsService textAnalyticsService = new TextAnalyticsService(clientMock.Object,configurationMock.Object,loggerMock.Object);
            var result = await textAnalyticsService.KeyPhrasesAsync(It.IsAny<string>());
            clientMock.Verify(s=>s.KeyPhrasesWithHttpMessagesAsync(It.IsAny<MultiLanguageBatchInput>(),null, 
                CancellationToken.None),Times.Once);
            Assert.IsType<List<string>>(result);
            Assert.Equal("phrase1", result[0]);
            Assert.Equal("phrase2", result[1]);
        }

        [Fact]
        public async Task SentimentAsyncResult()
        {   
            var httpResult = new HttpOperationResponse<SentimentBatchResult>();
            httpResult.Body = new SentimentBatchResult(new List<SentimentBatchResultItem>(new List<SentimentBatchResultItem>(){new SentimentBatchResultItem((double?)1)}),null);
            clientMock.Setup(s=>s.SentimentWithHttpMessagesAsync(It.IsAny<MultiLanguageBatchInput>(),null, 
                CancellationToken.None)).ReturnsAsync(httpResult);
            TextAnalyticsService textAnalyticsService = new TextAnalyticsService(clientMock.Object,configurationMock.Object,loggerMock.Object);
            var result = await textAnalyticsService.SentimentAsync(It.IsAny<string>());
            clientMock.Verify(s=>s.SentimentWithHttpMessagesAsync(It.IsAny<MultiLanguageBatchInput>(),null, 
                CancellationToken.None),Times.Once);
            Assert.IsType<double>(result);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task EntitiesAsyncResult()
        {   
            var httpResult = new HttpOperationResponse<EntitiesBatchResult>();
            httpResult.Body = new EntitiesBatchResult(new List<EntitiesBatchResultItem>(new List<EntitiesBatchResultItem>(){new EntitiesBatchResultItem(null,new List<EntityRecord>(){new EntityRecord("name1"), new EntityRecord("name2")})}),null);
            clientMock.Setup(s=>s.EntitiesWithHttpMessagesAsync(It.IsAny<MultiLanguageBatchInput>(),null, 
                CancellationToken.None)).ReturnsAsync(httpResult);
            TextAnalyticsService textAnalyticsService = new TextAnalyticsService(clientMock.Object,configurationMock.Object,loggerMock.Object);
            var result = await textAnalyticsService.EntitiesAsync(It.IsAny<string>());
            clientMock.Verify(s=>s.EntitiesWithHttpMessagesAsync(It.IsAny<MultiLanguageBatchInput>(),null, 
                CancellationToken.None),Times.Once);
            Assert.IsType<List<string>>(result);
            Assert.Equal("name1", result[0]);
            Assert.Equal("name2", result[1]);
        }
    }
}