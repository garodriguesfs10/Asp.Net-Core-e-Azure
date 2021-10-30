using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DevCompanyRating.API.Services.CognitiveServices
{
    public class AzureCognitiveService : ICognitiveService
    {
        private const string REGION = "brazilsouth";
        private readonly string _translatorUrl;
        private readonly string _translatorKey;
        private readonly string _textAnalysisUrl;
        private readonly string _textAnalysisKey;
        public AzureCognitiveService(IConfiguration configuration)
        {
            _translatorUrl = configuration.GetSection("CognitiveServices:TranslatorUrl").Value;
            _translatorKey = configuration.GetSection("CognitiveServices:TranslatorKey").Value;
            _textAnalysisUrl = configuration.GetSection("CognitiveServices:TextAnalysisUrl").Value;
            _textAnalysisKey = configuration.GetSection("CognitiveServices:TextAnalysisKey").Value;
        }

        public async Task<string> ConvertFromPtToEn(string content)
        {
            var objectBody = new object[] { new { Text = content } };
            var objectJson = JsonConvert.SerializeObject(objectBody);

            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri(_translatorUrl + "/translate?api-version=3.0&to=en");
                    request.Content = new StringContent(objectJson, Encoding.UTF8, "application/json");

                    request.Headers.Add("Ocp-Apim-Subscription-Key", _translatorKey);
                    request.Headers.Add("Ocp-Apim-Subscription-Region", REGION);

                    HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);

                    string result = await response.Content.ReadAsStringAsync();
                    var deserializedData = JsonConvert.DeserializeObject<List<TranslationResult>>(result);

                    var translationResult = deserializedData.First();

                    return translationResult.Translations.First().Text;
                }
            }
        }

        public async Task<TextAnalysis> SentimentalAnalysis(string content)
        {
            var textAnalyticsClient = new TextAnalyticsClient(new Uri(_textAnalysisUrl), new AzureKeyCredential(_textAnalysisKey));

            DocumentSentiment documentResult = await textAnalyticsClient.AnalyzeSentimentAsync(content, "pt");

            return new TextAnalysis
            {
                Sentiment = documentResult.Sentiment.ToString(),
                Score = new ConfidenceScores
                {
                    Positive =  documentResult.ConfidenceScores.Positive,
                    Neutral = documentResult.ConfidenceScores.Neutral,
                    Negative = documentResult.ConfidenceScores.Negative
                }
            };
        }

        static public async Task<string> TranslateTextRequest(string subscriptionKey, string endpoint, string inputText)
        {
            return string.Empty;
        }
    }

    public class TranslationResult
    {
        public List<Translation> Translations { get; set; }
    }

    public class Translation
    {
        public string Text { get; set; }
        public string To { get; set; }
    }
    
    public class TextAnalysis
    {
        public int Id { get; set; }
        public string Sentiment { get; set; }
        public ConfidenceScores Score { get; set; }
    }

    public class ConfidenceScores
    {
        public double Positive { get; set; }
        public double Negative { get; set; }
        public double Neutral { get; set; }
    }
 }
