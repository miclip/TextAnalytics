using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Rest;

namespace TextAnalytics.Service.Services
{
  public class TextAnalyticsService : ITextAnalyticsService
  {
    private readonly ITextAnalyticsClient client;
    private readonly ILogger<TextAnalyticsService> logger;
    private readonly string endpoint;
    public TextAnalyticsService(ITextAnalyticsClient client, IConfiguration configuration, ILogger<TextAnalyticsService> logger)
    {
      this.endpoint = configuration["TextAnalyticsEndpoint"];
      this.client = client;  
      this.logger = logger;

      logger.LogInformation($"Text Analytics Endpoint {this.endpoint}");

      client.Endpoint = this.endpoint;
    }

    public async Task<IList<string>> KeyPhrasesAsync(string text)
    {
      var keyPhraseBatchResult = await client.KeyPhrasesAsync(new MultiLanguageBatchInput(
        new List<MultiLanguageInput>()
        {
          new MultiLanguageInput("en", "1", text)
        }));

      var results = new List<string>();
      foreach (var document in keyPhraseBatchResult.Documents)
      {
        results.AddRange(document.KeyPhrases);
      }
      return results;
    }

    public async Task<double?> SentimentAsync(string text)
    {
      var results = await client.SentimentAsync(new MultiLanguageBatchInput(
        new List<MultiLanguageInput>()
        {
          new MultiLanguageInput("en", "1", text)
        }));
      
      foreach (var document in results.Documents)
      {
        return document.Score;
      }
      return null;
    
    }
    public async Task<IList<string>> EntitiesAsync(string text)
    {
      var keyPhraseBatchResult = await client.EntitiesAsync(new MultiLanguageBatchInput(
        new List<MultiLanguageInput>()
        {
          new MultiLanguageInput("en", "1", text)
        }));

      var results = new List<string>();
      foreach (var document in keyPhraseBatchResult.Documents)
      {
        results.AddRange(document.Entities.Select(s=>s.Name));
      }
      return results;
    }
  }
}