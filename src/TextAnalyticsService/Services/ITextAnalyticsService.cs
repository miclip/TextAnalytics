using System.Collections.Generic;
using System.Threading.Tasks;

namespace TextAnalytics.Service.Services
{
    public interface ITextAnalyticsService
    {
        Task<IList<string>> KeyPhrasesAsync(string text);

        Task<double?> SentimentAsync(string text);

        Task<IList<string>> EntitiesAsync(string text);
        
    }
}