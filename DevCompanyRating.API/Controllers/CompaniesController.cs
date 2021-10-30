using DevCompanyRating.API.Domain;
using DevCompanyRating.API.Models;
using DevCompanyRating.API.Repositories;
using DevCompanyRating.API.Services.CognitiveServices;
using DevCompanyRating.API.Services.MessageBus;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DevCompanyRating.API.Controllers
{
    [Route("api/companies")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _repository;
        private readonly IMessageBusService _messageBusService;
        private readonly ICognitiveService _cognitiveService;
        public CompaniesController(ICompanyRepository companyRepository, IMessageBusService messageBusService, ICognitiveService cognitiveService)
        {
            _repository = companyRepository;
            _messageBusService = messageBusService;
            _cognitiveService = cognitiveService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var companyWithRatings = _repository.GetByIdWithRatings(id);

            var companyWithRatingsViewModel = new CompanyAsAdminViewModel
            {
                Name = companyWithRatings.Name,
                Description = companyWithRatings.Description,
                Ratings = companyWithRatings.Ratings.Select(r => new CompanyRatingAsAdminViewModel(r.Rating, r.Comments, r.Sentiment, r.PositiveScore, r.NeutralScore, r.NegativeScore)).ToList()
            };

            return Ok(companyWithRatingsViewModel);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateCompanyInputModel createCompany)
        {
            var company = new Company(createCompany.Name, createCompany.Description);

            _repository.Add(company);

            return NoContent();
        }

        [HttpPost("{id}/ratings")]
        public async Task<IActionResult> PostRating(int id, [FromBody] CreateCompanyRatingInputModel createCompanyRating)
        {
            // Instancia de CompanyRating
            var companyRating = new CompanyRating(id, createCompanyRating.Rating, createCompanyRating.Comments);

            // Traduz comentario para o ingles
            companyRating.CommentsInEnglish = await _cognitiveService.ConvertFromPtToEn(companyRating.Comments);

            // Analise de Sentimento, Set Scores
            var sentimentAnalysis = await _cognitiveService.SentimentalAnalysis(companyRating.Comments);
            companyRating.SetScores(
                sentimentAnalysis.Sentiment,
                sentimentAnalysis.Score.Positive,
                sentimentAnalysis.Score.Negative,
                sentimentAnalysis.Score.Neutral);

            // Salva (sem Service Bus)
            // _repository.AddRating(companyRating);

            // Cria mensagem e envia ao Service Bus para fila new-rating
            var companyRatingJson = JsonSerializer.Serialize(companyRating);
            var companyRatingBytes = Encoding.UTF8.GetBytes(companyRatingJson);

            await _messageBusService.Publish("new-rating", companyRatingBytes);

            return NoContent();
        }

        [HttpGet("{id}/admin")]
        public IActionResult GetByIdAsAdmin(int id)
        {
            // Consulta com Ratings
            
            // Formata com CompanyAsAdminViewModel

            return Ok();
        }
    }
}
