namespace RatingAPI.Contract.Request;

public class GetRatingsRequest
{
    public string TitleId { get; set; }

    public GetRatingsRequest()
    {
        
    }
    
    public GetRatingsRequest(string titleId)
    {
        TitleId = titleId;
    }
}