using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Rest;

namespace TextAnalytics.Service.Services
{
  public class ApiKeyServiceClientCredentials : ServiceClientCredentials
  {
    private const string SubscriptionKeyHeaderName = "Ocp-Apim-Subscription-Key";
    private readonly string subscriptionKey;
    public ApiKeyServiceClientCredentials(IConfiguration configuration)
    {
      this.subscriptionKey = configuration["SubscriptionKey"];

      if (string.IsNullOrWhiteSpace(this.subscriptionKey))
        throw new Exception("ApiKeyServiceClientCredentials missing SubscriptionKey");
    }
    public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      request.Headers.Add(SubscriptionKeyHeaderName, subscriptionKey);
      return base.ProcessHttpRequestAsync(request, cancellationToken);
    }
  }

}