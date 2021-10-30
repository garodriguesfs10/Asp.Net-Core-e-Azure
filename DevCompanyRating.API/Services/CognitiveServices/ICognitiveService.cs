using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCompanyRating.API.Services.CognitiveServices
{
    public interface ICognitiveService
    {
        Task<string> ConvertFromPtToEn(string content);
        Task<TextAnalysis> SentimentalAnalysis(string content);
    }
}
