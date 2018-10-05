namespace TextAnalytics.Service.Models
{
    public class JemasResult 
    {
      public JemasResult (double dominance, double valence, double arousal)
      {
         this.Arousal = arousal;
         this.Dominance = dominance;
         this.Valence = valence;
      }

       public double Dominance {get; private set;}

       public double Valence {get; private set;}

       public double Arousal {get; private set;}

    }
}