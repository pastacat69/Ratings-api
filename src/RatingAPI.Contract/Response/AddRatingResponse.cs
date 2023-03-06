namespace RatingAPI.Contract.Response;

public class AddRatingResponse
{
    public string Id { get; set; }

    public AddRatingResponse()
    {
    }

    public AddRatingResponse(string id)
    {
        Id = id;
    }
}