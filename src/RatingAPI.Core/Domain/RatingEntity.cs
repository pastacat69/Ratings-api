namespace RatingAPI.Core.Domain;

public class RatingEntity
{
    public string Id { get; set; }
    public string TitleId { get; set; }
    public int Rating { get; set; } // 0 - 100
    public string? Comment { get; set; }

    public RatingEntity()
    {
    }
    
    public RatingEntity(string id, string titleId, int rating, string? comment)
    {
        Id = id;
        TitleId = titleId;
        Rating = rating;
        Comment = comment;
    }
}