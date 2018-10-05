using System;
using System.Collections.Generic;
using System.Net;
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
    public class JemasServiceTests
    {
        Mock<IHttpClientFactory> httpClientFactoryMock = new Mock<IHttpClientFactory>();
        Mock<HttpClient> httpClientMock = new Mock<HttpClient>();
        Mock<ILogger<JemasService>> loggerMock = new Mock<ILogger<JemasService>>();

        public JemasServiceTests()
        {
            httpClientFactoryMock.Setup(s => s.CreateClient(It.IsAny<string>())).Returns(httpClientMock.Object);
        }

        [Fact]
        public void JemasEmotionAccepted()
        {
            var responseString = @"[{'dominance': 1.06,'valence': 1.2140000000000002,'arousal': -0.672,'dominanceStdDev': 0.7495331880577404,'valenceStdDev': 1.3798347727173714,'arousalStdDev': 0.9795182489366903,'tokens': 15,'alphabeticTokens': 11,'onStopwordTokens': 6,'recognizedTokens': 5,'numberCount': 0}]";
            var httpResponse = new HttpResponseMessage(HttpStatusCode.Accepted);
            httpResponse.Content = new StringContent(responseString); 
            httpClientMock.Setup(client => client.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>())).ReturnsAsync(httpResponse);
            var jemasService = new JemasService(httpClientFactoryMock.Object);
            var response = jemasService.JemasEmotion("InputString").Result;
            Assert.Equal(1.06,response.Dominance);
            Assert.Equal(1.2140000000000002,response.Valence);
            Assert.Equal(-0.672,response.Arousal);
        }

    }
}