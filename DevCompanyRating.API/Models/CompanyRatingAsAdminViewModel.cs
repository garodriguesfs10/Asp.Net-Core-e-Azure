namespace DevCompanyRating.API.Models
{
    public class CompanyRatingAsAdminViewModel
    {
        public CompanyRatingAsAdminViewModel(int rating, string comments, string sentiment, double positiveScore, double neutralScore, double negativeScore)
        {
            Rating = rating;
            Comments = comments;
            Sentiment = sentiment;
            PositiveScore = positiveScore;
            NeutralScore = neutralScore;
            NegativeScore = negativeScore;
        }

        public int Rating { get; set; }
        public string Comments { get; set; }
        public string Sentiment { get; set; }
        public double PositiveScore { get; set; }
        public double NeutralScore { get; set; }
        public double NegativeScore { get; set; }
    }
}
