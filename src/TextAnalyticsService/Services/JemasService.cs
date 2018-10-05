using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TextAnalytics.Service.Models;

namespace TextAnalytics.Service.Services
{
  public class JemasService : IJemasService
  {
    IHttpClientFactory httpClientFactory;
    public JemasService(IHttpClientFactory httpClientFactory)
    {
      this.httpClientFactory = httpClientFactory;
    }
    public async Task<JemasResult> JemasEmotion(string value)
    {
       var cleanValue = value.Replace("\n","");
       var client = httpClientFactory.CreateClient("Jemas");
       var response = await client.PostAsync("/emotion", new StringContent(cleanValue));
       response.EnsureSuccessStatusCode();
       string content = await response.Content.ReadAsStringAsync();
       return JsonConvert.DeserializeObject<JemasResult[]>(content)[0];
    }
  }
}