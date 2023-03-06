using System.Text.Json.Serialization;

namespace RatingAPI.Contract.Response;

public class GetRatingsResponse
{
    [JsonPropertyName("ratings")]
    public IEnumerable<RatingObject> Ratings { get; set; }

    public GetRatingsResponse()
    {
    }
    
    public GetRatingsResponse(IEnumerable<RatingObject> ratings)
    {
        Ratings = ratings;
    }
}

public class RatingObject
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("titleId")]
    public string TitleId { get; set; }
    [JsonPropertyName("rating")]
    public int Rating { get; set; }
    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    public RatingObject()
    {
    }
    
    public RatingObject(string id, string titleId, int rating, string? comment)
    {
        Id = id;
        TitleId = titleId;
        Rating = rating;
        Comment = comment;
    }
}