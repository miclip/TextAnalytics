using System.Threading.Tasks;
using TextAnalytics.Service.Models;

namespace TextAnalytics.Service.Services
{
    public interface IJemasService
    {
      Task<JemasResult> JemasEmotion(string value); 
    }
}