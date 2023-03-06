namespace RatingAPI.Contract.Request;

public class AddRatingRequest
{
    public string TitleId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }

    public AddRatingRequest()
    {
    }
    
    public AddRatingRequest(string titleId, int rating, string? comment)
    {
        TitleId = titleId;
        Rating = rating;
        Comment = comment;
    }
}