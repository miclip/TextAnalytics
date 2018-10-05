using System.Collections.Generic;

namespace  TextAnalytics.Service.Models
{
    public class AnalyzeResult
    {

      public AnalyzeResult(IEnumerable<string> keyPhrases, double? sentiment, IEnumerable<string> entities, double dominance,double valence, double arousal)
      {
        this.KeyPhrases = keyPhrases;
        this.Sentiment = sentiment;
        this.Entities = entities;
        this.Dominance = dominance;
        this.Valence = valence;
        this.Arousal = arousal;
      }
      public IEnumerable<string> KeyPhrases {get;private set;}

      public IEnumerable<string> Entities {get;private set;}

      public double? Sentiment {get;private set;}
      public double Dominance {get;private set;}
      public double Valence {get;private set;}
      public double Arousal {get;private set;}
    }
}