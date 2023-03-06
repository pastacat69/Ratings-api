namespace RatingAPI.Core.Domain.Error;

public class ValidationError
{
    public string Message { get; set; }

    public ValidationError()
    {
    }

    public ValidationError(string message)
    {
        Message = message;
    }
}