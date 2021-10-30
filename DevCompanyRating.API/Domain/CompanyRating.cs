using System;

namespace DevCompanyRating.API.Domain
{
    public class CompanyRating
    {
        protected CompanyRating() { }
        public CompanyRating(int idCompany, int rating, string comments)
        {
            IdCompany = idCompany;
            Rating = rating;
            Comments = comments;

            CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }
        public int IdCompany { get; set; }
        public string Comments { get; set; }
        public string CommentsInEnglish { get; set; }
        public int Rating { get; set; }

        public string Sentiment { get; set; }
        public double PositiveScore { get; set; }
        public double NeutralScore { get; set; }
        public double NegativeScore { get; set; }

        public DateTime CreatedAt { get; set; }

        public void SetScores(string sentiment, double positive, double negative, double neutral)
        {
            Sentiment = sentiment;
            PositiveScore = positive;
            NegativeScore = negative;
            NeutralScore = neutral;
        }
    }
}
