using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TextAnalytics.Service.Models;
using TextAnalytics.Service.Services;

namespace TextAnalytics.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyzeController : ControllerBase
    {
        private readonly ITextAnalyticsService textAnalyticsService;
        private readonly ILogger<AnalyzeController> logger;

        private readonly IJemasService jemasService;
        public AnalyzeController(ITextAnalyticsService textAnalyticsService,IJemasService jemasService, ILogger<AnalyzeController> logger)
        {
            this.textAnalyticsService = textAnalyticsService;
            this.jemasService = jemasService;
            this.logger = logger;            
        }

        // POST api/values
        [HttpPost]
        public async Task<AnalyzeResult> Post([FromBody] string value)
        {
            var keyPhrasesTask = textAnalyticsService.KeyPhrasesAsync(value);
            var sentimentTask = textAnalyticsService.SentimentAsync(value);
            var entitiesTask = textAnalyticsService.EntitiesAsync(value);
            var jemasResult = await jemasService.JemasEmotion(value);

            return new AnalyzeResult(await keyPhrasesTask, await sentimentTask, await entitiesTask, jemasResult.Dominance, jemasResult.Valence, jemasResult.Arousal);            
        }

    }
}